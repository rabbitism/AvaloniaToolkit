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
        public ViewModelFlavor SelectedFlavor { get => _selectedFlavor; set => SetProperty(ref _selectedFlavor, value); } 

        private ObservableCollection<ViewModelFlavor> _flavors;
        public ObservableCollection<ViewModelFlavor> Flavors { get => _flavors; set => SetProperty(ref _flavors, value); }

        private string _viewModelName;
        public string ViewModelName { get => _viewModelName; set { SetProperty(ref _viewModelName, value); AddCommand?.RaiseCanExecuteChanged(); } }

        private string _suffix;
        public string Suffix { get => _suffix; set => SetProperty(ref _suffix, value); }

        private string _rootNamespace;
        public string RootNamespace { get => _rootNamespace; set => SetProperty(ref _rootNamespace, value); }

        private string _rootPath;
        public string RootPath { get => _rootPath; set => SetProperty(ref _rootPath, value); }


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
            return !string.IsNullOrWhiteSpace(ViewModelName);
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
