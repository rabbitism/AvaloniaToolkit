using Galaxism.CodeSnippets.VisualStudio;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaToolkit.CodeSnippetTool.ViewModels
{
    public class CodeSnippetViewModel: BindableBase
    {
        public CodeSnippetElement CodeSnippet { get; set; }

        private bool _selected;
        public bool Selected {
            get { return _selected; }
            set { _selected = value; RaisePropertyChanged(); }
        }

        private string _shortcut;
        public string Shortcut
        {
            get { return _shortcut; }
            set { _shortcut = value; RaisePropertyChanged(); }
        }

        private string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; RaisePropertyChanged(); }
        }

        public CodeSnippetViewModel()
        {
            Shortcut = "prop";
        }

        public CodeSnippetViewModel(CodeSnippetElement codeSnippet)
        {
            CodeSnippet = codeSnippet;
            Title = codeSnippet.Header.Title;
            Shortcut = codeSnippet.Header.Shortcut;
        }

    }
}
