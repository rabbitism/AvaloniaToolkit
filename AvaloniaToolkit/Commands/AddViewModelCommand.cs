using AvaloniaToolkit.Views;
using System.Linq;

namespace AvaloniaToolkit
{
    [Command(PackageGuids.AvaloniaToolkitString, PackageIds.AddViewModelCommand)]
    internal sealed class AddViewModelCommand : BaseCommand<AddViewModelCommand>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            await VS.Windows.ShowDialogAsync(new AddViewModelWindow());
        }

        protected override void BeforeQueryStatus(EventArgs e)
        {

            var solutionItems = ThreadHelper.JoinableTaskFactory.Run(VS.Solutions.GetActiveItemsAsync)?.ToList();
            if (solutionItems is null || solutionItems.Count != 1)
            {
                this.Command.Enabled = false;
            }
            else
            {
                this.Command.Enabled = true;
            }
            base.BeforeQueryStatus(e);
        }
    }
}
