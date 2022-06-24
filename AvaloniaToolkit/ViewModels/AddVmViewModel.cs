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
using Microsoft.VisualStudio.Threading;
using AvaloniaToolkit.TextTemplates;
using System.IO;

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
            ThreadHelper.ThrowIfNotOnUIThread();
            ThreadHelper.JoinableTaskFactory.Run(InitializeAsync);
            AddCommand = new DelegateCommand(() => ThreadHelper.JoinableTaskFactory.Run(OnAddAsync), CanAdd);
            Suffix = "ViewModel";
        }

        private async Task OnAddAsync()
        {
            try
            {
                switch (SelectedFlavor)
                {
                    case ViewModelFlavor.Prism:
                        await AddViewModelAsync(new PrismViewModelTemplate());
                        break;
                    case ViewModelFlavor.ReactiveUI:
                        await AddViewModelAsync(new ReactiveViewModelTemplate());
                        break;
                    case ViewModelFlavor.INotifyPropertyChanged:
                        await AddViewModelAsync(new INotifyViewModelTemplate());
                        break;
                }
            }
            catch(Exception ex)
            {
                await VS.MessageBox.ShowErrorAsync("Fail to create File. ", ex.Message);
            }
        }
        private bool CanAdd()
        {
            return ViewModelName.Length != 0;
        }

        private async Task InitializeAsync()
        {
            var solutionItems = (await VS.Solutions.GetActiveItemsAsync()).ToList();
            if (solutionItems.Count != 1)
            {
                await VS.MessageBox.ShowErrorAsync("Cannot determine where to add this file. Please select only one folder. ");
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

        private async Task AddViewModelAsync<T>(T template) where T: BaseTemplate
        {
            template.Session = new Dictionary<string, object>();
            string name = GetName();
            template.Session.Add("Name", name);
            template.Session.Add("Namespace", RootNamespace);
            template.Initialize();

            string s = template.TransformText();
            string separator = RootPath.EndsWith(Path.DirectorySeparatorChar.ToString()) ? string.Empty : Path.DirectorySeparatorChar.ToString();
            string path = RootPath + separator + name + ".cs";

            await FileHelper.ThrowIfExistAsync(path);
            await FileHelper.CreateTextFileAsync(path, s);
            
            var project = _solutionItem.GetContainingProject();
            await project.AddExistingFilesAsync(path);
            await VS.Documents.OpenAsync(path);
            OnCreateSucceedEventHandler?.Invoke(this, null);
        }

        private string GetName()
        {
            if (ViewModelName is null) return null;
            if (ViewModelName.EndsWith(Suffix))
            {
                return ViewModelName;
            }
            else
            {
                return ViewModelName + Suffix;
            }
        }
    }
}
