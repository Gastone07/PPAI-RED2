using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAI_REDSISMICA.Entidades
{
    public class CambioEstado
    {
        private DateTime fechaHoraInicio { get; set; }

        private DateTime? fechaHoraFin { get; set; }
        private Estado estado { get; set; }

        public CambioEstado(DateTime fechaHoraInicio, DateTime? fechaHoraFin, Estado estado)
        {
            this.fechaHoraInicio = fechaHoraInicio;
            this.fechaHoraFin = fechaHoraFin;
            this.estado = estado;
        }

        public CambioEstado()
        {
            // Constructor por defecto
        }

        public CambioEstado sosActual(DateTime fechaHoraActual)
        {
            this.setFechaHoraFin(fechaHoraActual);
            return this; // Retorna el primer cambio de estado abierto encontrado
        }

        public void setFechaHoraFin(DateTime fecha)
        {
           fechaHoraFin = fecha;
        }

        
    }
}
