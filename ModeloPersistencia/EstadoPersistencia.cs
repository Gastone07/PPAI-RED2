using PPAI_REDSISMICA.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static PPAI_REDSISMICA.Persistencia.Persistencia;

namespace PPAI_REDSISMICA.ModeloPersistencia
{
    internal class EstadoPersistencia
    {
        public static List<Estado> obtenerEstados()
        {
            GeneralAdapterSQL generalAdapterSQL = new GeneralAdapterSQL();
            DataTable respuesta = generalAdapterSQL.EjecutarVista("Estados");
            List<Estado> listaEstados = new List<Estado>();

            if (respuesta != null && respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() != "ERROR")
            {

                //Hubo un error al consultar la base de datos
                foreach (DataRow item in respuesta.Rows)
                {
                    listaEstados.Add(
                        crearEstadoXTipo(int.Parse(item["idEstado"].ToString() ?? "0"),
                        item["ambito"].ToString() ?? "",
                        item["nombreEstado"].ToString() ?? ""
                        )
                    );
                }

            }
            return listaEstados;
        }

        public static Estado recuperarEstadoXID(int id)
        {
            GeneralAdapterSQL generalAdapterSQL = new GeneralAdapterSQL();
            DataTable respuesta = generalAdapterSQL.EjecutarVista("Estados WHERE idEstado = " + id);
            if (respuesta != null && respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() != "ERROR")
            {
                return crearEstadoXTipo(int.Parse(respuesta.Rows[0]["idEstado"].ToString() ?? "0"),
                        respuesta.Rows[0]["ambito"].ToString() ?? "",
                        respuesta.Rows[0]["nombreEstado"].ToString() ?? ""
                        );

            }
            else return crearEstadoXTipo(0, "", "");

        }

        private static Estado crearEstadoXTipo(int tipo, string ambito, string nombreEstado)
        {
            switch (tipo)
            {
                //Agregar las demas clases de estado

                case 2:
                    return new AutoDetectado(ambito, nombreEstado);

                case 3:
                    return new BloqueadoEnRevision(ambito, nombreEstado);

                case 4:
                    return new PendienteParaRevision(ambito, nombreEstado);

                case 5:
                    return new Rechazado(ambito, nombreEstado);

                case 6:
                    return new Confirmado(ambito, nombreEstado);

                case 7:
                    return new PendienteRevisionExperto(ambito, nombreEstado);

                default:
                    return new AutoDetectado(ambito, nombreEstado);
            }
        }

    }
}
