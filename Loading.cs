using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wish_Checker_by_Poordev
{
    class Loading
    {

        public static List<string> comboList = new List<string>();
        public static List<string> proxyList = new List<string>();

        public static List<Proxies> proxyHandlerList = new List<Proxies>();
        public static void LoadFiles()
        {
            string[] combos = File.ReadAllLines("ComboList.txt");

            foreach (string line in combos)
            {
                comboList.Add(line);
            }

            Threading.Combo = comboList.ToArray();

            string[] proxy = File.ReadAllLines("ProxyList.txt");

            foreach (string line in proxy)
            {
                proxyList.Add(line);
            }

            foreach (string line in proxy)
            {
                proxyHandlerList.Add(new Proxies(line));
            }

            if (comboList.Count() > 0)
            { }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Either your proxies or your combolists are empty.");


                Console.ResetColor();
                Console.ReadKey();
                Environment.Exit(0);
            }


        }
    }
}
