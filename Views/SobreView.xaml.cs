using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_MVVM_SPA_Template.Views
{
    /// <summary>
    /// Lógica de interacción para UserControl5.xaml
    /// </summary>
    public partial class SobreView : UserControl
    {
        public SobreView()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                // Manejar la excepción (por ejemplo, registrar el error)
                LogError(ex);
            }
        }

        private void LogError(Exception ex)
        {
            // Implementar el registro del error
            // Por ejemplo, escribir en un archivo de log
            System.IO.File.AppendAllText("error.log", $"{DateTime.Now}: {ex.Message}{Environment.NewLine}");
        }
    }
}