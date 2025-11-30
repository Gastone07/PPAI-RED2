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
