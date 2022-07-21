using AvaloniaToolkit.ViewModels;
using Microsoft.VisualStudio.PlatformUI;
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
using System.Windows.Shapes;

namespace AvaloniaToolkit.Views
{
    /// <summary>
    /// Interaction logic for AddDataTemplateWindow.xaml
    /// </summary>
    public partial class AddDataTemplateWindow : DialogWindow
    {
        public AddDataTemplateWindow()
        {
            InitializeComponent();
            var vm = new AddDataTemplateViewModel();
            vm.OnCreateSucceedEventHandler += Vm_OnCreateSucceedEventHandler;
            this.DataContext = vm;
        }

        private void Vm_OnCreateSucceedEventHandler(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
