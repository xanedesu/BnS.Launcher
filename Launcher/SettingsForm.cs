using System;
using System.Windows.Forms;
using Unlakki.Bns.Launcher.Core.Services.Interfaces;

namespace Unlakki.Bns.Launcher
{
    public partial class SettingsForm : Form
    {
        private readonly ILauncherConfigProvider _launcherConfigProvider;

        public SettingsForm(ILauncherConfigProvider launcherConfigProvider)
        {
            _launcherConfigProvider = launcherConfigProvider;

            InitializeComponent();

            argsTextBox.Text = _launcherConfigProvider.GetGameArguments();
        }

        private void saveSettingButton_Click(object sender, EventArgs e)
        {
            string args = argsTextBox.Text.ToUpper();
            _launcherConfigProvider.UpdateStartGameArguments(args);

            DialogResult = DialogResult.OK;
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

                argsTextBox.Text = args.Length == 0 ? argName : $"{args} {argName}";
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
