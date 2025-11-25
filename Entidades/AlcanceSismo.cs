using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PPAI_REDSISMICA.Persistencia.Persistencia;

namespace PPAI_REDSISMICA.Entidades
{
    public class AlcanceSismo
    {
        private string descripcion;
        private string nombre;

        public AlcanceSismo(string nombre, string descripcion)
        {
            this.nombre = nombre;
            this.descripcion = descripcion;
        }

        public AlcanceSismo()
        {

        }

        public AlcanceSismo(DataRow data)
        {
            this.nombre = Convert.ToString(data["nombre"]) ?? string.Empty;
            this.descripcion = Convert.ToString(data["descripcion"]) ?? string.Empty;
        }

        public string getNombre()
        {
            return nombre;
        }

        public static AlcanceSismo recuperarAlcanceSismoXID(int id)
        {
            GeneralAdapterSQL generalAdapterSQL = new GeneralAdapterSQL();
            DataTable respuesta = generalAdapterSQL.EjecutarVista("Alcance WHERE idSismo = " + id);
            AlcanceSismo alcance = new AlcanceSismo();
            if (respuesta != null && respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() != "ERROR")
            {
                alcance = new(respuesta.Rows[0]);

            }
            return alcance;

        }



    }
}
