using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WPF_MVVM_SPA_Template.Models;
using WPF_MVVM_SPA_Template.Views;

namespace WPF_MVVM_SPA_Template.ViewModels
{
    //Els ViewModels deriven de INotifyPropertyChanged per poder fer Binding de propietats
    class ClientsViewModel : INotifyPropertyChanged
    {
        // Referència al ViewModel principal
        private readonly MainViewModel _mainViewModel;

        // Col·lecció de Clients (podrien carregar-se d'una base de dades)
        // ObservableCollection és una llista que notifica els canvis a la vista
        public ObservableCollection<Clients> Clients { get; set; } = new ObservableCollection<Clients>();

        // Propietat per controlar el Client seleccionat a la vista
        private Clients? _selectedClients;
        public Clients? SelectedClients
        {
            get { return _selectedClients; }
            set { _selectedClients = value; OnPropertyChanged(); }
        }

        // RelayCommands connectats via Binding als botons de la vista
        public RelayCommand AfegirClientForm { get; set; }
        public RelayCommand MostarEstadisticaCommand { get; set; }

        public ClientsViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            // Carreguem estudiants a memòria mode de prova
            Clients.Add(new Clients { Id = 1, DNI = "12345678A", Nom = "David", Cognoms = "Gonzalez", Email = "dgonalez@email.com", Telefon = "684 32 23 34" });
            Clients.Add(new Clients { Id = 2, DNI = "98765432B", Nom = "Jordi", Cognoms = "Surinyac", Email = "jsurinyac@email.com", Telefon = "652 42 11 56" });

            // Inicialitzem els diferents commands disponibles (accions)
            AfegirClientForm = new RelayCommand(x => AfegirClients());
            MostarEstadisticaCommand = new RelayCommand(x => MostrarRendiment());
        }

        //Mètodes per afegir i eliminar estudiants de la col·lecció
        private void AfegirClients()
        {
            _mainViewModel.CurrentView = new FormulariView { DataContext = null };
        }

        private void MostrarRendiment()
        {
            _mainViewModel.CurrentView = new EstadisticaView{ DataContext = null };
        }

        // Això és essencial per fer funcionar el Binding de propietats entre Vistes i ViewModels
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}

