using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PPAI_REDSISMICA.Persistencia.Persistencia;

namespace PPAI_REDSISMICA.Entidades
{
    public abstract class Estado
    {
        public string ambito { get; set; }
        public string nombreEstado { get; set; }

        public Estado(string ambito, string nombreEstado)
        {
            this.ambito = ambito;
            this.nombreEstado = nombreEstado;
        }

        public Estado()
        {
            // Constructor por defecto
        }

        public abstract bool esAutoDetectado();
        public abstract bool esBloqueadoEnRevision();
        public abstract bool esPendienteParaRevision();
        public abstract bool esPendienteRevisionExperto();
        public abstract bool esRechazado();
        public abstract bool esConfirmado();

        public abstract void cambiarEstadoEventoSismico(DateTime fechaHora, CambioEstado cambio, EventoSismico idEvento);
        public bool esAmbitoEvento()
        {
            if (this.ambito == "evento")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool esEstadoBloqueado()
        {
            if (this.nombreEstado == "Bloqueado")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool esEstadoRechazado()
        {
            if (this.nombreEstado == "Rechazado")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool esEstadoConfirmado()
        {
            if (this.nombreEstado == "Confirmado")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool esEstadoRevisadoPorExperto()
        {
            if (this.nombreEstado == "RevisadoPorExperto")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string getNombreEstado()
        {
            return this.nombreEstado;
        }

        public bool esPendienteDeRevision()
        {
            if (this.nombreEstado == "NoRevisado")
            {
                return true; // El estado es pendiente de revision
            }
            else
            {
                return false; // El estado no es pendiente de revision
            }
        }

        
    }
}


