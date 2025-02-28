﻿using System.Collections.ObjectModel;
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

        public ClientsViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            // Carreguem estudiants a memòria mode de prova
            Clients.Add(new Client { Id = 1, DNI = "12345678A", Nom = "David", Cognoms = "Gonzalez", Email = "dgonzalez@email.com", Telefon = "684 32 23 34", DataAlta = new DateOnly(2010,8,2)});
            Clients.Add(new Client { Id = 2, DNI = "98765432B", Nom = "Jordi", Cognoms = "Surinyac", Email = "jsurinyac@email.com", Telefon = "652 42 11 56", DataAlta = new DateOnly(2022,3,21)});

            // Inicialitzem els diferents commands disponibles (accions)
            AfegirClientForm = new RelayCommand(x => AfegirClients());
            MostarEstadisticaCommand = new RelayCommand(x => MostrarRendiment());

            FormulariVM   = new FormViewModel(_mainViewModel);
            EstadisticaVM = new EstadisticaViewModel(_mainViewModel);

        }

        //Mètodes per afegir i eliminar estudiants de la col·lecció
        private void AfegirClients()
        {
            _mainViewModel.CurrentView = new FormulariView { DataContext = FormulariVM };
        }

        private void MostrarRendiment()
        {
            _mainViewModel.CurrentView = new EstadisticaView { DataContext = EstadisticaVM };
        }

        // Això és essencial per fer funcionar el Binding de propietats entre Vistes i ViewModels
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}

