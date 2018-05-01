using ImageServiceDesktopApp.Model;
using ImageServiceDesktopApp.VM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for SettingsUC.xaml
    /// </summary>
    public partial class SettingsUC : UserControl
    {
        public ObservableCollection<string> Handlers { get; set; }

        public SettingsUC()
        {
            InitializeComponent();
            this.DataContext = new SettingsVM(new SettingModel());
           // this.lsbHandlers.ItemsSource = settingsVM.VM_Handlers;
            //settingsVM.VM_Handlers.Add("jkjk");
        }
        //todo: move from here!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
      
    }
}
