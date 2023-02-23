using MvcCoreMultiplesBBDD.Models;

namespace MvcCoreMultiplesBBDD.Repositories
{
    public interface IRepositoryEmpleados
    {
        List<Empleado> GetEmpleados();

        Empleado DetalleEmpleado(int idEmpleado);

        Task DeleteEmpleado(int id);

        Task UpdateEmpleado(int idempleado, int salario, string oficio);
    }
}
