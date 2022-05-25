using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaToolkit.Options
{
    public class General: BaseOptionModel<General>
    {
        public ViewModelFlavor ViewModelFlavor { get; set; }
    }

    public partial class OptionsProvider
    {
        [ComVisible(true)]
        public class GeneralOptions : BaseOptionPage<General> { }
    }

    public enum ViewModelFlavor
    {
        [Display(Name = "ReactiveUI (ReactiveObject)")]
        ReactiveUI,
        [Display(Name = "Prism (BindableBase)")]
        Prism,
        [Display(Name = "INotifyPropertyChanged")]
        INotifyPropertyChanged,
    }
}
