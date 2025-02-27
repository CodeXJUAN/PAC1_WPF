using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WPF_MVVM_SPA_Template.Models;

namespace WPF_MVVM_SPA_Template.ViewModels
{
    // El ViewModel deriva de INotifyPropertyChanged para poder hacer Binding de propiedades
    class FormViewModel : INotifyPropertyChanged
    {
        // Referencia al ViewModel principal
        private readonly MainViewModel _mainViewModel;

        // Propiedades para el formulario
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(); }
        }

        private string _nom;
        public string Nom
        {
            get { return _nom; }
            set { _nom = value; OnPropertyChanged(); }
        }

        private string _cognoms;
        public string Cognoms
        {
            get { return _cognoms; }
            set { _cognoms = value; OnPropertyChanged(); }
        }

        private string _dni;
        public string DNI
        {
            get { return _dni; }
            set { _dni = value; OnPropertyChanged(); }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(); }
        }

        private string _telefono;
        public string Telefono
        {
            get { return _telefono; }
            set { _telefono = value; OnPropertyChanged(); }
        }

        private DateTime _dataAlta;
        public DateTime DataAlta
        {
            get { return _dataAlta; }
            set { _dataAlta = value; OnPropertyChanged(); }
        }

        // Comandos para los botones de la vista
        public RelayCommand GuardarCommand { get; set; }
        public RelayCommand CancelarCommand { get; set; }

        public FormViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;

            // Inicializamos los comandos
            GuardarCommand = new RelayCommand(x => Guardar());
            CancelarCommand = new RelayCommand(x => Cancelar());
        }

        // Método para guardar los datos del formulario
        private void Guardar()
        {
            // Aquí puedes agregar la lógica para guardar los datos
            // Por ejemplo, agregar un nuevo cliente a la lista o actualizar uno existente
        }

        // Método para cancelar la edición del formulario
        private void Cancelar()
        {
            // Aquí puedes agregar la lógica para limpiar el formulario o cerrar la vista
        }

        // Implementación de INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}