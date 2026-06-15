using System.Linq;
using System.Net;
using System.Web.Mvc;
using ToDoList.Business;
using ToDoList.Entities;

namespace ToDoList.Web.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;

        // Constructor: Instanciamos el servicio de la capa de Negocio
        public TaskController()
        {
            _taskService = new TaskService();
        }

        // GET: Task (Listado principal con soporte para filtros)
        public ActionResult Index(string filter = "all")
        {
            var tasks = _taskService.GetTasks();

            // Aplicación de filtros (Innovación técnica)
            if (filter == "pending")
            {
                tasks = tasks.Where(t => !t.IsCompleted);
            }
            else if (filter == "completed")
            {
                tasks = tasks.Where(t => t.IsCompleted);
            }

            ViewBag.CurrentFilter = filter;
            return View(tasks);
        }

        // GET: Task/Create (Muestra el formulario vacío)
        public ActionResult Create()
        {
            return View();
        }

        // POST: Task/Create (Recibe los datos del formulario y los guarda)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TaskItem task)
        {
            if (ModelState.IsValid)
            {
                _taskService.CreateTask(task);
                return RedirectToAction("Index");
            }
            return View(task);
        }

        // GET: Task/Edit/5 (Muestra el formulario con los datos a editar)
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TaskItem task = _taskService.GetTaskById(id.Value);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // POST: Task/Edit/5 (Recibe los datos modificados y los actualiza)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TaskItem task)
        {
            if (ModelState.IsValid)
            {
                _taskService.UpdateTask(task);
                return RedirectToAction("Index");
            }
            return View(task);
        }

        // GET: Task/Delete/5 (Pantalla de confirmación para eliminar)
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TaskItem task = _taskService.GetTaskById(id.Value);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // POST: Task/Delete/5 (Ejecuta la eliminación real)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _taskService.DeleteTask(id);
            return RedirectToAction("Index");
        }

        // POST: Task/ToggleStatus/5 (Alternar estado completado/pendiente)
        [HttpPost]
        public ActionResult ToggleStatus(int id)
        {
            var task = _taskService.GetTaskById(id);
            if (task != null)
            {
                task.IsCompleted = !task.IsCompleted;
                _taskService.UpdateTask(task);
            }
            return RedirectToAction("Index");
        }
    }
}