using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace utmauth
{
    class Program
    {
        struct Status
        {
            public const int SUCCESS = 0;
            public const int ERR_UNKNOW = -1;
            public const int ERR_PARAM = -2;
            public const int ERR_CONN = -3;
            public const int ERR_AUTH = -4;
        };

        static async System.Threading.Tasks.Task Main(string[] args)
        {
            if (args.Length < 1 || args.Any(x => x.Contains("--help")))
            {
                Help();
                Environment.Exit(Status.ERR_PARAM);
            }

            if (args.Any(x => x.Contains("--ca")) || args.Any(x => x.Contains("--cert")))
            {
                Console.WriteLine("Arguments not implemented yet");
                Help();
                Environment.Exit(Status.ERR_PARAM);
            }

            var opt = new Args(args);
            if (!opt.Status)
            {
                Console.WriteLine("Invalid arguments");
                Help();
                Environment.Exit(Status.ERR_PARAM);
            }

            var serverOK = false;
            try
            {
                var client = new TcpClient(opt.Server, Int32.Parse(opt.Port));
                if (client.Connected)
                    serverOK = true;
                client.Close();
            }
            catch { }
            if(!serverOK)
            {
                Console.WriteLine($"Unable to connect server [{opt.Server}:{opt.Port}]");
                Environment.Exit(Status.ERR_CONN);
            }

            var auth = new Auth(opt);
            if(opt.Action == "login")
                await auth.LoginAsync();
            else if (opt.Action == "logout")
                await auth.LogoutAsync();
            else if (opt.Action == "keepalive")
                await auth.KeepaliveAsync();

            Environment.Exit(Status.SUCCESS);
        }

        static void Help()
        {
            Console.WriteLine("");
            Console.WriteLine("Unofficial command line client for authentication in Blockbit® UTM");
            Console.WriteLine("Version 0.1");
            Console.WriteLine("Using:");
            Console.WriteLine("  utmauth.exe --action[ACTION] --server[ip/host] --login[login/email] --pass[password]");
            Console.WriteLine("");
            Console.WriteLine("Options:");
            Console.WriteLine("  --help                -> Show this help message.");
            Console.WriteLine("  --action[ACTION]      -> [Optional] login, logout, keepalive or status [default: login]. ex: login");
            Console.WriteLine("  --server[ip/host]     -> [Required] UTM server ip or host. ex: utm.example.com");
            Console.WriteLine("  --port[port]          -> [Optional] UTM server port [default: 9803]. ex: 9803");
            Console.WriteLine("  --login[login/email]  -> [Required] UTM login or login@domain. ex: user@domain.com");
            Console.WriteLine("  --pass[password]      -> [Required] UTM user password.");
            //Console.WriteLine("  --ca[uri]             -> [Coming soon] X509 CA file using tls handshake.");
            //Console.WriteLine("  --cert[uri]           -> [Coming soon] User X509 certificate for 2FA.");
            Console.WriteLine("  --agent[uri]          -> [Optional] Client ID user agent [default: CMDClient/[version]].");
            Console.WriteLine("  --cookie[uri]         -> [Optional] Uri cookie file [default: ./utmauth.cookie]. ex: c:\\utmauth.cookie");
            //Console.WriteLine("  --keepalive[interval] -> [Coming soon] Keepalive interval in seconds limited 3600 [default: 0]. ex: 30");
            //Console.WriteLine("    *if none keepalive, empty or equal to 0(zero) keepalive disabled");
            Console.WriteLine("");
            Console.WriteLine("Example:");
            Console.WriteLine("  > utmauth.exe --action login --server utm.example.com --login user@domain.com --pass \"*******\"");
            Console.WriteLine("  > utmauth.exe --action logout --server utm.example.com --login user@domain.com");
            Console.WriteLine("  > utmauth.exe --action keepalive --server utm.example.com --login user@v.com");
            Console.WriteLine("");
            Console.WriteLine("Developer by Andre StuartDev [nbbr.andre@gmail.com]");
            Console.WriteLine("");
            Console.WriteLine("Blockbit® is a registered trademark of BLOCKBIT TECNOLOGIA LTDA [https://www.blockbit.com/about/]");
        }
    }
}
