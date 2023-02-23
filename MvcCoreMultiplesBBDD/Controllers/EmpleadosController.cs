using Microsoft.AspNetCore.Mvc;
using MvcCoreMultiplesBBDD.Models;
using MvcCoreMultiplesBBDD.Repositories;

namespace MvcCoreMultiplesBBDD.Controllers
{
    public class EmpleadosController : Controller
    {
        private RepositoryEmpleados repo;

        public EmpleadosController(RepositoryEmpleados repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            List<Empleado> empleados = this.repo.GetEmpleados();
            return View(empleados);
        }

        public IActionResult DetallesEmpleados(int idEmpleado)
        {
            Empleado empleado = this.repo.DetalleEmpleado(idEmpleado);
            return View(empleado);
        }
    }
}
