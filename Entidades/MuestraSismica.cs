using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PPAI_REDSISMICA.Persistencia.Persistencia;

namespace PPAI_REDSISMICA.Entidades
{
    public class MuestraSismica
    {
        public DateTime fechaHoraMuestra { get; set; }

        private List<DetalleMuestraSismica> detallesMuestrasSismicas = new List<DetalleMuestraSismica>();

        public MuestraSismica(DateTime fechaHoraMuestra, List<DetalleMuestraSismica> detallesMuestrasSismicas)
        {
            this.fechaHoraMuestra = fechaHoraMuestra;
            this.detallesMuestrasSismicas = detallesMuestrasSismicas;

        }

        public void getDatos(List<DetalleMuestraSismica> detallesVisitados, List<(DetalleMuestraSismica, TipoDeDato)> tipoDatoPorDetalle)
        {
            if (this.detallesMuestrasSismicas == null || this.detallesMuestrasSismicas.Count == 0)
            {
                throw new InvalidOperationException("No hay detalles de muestra sismica asociados a la muestra.");
            }
            else
            {
                foreach (var detalle in this.detallesMuestrasSismicas)
                {
                    detallesVisitados.Add(detalle);
                    var tipo = detalle.getTipoDato();

                    tipoDatoPorDetalle.Add((detalle, tipo));
                    //return detalle.getTipoDato(); // Obtiene los datos de la muestra sismica
                }   
            }
        }

        public static List<MuestraSismica> obtenerMuestras()
        {
            GeneralAdapterSQL generalAdapterSQL = new GeneralAdapterSQL();
            DataTable respuesta = generalAdapterSQL.EjecutarVista("Muestras");

            List<MuestraSismica> detalles = new List<MuestraSismica>();

            if (respuesta != null && respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() != "ERROR")
            {
                foreach (DataRow item in respuesta.Rows)
                {
                    detalles.Add(new MuestraSismica(item));
                }
            }
            return detalles;
        }

        public MuestraSismica(DataRow data)
        { 
            this.fechaHoraMuestra = Convert.ToDateTime(data["fechaHoraMuestra"]);
            this.detallesMuestrasSismicas = DetalleMuestraSismica.detallesMuestraSismicaFromDataTable((int)data["idMuestra"]);
        }
    }

}
