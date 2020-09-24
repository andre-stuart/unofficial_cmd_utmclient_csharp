using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace utmauth
{
    internal class Auth
    {
        private readonly string agent = "[AGENT]/0.1";
        private readonly string urlModel = "https://[SERVER:PORT]/authd.fcgi";
        private readonly Args opt;
        private readonly HttpClient client;

        public Auth(Args opt)
        {
            this.opt = opt;

            var handle = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; }
            };
            client = new HttpClient(handle);
        }

        public async Task LoginAsync()
        {
            var content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "Action", "AUTH_LOGIN" },
                    { "Login", StringToHex(opt.Login) },
                    { "Password", StringToHex(opt.Pass) },
                    { "UserAgent", StringToHex(agent.Replace("[AGENT]",opt.Agent)) }
                }
            );

            var url = urlModel.Replace("[SERVER:PORT]", $"{opt.Server}:{opt.Port}");
            var result = await client.PostAsync(url, content);
            if(result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                if (File.Exists(opt.CookieFile))
                    File.Delete(opt.CookieFile);

                Console.WriteLine(result.StatusCode);
                Console.WriteLine(result.ReasonPhrase);
            }
            else
            {
                var response = await result.Content.ReadAsStringAsync();
                Console.WriteLine(response);
                if (response.Contains("110 AUTH_LOGIN_OK"))
                {
                    if(File.Exists(opt.CookieFile))
                        File.Delete(opt.CookieFile);
                    File.WriteAllText(opt.CookieFile, response);
                }
            }
        }
        public async Task LogoutAsync()
        {
            var content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "Action", "AUTH_LOGOUT" },
                    { "Login", StringToHex(opt.Login) }
                }
            );

            var url = urlModel.Replace("[SERVER:PORT]", $"{opt.Server}:{opt.Port}");
            var result = await client.PostAsync(url, content);
            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine(result.StatusCode);
                Console.WriteLine(result.ReasonPhrase);
            }
            else
            {
                var response = await result.Content.ReadAsStringAsync();
                Console.WriteLine(response);
                if (File.Exists(opt.CookieFile))
                    File.Delete(opt.CookieFile);
            }
        }
        public async Task KeepaliveAsync()
        {
            if (!File.Exists(opt.CookieFile) || File.ReadAllLines(opt.CookieFile).Length < 1)
                Console.WriteLine($"Cookie file not found [{opt.CookieFile}]");
            else
            {
                var str = File.ReadAllText(opt.CookieFile);
                var arr = str.Split(" ");
                if (arr.Length < 3)
                    Console.WriteLine($"invalid cookie file content [{opt.CookieFile}]");
                else
                {
                    var id = arr[2].Trim().Replace("ticket:", "");
                    var content = new FormUrlEncodedContent(new Dictionary<string, string>
                        {
                            { "Action", "AUTH_KEEPALIVE" },
                            { "Login", StringToHex(opt.Login) },
                            { "id", id }
                        }
                    );

                    var url = urlModel.Replace("[SERVER:PORT]", $"{opt.Server}:{opt.Port}");
                    var result = await client.PostAsync(url, content);
                    if (result.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        Console.WriteLine(result.StatusCode);
                        Console.WriteLine(result.ReasonPhrase);
                    }
                    else
                    {
                        var response = await result.Content.ReadAsStringAsync();
                        Console.WriteLine(response);
                    }
                }
            }
        }

        public static string StringToHex(string asciiString)
        {
            string hex = "";
            foreach (char c in asciiString)
            {
                int tmp = c;
                hex += string.Format("{0:x2}", (uint)System.Convert.ToUInt32(tmp.ToString()));
            }
            return hex;
        }
    }
}