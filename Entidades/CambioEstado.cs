using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PPAI_REDSISMICA.Persistencia.Persistencia;

namespace PPAI_REDSISMICA.Entidades
{
    public class CambioEstado
    {
        private DateTime fechaHoraInicio { get; set; }

        private DateTime? fechaHoraFin { get; set; }
        private Estado estado { get; set; }

        private int idCambioEstado { get; set; }


        public CambioEstado(DateTime fechaHoraInicio, DateTime? fechaHoraFin, Estado estado)
        {
            this.fechaHoraInicio = fechaHoraInicio;
            this.fechaHoraFin = fechaHoraFin;
            this.estado = estado;
        }

        public CambioEstado(int id, DateTime fechaHoraInicio, DateTime? fechaHoraFin, Estado estado)
        {
            this.idCambioEstado = id;
            this.fechaHoraInicio = fechaHoraInicio;
            this.fechaHoraFin = fechaHoraFin;
            this.estado = estado;
        }

        public int getId()
        {
            return idCambioEstado;
        }
        public CambioEstado()
        {
            // Constructor por defecto
        }

        public bool sosActual()
        {
            if (fechaHoraFin == null)
            {
                return true;
            }
            else return false;
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
