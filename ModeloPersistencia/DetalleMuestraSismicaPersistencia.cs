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
    internal class DetalleMuestraSismicaPersistencia
    {
        public static List<DetalleMuestraSismica> detallesMuestraSismicaFromDataTable(int idMuestra)
        {
            GeneralAdapterSQL generalAdapterSQL = new GeneralAdapterSQL();
            DataTable respuesta = generalAdapterSQL.EjecutarVista("DetallesMuestra WHERE idMuestra = " + idMuestra);

            List<DetalleMuestraSismica> detalles = new List<DetalleMuestraSismica>();

            if (respuesta != null && respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() != "ERROR")
            {
                foreach (DataRow item in respuesta.Rows)
                {
                    int idDetalle = int.Parse(item["idTipoValor"].ToString() ?? "");

                    TipoDeDato tipo = TipoDeDatoPersistencia.buscarTipoDatoXID(idMuestra);

                    detalles.Add(new DetalleMuestraSismica(
                        int.Parse(item["valor"].ToString() ?? ""),
                         tipo
                        )
                        );
                }
            }
            return detalles;
        }
    }
}
