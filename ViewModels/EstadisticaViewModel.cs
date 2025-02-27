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

        public List<string> ChartTypes { get; } = new() { "Líneas", "Barras" };

        public string[] Labels { get; set; } = new string[]
        { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

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
            SelectedChartType = "Líneas";
        }

        private void GenerateData()
        {
            Random rand = new();
            var values = new ChartValues<double>(Enumerable.Range(1, 12).Select(_ => (double)rand.Next(50, 200)));

            SeriesCollection = new SeriesCollection
    {
        new LineSeries { Title = "Client A", Values = values }
    };
        }


        private void UpdateChartType()
        {
            var values = SeriesCollection[0].Values;

            if (SelectedChartType == "Líneas")
            {
                SeriesCollection = new SeriesCollection
            {
                new LineSeries { Title = "Client A", Values = values }
            };
            }
            else if (SelectedChartType == "Barras")
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
