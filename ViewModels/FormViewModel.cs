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


        // Comandos para los botones de la vista
        public RelayCommand GuardarCommand { get; set; }
        public RelayCommand CancelarCommand { get; set; }

        // Constructor del ViewModel
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
            // Implementación del método para guardar los datos
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
