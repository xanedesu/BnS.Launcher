using System;
using System.Windows.Forms;
using Unlakki.Bns.Launcher.Components.Router;
using Unlakki.Bns.Launcher.Core.Services.Interfaces;

namespace Unlakki.Bns.Launcher.Components
{
  public partial class SettingsPage : RoutableComponent
  {
    ILauncherConfigProvider _launcherConfigProvider;

    public SettingsPage(ILauncherConfigProvider launcherConfigProvider)
    {
      InitializeComponent();

      _launcherConfigProvider = launcherConfigProvider;

      argsTextBox.Text = _launcherConfigProvider.GetGameArguments();
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      Router.SetLocation("/");
    }

    private void saveSettingButton_Click(object sender, EventArgs e)
    {
      _launcherConfigProvider.UpdateStartGameArguments(argsTextBox.Text);
      Router.SetLocation("/");
    }

    private void useAllAvailableCoresCheckbox_CheckedChanged(object sender, EventArgs e)
    {
      ToggleLaunchArgument(useAllAvailableCoresCheckbox);
    }

    private void noTextureStreamingCheckbox_CheckedChanged(object sender, EventArgs e)
    {
      ToggleLaunchArgument(noTextureStreamingCheckbox);
    }

    private void argsTextBox_TextChanged(object sender, EventArgs e)
    {
      WatchTextBoxChanges(useAllAvailableCoresCheckbox);
      WatchTextBoxChanges(noTextureStreamingCheckbox);
    }

    private void ToggleLaunchArgument(CheckBox checkBox)
    {
      string argName = checkBox.Text.ToUpper();
      string args = argsTextBox.Text.ToUpper();

      if (checkBox.Checked)
      {
        if (args.Contains(argName))
          return;

        argsTextBox.Text = args.Length == 0 ? argName : string.Concat("", args, argName);
        return;
      }

      argsTextBox.Text = args.Replace(argName, "").Trim();
    }

    private void WatchTextBoxChanges(CheckBox checkBox)
    {
      string argName = checkBox.Text.ToUpper();
      string args = argsTextBox.Text.ToUpper();

      checkBox.Checked = args.Contains(argName);
    }
  }
}
