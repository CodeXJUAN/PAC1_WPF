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

        // Col·lecció de Students (podrien carregar-se d'una base de dades)
        // ObservableCollection és una llista que notifica els canvis a la vista
        public ObservableCollection<Clients> Clients { get; set; } = new ObservableCollection<Clients>();

        // Propietat per controlar l'estudiant seleccionat a la vista
        private Clients? _selectedClients;
        public Clients? SelectedClients
        {
            get { return _selectedClients; }
            set { _selectedClients = value; OnPropertyChanged(); }
        }

        // RelayCommands connectats via Binding als botons de la vista
        public RelayCommand AddClientsCommand { get; set; }
        public RelayCommand DelClientsCommand { get; set; }

        public ClientsViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            // Carreguem estudiants a memòria mode de prova
            Clients.Add(new Clients { Id = 1, Nom = "David",  });
            Clients.Add(new Clients { Id = 2, Nom = "Jordi",  });

            // Inicialitzem els diferents commands disponibles (accions)
            AddClientsCommand = new RelayCommand(x => AddClients());
            DelClientsCommand = new RelayCommand(x => DelClients());
        }

        //Mètodes per afegir i eliminar estudiants de la col·lecció
        private void AddClients()
        {
            _mainViewModel.CurrentView = new IniciView { DataContext = null };
        }

        private void DelClients()
        {
            if (SelectedClients != null)
                Clients.Remove(SelectedClients);
        }

        // Això és essencial per fer funcionar el Binding de propietats entre Vistes i ViewModels
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}

