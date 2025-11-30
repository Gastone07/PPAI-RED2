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
    internal class MuestraSismicaPersistencia
    {

        public static List<MuestraSismica> obtenerMuestrasXID(int id)
        {
            GeneralAdapterSQL generalAdapterSQL = new GeneralAdapterSQL();
            DataTable respuesta = generalAdapterSQL.EjecutarVista("Muestras WHERE idSerieTemporal = " + id);

            List<MuestraSismica> detalles = new List<MuestraSismica>();

            if (respuesta != null && respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() != "ERROR")
            {
                foreach (DataRow item in respuesta.Rows)
                {
                    int idMuestra = int.Parse(item["idMuestra"].ToString() ?? "");

                    List<DetalleMuestraSismica> detallesMuestras = DetalleMuestraSismicaPersistencia.detallesMuestraSismicaFromDataTable(idMuestra);

                    detalles.Add(new MuestraSismica(
                         DateTime.Parse(item["fechaHoraMuestra"].ToString() ?? ""),
                         detallesMuestras
                        ));
                }
            }
            return detalles;
        }
    }
}
