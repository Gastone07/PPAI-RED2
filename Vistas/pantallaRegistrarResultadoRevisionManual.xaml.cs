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
            // Aqu� puedes agregar la l�gica para procesar el resultado y observaciones
            MessageBox.Show("Resultado registrado correctamente.", "Informaci�n", MessageBoxButton.OK, MessageBoxImage.Information);
            this.DialogResult = true;
            this.Close();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        public void presentarEventosSismicosPendientesDeRevision(System.Collections.Generic.List<object> eventosPreparados)
        {
            // Asignar la lista (objetos anónimos preparados por el controlador)
            dgEventosSismicos.ItemsSource = null;
            dgEventosSismicos.ItemsSource = eventosPreparados;

            // Configurar las columnas del DataGrid para mostrar las propiedades esperadas en los objetos preparados
            dgEventosSismicos.Columns.Clear();
            dgEventosSismicos.Columns.Add(new DataGridTextColumn
            {
                Header = "FechaHora",
                Binding = new Binding("FechaHora")
            });
            dgEventosSismicos.Columns.Add(new DataGridTextColumn
            {
                Header = "Magnitud",
                Binding = new Binding("Magnitud")
            });
            dgEventosSismicos.Columns.Add(new DataGridTextColumn
            {
                Header = "Estado",
                Binding = new Binding("Estado")
            });
            dgEventosSismicos.Columns.Add(new DataGridTextColumn
            {
                Header = "CoordenadasEpicentro",
                Binding = new Binding("CoordenadasEpicentro")
            });
            dgEventosSismicos.Columns.Add(new DataGridTextColumn
            {
                Header = "CoordenadasHipocentro",
                Binding = new Binding("CoordenadasHipocentro")
            });
        }

        private void dgEventosSismicos_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var selected = dgEventosSismicos.SelectedItem;
            if (selected == null)
            {
                MessageBox.Show("No hay ningún elemento seleccionado.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Si la vista recibe directamente un EventoSismico
            if (selected is EventoSismico eventoSeleccionado)
            {
                gestor.tomarSeleccionEventoSismico(eventoSeleccionado);
                return;
            }

            // Si la vista recibió objetos anónimos preparados por el controlador,
            // estos deben incluir una propiedad 'EventoOriginal' que referencia el EventoSismico real.
            var prop = selected.GetType().GetProperty("EventoOriginal");
            if (prop != null)
            {
                var original = prop.GetValue(selected) as EventoSismico;
                if (original != null)
                {
                    gestor.tomarSeleccionEventoSismico(original);
                    return;
                }
            }

            MessageBox.Show("No se pudo obtener el evento seleccionado.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

            // Validar campos vac�os
            if (string.IsNullOrWhiteSpace(alcance) || string.IsNullOrWhiteSpace(origen) || string.IsNullOrWhiteSpace(magnitudStr))
            {
                MessageBox.Show("Debe completar los campos Alcance, Origen y Magnitud.", "Campos obligatorios", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Validar tipo de magnitud
            if (!double.TryParse(magnitudStr, out double magnitud))
            {
                MessageBox.Show("El campo Magnitud debe ser un n�mero v�lido.", "Valor incorrecto", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Aqu� puedes usar los valores como necesites, por ejemplo:
            MessageBox.Show($"Opci�n: {opcionSeleccionada}\nAlcance: {alcance}\nOrigen: {origen}\nMagnitud: {magnitud}", "Valores seleccionados");

            gestor.tomarOpcionGrilla(opcionSeleccionada, alcance, origen, magnitud);
        }
    }
}