using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace kaland_jatek
{
    class karakter
    {
        public int ugyesseg = 0;
        public int eletero = 0;
        public int szerencse = 0;
        public int arany = 0;
        public int etel = 0;
        public int ekkovek = 0;
        public List<string> felszereles = new List<string> { "Kard", "Bőrvért" };
        public string[] italok = new string[2];


    }
    internal class Program
    {

        public static int dice()
        {
            Random rnd = new Random();
            int kocka = rnd.Next(1,7);
            return kocka;
        } 
        static void Main(string[] args)
        {
            string[] kartyak = new string[200];
            #region TXT import
            StreamReader sr = new StreamReader("kartya.txt", Encoding.UTF8);
            int kdb = 0;
            int bdb = 1;
            string str = "";
            while (!sr.EndOfStream)
            {
                str = sr.ReadLine();
                if (kdb != kartyak.Length && bdb % 3 == 0)
                {
                    kartyak[kdb] = str;
                    Console.WriteLine(kdb + ",," + str);
                    kdb++;
                }
                bdb++;
                //Console.WriteLine(bdb);
            }
            sr.Close();
            #endregion
            int start_ugyesseg = dice() + 6;
            int start_eletero = dice() + dice() + 12;
            int start_szerencse = dice() + 6;
            int start_arany = 20;
            int start_etel = 5;
            
            karakter karakter = new karakter();
            karakter.ugyesseg = start_ugyesseg;
            karakter.eletero = start_eletero;
            karakter.szerencse=start_szerencse;
            karakter.arany = start_arany;
            karakter.etel = start_etel;
            Console.WriteLine(kartyak[150]);
            Console.ReadKey();
        }
    }
}
