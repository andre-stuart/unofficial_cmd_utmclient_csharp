# unofficial_cmd_utmclient_csharp

Unofficial command line client for authentication in Blockbit® UTM [c# .NET core 3.1]

## Dependency

- Visual Studio 2019+
- .Net Core 3.1

## Build

- Open utmauth\utmauth.sln;
- Select configuration and target platform;
- Build in Visual Studio 2019+ (unknown with previous versions).

## Instructions

Tested in Windows 10 Command Prompt with .Net Core 3.1;

```batch
$ utmauth.exe --help

Unofficial command line client for authentication in Blockbit® UTM
Version 0.1
Using:
  utmauth.exe --action[ACTION] --server[ip/host] --login[login/email] --pass[password]

Options:
  --help                -> Show this help message.
  --action[ACTION]      -> [Optional] login, logout, keepalive or status [default: login]. ex: login
  --server[ip/host]     -> [Required] UTM server ip or host. ex: utm.exmaple.com
  --port[port]          -> [Optional] UTM server port [default: 9803]. ex: 9803
  --login[login/email]  -> [Required] UTM login or login@domain. ex: user@exmaple.com
  --pass[password]      -> [Required] UTM user password.
  --agent[uri]          -> [Optional] Client ID user agent [default: CMDClient/[version]].
  --cookie[uri]         -> [Optional] Uri cookie file [default: ./utmauth.cookie]. ex: c:\utmauth.cookie

Example:
  > utmauth.exe --action login --server utm.example.com --login user@domain.com --pass "*******"
  > utmauth.exe --action logout --server utm.example.com --login user@domain.com
  > utmauth.exe --action keepalive --server utm.example.com --login user@domain.com

Developer by Andre StuartDev [nbbr.andre@gmail.com]

Blockbit® is a registered trademark of BLOCKBIT TECNOLOGIA LTDA [https://www.blockbit.com/about/]
```

## Credits

- Blockbit® is a registered trademark of BLOCKBIT TECNOLOGIA LTDA [https://www.blockbit.com/];
