using AvaloniaToolkit.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AvaloniaToolkit.Options
{
    [ComVisible(true)]
    [Guid("51E91EC6-D33F-4CAC-B238-1C4909BDFEDC")]
    public class GeneralOptionPage : UIElementDialogPage
    {
        protected override UIElement Child
        {
            get
            {
                OptionsView page = new()
                {
                    OptionGrid = this,
                };
                page.InitializeOptions();
                return page;
            }
        }
    }
}
