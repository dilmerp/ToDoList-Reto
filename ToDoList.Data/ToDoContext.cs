using System.Data.Entity;
using ToDoList.Entities;

namespace ToDoList.Data
{
    public class ToDoContext : DbContext
    {
        // "DefaultConnection" será el nombre de la cadena de conexión en el Web.config
        public ToDoContext() : base("name=DefaultConnection")
        {
        }

        public DbSet<TaskItem> Tasks { get; set; }
    }
}