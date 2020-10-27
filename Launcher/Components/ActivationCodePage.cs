using System;
using System.Windows.Forms;
using Unlakki.Bns.Launcher.Components.Router;
using Unlakki.Bns.Launcher.Core.Models.Account;
using Unlakki.Bns.Launcher.Core.Services.Interfaces;

namespace Unlakki.Bns.Launcher.Components
{
  public partial class ActivationCodePage : RoutableComponent
  {
    private ILauncherConfigProvider _launcherConfigProvider;

    private IForgameAuthProvider _forgameAuthProvider;

    public ActivationCodePage(
        ILauncherConfigProvider launcherConfigProvider,
        IForgameAuthProvider forgameAuthProvider)
    {
      InitializeComponent();

      _launcherConfigProvider = launcherConfigProvider;
      _forgameAuthProvider = forgameAuthProvider;

      Load += ActivationCodePage_Load;
    }

    private void ActivationCodePage_Load(object sender, EventArgs e)
    {
      messageContainer.Text = Router.Query["message"];
    }

    private async void activationCodeTextBox_TextChanged(object sender, EventArgs e)
    {
      if (activationCodeTextBox.TextLength != 6)
      {
        return;
      }

      string sessionId = Router.Params["sessionId"];

      try
      {
        await _forgameAuthProvider.SendActivationCode(
            sessionId,
            activationCodeTextBox.Text);

        string username = Router.Query["username"];
        string password = Router.Query["password"];

        Token token = await _forgameAuthProvider.Authorize(username, password);
        _launcherConfigProvider.AddOrUpdateAccount(new Account
        {
          Username = username,
          Token = token
        });
        _launcherConfigProvider.UpdateLastUsedAccount(username);

        Router.SetLocation("/");
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }
  }
}
