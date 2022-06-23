using AvaloniaToolkit.Options;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using AvaloniaToolkit.Common;
using Prism.Commands;

namespace AvaloniaToolkit.ViewModels
{
    internal class AddVmViewModel: BindableBase
    {
        private SolutionItem _solutionItem;
        public DelegateCommand AddCommand { get; set; }

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

        private string _viewModelName;
        public string ViewModelName
        {
            get { return _viewModelName; }
            set { _viewModelName = value; RaisePropertyChanged(); AddCommand?.RaiseCanExecuteChanged(); }
        }

        private string _suffix;
        public string Suffix
        {
            get { return _suffix; }
            set { _suffix = value; RaisePropertyChanged(); }
        }

        private string _rootNamespace;
        public string RootNamespace
        {
            get { return _rootNamespace; }
            set { _rootNamespace = value; RaisePropertyChanged(); }
        }

        private string _rootPath;
        public string RootPath
        {
            get { return _rootPath; }
            set { _rootPath = value; RaisePropertyChanged(); }
        }


        public AddVmViewModel()
        {
            Initialize();


        }

        private async void Initialize()
        {
            var solutionItems = (await VS.Solutions.GetActiveItemsAsync()).ToList();
            if (solutionItems.Count != 1)
            {
                await VS.MessageBox.ShowErrorAsync("Cannot determine where to add this ViewModel. Please select only one folder. ");
                return;
            }
            _solutionItem = solutionItems.First();
            var ns = await ProjectHelpers.GetNamespaceAsync(_solutionItem);
            var path = ProjectHelpers.GetContainingFolder(_solutionItem);
            RootNamespace = ns;
            RootPath = path.FullName;

            Flavors = new ObservableCollection<ViewModelFlavor>(EnumHelpers.GetEnumValues<ViewModelFlavor>());
            General g = General.Instance;
            if (g != null)
            {
                SelectedFlavor = g.ViewModelFlavor;
            }
        }
    }
}
