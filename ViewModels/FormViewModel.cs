using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using WPF_MVVM_SPA_Template.Models;
using WPF_MVVM_SPA_Template.Views;

namespace WPF_MVVM_SPA_Template.ViewModels
{
    // El ViewModel deriva de INotifyPropertyChanged para poder hacer Binding de propiedades
    class FormViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        // Referencia al ViewModel principal
        private readonly MainViewModel _mainViewModel;
        private readonly ClientsViewModel _clientsViewModel;

        private Client _client;
        private Client _originalClient;

        public Client Client
        {
            get { return _client; }
            set
            {
                _client = value;
                _originalClient = CloneClient(_client); // Guardar una copia del cliente original
                OnPropertyChanged();
                ValidateAllProperties();
            }
        }

        private Client CloneClient(Client client)
        {
            return new Client
            {
                Id = client.Id,
                DNI = client.DNI,
                Nom = client.Nom,
                Cognoms = client.Cognoms,
                Email = client.Email,
                Telefon = client.Telefon,
                DataAlta = client.DataAlta
            };
        }

        // Propiedades para los mensajes de error
        public string DNIError { get; set; }
        public string NomError { get; set; }
        public string CognomsError { get; set; }
        public string EmailError { get; set; }
        public string TelefonError { get; set; }
        public string DataAltaError { get; set; }

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
                // Verifica si hay errores antes de guardar
                var errores = new List<string>
                {
                    this[nameof(Client.DNI)],
                    this[nameof(Client.Nom)],
                    this[nameof(Client.Cognoms)],
                    this[nameof(Client.Email)],
                    this[nameof(Client.Telefon)],
                    this[nameof(Client.DataAlta)]
                }.Where(e => !string.IsNullOrEmpty(e)).ToList();

                if (errores.Any())
                {
                    // Establece los mensajes de error
                    ErrorMessages = string.Join("\n", errores);
                    OnPropertyChanged(nameof(ErrorMessages));
                    return;
                }

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
            // Restaurar los valores originales del cliente
            if (_originalClient != null)
            {
                Client.DNI = _originalClient.DNI;
                Client.Nom = _originalClient.Nom;
                Client.Cognoms = _originalClient.Cognoms;
                Client.Email = _originalClient.Email;
                Client.Telefon = _originalClient.Telefon;
                Client.DataAlta = _originalClient.DataAlta;
            }

            // Cambia la vista actual a la vista de clientes sin guardar cambios
            _mainViewModel.CurrentView = new ClientsView { DataContext = _mainViewModel.ClientsVM };
        }

        // Implementación de INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        // Implementación de IDataErrorInfo
        public string Error => null;
        public string ErrorMessages { get; set; }

        public string this[string columnName]
        {
            get
            {
                string result = string.Empty;
                switch (columnName)
                {
                    case nameof(Client.DNI):
                        if (string.IsNullOrWhiteSpace(Client.DNI))
                        {
                            result = "DNI no puede estar vacío.";
                            DNIError = result;
                        }
                        else
                        {
                            DNIError = string.Empty;
                        }
                        break;
                    case nameof(Client.Nom):
                        if (string.IsNullOrWhiteSpace(Client.Nom) || Client.Nom.Length < 3)
                        {
                            result = "Nom ha de tenir al menys 3 caràcters.";
                            NomError = result;
                        }
                        else
                        {
                            NomError = string.Empty;
                        }
                        break;
                    case nameof(Client.Cognoms):
                        if (string.IsNullOrWhiteSpace(Client.Cognoms) || Client.Cognoms.Length < 3)
                        {
                            result = "Cognoms ha de tenir al menys 3 caràcters.";
                            CognomsError = result;
                        }
                        else
                        {
                            CognomsError = string.Empty;
                        }
                        break;
                    case nameof(Client.Email):
                        if (string.IsNullOrWhiteSpace(Client.Email) || !Regex.IsMatch(Client.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                        {
                            result = "Correu electrònic no té un format vàlid.";
                            EmailError = result;
                        }
                        else
                        {
                            EmailError = string.Empty;
                        }
                        break;
                    case nameof(Client.Telefon):
                        if (string.IsNullOrWhiteSpace(Client.Telefon) || Client.Telefon.Length < 9)
                        {
                            result = "Teléfon ha de tenir al menys 9 digits.";
                            TelefonError = result;
                        }
                        else
                        {
                            TelefonError = string.Empty;
                        }
                        break;
                    case nameof(Client.DataAlta):
                        if (Client.DataAlta < DateOnly.FromDateTime(DateTime.Today))
                        {
                            result = "Data d'Alta no pot ser anterior a la data d'avui.";
                            DataAltaError = result;
                        }
                        else
                        {
                            DataAltaError = string.Empty;
                        }
                        break;
                }
                OnPropertyChanged(columnName + "Error");
                return result;
            }
        }

        private void ValidateAllProperties()
        {
            _ = this[nameof(Client.DNI)];
            _ = this[nameof(Client.Nom)];
            _ = this[nameof(Client.Cognoms)];
            _ = this[nameof(Client.Email)];
            _ = this[nameof(Client.Telefon)];
            _ = this[nameof(Client.DataAlta)];
        }
    }
}

class Client : INotifyPropertyChanged
{
    private int _id;
    private string _dni;
    private string _nom;
    private string _cognoms;
    private string _email;
    private string _telefon;
    private DateOnly _dataAlta;

    public int Id
    {
        get => _id;
        set
        {
            _id = value;
            OnPropertyChanged();
        }
    }

    public string DNI
    {
        get => _dni;
        set
        {
            _dni = value;
            OnPropertyChanged();
        }
    }

    public string Nom
    {
        get => _nom;
        set
        {
            _nom = value;
            OnPropertyChanged();
        }
    }

    public string Cognoms
    {
        get => _cognoms;
        set
        {
            _cognoms = value;
            OnPropertyChanged();
        }
    }

    public string Email
    {
        get => _email;
        set
        {
            _email = value;
            OnPropertyChanged();
        }
    }

    public string Telefon
    {
        get => _telefon;
        set
        {
            _telefon = value;
            OnPropertyChanged();
        }
    }

    public DateOnly DataAlta
    {
        get => _dataAlta;
        set
        {
            _dataAlta = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
