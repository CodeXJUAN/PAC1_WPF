using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WPF_MVVM_SPA_Template.Models;

namespace WPF_MVVM_SPA_Template.ViewModels
{
    //Els ViewModels deriven de INotifyPropertyChanged per poder fer Binding de propietats
    class IniciViewModel : INotifyPropertyChanged
    {
        // Referència al ViewModel principal
        private readonly MainViewModel _mainViewModel;
        // Col·lecció de Courses (podrien carregar-se d'una base de dades)
        // ObservableCollection és una llista que notifica els canvis a la vista
        public ObservableCollection<Course> Courses { get; set; } = new ObservableCollection<Course>();

        // Propietat per controlar el curs seleccionat a la vista
        private Course? _selectedCourse;
        public Course? SelectedCourse
        {
            get { return _selectedCourse; }
            set { _selectedCourse = value; OnPropertyChanged(); }
        }

        // RelayCommands connectats via Binding als botons de la vista
        public RelayCommand AddCourseCommand { get; set; }
        public RelayCommand DelCourseCommand { get; set; }

        public IniciViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            // Carreguem cursos a memòria mode de prova
            Courses.Add(new Course { Id = 1, Name = "Bases de Dades" });
            Courses.Add(new Course { Id = 2, Name = "Programació d'Interfícies" });

            // Inicialitzem els diferents commands disponibles (accions)
            AddCourseCommand = new RelayCommand(x => AddCourse());
            DelCourseCommand = new RelayCommand(x => DelCourse());
        }

        //Mètodes per afegir i eliminar cursos de la col·lecció
        private void AddCourse()
        {
            Courses.Add(new Course { Id = Courses.Count + 1, Name = "Nou" });
        }

        private void DelCourse()
        {
            if (SelectedCourse != null)
                Courses.Remove(SelectedCourse);
        }

        // Això és essencial per fer funcionar el Binding de propietats entre Vistes i ViewModels
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
