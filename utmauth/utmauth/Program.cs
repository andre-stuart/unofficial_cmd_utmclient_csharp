using System;
using System.Linq;

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

        static void Main(string[] args)
        {
            if (args.Length < 1 || args.Any(x => x.Contains("--help")))
            {
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

            Console.WriteLine("OKOK");
            Environment.Exit(Status.SUCCESS);
        }

        static void Help()
        {
            Console.WriteLine("");
            Console.WriteLine("Unofficial command line client for authentication in Blockbit® UTM");
            Console.WriteLine("");
            Console.WriteLine("Using:");
            Console.WriteLine("  utmauth.exe --action[ACTION] --server[ip/host] --login[login/email] --pass[password]");
            Console.WriteLine("");
            Console.WriteLine("Options:");
            Console.WriteLine("  --help                -> Show this help message.");
            Console.WriteLine("  --action[ACTION]      -> [Optional] login, logout, keepalive or status [default: login]. ex: login");
            Console.WriteLine("  --server[ip/host]     -> [Required] UTM server ip or host. ex: utm.exmaple.com");
            Console.WriteLine("  --port[port]          -> [Optional] UTM server port [default: 9803]. ex: 9803");
            Console.WriteLine("  --login[login/email]  -> [Required] UTM login or login@domain. ex: user@exmaple.com");
            Console.WriteLine("  --pass[password]      -> [Required] UTM user password.");
            Console.WriteLine("  --logfile[uri]        -> [Optional] Uri log file [default: ./utmauth.log]. ex: c:\\utmauth.log");
            Console.WriteLine("  --cookie[uri]         -> [Optional] Uri cookie file [default: ./utmauth.cookie]. ex: c:\\utmauth.cookie");
            Console.WriteLine("  --keepalive[interval] -> [Optional] Keepalive interval in seconds limited 3600 [default: 0]. ex: 30");
            Console.WriteLine("    *if none keepalive, empty or equal to 0(zero) keepalive disabled");
            Console.WriteLine("");
            Console.WriteLine("Example:");
            Console.WriteLine("  > utmauth.exe --action login --server utm.exmaple.com --login user@exmaple.com --pass \"*******\"");
            Console.WriteLine("  > utmauth.exe --action logout --server utm.exmaple.com --login user@exmaple.com");
            Console.WriteLine("");
            Console.WriteLine("Developer by Andre Stuart [nbbr.andre@gmail.com]");
            Console.WriteLine("");
            Console.WriteLine("Blockbit® is a registered trademark of BLOCKBIT TECNOLOGIA LTDA [https://www.blockbit.com/about/]");
        }
    }
}
