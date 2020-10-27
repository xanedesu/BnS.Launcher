using System.Collections.Generic;
using Unlakki.Bns.Launcher.Core.Enums;
using Unlakki.Bns.Launcher.Core.Models.Account;

namespace Unlakki.Bns.Launcher.Core.Services.Interfaces
{
  public interface ILauncherConfigProvider
  {
    void Init();

    LauncherConfigProvider InitAndGet();

    void AddOrUpdateAccount(Account account);

    void UpdateLastUsedAccount(string lastUsedAccountUsername);

    void UpdateGameVersion(GameVersion gameVersion);

    void UpdateStartGameArguments(string arguments);

    void UpdateAutoCloseLauncher(bool autoClose);

    List<Account> GetAccounts();

    string GetLastUsedAccount();

    GameVersion GetGameVersion();

    string GetGameArguments();

    bool GetAutoCloseLauncher();
  }
}
