using MvcCoreMultiplesBBDD.Models;

namespace MvcCoreMultiplesBBDD.Repositories
{
    public interface IRepositoryEmpleados
    {
        List<Empleado> GetEmpleados();

        Empleado DetalleEmpleado(int idEmpleado);
    }
}
