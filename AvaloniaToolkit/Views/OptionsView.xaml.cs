using AvaloniaToolkit.Common;
using AvaloniaToolkit.Options;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Shell.Settings;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Settings;
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
using System.Collections.ObjectModel;

namespace AvaloniaToolkit.Views
{
    /// <summary>
    /// Interaction logic for OptionsView.xaml
    /// </summary>
    public partial class OptionsView : UserControl
    {

        private ObservableCollection<ViewModelFlavor> _flavors;

        public ObservableCollection<ViewModelFlavor> Flavors
        {
            get { return _flavors; }
            set { _flavors = value; }
        }

        private ViewModelFlavor _selectedFlavor;

        public ViewModelFlavor SelectedFlavor
        {
            get { return _selectedFlavor; }
            set { _selectedFlavor = value; }
        }


        public OptionsView()
        {
            InitializeComponent();
            Flavors = new ObservableCollection<ViewModelFlavor>() { ViewModelFlavor.ReactiveUI, ViewModelFlavor.Prism, ViewModelFlavor.INotifyPropertyChanged };
        }
        public GeneralOptionPage OptionGrid { get; init; }

        public void InitializeOptions()
        {
            General.Instance.Load();
            // this.viewModelFlavor.Text = General.Instance.ViewModelFlavor;
        }

        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            //var item = viewModelFlavor.SelectedItem;
            //General.Instance.ViewModelFlavor = (item as TextBlock).Text;
            //General.Instance.Save();
        }
    }
}
