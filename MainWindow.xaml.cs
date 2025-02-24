using System.Windows;
using WPF_MVVM_SPA_Template.ViewModels;

namespace WPF_MVVM_SPA_Template
{
    //Finestra principal de l'aplicació (SPA Container)
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}