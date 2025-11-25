using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using static PPAI_REDSISMICA.Persistencia.Persistencia;

namespace PPAI_REDSISMICA.Entidades
{
    public class DetalleMuestraSismica
    {
        public double valor { get; set; }

        private TipoDeDato tipoDeDato;
        public DetalleMuestraSismica(double valor, TipoDeDato tipoDeDato)
        {
            this.valor = valor;
            this.tipoDeDato = tipoDeDato;
        }

        public static List<DetalleMuestraSismica > detallesMuestraSismicaFromDataTable(int idMuestra)
        {
            GeneralAdapterSQL generalAdapterSQL = new GeneralAdapterSQL();
            DataTable respuesta = generalAdapterSQL.EjecutarVista("DetallesMuestra WHERE idMuestra = " + idMuestra);

            List<DetalleMuestraSismica> detalles = new List<DetalleMuestraSismica>();

            if (respuesta != null && respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() != "ERROR")
            {
                foreach (DataRow item in respuesta.Rows)
                {
                    detalles.Add(new DetalleMuestraSismica(item));
                }
            }
            return detalles;
        }       

        public DetalleMuestraSismica(DataRow data)
        {
            this.valor = Convert.ToDouble(data["valor"]);
            
            this.tipoDeDato = TipoDeDato.buscarTipoDatoXID((int)data["idTipoValor"]);
        }

        public TipoDeDato getTipoDato()
        {
            if (tipoDeDato == null)
            {
                throw new InvalidOperationException("No hay tipo de dato asociado a la muestra sismica.");
            }
            return tipoDeDato.getDatos();
        }
    }
}
