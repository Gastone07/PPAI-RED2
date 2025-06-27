using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPAI_REDSISMICA.Entidades;

namespace PPAI_REDSISMICA.DTO
{
    public class EventoSismicoDTO
    {
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public string Magnitud { get; set; }

        // Epicentro separado
        public double LatitudEpicentro { get; set; }
        public double LongitudEpicentro { get; set; }

        // Hipocentro separado
        public double LatitudHipocentro { get; set; }
        public double LongitudHipocentro { get; set; }
        public string Estado { get; set; }
        public string Clasificacion { get; set; }
        public string Alcance { get; set; }
        public string Origen { get; set; }

        public EventoSismico EventoOriginal { get; set; }

        public EventoSismicoDTO(EventoSismico evento)
        {
            EventoOriginal = evento;
            FechaInicio = evento.getFechaHoraOcurrencia().ToString("dd/MM/yyyy HH:mm");
            FechaFin = evento.getFechaHoraFin().ToString("dd/MM/yyyy HH:mm");
            Magnitud = $"{evento.getMagnitud():0.0}";
            var epicentro = (double[])evento.getCordenadasEpicentro();
            LatitudEpicentro = epicentro[0];
            LongitudEpicentro = epicentro[1];

            var hipocentro = (double[])evento.getCordenadasHipocentro();
            LatitudHipocentro = hipocentro[0];
            LongitudHipocentro = hipocentro[1];
        }
    }

}
