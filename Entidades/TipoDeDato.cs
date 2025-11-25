using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PPAI_REDSISMICA.Persistencia.Persistencia;

namespace PPAI_REDSISMICA.Entidades
{
    public class TipoDeDato
    {
        public string denominacion { get; set; }

        public string nombreUnidadMedida { get; set; }

        public double valorUmbral { get; set; }

        public TipoDeDato(string denominacion, string nombreUnidadMedida, double valorUmbral)
        {
            this.denominacion = denominacion;
            this.nombreUnidadMedida = nombreUnidadMedida;
            this.valorUmbral = valorUmbral;
        }

        public TipoDeDato(DataRow data)
        {
            this.denominacion = Convert.ToString(data["denominacion"]) ?? string.Empty;
            this.nombreUnidadMedida = Convert.ToString(data["nombreUnidadMedida"]) ?? string.Empty;
            this.valorUmbral = Convert.ToDouble(data["valorUmbral"]);
        }

        public static TipoDeDato buscarTipoDatoXID(int id)
        {
            GeneralAdapterSQL generalAdapterSQL = new GeneralAdapterSQL();
            DataTable respuesta = generalAdapterSQL.EjecutarVista("TiposDato WHERE idTipo = " + id);

            if (respuesta != null && respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() != "ERROR")
            {
                TipoDeDato tipo = new TipoDeDato(respuesta.Rows[0]);
                return tipo;
            }
            else
            {
                return new TipoDeDato("", "", -1); // Retorna un TipoDeDato inválido si no se encuentra
            }
            
        }

        public TipoDeDato getDatos()
        {
            if (string.IsNullOrEmpty(denominacion) || string.IsNullOrEmpty(nombreUnidadMedida) || valorUmbral < 0)
            {
                throw new InvalidOperationException("Los datos del tipo de dato no son válidos.");
            }
            return this; // Retorna el objeto actual si los datos son válidos
        }
    }
}
