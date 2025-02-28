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


        // Constructor del ViewModel
        public EstadisticaViewModel(MainViewModel mainViewModel)
        {
            GenerateData(); //valors random
            SelectedChartType = "Barres"; // Establece el tipo de gráfico predeterminado
            AxisXLabels = Mesos;

            UpdateChartView = new RelayCommand(x => SelectedChartType());
        }

        // Método para generar datos de ejemplo
        private void GenerateData()
        {
            Random rand = new();
            var values = new ChartValues<double>
            {
                13, 9, 12, 14, 8, 6, 6, 1, 5, 8, 19, 11 // Valores de rendimiento de ejemplo
            };

            SeriesCollection = new SeriesCollection
            {
                new LineSeries {Values = values }
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
