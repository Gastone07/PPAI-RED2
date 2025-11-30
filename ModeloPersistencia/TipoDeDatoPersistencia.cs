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
    internal class TipoDeDatoPersistencia
    {
        public static TipoDeDato buscarTipoDatoXID(int id)
        {
            GeneralAdapterSQL generalAdapterSQL = new GeneralAdapterSQL();
            DataTable respuesta = generalAdapterSQL.EjecutarVista("TiposDato WHERE idTipo = " + id);

            if (respuesta != null && respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() != "ERROR")
            {
                TipoDeDato tipo = new TipoDeDato(
                    respuesta.Rows[0][0].ToString() ?? "", 
                    respuesta.Rows[0][1].ToString() ?? "", 
                    double.Parse(respuesta.Rows[0][2].ToString() ?? "") 
                    );
                
                return tipo;
            }
            else
            {
                return new TipoDeDato("", "", -1); // Retorna un TipoDeDato inválido si no se encuentra
            }

        }
    }
}
