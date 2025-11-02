using Controladores;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Linq;
using System.Windows.Data;

namespace Vistas
{
    public partial class pantallaRegistrarResultadoRevisionManual : Window
    {
        #region atributos

        public controladorRegistrarResultadoRevisionManual gestor;
        public List<EventoSismico> eventosSismicos = new List<EventoSismico>();

        #endregion

        public pantallaRegistrarResultadoRevisionManual()
        {
            habilitar();
            gestor = new controladorRegistrarResultadoRevisionManual(this);
            gestor.registrarResultadoDeRevisionManual();
        }

        public void habilitar()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            // Aquí puedes agregar la lógica para procesar el resultado y observaciones
            MessageBox.Show("Resultado registrado correctamente.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            this.DialogResult = true;
            this.Close();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        public void presentarEventosSismicosPendientesDeRevision(List<EventoSismico> eventosPreparados)
        {
            // Asignar directamente la lista de eventos sísmicos al DataGrid
            dgEventosSismicos.ItemsSource = null;
            dgEventosSismicos.ItemsSource = eventosPreparados;

            // Configurar las columnas del DataGrid para mostrar las propiedades del objeto EventoSismico
            dgEventosSismicos.Columns.Clear();
            dgEventosSismicos.Columns.Add(new DataGridTextColumn
            {
                Header = "FechaHora",
                Binding = new Binding("FechaHoraOcurrencia")
            });
            dgEventosSismicos.Columns.Add(new DataGridTextColumn
            {
                Header = "Magnitud",
                Binding = new Binding("ValorMagnitud")
            });
            dgEventosSismicos.Columns.Add(new DataGridTextColumn
            {
                Header = "Estado",
                Binding = new Binding("estadoActual.nombreEstado")
            });
            dgEventosSismicos.Columns.Add(new DataGridTextColumn
            {
                Header = "CoordenadasEpicentro",
                Binding = new Binding("LatitudEpicentro") // Ejemplo: mostrar latitud
            });
            dgEventosSismicos.Columns.Add(new DataGridTextColumn
            {
                Header = "CoordenadasHipocentro",
                Binding = new Binding("LatitudHipocentro") // Ejemplo: mostrar latitud
            });
        }

        private void dgEventosSismicos_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (dgEventosSismicos.SelectedItem is EventoSismico eventoSeleccionado)
            {
                gestor.tomarSeleccionEventoSismico(eventoSeleccionado);
            }
            else
            {
                MessageBox.Show("No se pudo obtener el evento seleccionado.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void mostrarDetalleEventoSismico(string alcance, string clasificacion, string origen, double magnitud)
        {
            panelDetalles.Visibility = Visibility.Visible;
            txtAlcance.Text = alcance;
            txtClasificacion.Text = clasificacion;
            txtOrigen.Text = origen;
            txtMagnitud.Text = magnitud.ToString(); // Convert 'double' to 'string' using 'ToString()'  
        }

        private void btnVisualizarMapa_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                imgMapa.Source = new BitmapImage(new Uri("pack://application:,,,/img/redSismica1.jpg"));
                imgMapa.Visibility = Visibility.Visible;
            }
            catch
            {
                MessageBox.Show("No se pudo mostrar el mapa", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void habilitarOpcionModificarDatos()
        {
        }

        private void cbOpciones_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnConfirmar.IsEnabled = cbOpciones.SelectedIndex >= 0;
        }

        private void btnConfirmar_Click(object sender, RoutedEventArgs e)
        {
            string? opcionSeleccionada = (cbOpciones.SelectedItem as ComboBoxItem)?.Content.ToString();
            string alcance = txtAlcance.Text.Trim();
            string origen = txtOrigen.Text.Trim();
            string magnitudStr = txtMagnitud.Text.Trim();

            // Validar campos vacíos
            if (string.IsNullOrWhiteSpace(alcance) || string.IsNullOrWhiteSpace(origen) || string.IsNullOrWhiteSpace(magnitudStr))
            {
                MessageBox.Show("Debe completar los campos Alcance, Origen y Magnitud.", "Campos obligatorios", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Validar tipo de magnitud
            if (!double.TryParse(magnitudStr, out double magnitud))
            {
                MessageBox.Show("El campo Magnitud debe ser un número válido.", "Valor incorrecto", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Aquí puedes usar los valores como necesites, por ejemplo:
            MessageBox.Show($"Opción: {opcionSeleccionada}\nAlcance: {alcance}\nOrigen: {origen}\nMagnitud: {magnitud}", "Valores seleccionados");

            gestor.tomarOpcionGrilla(opcionSeleccionada, alcance, origen, magnitud);
        }
    }
}