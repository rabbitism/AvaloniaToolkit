using AvaloniaToolkit.CodeSnippetTool.ViewModels;
using Galaxism.CodeSnippets.VisualStudio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AvaloniaToolkit.CodeSnippetTool.Views
{
    /// <summary>
    /// Interaction logic for CodeSnippetWindow.xaml
    /// </summary>
    public partial class CodeSnippetWindow : Window
    {
        public CodeSnippetWindow()
        {
            InitializeComponent();
            this.DataContext = new CodeSnippetToolViewModel();
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if(e.NewValue is CodeSnippetViewModel element && this.DataContext is CodeSnippetToolViewModel v)
            {
                v.SelectedSnippet = element;
            }
        }
    }
}
