using AvaloniaToolkit.Views;
using System.Linq;

namespace AvaloniaToolkit
{
    [Command(PackageGuids.AvaloniaToolkitString, PackageIds.AddConverterCommand)]
    internal sealed class AddConverterCommand : BaseCommand<AddConverterCommand>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            var solutionItems = (await VS.Solutions.GetActiveItemsAsync())?.ToList();
            if(solutionItems is null || solutionItems.Count != 1)
            {
                await VS.MessageBox.ShowErrorAsync("Cannot determine where to add this Converter. Please select only one folder. ");
                return;
            }
            await VS.Windows.ShowDialogAsync(new AddConverterWindow());
        }
    }
}
