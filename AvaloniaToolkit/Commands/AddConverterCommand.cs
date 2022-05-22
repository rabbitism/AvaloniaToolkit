namespace AvaloniaToolkit
{
    [Command(PackageGuids.AvaloniaToolkitString, PackageIds.AddConverterCommand)]
    internal sealed class AddConverterCommand : BaseCommand<AddConverterCommand>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            await VS.MessageBox.ShowWarningAsync("AddConverterCommand", "Button clicked");
        }
    }
}
