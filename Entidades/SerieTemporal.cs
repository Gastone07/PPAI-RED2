using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAI_REDSISMICA.Entidades
{
    public class SerieTemporal
    {
        private bool condicionAlarma { get; set; }

        private DateTime fechaHoraInicioRegistroMuestras { get; set; }

        private DateTime fechaHoraRegistro { get; set; }

        private int frecuenciaMuestreo { get; set; }

        private List<MuestraSismica> muestrasSismicas = new List<MuestraSismica>();

        // Relacion con Sismografo inversa
        //private Sismografo? sismografo;

        public SerieTemporal(bool condicionAlarma, DateTime fechaHoraInicioRegistroMuestras, DateTime fechaHoraRegistro, int frecuenciaMuestreo, List<MuestraSismica> muestrasSismicas)
        {
            this.condicionAlarma = condicionAlarma;
            this.fechaHoraInicioRegistroMuestras = fechaHoraInicioRegistroMuestras;
            this.fechaHoraRegistro = fechaHoraRegistro;
            this.frecuenciaMuestreo = frecuenciaMuestreo;
            this.muestrasSismicas = muestrasSismicas;
            //this.sismografo = sismografo;
        }

        public void getDatos(List<MuestraSismica> muestrasVisitadas, List<DetalleMuestraSismica> detallesVisitados, List<(DetalleMuestraSismica, TipoDeDato)> tipoDatoPorDetalle)
        {
            //this.sismografo.getNombreEstacion(); // Obtiene el nombre de la estacion del sismografo

            if (muestrasSismicas == null || muestrasSismicas.Count == 0)
            {
                throw new InvalidOperationException("No hay muestras asociadas a la serie temporal.");
            }
            else
            {
                foreach (var muestra in muestrasSismicas)
                {
                    muestrasVisitadas.Add(muestra);
                    muestra.getDatos(detallesVisitados, tipoDatoPorDetalle);

                    //muestra.getDatos(); // Obtiene los datos de la serie temporal
                }
            }
            //throw new InvalidOperationException("No hay muestras temporal validas.");
        }


    }
}
