using AvaloniaToolkit.Views;
using System.Linq;

namespace AvaloniaToolkit
{
    [Command(PackageGuids.AvaloniaToolkitString, PackageIds.AddDataTemplateCommand)]
    internal sealed class AddDataTemplateCommand : BaseCommand<AddDataTemplateCommand>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            await VS.Windows.ShowDialogAsync(new AddDataTemplateWindow());
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
