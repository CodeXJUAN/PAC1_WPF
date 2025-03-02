using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WPF_MVVM_SPA_Template.Models;
using WPF_MVVM_SPA_Template.Views;

namespace WPF_MVVM_SPA_Template.ViewModels
{
    // El ViewModel deriva de INotifyPropertyChanged para poder hacer Binding de propiedades
    class FormViewModel : INotifyPropertyChanged
    {
        // Referencia al ViewModel principal
        private readonly MainViewModel _mainViewModel;
        private readonly ClientsViewModel _clientsViewModel;

        private Client _client;

        public Client Client
        {
            get { return _client; }
            set { _client = value; OnPropertyChanged(); }
        }
        // Comandos para los botones de la vista
        public RelayCommand GuardarCommand { get; set; }
        public RelayCommand CancelarCommand { get; set; }

        // Constructor del ViewModel
        public FormViewModel(MainViewModel mainViewModel, ClientsViewModel clientsViewModel)
        {
            _mainViewModel = mainViewModel;
            _clientsViewModel = clientsViewModel;

            // Inicializamos los comandos
            GuardarCommand = new RelayCommand(x => Guardar());
            CancelarCommand = new RelayCommand(x => Cancelar());

        }

        // Método para guardar los datos del formulario
        private void Guardar()
        {
            if (Client != null)
            {
                var clienteExistente = _clientsViewModel.Clients.FirstOrDefault(c => c.Id == Client.Id);

                if (clienteExistente != null)
                {
                    // Actualiza las propiedades del cliente existente
                    clienteExistente.DNI = Client.DNI;
                    clienteExistente.Nom = Client.Nom;
                    clienteExistente.Cognoms = Client.Cognoms;
                    clienteExistente.Email = Client.Email;
                    clienteExistente.Telefon = Client.Telefon;
                    clienteExistente.DataAlta = Client.DataAlta;
                }
                else
                {
                    // Si es un nuevo cliente, lo agregas a la lista
                    _clientsViewModel.Clients.Add(Client);
                }

                // Regresar a la vista de clientes
                _mainViewModel.CurrentView = new ClientsView { DataContext = _clientsViewModel };
            }
        }

        // Método para cancelar la edición del formulario
        private void Cancelar()
        {
            // Cambia la vista actual a la vista de clientes
            _mainViewModel.CurrentView = new ClientsView { DataContext = _mainViewModel.ClientsVM };
        }

        // Implementación de INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
