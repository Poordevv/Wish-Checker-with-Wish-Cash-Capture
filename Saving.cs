using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wish_Checker_by_Poordev
{
    class Saving
    {

        static object LOCKER = new object();

        public static void Save(string Combo, int file)
        {
            if (file == 1)
            {
                lock (LOCKER)
                {
                    using (FileStream textDocument = new FileStream("WishHits.txt", FileMode.Append, FileAccess.Write, FileShare.None))
                    {
                        using (StreamWriter writer = new StreamWriter(textDocument))
                        {
                            writer.WriteLine($"[HIT] - {Combo}");

                            textDocument.Flush();

                        }
                    }
                }
            }
            else if (file == 2)
            {

            }
            else if (file == 3)
            {

            }
        }
    }
}
