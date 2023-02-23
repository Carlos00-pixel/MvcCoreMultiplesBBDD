using Microsoft.AspNetCore.Mvc;
using MvcCoreMultiplesBBDD.Models;
using MvcCoreMultiplesBBDD.Repositories;

namespace MvcCoreMultiplesBBDD.Controllers
{
    public class EmpleadosController : Controller
    {
        private IRepositoryEmpleados repo;

        public EmpleadosController(IRepositoryEmpleados repo)
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

        public async Task<IActionResult> Delete(int id)
        {
            await this.repo.DeleteEmpleado(id);
            return RedirectToAction("Index");
        }

        public IActionResult EditEmpleado(int id)
        {
            Empleado empleado = this.repo.DetalleEmpleado(id);
            return View(empleado);
        }

        [HttpPost]
        public async Task<IActionResult> EditEmpleado(Empleado empleado)
        {
            await this.repo.UpdateEmpleado
                (empleado.IdEmpleado, empleado.Salario, empleado.Oficio);
            return RedirectToAction("Index");
        }
    }
}
