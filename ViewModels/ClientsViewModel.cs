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
        public FormViewModel FormulariVM { get; set; }
        public EstadisticaViewModel EstadisticaVM { get; set; }
        // Referència al ViewModel principal
        private readonly MainViewModel _mainViewModel;


        // Col·lecció de Clients (podrien carregar-se d'una base de dades)
        // ObservableCollection és una llista que notifica els canvis a la vista
        public ObservableCollection<Client> Clients { get; set; } = new ObservableCollection<Client>();

        // Propietat per controlar el Client seleccionat a la vista
        private Client? _selectedClients;
        public Client? SelectedClients
        {
            get { return _selectedClients; }
            set { _selectedClients = value; OnPropertyChanged(); }
        }

        // RelayCommands connectats via Binding als botons de la vista
        public RelayCommand AfegirClientForm { get; set; }
        public RelayCommand MostarEstadisticaCommand { get; set; }

        public RelayCommand EliminarClientCommand { get; set; }
        public RelayCommand EditarClientCommand { get; set; }

        public ClientsViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;

            FormulariVM = new FormViewModel(_mainViewModel, this);

            // Inicialitzem els diferents commands disponibles (accions)
            AfegirClientForm = new RelayCommand(x => AfegirClients());
            MostarEstadisticaCommand = new RelayCommand(x => MostrarRendiment());

            EliminarClientCommand = new RelayCommand(x => EliminarClients());
            EditarClientCommand = new RelayCommand(x => EditarClient());


            EstadisticaVM = new EstadisticaViewModel(_mainViewModel);

        }

        //Mètodes per afegir i eliminar estudiants de la col·lecció
        private void AfegirClients()
        {
            // Crea un nuevo cliente con un ID basado en el número de clientes existentes
            FormulariVM.Client = new Client { Id = Clients.Count + 1 }; // ID = Número de clientes + 1
            _mainViewModel.CurrentView = new FormulariView { DataContext = FormulariVM };
        }

        private void MostrarRendiment()
        {
            _mainViewModel.CurrentView = new EstadisticaView { DataContext = EstadisticaVM };
        }

        private void EliminarClients()
        {
            if (SelectedClients != null)
                Clients.Remove(SelectedClients);

        }
        public bool HayClienteSeleccionado => SelectedClients != null;

        private void EditarClient()
        {
            if (SelectedClients != null)
            {
                // Pasar el cliente seleccionado al FormViewModel
                FormulariVM.Client = SelectedClients;
                _mainViewModel.CurrentView = new FormulariView { DataContext = FormulariVM };
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

