using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPF_MVVM_SPA_Template.ViewModels
{
    class SobreViewModel : INotifyPropertyChanged
    {
        private string _titulo;
        private string _autor1;
        private string _autor2;

        public string Titulo
        {
            get { return _titulo; }
            set
            {
                _titulo = value;
                OnPropertyChanged();
            }
        }

        public string Autor1
        {
            get { return _autor1; }
            set
            {
                _autor1 = value;
                OnPropertyChanged();
            }
        }

        public string Autor2
        {
            get { return _autor2; }
            set
            {
                _autor2 = value;
                OnPropertyChanged();
            }
        }

        public SobreViewModel(MainViewModel mainViewModel)
        {
            // Inicializar valores
            Titulo = "Sobre la PAC:";
            Autor1 = "Juan Manuel López";
            Autor2 = "Pau Vilardell";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}