using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security;

namespace utmauth
{
    class Args
    {
        private readonly string[] _args;

        public Args() { }
        public Args(string[] args)
        {
            _args = args;
            try { this.Parse(); Status = true; } catch { Status = false; };
        }

        public void Parse()
        {
            for (int i = 0; i < _args.Length; i++)
            {
                if (_args[i] == "--action")
                    Action = _args[i++].Contains("--") ? "" : _args[i++].Trim().ToLower();

                else if (_args[i] == "--server")
                    Server = _args[i++].Contains("--") ? "" : _args[i++].Trim().ToLower();

                else if (_args[i] == "--port")
                    Port = _args[i++].Contains("--") ? "" : _args[i++].Trim();

                else if (_args[i] == "--login")
                    Login = _args[i++].Contains("--") ? "" : _args[i++].Trim().ToLower();

                else if (_args[i] == "--pass")
                    Pass = _args[i++].Contains("--") ? new NetworkCredential("", "").SecurePassword : new NetworkCredential("", _args[i++].Trim()).SecurePassword;

                else if (_args[i] == "--logfile")
                    LogFile = _args[i++].Contains("--") ? "" : _args[i++].Trim();

                else if (_args[i] == "--cookie")
                    CookieFile = _args[i++].Contains("--") ? "" : _args[i++].Trim();

                else if (_args[i] == "--keepalive")
                    KeepaliveTime = _args[i++].Contains("--") ? "" : _args[i++].Trim();
            }

            if (Server.Length < 1 || Login.Length < 1 || Pass.Length < 1)
                throw new System.ArgumentException("Parameter invalid", "");

            Validate v = new Validate();

            if (Action.Length < 1)
                Action = "login";
            else if (!v.IsAction(Action))
                throw new System.ArgumentException("Parameter invalid", "action");

            if (Port.Length < 1)
                Port = "9803";
            else if (!v.IsPort(Port))
                throw new System.ArgumentException("Parameter invalid", "port");

            if (LogFile.Length < 1)
                LogFile = "utmauth.log";
            else if (!v.IsLogFile(LogFile))
                throw new System.ArgumentException("Parameter invalid", "logfile");

            if (CookieFile.Length < 1)
                CookieFile = "utmauth.cookie";
            else if (!v.IsCookieFile(LogFile))
                throw new System.ArgumentException("Parameter invalid", "cookie");

            if (KeepaliveTime.Length < 1)
                KeepaliveTime = "0";
            else if (!v.IsKeepaliveTime(LogFile))
                throw new System.ArgumentException("Parameter invalid", "keepalive");
        }

        public bool Status { get; }

        [StringLength(20)]
        public string Action { get; set; }
        [StringLength(253)]
        public string Server { get; set; }
        [StringLength(5)]
        public string Port { get; set; }
        [StringLength(320)]
        public string Login { get; set; }
        [StringLength(320)]
        public SecureString Pass { get; set; }
        [StringLength(4)]
        public string KeepaliveTime { get; set; }
        [StringLength(1024)]
        public string LogFile { get; set; }
        [StringLength(1024)]
        public string CookieFile { get; set; }
    }
}
