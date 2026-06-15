using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using ToDoList.Data;
using ToDoList.Entities;

namespace ToDoList.Business
{
    public class TaskService : ITaskService
    {
        // Manejamos ambos repositorios: el físico (SQL) y el efímero (Caché)
        private readonly TaskRepository _sqlRepository;
        private readonly ObjectCache _cache;
        private readonly string _cacheKey = "UserTasksData";

        public TaskService()
        {
            _sqlRepository = new TaskRepository();
            _cache = MemoryCache.Default;
        }

        public IEnumerable<TaskItem> GetTasks()
        {
            // 1. Intentar obtener los datos de la Caché
            var cachedTasks = _cache[_cacheKey] as List<TaskItem>;

            if (cachedTasks == null)
            {
                // 2. CACHE MISS: Si no está en memoria, ir a SQL Server
                cachedTasks = _sqlRepository.GetAll().ToList();

                // 3. Guardar en Caché los datos de SQL por 2 horas para futuras consultas
                var policy = new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddHours(2) };
                _cache.Set(_cacheKey, cachedTasks, policy);
            }

            // 4. CACHE HIT: Devolver los datos optimizados en memoria
            return cachedTasks;
        }

        public TaskItem GetTaskById(int id)
        {
            // Para mantener la consistencia, buscamos en el listado optimizado de la sesión
            return GetTasks().FirstOrDefault(t => t.Id == id);
        }

        public void CreateTask(TaskItem task)
        {
            // 1. Persistencia permanente en la base de datos SQL Server
            _sqlRepository.Add(task);

            // 2. Invalidar la caché para forzar a que la siguiente lectura recargue desde SQL
            EvictCache();
        }

        public void UpdateTask(TaskItem task)
        {
            // 1. Actualizar de forma definitiva en SQL Server
            _sqlRepository.Update(task);

            // 2. Invalidar la caché para mantener sincronía
            EvictCache();
        }

        public void DeleteTask(int id)
        {
            // 1. Eliminar físicamente de SQL Server
            _sqlRepository.Delete(id);

            // 2. Invalidar la caché
            EvictCache();
        }

        private void EvictCache()
        {
            if (_cache.Contains(_cacheKey))
            {
                _cache.Remove(_cacheKey);
            }
        }
    }
}