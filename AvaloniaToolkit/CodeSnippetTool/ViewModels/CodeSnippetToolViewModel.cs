using Galaxism.CodeSnippets.VisualStudio;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.IO;

namespace AvaloniaToolkit.CodeSnippetTool.ViewModels
{
    public class CodeSnippetToolViewModel : BindableBase
    {
        private ObservableCollection<SnippetCollection> _collections;
        public ObservableCollection<SnippetCollection> Collections
        {
            get { return _collections; }
            set { _collections = value; RaisePropertyChanged(); }
        }

        private CodeSnippetViewModel _selectedSnippet;

        public CodeSnippetViewModel SelectedSnippet
        {
            get { return _selectedSnippet; }
            set
            {
                _selectedSnippet = value; RaisePropertyChanged();
            }
        }

        public CodeSnippetToolViewModel()
        {
            Collections = new ObservableCollection<SnippetCollection>();
            try
            {
                AddSnippets(Properties.Resources.AvaloniaSnippets, "Avalonia");
                AddSnippets(Properties.Resources.ReactiveUI, "Reactive UI");
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        private void AddSnippets(byte[] file, string name)
        {
            using Stream m = new MemoryStream(file);
            SnippetDocument document = SnippetDocument.Load(m);
            SnippetCollection collection = new SnippetCollection() { CollectionName = name };
            foreach(var snippet in document.CodeSnippets.CodeSnippets)
            {
                collection.Snippets.Add(new CodeSnippetViewModel(snippet));
            }
            Collections.Add(collection);
        }
    }
}
