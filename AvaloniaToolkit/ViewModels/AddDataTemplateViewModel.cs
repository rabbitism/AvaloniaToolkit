using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Commands;
using AvaloniaToolkit.Common;
using AvaloniaToolkit.TextTemplates;
using System.IO;

namespace AvaloniaToolkit.ViewModels
{
    internal class AddDataTemplateViewModel: BindableBase
    {
        private SolutionItem _solutionItem;

        public event EventHandler OnCreateSucceedEventHandler;

        private string _rootNamespace;
        public string RootNamespace { get => _rootNamespace; set => SetProperty(ref _rootNamespace, value); }

        private string _rootPath;
        public string RootPath { get => _rootPath; set => SetProperty(ref _rootPath, value); }
        private string _templateName;
        public string TemplateName { get => _templateName; set { SetProperty(ref _templateName, value); AddCommand.RaiseCanExecuteChanged(); } }
        private bool _selector;
        public bool Selector { get => _selector; set => SetProperty(ref _selector, value); }

        public DelegateCommand AddCommand { get; set; }

        public AddDataTemplateViewModel()
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            ThreadHelper.JoinableTaskFactory.Run(InitializeAsync);
            Selector = false;
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
                if (Selector)
                {
                    await AddTemplateAsync(new DataTemplateSelectorTemplate());
                }
                else
                {
                    await AddTemplateAsync(new DataTemplateTemplate());
                }
            }
            catch (Exception ex)
            {
                await VS.MessageBox.ShowErrorAsync("Fail to create File. ", ex.Message);

            }
        }

        private async Task AddTemplateAsync<T>(T template) where T : BaseTemplate
        {
            template.Session = new Dictionary<string, object>();
            string templateName = this.TemplateName;
            template.Session.Add("Name", templateName);
            template.Session.Add("Namespace", RootNamespace);
            template.Initialize();
            string s = template.TransformText();
            string seperator = RootPath.EndsWith(Path.DirectorySeparatorChar.ToString()) ? string.Empty : Path.DirectorySeparatorChar.ToString();
            string path = RootPath + seperator + templateName + ".cs";

            await FileHelper.ThrowIfExistAsync(path);
            await FileHelper.CreateTextFileAsync(path, s);

            var project = _solutionItem.GetContainingProject();
            await project.AddExistingFilesAsync(path);
            await VS.Documents.OpenAsync(path);
            OnCreateSucceedEventHandler?.Invoke(this, null);
        }

        private bool CanAdd()
        {
            return !string.IsNullOrWhiteSpace(TemplateName);
        }
    }
}
