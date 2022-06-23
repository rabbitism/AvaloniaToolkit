using AvaloniaToolkit.Views;
using System.Linq;

namespace AvaloniaToolkit
{
    [Command(PackageGuids.AvaloniaToolkitString, PackageIds.AddViewModelCommand)]
    internal sealed class AddViewModelCommand : BaseCommand<AddViewModelCommand>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            var solutionItems = (await VS.Solutions.GetActiveItemsAsync())?.ToList();
            if (solutionItems is null || solutionItems.Count != 1)
            {
                await VS.MessageBox.ShowErrorAsync("Cannot determine where to add this ViewModel. Please select only one folder. ");
                return;
            }
            await VS.Windows.ShowDialogAsync(new AddViewModelWindow());
        }
    }
}
