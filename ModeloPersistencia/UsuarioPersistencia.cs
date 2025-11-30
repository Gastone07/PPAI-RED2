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
    internal class UsuarioPersistencia
    {
        public static Usuario obtenerUsuarios()
        {
            GeneralAdapterSQL generalAdapterSQL = new GeneralAdapterSQL();
            DataTable respuesta = generalAdapterSQL.EjecutarVista("Usuarios");
            Usuario usuario = new Usuario();

            if (respuesta != null && respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() != "ERROR")
            {

                //Hubo un error al consultar la base de datos
                foreach (DataRow item in respuesta.Rows)
                {
                    usuario = new Usuario(
                        respuesta.Rows[0][0].ToString() ?? "",
                        respuesta.Rows[0][1].ToString() ?? ""
                        );
                }

            }
            return usuario;
        }

        public static Usuario recuperarUsuarioXID(int id)
        {
            GeneralAdapterSQL generalAdapterSQL = new GeneralAdapterSQL();
            DataTable respuesta = generalAdapterSQL.EjecutarVista("Usuarios WHERE idUsuario = " + id);
            Usuario usuario = new Usuario();
            if (respuesta != null && respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() != "ERROR")
            {
                usuario = new Usuario(
                         respuesta.Rows[0][0].ToString() ?? "",
                         respuesta.Rows[0][1].ToString() ?? ""
                         );

            }
            return usuario;

        }
    }
}
