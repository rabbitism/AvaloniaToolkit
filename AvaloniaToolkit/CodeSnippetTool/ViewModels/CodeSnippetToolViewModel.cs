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
            try
            {
                byte[] bytes = Properties.Resources.AvaloniaSnippets;
                Stream stream = new MemoryStream(bytes);

                SnippetDocument document = SnippetDocument.Load(stream);

                Collections = new ObservableCollection<SnippetCollection>()
                {
                    new SnippetCollection
                    {
                        CollectionName = "Avalonia",
                        Snippets = new ObservableCollection<CodeSnippetViewModel>
                        {
                            new CodeSnippetViewModel(),
                            new CodeSnippetViewModel(),
                            new CodeSnippetViewModel(),
                        }
                    },

                };
                SnippetCollection collection = new SnippetCollection() { CollectionName = "Test" };
                foreach (var snippet in document.CodeSnippets.CodeSnippets)
                {
                    collection.Snippets.Add(new CodeSnippetViewModel(snippet));
                }
                Collections.Add(collection);
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
