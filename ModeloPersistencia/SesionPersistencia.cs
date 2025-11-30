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
    internal class SesionPersistencia
    {
        public static List<Sesion> obtenerSesiones()
        {
            GeneralAdapterSQL generalAdapterSQL = new GeneralAdapterSQL();
            DataTable respuesta = generalAdapterSQL.EjecutarVista("Sesiones");
            List<Sesion> listaSesiones = new List<Sesion>();

            if (respuesta != null && respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() != "ERROR")
            {
                //Hubo un error al consultar la base de datos
                foreach (DataRow item in respuesta.Rows)
                {
                    int idUsuario = int.Parse(respuesta.Rows[0][0].ToString() ?? "");

                    Usuario usuario = UsuarioPersistencia.recuperarUsuarioXID(idUsuario);

                    listaSesiones.Add(new Sesion(
                        usuario,
                        DateTime.Parse(item["fechaHoraInicio"].ToString() ?? ""),
                        DateTime.Parse(item["fechaHoraFin"].ToString() ?? "")
                        ));
                }

            }
            return listaSesiones;
        }
    }
}
