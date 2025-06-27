using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAI_REDSISMICA.Entidades
{
    public class Estado
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

        public static Estado esRechazado(List<Estado> estados)
        {
            foreach (var estado in estados)
            {
                if (estado.esAmbitoEvento())
                {
                    if (estado.esEstadoRechazado())
                    {
                        return estado; // Retorna el estado bloqueado encontrado
                    }
                }
            }
            return new Estado(); // Si no se encuentra el estado bloqueado
        }

        public static Estado esConfirmado(List<Estado> estados)
        {
            foreach (var estado in estados)
            {
                if (estado.esAmbitoEvento())
                {
                    if (estado.esEstadoConfirmado())
                    {
                        return estado; // Retorna el estado bloqueado encontrado
                    }
                }
            }
            return new Estado(); // Si no se encuentra el estado bloqueado
        }

        public static Estado esRevisadoExperto(List<Estado> estados)
        {
            foreach (var estado in estados)
            {
                if (estado.esAmbitoEvento())
                {
                    if (estado.esEstadoRevisadoPorExperto())
                    {
                        return estado; // Retorna el estado bloqueado encontrado
                    }
                }
            }
            return new Estado(); // Si no se encuentra el estado bloqueado
        }

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

