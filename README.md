# RU BnsLauncher
bns-ru launcher.

Запускатор бнс, минующий frostUpdater.exe.

Если и использовать, то только без папок frost в  bin и bin64, но и без них не факт что не забанят.

Исходный код выкладываю в ознакомительных целях.

В коде реализовано получение computerName, launcherId, hardwareId значений также, как это делает Innova.
Реализована регистрация лаунчера в системе и добавление компьютера в доверенные. Лаунчер притворяется иннововским, поэтому если их лаунчер был добавлен в список доверенных, подтверждения нового входа не будет.

Реализована работа с api логина, обновления токена доступа и подтверждения входа (с помощью кода, отправляемого инновой на почту/телефон).

Реализовано получение логина и пароля от игры (json rpc over websocket).