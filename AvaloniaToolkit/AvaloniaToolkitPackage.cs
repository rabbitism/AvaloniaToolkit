global using Community.VisualStudio.Toolkit;
global using Microsoft.VisualStudio.Shell;
global using System;
global using Task = System.Threading.Tasks.Task;
using EnvDTE80;
using EnvDTE;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.ComponentModel;
using AvaloniaToolkit.Options;

namespace AvaloniaToolkit
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration(Vsix.Name, Vsix.Description, Vsix.Version)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideOptionPage(typeof(GeneralOptionPage), "Avalonia Toolkit", "General", 0, 0, true)]
    [ProvideProfile(typeof(GeneralOptionPage), "Avalonia Toolkit", "General", 0, 0, true)]
    [Guid(PackageGuids.AvaloniaToolkitString)]
    public sealed class AvaloniaToolkitPackage : ToolkitPackage
    {
        public static DTE2 DTE { get; private set; }

        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            DTE = await GetServiceAsync(typeof(DTE)) as DTE2;
            InitializeTheme();
            await this.RegisterCommandsAsync();
        }

        private void InitializeTheme()
        {
            Application.Current.Resources["ExtensionDefaultBackground"] = Application.Current.Resources["VsBrush.StartPageTabBackground"];
        }
    }
}