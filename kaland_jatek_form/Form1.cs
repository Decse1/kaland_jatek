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
                string[] sk = line.Split('_');
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
        static int[,] kartyaopciok = new int[5,90];

        static karakter player = new karakter();

        static int start_ugyesseg = 0;
        static int start_eletero = 0;
        static int start_szerencse = 0;
        static int start_arany = 0;
        static int start_etel = 0;

        static int kovetkezo_kartya = 0;
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
                string z = sr.ReadLine();
                if (kdb != kartyak.Length && bdb % 3 == 0)
                {
                    kartya tmp = new kartya(z);
                    kartyak[kdb] = tmp;
                    kdb++;
                }
                bdb++;
            }
            sr.Close();
            StreamReader sr2 = new StreamReader("kartya_tobb_opcio.txt", Encoding.UTF8);
            int kdb2 = 0;
            while (!sr2.EndOfStream)
            {
                string[] input = sr2.ReadLine().Split('_');
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
                kdb2++;
            }
            sr2.Close();
            option1_rbtn.Visible = false;
            option2_rbtn.Visible = false;
            option3_rbtn.Visible = false;
            option4_rbtn.Visible = false;

            continue_btn.Visible = false;
            #endregion
        }

        private void start_btn_Click(object sender, EventArgs e)
        {
            start_btn.Visible = false;
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
            card_lbl.Text = kartyak[kovetkezo_kartya].szoveg;
            kovetkezo_kartya = kartyak[0].kovetkezo - 1;
            continue_btn.Visible = true;            

        }

        private void continue_btn_Click(object sender, EventArgs e)
        {
            
            luck_lbl.Text = kartyak[kovetkezo_kartya].kovetkezo.ToString() + "- -" + kovetkezo_kartya;
            if (kartyak[kovetkezo_kartya].kovetkezo < 201)
            {
                card_lbl.Text = kartyak[kovetkezo_kartya].szoveg;
                
            }
            else if (kartyak[kovetkezo_kartya].kovetkezo > 201)
            {
                if (option1_rbtn.Checked)
                {
                    kovetkezo_kartya = int.Parse(option1_rbtn.Text) - 1;
                }
                if (option2_rbtn.Checked)
                {
                    kovetkezo_kartya = int.Parse(option2_rbtn.Text) - 1;
                }
                if (option3_rbtn.Checked)
                {
                    kovetkezo_kartya = int.Parse(option3_rbtn.Text) - 1;
                }
                if (option4_rbtn.Checked)
                {
                    kovetkezo_kartya = int.Parse(option4_rbtn.Text) -1;
                }
                card_lbl.Text = kartyak[kovetkezo_kartya].szoveg;
            }
            option1_rbtn.Visible = false;
            option2_rbtn.Visible = false;
            option3_rbtn.Visible = false;
            option4_rbtn.Visible = false;



            if (kartyak[kovetkezo_kartya].kovetkezo < 201)
            {
                kovetkezo_kartya = kartyak[kovetkezo_kartya].kovetkezo-1;
                continue_btn.Visible = true;
            }
            else if (kartyak[kovetkezo_kartya].kovetkezo == 201)
            {
                start_btn.Text = "Új játék";
                start_btn.Visible = true;
                continue_btn.Visible = false;
            }
            else if (kartyak[kovetkezo_kartya].kovetkezo == 202)
            {
                int g = 0;
                int opcio_db = 0;
                while (g < kartyaopciok.GetLength(1) && kartyaopciok[0, g] != kovetkezo_kartya+1 )
                {
                    g++;
                }
                if (g != 90)
                {
                    for (int i = 1; i < kartyaopciok.GetLength(0); i++)
                    {
                        if (kartyaopciok[i, g] != 0)
                        {
                            opcio_db++;
                        }
                    }
                    if (opcio_db == 2)
                    {
                        option1_rbtn.Text = kartyaopciok[1, g].ToString();
                        option2_rbtn.Text = kartyaopciok[2, g].ToString();
                        option1_rbtn.Visible = true;
                        option2_rbtn.Visible = true;
                    }
                    else if (opcio_db == 3)
                    {
                        option1_rbtn.Text = kartyaopciok[1, g].ToString();
                        option2_rbtn.Text = kartyaopciok[2, g].ToString();
                        option3_rbtn.Text = kartyaopciok[3, g].ToString();
                        option1_rbtn.Visible = true;
                        option2_rbtn.Visible = true;
                        option3_rbtn.Visible = true;
                    }
                    else if (opcio_db == 4)
                    {
                        option1_rbtn.Text = kartyaopciok[1, g].ToString();
                        option2_rbtn.Text = kartyaopciok[2, g].ToString();
                        option3_rbtn.Text = kartyaopciok[3, g].ToString();
                        option4_rbtn.Text = kartyaopciok[4, g].ToString();
                        option1_rbtn.Visible = true;
                        option2_rbtn.Visible = true;
                        option3_rbtn.Visible = true;
                        option4_rbtn.Visible = true;
                    }
                }
                
            }
        }
    }
}
