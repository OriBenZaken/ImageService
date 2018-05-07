using ImageServiceDesktopApp.VM;
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

namespace ImageServiceDesktopApp
{
    /// <summary>
    /// Interaction logic for LogUC.xaml
    /// </summary>
    public partial class LogUC : UserControl
    {
        /// <summary>
        /// Logs View.
        /// </summary>
        public LogUC()
        {
            InitializeComponent();
            this.DataContext = new LogVM();
        }
    }
}
