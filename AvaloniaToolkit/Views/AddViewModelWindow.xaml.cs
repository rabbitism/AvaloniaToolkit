﻿using AvaloniaToolkit.Options;
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

namespace AvaloniaToolkit.Views
{
    /// <summary>
    /// Interaction logic for AddViewModelWindow.xaml
    /// </summary>
    public partial class AddViewModelWindow : Window
    {
        public AddViewModelWindow()
        {
            InitializeComponent();
            General g = General.Instance;
            if (g.ViewModelFlavor != null)
            {
                // this.Title = g.ViewModelFlavor;
            }
        }
    }
}
