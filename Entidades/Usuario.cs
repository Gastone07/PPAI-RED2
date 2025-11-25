using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PPAI_REDSISMICA.Persistencia.Persistencia;

namespace PPAI_REDSISMICA.Entidades
{
    public class Usuario
    {
        public string contraseña { get; set; }

        public string nombreUsuario { get; set; }

        public Usuario(string nombreUsuario, string contraseña)
        {
            this.nombreUsuario = nombreUsuario;
            this.contraseña = contraseña;
        }

        public Usuario(DataRow data)
        {
            this.nombreUsuario = Convert.ToString(data["nombreUsuario"]) ?? string.Empty;
            this.contraseña = Convert.ToString(data["contrasenia"]) ?? string.Empty;
        } 
        
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
                    usuario = new(respuesta.Rows[0]);
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
                usuario = new(respuesta.Rows[0]);

            }
            return usuario;

        }

        public Usuario()
        {
            // Constructor por defecto
        }

        public Usuario obtenerLogueado()
        {
            return this;
        }


    }
}
