using System.Collections.Generic;
using ToDoList.Entities;

namespace ToDoList.Business
{
    public interface ITaskService
    {
        IEnumerable<TaskItem> GetTasks();
        TaskItem GetTaskById(int id);
        void CreateTask(TaskItem task);
        void UpdateTask(TaskItem task);
        void DeleteTask(int id);
    }
}