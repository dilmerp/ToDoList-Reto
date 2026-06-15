using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using ToDoList.Entities;

namespace ToDoList.Data
{
    public class CacheTaskRepository : ITaskRepository
    {
        private readonly ObjectCache _cache;
        private readonly string _cacheKey = "UserSessionTasks";

        public CacheTaskRepository()
        {
            _cache = MemoryCache.Default;

            // Inicializamos la caché con una lista vacía si es la primera vez que ingresa
            if (_cache[_cacheKey] == null)
            {
                _cache.Set(_cacheKey, new List<TaskItem>(), new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddHours(2) });
            }
        }

        private List<TaskItem> GetCurrentTasks()
        {
            return _cache[_cacheKey] as List<TaskItem> ?? new List<TaskItem>();
        }

        private void SaveTasks(List<TaskItem> tasks)
        {
            _cache.Set(_cacheKey, tasks, new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddHours(2) });
        }

        public IEnumerable<TaskItem> GetAll()
        {
            return GetCurrentTasks();
        }

        public TaskItem GetById(int id)
        {
            return GetCurrentTasks().FirstOrDefault(t => t.Id == id);
        }

        public void Add(TaskItem task)
        {
            var tasks = GetCurrentTasks();

            // Simulamos el autoincremental de la base de datos
            task.Id = tasks.Any() ? tasks.Max(t => t.Id) + 1 : 1;

            tasks.Add(task);
            SaveTasks(tasks);
        }

        public void Update(TaskItem task)
        {
            var tasks = GetCurrentTasks();
            var existingTask = tasks.FirstOrDefault(t => t.Id == task.Id);

            if (existingTask != null)
            {
                existingTask.Title = task.Title;
                existingTask.Description = task.Description;
                existingTask.IsCompleted = task.IsCompleted;
                SaveTasks(tasks);
            }
        }

        public void Delete(int id)
        {
            var tasks = GetCurrentTasks();
            var taskToRemove = tasks.FirstOrDefault(t => t.Id == id);

            if (taskToRemove != null)
            {
                tasks.Remove(taskToRemove);
                SaveTasks(tasks);
            }
        }
    }
}