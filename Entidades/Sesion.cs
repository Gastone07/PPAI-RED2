using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        /*
        public Sesion(Usuario usuario, DateTime fechaHoraInicio)
        {
            this.usuario = usuario;
            this.fechaHoraInicio = fechaHoraInicio;
            this.fechaHoraFin = null;
        }*/

        public Usuario obtenerUsuarioLogeado()
        {
            return this.usuario.obtenerLogueado();
        }
    }
}
