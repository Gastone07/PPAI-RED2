using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PPAI_REDSISMICA.Persistencia.Persistencia;

namespace PPAI_REDSISMICA.Entidades
{
    public class Sesion
    {
        public Usuario usuario { get; set; }
        public DateTime fechaHoraInicio { get; set; }
        public DateTime? fechaHoraFin { get; set; }

        public Sesion(Usuario usuario, DateTime fechaHoraInicio, DateTime? fechaHoraFin)
        {
            this.usuario = usuario;
            this.fechaHoraInicio = fechaHoraInicio;
            this.fechaHoraFin = fechaHoraFin;
        }

        public Sesion(DataRow data)
        {
            this.fechaHoraFin = data["fechaHoraFin"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(data["fechaHoraFin"]) : null;
            this.fechaHoraInicio = Convert.ToDateTime(data["fechaHoraInicio"]);
            this.usuario = Usuario.recuperarUsuarioXID((int)data["idUsuario"]);
        }

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
                    listaSesiones.Add(new Sesion(item));
                }

            }
            return listaSesiones;
        }



        public Usuario obtenerUsuarioLogeado()
        {
            return this.usuario.obtenerLogueado();
        }

        // Agregar este método para encapsular la lógica de sesión activa
        public bool esSesionActiva()
        {
            return this.fechaHoraFin == null;
        }
    }
}
