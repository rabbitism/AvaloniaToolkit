using AvaloniaToolkit.Options;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using AvaloniaToolkit.Common;

namespace AvaloniaToolkit.ViewModels
{
    internal class AddVmViewModel: BindableBase
    {
        public event EventHandler OnCreateSucceedEventHandler;

        private ViewModelFlavor _selectedFlavor;
        public ViewModelFlavor SelectedFlavor
        {
            get { return _selectedFlavor; }
            set { _selectedFlavor = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<ViewModelFlavor> _flavors;
        public ObservableCollection<ViewModelFlavor> Flavors
        {
            get { return _flavors; }
            set { _flavors = value; RaisePropertyChanged(); }
        }


        public AddVmViewModel()
        {
            Flavors = new ObservableCollection<ViewModelFlavor>(EnumHelpers.GetEnumValues<ViewModelFlavor>());
            General g = General.Instance;
            if( g!= null )
            {
                SelectedFlavor = g.ViewModelFlavor;
            }

        }
    }
}
