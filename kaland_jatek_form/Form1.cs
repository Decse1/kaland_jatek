using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace kaland_jatek_form
{
    public partial class Form1 : Form
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
        class kartya
        {
            public string szoveg = "";
            public int kovetkezo = 0;
            public kartya(string line)
            {
                string[] sk = line.Split(';');
                this.szoveg = sk[0];
                this.kovetkezo = int.Parse(sk[1]);
            }
        }

        public static int dice()
        {
            int kocka = 0;
            Random rnd = new Random();
            kocka = rnd.Next(1, 7);
            return kocka;
        }

        static kartya[] kartyak = new kartya[200];
        static int[,] kartyaopciok = new int[5,200];

        static karakter player = new karakter();

        static int start_ugyesseg = 0;
        static int start_eletero = 0;
        static int start_szerencse = 0;
        static int start_arany = 0;
        static int start_etel = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void exit_btn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            #region TXT import
            StreamReader sr = new StreamReader("kartya.txt", Encoding.UTF8);
            int kdb = 0;
            int bdb = 1;
            while (!sr.EndOfStream)
            {
                kartya tmp = new kartya(sr.ReadLine());
                if (kdb != kartyak.Length && bdb % 3 == 0)
                {
                    
                    kartyak[kdb] = tmp;
                    kdb++;
                }
                bdb++;
            }
            sr.Close();
            StreamReader sr2 = new StreamReader("kartya.txt", Encoding.UTF8);
            int kdb2 = 0;
            while (!sr2.EndOfStream)
            {
                string[] input = sr2.ReadLine().Split(';');
                if (input.Length == 3)
                {
                    kartyaopciok[0, kdb2] = int.Parse(input[0]);
                    kartyaopciok[1, kdb2] = int.Parse(input[1]);
                    kartyaopciok[2, kdb2] = int.Parse(input[2]);
                    kartyaopciok[3, kdb2] = 0;
                    kartyaopciok[4, kdb2] = 0;
                }
                else if (input.Length == 4)
                {
                    kartyaopciok[0, kdb2] = int.Parse(input[0]);
                    kartyaopciok[1, kdb2] = int.Parse(input[1]);
                    kartyaopciok[2, kdb2] = int.Parse(input[2]);
                    kartyaopciok[3, kdb2] = int.Parse(input[3]);
                    kartyaopciok[4, kdb2] = 0;
                }
                else if (input.Length == 5)
                {
                    kartyaopciok[0, kdb2] = int.Parse(input[0]);
                    kartyaopciok[1, kdb2] = int.Parse(input[1]);
                    kartyaopciok[2, kdb2] = int.Parse(input[2]);
                    kartyaopciok[3, kdb2] = int.Parse(input[3]);
                    kartyaopciok[4, kdb2] = int.Parse(input[4]);
                }

            }
            sr2.Close();
            #endregion
        }

        private void start_btn_Click(object sender, EventArgs e)
        {
            start_ugyesseg = dice() + 6;
            start_eletero = dice() + dice() + 12;
            start_szerencse = dice() + 6;
            start_arany = 20;
            start_etel = 5;

            player.ugyesseg = start_ugyesseg;
            player.eletero = start_eletero;
            player.szerencse = start_szerencse;
            player.arany = start_arany;
            player.etel = start_etel;

            health_lbl.Text = $"Élet erő: {player.eletero}";
            strenght_lbl.Text = $"Harc erő: {player.ugyesseg}";
            luck_lbl.Text = $"Szerencse: {player.szerencse}";
            card_lbl.Text = kartyak[0];
        }
    }
}
