using AvaloniaToolkit.CodeSnippetTool.Views;

namespace AvaloniaToolkit
{
    [Command(PackageGuids.AvaloniaToolkitString, PackageIds.CodeSnippetToolCommand)]
    internal sealed class CodeSnippetToolCommand : BaseCommand<CodeSnippetToolCommand>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            await VS.Windows.ShowDialogAsync(new CodeSnippetWindow());
        }
    }
}
