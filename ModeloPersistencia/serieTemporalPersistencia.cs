using PPAI_REDSISMICA.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PPAI_REDSISMICA.Persistencia.Persistencia;

namespace PPAI_REDSISMICA.ModeloPersistencia
{
    internal class serieTemporalPersistencia
    {

        public static List<SerieTemporal> recuperarSeriesTemporalesPorEventoSismico(int id)
        {
            GeneralAdapterSQL generalAdapterSQL = new GeneralAdapterSQL();
            DataTable respuesta = generalAdapterSQL.EjecutarVista("SeriesTemporales WHERE idEventoSismico = " + id);
            List<SerieTemporal> listaSeries = new List<SerieTemporal>();

            if (respuesta != null && respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() != "ERROR")
            {
                foreach (DataRow item in respuesta.Rows)
                {
                    int idSerie = int.Parse(item["idSerie"].ToString() ?? "");
                    List<MuestraSismica> muestras = MuestraSismicaPersistencia.obtenerMuestrasXID(id);


                    listaSeries.Add(new SerieTemporal(
                        idSerie,
                        item["condicionAlarma"].ToString() ?? "",
                        DateTime.Parse(item["fechaHoraInicioRegistroMuestras"].ToString() ?? ""),
                        DateTime.Parse(item["fechaHoraRegistro"].ToString() ?? ""),
                        item["frecuenciaMuestreo"].ToString() ?? "",
                        muestras
                        )
                        );
                }
            }
            return listaSeries;

        }
    }
}
