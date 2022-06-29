using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvaloniaToolkit.Common;
using AvaloniaToolkit.TextTemplates;
using Prism.Commands;
using Prism.Mvvm;

namespace AvaloniaToolkit.ViewModels
{
    internal class AddConverterViewModel: BindableBase
    {
        private SolutionItem _solutionItem;
        public DelegateCommand AddCommand { get; set; }
        public event EventHandler OnCreateSucceedEventHandler;

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

        private string _converterName;
        public string ConverterName
        {
            get { return _converterName; }
            set { _converterName = value; RaisePropertyChanged(); AddCommand?.RaiseCanExecuteChanged(); }
        }

        private string _suffix;
        public string Suffix
        {
            get { return _suffix; }
            set { _suffix = value; RaisePropertyChanged(); }
        }
        private bool _isMultiValue;

        public bool IsMultiValue
        {
            get { return _isMultiValue; }
            set { _isMultiValue = value; RaisePropertyChanged(); }
        }

        public AddConverterViewModel() 
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            ThreadHelper.JoinableTaskFactory.Run(InitializeAsync);
            Suffix = "Converter";
            ConverterName = "BooleanToVisibility";
            IsMultiValue = false;
            AddCommand = new DelegateCommand(() => ThreadHelper.JoinableTaskFactory.Run(OnAddAsync), CanAdd);
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
            RootNamespace = ns;
            var path = ProjectHelpers.GetContainingFolder(_solutionItem);
            RootPath = path.FullName;
        }

        private async Task OnAddAsync()
        {
            try
            {
                if (IsMultiValue)
                {
                    await AddConverterAsync(new ConverterTemplate());
                }
                else
                {
                    await AddConverterAsync(new MultiValueConverterTemplate());
                }
            }
            catch (Exception ex)
            {
                await VS.MessageBox.ShowErrorAsync("Fail to create File. ", ex.Message);

            }
        }

        private async Task AddConverterAsync<T>(T template) where T: BaseTemplate
        {
            template.Session = new Dictionary<string, object>();
            string converterName = GetConverterName();
            template.Session.Add("Name", converterName);
            template.Session.Add("Namespace", RootNamespace);
            template.Initialize();
            string s = template.TransformText();
            string seperator = RootPath.EndsWith(Path.DirectorySeparatorChar.ToString()) ? string.Empty : Path.DirectorySeparatorChar.ToString();
            string path = RootPath + seperator + converterName + ".cs";

            await FileHelper.ThrowIfExistAsync(path);
            await FileHelper.CreateTextFileAsync(path, s);

            var project = _solutionItem.GetContainingProject();
            await project?.AddExistingFilesAsync(path);
            await VS.Documents.OpenAsync(path);
            OnCreateSucceedEventHandler?.Invoke(this, null);
        }

        private bool CanAdd()
        {
            return !string.IsNullOrWhiteSpace(ConverterName);
        }

        private string GetConverterName()
        {
            if (ConverterName == null) return null;
            if (ConverterName.EndsWith(Suffix))
            {
                return ConverterName;
            }
            else
            {
                return ConverterName + Suffix;
            }
        }
    }
}
