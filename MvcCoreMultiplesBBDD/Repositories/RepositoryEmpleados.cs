using MvcCoreEfProcedures.Data;
using MvcCoreMultiplesBBDD.Models;

namespace MvcCoreMultiplesBBDD.Repositories
{
    public class RepositoryEmpleados: IRepositoryEmpleados
    {
        private HospitalContext context;

        public RepositoryEmpleados(HospitalContext context)
        {
            this.context = context;
        }

        public List<Empleado> GetEmpleados()
        {
            var consulta = from datos in this.context.Empleados
                           select datos;
            return consulta.ToList();
        }

        public Empleado DetalleEmpleado(int idEmpleado)
        {
            var consulta = from datos in this.context.Empleados
                           where datos.IdEmpleado == idEmpleado
                           select datos;
            return consulta.ToList().FirstOrDefault();
        }
    }
}
