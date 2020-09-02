using System;
using System.Windows.Forms;
using Unlakki.Bns.Launcher.Core.Services.Interfaces;

namespace Unlakki.Bns.Launcher
{
    public partial class ActivationCodeForm : Form
    {
        private readonly IForgameAuthProvider _forgameAuthProvider;

        private readonly string _sessionId;

        public ActivationCodeForm(IForgameAuthProvider forgameAuthProvider, string message, string sessionId)
        {
            InitializeComponent();

            _forgameAuthProvider = forgameAuthProvider;
            _sessionId = sessionId;

            this.message.Text = message;
        }

        private async void activationCodeTextBox_TextChanged(object sender, EventArgs e)
        {
            if (activationCodeTextBox.TextLength == 6)
            {
                try
                {
                    await _forgameAuthProvider.SendActivationCode(_sessionId, activationCodeTextBox.Text);
                    DialogResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
