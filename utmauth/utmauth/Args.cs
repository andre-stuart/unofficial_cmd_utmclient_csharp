using System;
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
            try 
            { 
                this.Parse(); 
                Status = true; 
            } 
            catch (Exception e) 
            { 
                //Console.WriteLine(e); 
                Status = false; 
            };
        }

        public void Parse()
        {
            for (int i = 0; i < _args.Length; i++)
            {
                int iv = i + 1;

                if (_args[i] == "--action")
                    Action = _args[iv].Contains("--") ? "" : _args[iv].Trim().ToLower();

                else if (_args[i] == "--server")
                    Server = _args[iv].Contains("--") ? "" : _args[iv].Trim().ToLower();

                else if (_args[i] == "--port")
                    Port = _args[iv].Contains("--") ? "" : _args[iv].Trim();

                else if (_args[i] == "--login")
                    Login = _args[iv].Contains("--") ? "" : _args[iv].Trim().ToLower();

                else if (_args[i] == "--pass")
                    Pass = _args[iv].Contains("--") ? new NetworkCredential("", "").SecurePassword : new NetworkCredential("", _args[iv].Trim()).SecurePassword;

                else if (_args[i] == "--logfile")
                    LogFile = _args[iv].Contains("--") ? "" : _args[iv].Trim();

                else if (_args[i] == "--cookie")
                    CookieFile = _args[iv].Contains("--") ? "" : _args[iv].Trim();

                else if (_args[i] == "--keepalive")
                    KeepaliveTime = _args[iv].Contains("--") ? "" : _args[iv].Trim();

                else if (_args[i].Contains("--"))
                    throw new System.ArgumentException("Parameter invalid", "");
            }

            if (Server.Length < 1 || Login.Length < 1 || Pass.Length < 1)
                throw new System.ArgumentException("Parameter invalid", "");

            Validate v = new Validate();

            if (Action == null || Action.Length < 1)
                Action = "login";
            else if (!v.IsAction(Action))
                throw new System.ArgumentException("Parameter action invalid", "action");

            if (Port == null || Port.Length < 1)
                Port = "9803";
            else if (!v.IsPort(Port))
                throw new System.ArgumentException("Parameter port invalid", "port");

            if (LogFile == null || LogFile.Length < 1)
                LogFile = "utmauth.log";
            else if (!v.IsLogFile(LogFile))
                throw new System.ArgumentException("Parameter logfile invalid", "logfile");

            if (CookieFile == null || CookieFile.Length < 1)
                CookieFile = "utmauth.cookie";
            else if (!v.IsCookieFile(LogFile))
                throw new System.ArgumentException("Parameter cookie invalid", "cookie");

            if (KeepaliveTime == null || KeepaliveTime.Length < 1)
                KeepaliveTime = "0";
            else if (!v.IsKeepaliveTime(LogFile))
                throw new System.ArgumentException("Parameter keepalive invalid", "keepalive");
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
