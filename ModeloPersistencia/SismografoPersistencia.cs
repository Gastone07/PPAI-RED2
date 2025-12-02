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
    internal class SismografoPersistencia
    {

        public static List<Sismografo> obtenerSismografos()
        {
            GeneralAdapterSQL generalAdapterSQL = new GeneralAdapterSQL();
            DataTable respuesta = generalAdapterSQL.EjecutarVista("sismografos");
            List<Sismografo> listaSismografos = new List<Sismografo>();

            if (respuesta != null && respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() != "ERROR")
            {

                //Hubo un error al consultar la base de datos
                foreach (DataRow item in respuesta.Rows)
                {

                    int idSismografo = int.Parse(item["identificadorSismografo"].ToString() ?? "");
                    EstacionSismografica estacion = EstacionSismograficaPersistencia.obtenerEstacionXID(idSismografo);
                    List<SerieTemporal> series = serieTemporalPersistencia.recuperarSeriesTemporalesPorSismografo(idSismografo);

                    listaSismografos.Add(new Sismografo(
                        DateTime.Parse(item["fechaAdquisicion"].ToString() ?? ""),
                        int.Parse(item["identificadorSismografo"].ToString() ?? ""),
                        int.Parse(item["nroSerie"].ToString() ?? ""),
                        estacion,
                        series
                        ));
                }

            }
            return listaSismografos;
        }
    }
}
