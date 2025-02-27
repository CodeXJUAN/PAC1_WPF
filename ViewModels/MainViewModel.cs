using System.ComponentModel;
using System.Runtime.CompilerServices;
using WPF_MVVM_SPA_Template.Views;

namespace WPF_MVVM_SPA_Template.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        // ViewModels de les diferents opcions
        public IniciViewModel IniciVM { get; set; }
        public ClientsViewModel ClientsVM { get; set; }
        public FormViewModel FormulariVM { get; set; }
        public EstadisticaViewModel EstadisticaVM { get; set; }
        public SobreViewModel SobreVM { get; set; }

        // Propietat que conté la vista actual
        private object? _currentView;
        public object? CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        // Propietat per controlar la vista seleccionada al ListBox
        private string? _selectedView;
        public string? SelectedView
        {
            get => _selectedView;
            set
            {
                if (_selectedView != value)
                {
                    _selectedView = value;
                    OnPropertyChanged();
                    ChangeView();
                }
            }
        }

        public MainViewModel()
        {
            // Inicialitzem els diferents ViewModels
            IniciVM = new IniciViewModel(this);
            ClientsVM = new ClientsViewModel(this);
            FormulariVM = new FormViewModel(this);
            EstadisticaVM = new EstadisticaViewModel(this);
            SobreVM = new SobreViewModel(this);

            // Mostra la vista principal inicialment
            SelectedView = "IniciTag";
            ChangeView();
        }

        // Canvi de Vista
        private void ChangeView()
        {
            switch (SelectedView ?? string.Empty)
            {
                case "IniciTag":
                    IniciVM ??= new IniciViewModel(this); // Lazy loading
                    CurrentView = new IniciView { DataContext = IniciVM };
                    break;
                case "ClientsTag":
                    ClientsVM ??= new ClientsViewModel(this); // Lazy loading
                    CurrentView = new ClientsView { DataContext = ClientsVM };
                    break;
                case "SobreTag":
                    SobreVM ??= new SobreViewModel(this); // Lazy loading
                    CurrentView = new SobreView { DataContext = SobreVM };
                    break;
                default:
                    CurrentView = new IniciView { DataContext = IniciVM };
                    break;
            }
        }

        // Implementació de INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}