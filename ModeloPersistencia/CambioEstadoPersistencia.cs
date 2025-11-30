using PPAI_REDSISMICA.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PPAI_REDSISMICA.Persistencia.Persistencia;

namespace PPAI_REDSISMICA.ModeloPersistencia
{
    public class CambioEstadoPersistencia
    {
        public static List<CambioEstado> obtenerCambiosEstados()
        {
            GeneralAdapterSQL generalAdapterSQL = new GeneralAdapterSQL();
            DataTable respuesta = generalAdapterSQL.EjecutarVista("CambiosEstado");
            List<CambioEstado> listaCambioEstado = new List<CambioEstado>();

            if (respuesta != null && respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() != "ERROR")
            {

                //Hubo un error al consultar la base de datos
                foreach (DataRow item in respuesta.Rows)
                {
                    int id = int.Parse(item["idEstado"].ToString() ?? "");

                    Estado currentId = EstadoPersistencia.recuperarEstadoXID(id);

                    listaCambioEstado.Add(new CambioEstado(
                        DateTime.Parse(item["fechaHoraInicio"].ToString() ?? ""), 
                        DateTime.Parse(item["fechaHoraFin"].ToString() ?? ""),
                        currentId)
                    );
                }

            }
            return listaCambioEstado;
        }

        public static List<CambioEstado> obtenerCambiosEstadosXID(int id)
        {
            GeneralAdapterSQL generalAdapterSQL = new GeneralAdapterSQL();
            DataTable respuesta = generalAdapterSQL.EjecutarVista("CambiosEstado WHERE idEvento = " + id);
            List<CambioEstado> listaCambioEstado = new List<CambioEstado>();

            if (respuesta != null && respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() != "ERROR")
            {

                //Hubo un error al consultar la base de datos
                foreach (DataRow item in respuesta.Rows)
                {
                    int idEstado = int.Parse(item["idEstado"].ToString() ?? "");

                    Estado currentId = EstadoPersistencia.recuperarEstadoXID(idEstado);

                    listaCambioEstado.Add(new CambioEstado(
                        DateTime.Parse(item["fechaHoraInicio"].ToString() ?? ""),
                        DateTime.Parse(item["fechaHoraFin"].ToString() ?? ""),
                        currentId)
                    );
                }

            }
            return listaCambioEstado;
        }

        public static bool setHoraFin(DateTime fechaHoraFin, int idCambioEstado)
        {
            string consulta = "UPDATE CambiosEstado SET fechaHoraFin = " + fechaHoraFin.ToString("yyyy-mm-dd hh:mm:ss") + " WHERE IdCambioEstado = " + idCambioEstado + ";" +
                "Select * from CambiosEstado where IdCambioEstado = " + idCambioEstado;

            GeneralAdapterSQL generalAdapterSQL = new GeneralAdapterSQL();
            DataTable respuesta = generalAdapterSQL.EjecutarQuery(consulta);

            if (respuesta != null && respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() != "ERROR") return true;
            else return false;

        }

        public static bool insertarCambioEstado(int idEstado, DateTime fechaInicio, int idEvento)
        {
            string consulta = "INSERT INTO CambiosEstado VALUES("+ idEstado + ","+ fechaInicio.ToString("yyyy-mm-dd hh:mm:ss") + ", null, "+ idEvento + ");" + ");" +
                "Select top(1)* from CambiosEstado Order by idCambioEstado desc";

            GeneralAdapterSQL generalAdapterSQL = new GeneralAdapterSQL();
            DataTable respuesta = generalAdapterSQL.EjecutarQuery(consulta);

            if (respuesta != null && respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() != "ERROR") return true;
            else return false;
        }
    }
}
