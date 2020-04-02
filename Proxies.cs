using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wish_Checker_by_Poordev
{
    class Proxies
    {
        public string Proxy { get; set; }
        public int CurrentTries { get; set; } = 0;
        public int MaxTries { get; set; } = 4;
        public bool ProxyBanned { get; set; } = false;
        public bool InUse { get; set; } = false;


        public Proxies(string Proxy)
        {
            this.Proxy = Proxy;
            this.ProxyBanned = ProxyBanned;
            this.InUse = InUse;

            this.CurrentTries = CurrentTries;
            this.MaxTries = MaxTries;
        }
        public string NewProxy()
        {
            return this.Proxy;
        }
        public void IncreaseTries()
        {
            CurrentTries++;
        }
        public void FinishedUsingProxy()
        {
            this.InUse = false;
        }
        public void UsingProxy()
        {
            this.InUse = true;
        }
        public void BanProxyUsage()
        {
            this.ProxyBanned = true;
        }
        public void UnbanProxy()
        {
            this.ProxyBanned = false;
        }

    }
    class ProxyHandler
    {

        private List<Proxies> ProxyList { get; set; }
        private object locker;

        public Proxies NewProxy()
        {
            lock (ProxyList)
            {
                while (true)
                {
                    foreach (Proxies proxy in ProxyList)
                    {
                        if (proxy.ProxyBanned != true && proxy.InUse != true)
                        {
                            if (proxy.CurrentTries < proxy.MaxTries)
                            {
                                proxy.IncreaseTries();
                                proxy.UsingProxy();
                                return proxy;
                            }
                            else
                            {
                                proxy.BanProxyUsage();
                            }
                        }
                    }
                    TimeoutAllProxies();
                    UnbanAllProxies();
                }

            }
        }
        public ProxyHandler(List<Proxies> ProxyList)
        {
            this.ProxyList = ProxyList;
            this.locker = new object();
        }
        public void UnbanAllProxies()
        {
            foreach (Proxies proxy in ProxyList)
            {
                proxy.CurrentTries = 0;
                proxy.UnbanProxy();
            }
        }
        public void TimeoutAllProxies()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            while (true)
            {
                if (sw.ElapsedMilliseconds > 600000)
                {
                    break;
                }

            }

        }

    }

}
