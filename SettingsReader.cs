using Newtonsoft.Json;
using System.IO;

namespace PPAI_REDSISMICA
{
    /// <summary>
    /// La clase lectora de appsettings.json
    /// </summary>
    public class SettingsReader
    {
        /// <summary>
        /// Que Host permite
        /// </summary>
        public string AllowedHosts = string.Empty;
        /// <summary>
        /// Que entonrno tenemos
        /// </summary>
        public string Env = string.Empty;
        /// <summary>
        /// Las cadenas de conexion disponibles
        /// </summary>
        public Dictionary<string, string> ConnectionStrings = new();
        /// <summary>
        /// Constructor Vacio
        /// </summary>
        public SettingsReader()
        {
        }
        /// <summary>
        /// Metodo estatico que busca el AppSettings
        /// </summary>
        /// <returns>La clase con los datos del appsettings.json</returns>
        public static SettingsReader GetAppSettings()
        {
            try
            {
                //Va buscar desde donde lo estamos ejecutando
                string file = Directory.GetCurrentDirectory() + "\\appsettings.json";
                //Va a leer el archivo y convierte en una cadena
                using StreamReader reader = new(file);
                //Lo lee y convierte en String
                var json = reader.ReadToEnd();
                //Prueba convirtiendolo a la clase que creamos
                return JsonConvert.DeserializeObject<SettingsReader>(json) ?? new();
            }
            catch (Exception ex)
            {
                //Si falla lo crea de manera generica
                return new();
            }

        }
    }
}
