﻿namespace AvaloniaToolkit
{
    [Command(PackageGuids.AvaloniaToolkitString, PackageIds.CodeSnippetToolCommand)]
    internal sealed class CodeSnippetToolCommand : BaseCommand<CodeSnippetToolCommand>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            await VS.MessageBox.ShowWarningAsync("CodeSnippetToolCommand", "Button clicked");
        }
    }
}
