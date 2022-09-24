using OGUBumper.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OGUBumper.Services
{
    internal class OGUService
    {
        private readonly string cookieString;

        private readonly string UserAgent;

        private readonly HttpClient httpClient;

        public OGUService(string CookieString)
        {
            cookieString = CookieString;

            UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/105.0.0.0 Safari/537.36";

            httpClient = new HttpClient(new HttpClientHandler
            {
                UseCookies = false,
                Proxy = new WebProxy()
            });
        }

        public async Task PostReplyAsync(WebForm webForm, string replyMessage)
        {
            using var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"https://ogu.gg/newreply.php?tid={webForm.tid}&processed=1")
            {
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    ["my_post_key"] = webForm.my_post_key,
                    ["subject"] = webForm.subject,
                    ["action"] = "do_newreply",
                    ["posthash"] = webForm.posthash,
                    ["quoted_ids"] = "",
                    ["lastpid"] = webForm.lastpid,
                    ["from_page"] = webForm.from_page,
                    ["tid"] = webForm.tid,
                    ["method"] = webForm.method,
                    ["message"] = replyMessage
                })
            };

            requestMessage.Headers.Add("User-Agent", UserAgent);
            requestMessage.Headers.Add("Cookie", cookieString);

            await httpClient.SendAsync(requestMessage);
        }

        public async Task<WebForm> GetPostInfoAsync(string URL)
        {
            using var requestMessage = new HttpRequestMessage(HttpMethod.Get, URL);

            requestMessage.Headers.Add("User-Agent", UserAgent);
            requestMessage.Headers.Add("Cookie", cookieString);

            using var responseMessage = await httpClient.SendAsync(requestMessage);

            var responseString = await responseMessage.Content.ReadAsStringAsync();

            return GetRegInfoFromResponseString(responseString);
        }

        protected WebForm GetRegInfoFromResponseString(string responseString)
        {
            string MyPostKey = Regex.Match(responseString, "name=\"my_post_key\" value=\"(.*)\" />").Groups[1].Value;
            string Subject = Regex.Match(responseString, "name=\"subject\" value=\"(.*)\" />").Groups[1].Value;
            string Posthash = Regex.Match(responseString, "name=\"posthash\" value=\"(.*)\" id=\"posthash\"").Groups[1].Value;
            string LastPID = Regex.Match(responseString, "name=\"lastpid\" id=\"lastpid\" value=\"(.*)\"").Groups[1].Value;
            string FromPage = Regex.Match(responseString, "name=\"from_page\" value=\"(.*)\" />").Groups[1].Value;
            string TID = Regex.Match(responseString, "name=\"tid\" value=\"(.*)\" />").Groups[1].Value;
            string Method = Regex.Match(responseString, "name=\"method\" value=\"(.*)\" />").Groups[1].Value;

            return new WebForm(MyPostKey, Subject, Posthash, LastPID, FromPage, TID, Method);
        }
    }
}
