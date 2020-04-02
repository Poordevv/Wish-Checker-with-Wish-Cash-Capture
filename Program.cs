using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Wish_Checker_by_Poordev
{
    class Program
    {
        //First started on March 3rd, 2020
        static void Main(string[] args)
        {
            Wish_Checker_by_Poordev.Loading.LoadFiles();

            Console.Title = "ASCII Art";
            Console.ForegroundColor = ConsoleColor.Blue;
            string title = @"

               ░██╗░░░░░░░██╗██╗░██████╗██╗░░██╗  ░█████╗░██╗░░██╗███████╗░█████╗░██╗░░██╗███████╗██████╗░
              ░██║░░██╗░░██║██║██╔════╝██║░░██║  ██╔══██╗██║░░██║██╔════╝██╔══██╗██║░██╔╝██╔════╝██╔══██╗
               ██╗████╗██╔╝██║╚█████╗░███████║  ██║░░╚═╝███████║█████╗░░██║░░╚═╝█████═╝░█████╗░░██████╔╝
            ░ ░████╔═████║░██║░╚═══██╗██╔══██║  ██║░░██╗██╔══██║██╔══╝░░██║░░██╗██╔═██╗░██╔══╝░░██╔══██╗
             ░░ ╚██╔╝░╚██╔╝░██║██████╔╝██║░░██║  ╚█████╔╝██║░░██║███████╗╚█████╔╝██║░╚██╗███████╗██║░░██║
               ░░░╚═╝░░░╚═╝░░╚═╝╚═════╝░╚═╝░░╚═╝  ░╚════╝░╚═╝░░╚═╝╚══════╝░╚════╝░╚═╝░░╚═╝╚══════╝╚═╝░░╚═╝

                                                                                
                                                                                                          ";

            Console.WriteLine(title);


            Thread menuUpdater;
            menuUpdater = new Thread(new ThreadStart(CPM));
            menuUpdater.Start();

            string value;
            int count;


            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("-> Input Thread Counts [Recommended Amount: 30-50]: ");
            Console.ResetColor();

            value = Console.ReadLine();

            count = Convert.ToInt32(value);

            Threading.Initialize(count, Request.Start);
            Console.ReadKey();
        }

        static void CPM()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            while (true)
            {
                if (sw.ElapsedMilliseconds > 10000 && sw.ElapsedMilliseconds < 10500)
                {
                    Request.CPM = Request.CPMTimer * 6;
                    Request.CPMTimer = 0;
                    sw.Reset();
                    sw.Start();
                }

                    Console.Title = "Poordev's Wish Cracker ¬ Hits: " + Request.Hits + " || Progression: " + Request.Checked + "/" + Loading.comboList.Count() + " || [CPM: " + Request.CPM + " ]";
            }
        }

    }
}
