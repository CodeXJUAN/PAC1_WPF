using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using LiveCharts.Wpf;
using LiveCharts;
using WPF_MVVM_SPA_Template.Models;

namespace WPF_MVVM_SPA_Template.ViewModels
{
    // Los ViewModels derivan de INotifyPropertyChanged para poder hacer Binding de propiedades
    class EstadisticaViewModel : INotifyPropertyChanged
    {
        // Evento para notificar cambios en las propiedades
        public event PropertyChangedEventHandler? PropertyChanged;

        // Campos privados para las propiedades
        private SeriesCollection _seriesCollection;
        private string _selectedChartType;
        private int _clientId;

        public RelayCommand GuardarCommand { get; set; }

        // Lista de tipos de gráficos disponibles
        public List<string> ChartTypes { get; } = new() { "Línies", "Barres" };

        public string[] Mesos { get; set; } = new string[]
                { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };

        public string[] AxisXLabels { get; set; }

        // Propiedad para la colección de series de datos del gráfico
        public SeriesCollection SeriesCollection
        {
            get => _seriesCollection;
            set { _seriesCollection = value; OnPropertyChanged(); }
        }

        // Propiedad para el tipo de gráfico seleccionado
        public string SelectedChartType
        {
            get => _selectedChartType;
            set
            {
                if (_selectedChartType != value)
                {
                    _selectedChartType = value;
                    UpdateChartType(); // Actualiza el tipo de gráfico cuando cambia
                    OnPropertyChanged();
                }
            }
        }
        public int ClientId
        {
            get => _clientId;
            set
            {
                if (_clientId != value)
                {
                    _clientId = value;
                    GenerateData(_clientId); // Generar nuevos datos cuando cambia el ClientId
                    OnPropertyChanged();
                }
            }
        }


        // Constructor del ViewModel
        public EstadisticaViewModel(MainViewModel mainViewModel, int clientId)
        {
            ClientId = clientId; // Esto llamará a GenerateData automáticamente
            SelectedChartType = "Línies"; // Establece el tipo de gráfico predeterminado
            AxisXLabels = Mesos;
            ChartTypes = new List<string> { "Línies", "Barres" };
        }



        // Método para generar datos de ejemplo
        private void GenerateData(int clientId)
        {
            Random rand = new(clientId); // Usar clientId como semilla para generar datos diferentes
            var values = new ChartValues<double>();

            for (int i = 0; i < 12; i++)
            {
                values.Add(rand.Next(1, 20)); // Generar valores aleatorios entre 1 y 20
            }

            SeriesCollection = new SeriesCollection
            {
                new LineSeries { Values = values }
            };
        }


        // CAMBIAR segons el tipus de gràgic que hem seleccionat

        private void UpdateChartType()
        {
            var values = SeriesCollection[0].Values;

            if (SelectedChartType == "Línies")
            {
                
                SeriesCollection = new SeriesCollection
                {
                    new LineSeries { Values = values }
                };
            }
            else if (SelectedChartType == "Barres")
            {
                
                SeriesCollection = new SeriesCollection
                {
                    new ColumnSeries {Values = values }
                };
            }
        }

        // Método para notificar cambios en las propiedades
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
