using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPF_MVVM_SPA_Template.ViewModels
{
    class IniciViewModel : INotifyPropertyChanged
    {
        private string _titulo;
        public string Titulo
        {
            get { return _titulo; }
            set { _titulo = value; OnPropertyChanged(); }
        }

        public IniciViewModel(MainViewModel mainViewModel)
        {
            // Inicializar valores
            Titulo = "¡Benvingut a la Nostre PAC!";
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
