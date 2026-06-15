using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ToDoList.Entities;

namespace ToDoList.Data
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ToDoContext _context;

        public TaskRepository()
        {
            _context = new ToDoContext();
        }

        public IEnumerable<TaskItem> GetAll()
        {
            return _context.Tasks.ToList();
        }

        public TaskItem GetById(int id)
        {
            return _context.Tasks.Find(id);
        }

        public void Add(TaskItem task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
        }

        public void Update(TaskItem task)
        {
            _context.Entry(task).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                _context.SaveChanges();
            }
        }
    }
}