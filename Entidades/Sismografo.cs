using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static PPAI_REDSISMICA.Persistencia.Persistencia;

namespace PPAI_REDSISMICA.Entidades
{
    public class Sismografo
    {
        private DateTime fechaAdquisicion { get; set; }

        private int identificador { get; set; }

        private int nroSerie { get; set; }

        private EstacionSismografica estacionSismografica;

        private List<SerieTemporal> seriesTemporales = new List<SerieTemporal>();
        public Sismografo(DateTime fechaAdquisicion, int identificador, int nroSerie, EstacionSismografica estacionSismografica, List<SerieTemporal> seriesTemporales)
        {
            this.fechaAdquisicion = fechaAdquisicion;
            this.identificador = identificador;
            this.nroSerie = nroSerie;
            this.estacionSismografica = estacionSismografica;
            this.seriesTemporales = seriesTemporales;
        }

        public string getNombreEstacion()
        {
            return this.estacionSismografica.getNombre(); // Obtiene el nombre de la estacion sismografica
        }

        public List<SerieTemporal> getSeriesTemporales()
        {
            return this.seriesTemporales; // Retorna las series temporales del sismografo
        }

        public Boolean sosSerieTemporal(SerieTemporal serieTemporalBuscada)
        {
            if (this.seriesTemporales.Contains(serieTemporalBuscada)) { 
                return true; 
            } 
            else { 
                return false;
            }
        }

    }
}
