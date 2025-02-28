using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using LiveCharts.Wpf;
using LiveCharts;
using WPF_MVVM_SPA_Template.Models;

namespace WPF_MVVM_SPA_Template.ViewModels
{
    //Els ViewModels deriven de INotifyPropertyChanged per poder fer Binding de propietats
    class EstadisticaViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private SeriesCollection _seriesCollection;
        private string _selectedChartType;

        public List<string> ChartTypes { get; } = new() { "Línies", "Barres" };

        public string[] Mesos { get; set; } = new string[]
        { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };

        public string[] Rendiment { get; set; } = new string[]
        { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };

        public SeriesCollection SeriesCollection
        {
            get => _seriesCollection;
            set { _seriesCollection = value; OnPropertyChanged(); }
        }

        public string SelectedChartType
        {
            get => _selectedChartType;
            set
            {
                if (_selectedChartType != value)
                {
                    _selectedChartType = value;
                    UpdateChartType();
                    OnPropertyChanged();
                }
            }
        }

        public EstadisticaViewModel(MainViewModel mainViewModel)
        {
            GenerateData();
            SelectedChartType = "Línies";
        }

        private void GenerateData()
        {
            Random rand = new();
            var values = new ChartValues<double>
            {
                16, 14, 12, 10, 8, 6, 4, 0, 5, 7, 9, 11 // Valores de rendimiento de la imagen
            };

            SeriesCollection = new SeriesCollection
            {
                new LineSeries { Title = "Client A", Values = values }
            };
        }

        private void UpdateChartType()
        {
            var values = SeriesCollection[0].Values;

            if (SelectedChartType == "Línies")
            {
                SeriesCollection = new SeriesCollection
                {
                    new LineSeries { Title = "Client A", Values = values }
                };
            }
            else if (SelectedChartType == "Barres")
            {
                SeriesCollection = new SeriesCollection
                {
                    new ColumnSeries { Title = "Client A", Values = values }
                };
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
