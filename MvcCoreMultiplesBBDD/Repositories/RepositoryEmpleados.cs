using Microsoft.EntityFrameworkCore;
using MvcCoreEfProcedures.Data;
using MvcCoreMultiplesBBDD.Models;

#region PROCEDIMINETOS ALMACENADOS SQL
/*
 CREATE PROCEDURE SP_ALL_EMPLOYEES
AS
	SELECT * FROM EMP
GO


CREATE PROCEDURE SP_DETAILS_EMPLEADO
(@IDEMPLEADO INT)
AS
	SELECT * FROM EMP
	WHERE EMP_NO = @IDEMPLEADO
GO
 */
#endregion

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
            string sql = "SP_ALL_EMPLOYEES";
            var consulta = this.context.Empleados.FromSqlRaw(sql);
            List<Empleado> empleados = consulta.ToList();
            return empleados;
        }

        public Empleado DetalleEmpleado(int idEmpleado)
        {
            var consulta = from datos in this.context.Empleados
                           where datos.IdEmpleado == idEmpleado
                           select datos;
            return consulta.ToList().FirstOrDefault();
        }

        public async Task DeleteEmpleado(int id)
        {
            Empleado empleado = this.DetalleEmpleado(id);
            this.context.Empleados.Remove(empleado);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateEmpleado
            (int idempleado, int salario, string oficio)
        {
            Empleado empleado = this.DetalleEmpleado(idempleado);
            empleado.Salario = salario;
            empleado.Oficio = oficio;
            await this.context.SaveChangesAsync();
        }
    }
}
