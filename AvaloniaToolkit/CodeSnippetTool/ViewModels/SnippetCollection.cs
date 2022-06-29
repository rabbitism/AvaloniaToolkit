using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using Galaxism.CodeSnippets.VisualStudio;

namespace AvaloniaToolkit.CodeSnippetTool.ViewModels
{
    public class SnippetCollection: BindableBase
    {
        private string _collectionName;
        public string CollectionName
        {
            get { return _collectionName; }
            set { _collectionName = value; }
        }

        private ObservableCollection<CodeSnippetViewModel> _snippets;

        public ObservableCollection<CodeSnippetViewModel> Snippets
        {
            get { return _snippets; }
            set { _snippets = value; }
        }

        public SnippetCollection()
        {
            Snippets = new ObservableCollection<CodeSnippetViewModel>();
        }

    }
}
