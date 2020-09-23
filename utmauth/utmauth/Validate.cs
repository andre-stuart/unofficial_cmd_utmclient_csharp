using System.IO;
using System.Text.RegularExpressions;

namespace utmauth
{
    class Validate
    {
        private readonly Regex rName;
        private readonly Regex rPort;
        private readonly Regex rKeepTime;
        private readonly Regex rDmn;
        private readonly Regex rMail;
        private readonly Regex rIPv4;

        public Validate()
        {
            rName = new Regex(@"^[a-zA-Z0-9_.-]*$");
            rPort = new Regex(@"^()([1-9]|[1-5]?[0-9]{2,4}|6[1-4][0-9]{3}|65[1-4][0-9]{2}|655[1-2][0-9]|6553[1-5])$");
            rKeepTime = new Regex(@"^()([0-9]|[1-3]?[0-9]{2,3}|4[1-4][0-9]{3}|49[1-4][0-9]{2}|4900)$");
            rDmn = new Regex(@"^(([a-zA-Z0-9]|[a-zA-Z0-9][a-zA-Z0-9\-]*[a-zA-Z0-9])\.)*([A-Za-z]|[A-Za-z][A-Za-z0-9\-]*[A-Za-z0-9])$");
            rMail = new Regex(@"^(^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$)$");
            rIPv4 = new Regex(@"^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$");
        }

        public bool IsAction(string val) => val.Length > 0 && (val == "login" || val == "logout" || val == "keepalive");
        public bool IsServer(string val) => val.Length > 0 && (rIPv4.IsMatch(val) || !rDmn.IsMatch(val));
        public bool IsPort(string val) => val.Length > 0 && rPort.IsMatch(val);
        public bool IsLogin(string val) => val.Length > 0 && (rMail.IsMatch(val) || rName.IsMatch(val));
        public bool IsKeepaliveTime(string val) => val.Length > 0 && rKeepTime.IsMatch(val);
        public bool IsLogFile(string val)
        {
            try { File.WriteAllText(val, ""); return true; } catch { return false; };
        }
        public bool IsCookieFile(string val)
        {
            try { File.WriteAllText(val, ""); return true; } catch { return false; };
        }
    }
}
