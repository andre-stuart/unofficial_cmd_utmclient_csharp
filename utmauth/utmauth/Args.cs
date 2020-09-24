using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security;

namespace utmauth
{
    class Args
    {
        public readonly string Version = "0.1";
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
                var iv = i + 1;
                _args[i] = _args[i].ToLower();

                if (_args[i] == "--action")
                    Action = _args[iv].Contains("--") ? "" : _args[iv].Trim().ToLower();

                else if (_args[i] == "--server")
                    Server = _args[iv].Contains("--") ? "" : _args[iv].Trim().ToLower();

                else if (_args[i] == "--port")
                    Port = _args[iv].Contains("--") ? "" : _args[iv].Trim();

                else if (_args[i] == "--login")
                    Login = _args[iv].Contains("--") ? "" : _args[iv].Trim().ToLower();

                else if (_args[i] == "--pass")
                    Pass = _args[iv].Contains("--") ? "" : _args[iv].Trim();

                else if (_args[i] == "--cookie")
                    CookieFile = _args[iv].Contains("--") ? "" : _args[iv].Trim();

                else if (_args[i] == "--keepalive")
                    KeepaliveTime = _args[iv].Contains("--") ? "" : _args[iv].Trim();

                else if (_args[i] == "--agent")
                    Agent = _args[iv].Contains("--") ? "" : _args[iv].Trim();

                else if (_args[i].Contains("--"))
                    throw new System.ArgumentException("Invalid parameter", "");
            }

            //if (Server.Length < 1 || Login.Length < 1 || Pass.Length < 1)
            if (Server.Length < 1 || Login.Length < 1)
                throw new System.ArgumentException("Login or server parameter invalid", "");

            var v = new Validate();

            if (!v.IsServer(Server))
                throw new System.ArgumentException("Server prameter invalid", "action");

            if (Action == null || Action.Length < 1)
                Action = "login";
            else if (!v.IsAction(Action))
                throw new System.ArgumentException("Action prameter invalid", "action");

            if(Action == "login" && (Pass == null || Pass.Length < 1))
                throw new System.ArgumentException("Pass parameter invalid", "");

            if (Port == null || Port.Length < 1)
                Port = "9803";
            else if (!v.IsPort(Port))
                throw new System.ArgumentException("Port parameter invalid", "port");

            if (Agent == null || Agent.Length < 1)
                Agent = "CMDClient";
            else if (!v.IsName(Agent))
                throw new System.ArgumentException("Agent parameter invalid", "port");

            if (CookieFile == null || CookieFile.Length < 1)
                CookieFile = "utmauth.cookie";
            else if (!v.IsCookieFile(LogFile))
                throw new System.ArgumentException("Cookie parameter invalid", "cookie");

            if (KeepaliveTime == null || KeepaliveTime.Length < 1)
                KeepaliveTime = "0";
            else if (!v.IsKeepaliveTime(LogFile))
                throw new System.ArgumentException("Keepalive parameter invalid", "keepalive");
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
        public string Pass { get; set; }

        [StringLength(200)]
        public string Agent { get; set; }
        
        [StringLength(4)]
        public string KeepaliveTime { get; set; }
        
        [StringLength(1024)]
        public string LogFile { get; set; }
        
        [StringLength(1024)]
        public string CookieFile { get; set; }
    }
}
