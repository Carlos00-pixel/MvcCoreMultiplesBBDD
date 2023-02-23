using Microsoft.EntityFrameworkCore;
using MvcCoreEfProcedures.Data;
using MvcCoreMultiplesBBDD.Models;
using Oracle.ManagedDataAccess.Client;

#region PROCEDIMIENTOS ALMACENADOS
/*
///////////////////////BORRAR EMPLEADO///////////////////////
CREATE OR REPLACE PROCEDURE SP_DELETE_EMPLEADO
(P_IDEMPLEADO EMP.EMP_NO%TYPE)
AS
BEGIN
  DELETE FROM EMP WHERE EMP_NO = P_IDEMPLEADO;
  COMMIT;
END;


///////////////////////MODIFICAR EMPLEADO///////////////////////
CREATE OR REPLACE PROCEDURE SP_UPDATE_EMPLEADO
(P_IDEMPLEADO EMP.EMP_NO%TYPE,
P_OFICIO EMP.OFICIO%TYPE,
P_SALARIO EMP.SALARIO%TYPE)
AS
BEGIN
  UPDATE EMP
  SET
         OFICIO = P_OFICIO,
         SALARIO = P_SALARIO
  WHERE EMP_NO = P_IDEMPLEADO;
  COMMIT;
END;
*/
#endregion

namespace MvcCoreMultiplesBBDD.Repositories
{
    public class RepositoryEmpleadosOracle: IRepositoryEmpleados
    {
        private HospitalContext context;

        public RepositoryEmpleadosOracle(HospitalContext context)
        {
            this.context = context;
        }

        public List<Empleado> GetEmpleados()
        {
            string sql = "BEGIN ";
            sql += " SP_ALL_EMPLOYEES(:P_CURSOR_EMPLEADOS); ";
            sql += " END;";
            OracleParameter pamcursor = new OracleParameter();
            pamcursor.ParameterName = "P_CURSOR_EMPLEADOS";
            pamcursor.Value = null;
            pamcursor.Direction = System.Data.ParameterDirection.Output;
            pamcursor.OracleDbType = OracleDbType.RefCursor;
            var consulta =
                this.context.Empleados.FromSqlRaw(sql, pamcursor);
            List<Empleado> empleados = consulta.ToList();
            return empleados;
        }

        public Empleado DetalleEmpleado(int idEmpleado)
        {
            string sql = "BEGIN ";
            sql += "SP_DETAILS_EMPLEADO(:P_CURSOR_EMPLEADO, :P_IDEMPLEADO);";
            sql += "END;";

            OracleParameter pamcursor = new OracleParameter();
            pamcursor.ParameterName = "P_CURSOR_EMPLEADO";
            pamcursor.Value = null;
            pamcursor.Direction = System.Data.ParameterDirection.Output;
            pamcursor.OracleDbType = OracleDbType.RefCursor;

            OracleParameter pamid = new OracleParameter("P_IDEMPLEADO", idEmpleado);

            var consulta = this.context.Empleados.FromSqlRaw(sql, pamcursor, pamid);
            Empleado empleado = consulta.ToList().FirstOrDefault();
            return empleado;
        }

        public async Task DeleteEmpleado(int id)
        {
            //EN ORACLE LOS PARAMETROS SE DENOMINAN
            //MEDIANTE :parametro
            string sql = "begin";
            sql += " SP_DELETE_EMPLEADO(:P_IDEMPLEADO); ";
            sql += " end;";
            OracleParameter pamid = new OracleParameter("P_IDEMPLEADO", id);
            await this.context.Database.ExecuteSqlRawAsync(sql, pamid);
        }

        public async Task UpdateEmpleado
            (int idempleado, int salario, string oficio)
        {
            string sql = "begin";
            sql += " SP_UPDATE_EMPLEADO(:P_IDEMPLEADO, :P_OFICIO, :P_SALARIO); ";
            sql += " end;";
            OracleParameter pamid = new OracleParameter("P_IDEMPLEADO", idempleado);
            OracleParameter pamoficio = new OracleParameter("P_OFICIO", oficio);
            OracleParameter pamsalario = new OracleParameter("P_SALARIO", salario);
            await this.context.Database.ExecuteSqlRawAsync(sql, pamid, pamoficio, pamsalario);
        }
    }
}
