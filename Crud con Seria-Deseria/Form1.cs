using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Crud_con_Seria_Deseria
{
    public partial class Form1 : Form
    {
        Lista personelista = new Lista();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var file = new FileStream("Persone.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamReader lettore = new StreamReader(file);
            string riga = lettore.ReadLine();
            while (riga != null)
            {
                string[] dati = riga.Split(' ');
                Persone persone = new Persone(dati[0], dati[1]);
                personelista.lista.Add(persone);
                riga = lettore.ReadLine();                 
            }
            Refreshing();
            lettore.Close();
            file.Close();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            var fs = new FileStream("Persone.txt", FileMode.Truncate);
            fs.Close();
            var file = new FileStream("Persone.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamWriter sw = new StreamWriter(file);
            for (int r = 0; r < personelista.lista.Count; r++)
            {
                sw.WriteLine($"{personelista.lista[r].nome} {personelista.lista[r].cognome}");
            }
            sw.Close();
            file.Close();
        }

        class Lista
        {
            public List<Persone> lista = new List<Persone>();
        }

        class Persone
        {
            public Persone(string nome, string cognome)
            {
                this.nome = nome;
                this.cognome = cognome;
            }        

            public string nome;
            public string cognome;

            public string Nome
            {
                get { return nome; }
                set { nome = value; }
            }

            public string Cognome
            {
                get { return cognome; }
                set { cognome = value; }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
             Persone persone = new Persone(textBox1.Text, textBox2.Text);
             personelista.lista.Add(persone);
             Refreshing();
             textBox1.Clear();
             textBox2.Clear();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int a = listBox1.SelectedIndex;
            personelista.lista[a].nome = textBox1.Text;
            personelista.lista[a].cognome = textBox2.Text;
            textBox1.Clear();
            textBox2.Clear();
            Refreshing();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int a = listBox1.SelectedIndex;
            personelista.lista.RemoveAt(a);
            Refreshing();

        }

        public void Refreshing()
        {
            listBox1.Items.Clear();
            for (int r = 0; r < personelista.lista.Count; r++)
            {
                listBox1.Items.Add($"{personelista.lista[r].nome} {personelista.lista[r].cognome}");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int r = 0; r < personelista.lista.Count; r++)
            {
                if (personelista.lista[r].nome == textBox1.Text && personelista.lista[r].cognome == textBox2.Text)
                {
                    listBox1.SelectedIndex = r;
                }
            }
            textBox1.Clear();
            textBox2.Clear();
        }
    }
}
