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
                    int idUsuario = int.Parse(item[0]?.ToString() ?? "0");

                    Usuario usuario = UsuarioPersistencia.recuperarUsuarioXID(idUsuario);

                    DateTime fechaInicio;
                    DateTime fechaFin;

                    // convierte si puede, sino pone null o MinValue
                    DateTime.TryParse(item[2]?.ToString(), out fechaInicio);
                    DateTime.TryParse(item[3]?.ToString(), out fechaFin);

                    listaSesiones.Add(new Sesion(
                        usuario,
                        fechaInicio,
                        fechaFin
                    ));
                }

            }
            return listaSesiones;
        }
    }
}
