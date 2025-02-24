using System.ComponentModel;
using System.Runtime.CompilerServices;
using WPF_MVVM_SPA_Template.Views;

namespace WPF_MVVM_SPA_Template.ViewModels
{
    //Els ViewModels deriven de INotifyPropertyChanged per poder fer Binding de propietats
    class MainViewModel : INotifyPropertyChanged
    {

        // ViewModels de les diferents opcions
        public IniciViewModel IniciVM { get; set; }
        public ClientsViewModel ClientsVM { get; set; }
        public FormViewModel FormulariVM { get; set; }
        public EstadisticaViewModel EstadisticaVM { get; set; }

        // Propietat que conté la vista actual (és un objecte)
        private object? _currentView;
        public object? CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        // Propietat per controlar la vista seleccionada al ListBox (És un string)
        private string? _selectedView;
        public string? SelectedView
        {
            get { return _selectedView; }
            set
            {
                _selectedView = value;
                OnPropertyChanged();
                ChangeView();
            }
        }

        public MainViewModel()
        {
            // Inicialitzem els diferents ViewModels
            IniciVM = new IniciViewModel(this);
            ClientsVM = new ClientsViewModel(this);
            FormulariVM = new FormViewModel(this);
            EstadisticaVM = new EstadisticaViewModel(this);

            // Mostra la vista principal inicialment
            SelectedView = "IniciTag";
            ChangeView();
        }

        // Canvi de Vista
        private void ChangeView()
        {
            switch (SelectedView)
            {
                case "IniciTag": CurrentView = new IniciView { DataContext = IniciVM }; break;
                case "ClientsTag": CurrentView = new ClientsView { DataContext = ClientsVM }; break;
                case "//tagbuton": CurrentView = new SobreView { DataContext = ClientsVM }; break;
                
            }
        }

        // Això és essencial per fer funcionar el Binding de propietats entre Vistes i ViewModels
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
