using System;
using System.Threading;
using System.Windows.Forms;

namespace ThreadKitchen
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            button1.Click += Button1_Click; // Tasto Avvia
            button2.Click += Button2_Click; // Tasto Reset
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            richTextBox1.ReadOnly = true;

            var chefs = Chef.ChefsList;

            labelProgres1.Text = $"{chefs[0].Name} - {chefs[0].Dish}";
            labelProgres2.Text = $"{chefs[1].Name} - {chefs[1].Dish}";
            labelProgres3.Text = $"{chefs[2].Name} - {chefs[2].Dish}";
            labelProgres4.Text = $"{chefs[3].Name} - {chefs[3].Dish}";

            button2.Enabled = false;
            LogMessage("🍳 Cucina pronta. Premi «Avvia cucina» per iniziare.");
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            LogMessage("👨\u200d🍳 Cuochi al lavoro");
        }

        private void Button2_Click(object sender, EventArgs e)
        {

            LogMessage("↺ Reset eseguito. La cucina è pronta per un nuovo turno.");
        }

        private void LogMessage(string msg)
        {
            richTextBox1.AppendText($"{DateTime.Now:HH:mm:ss} - {msg}\n");
            richTextBox1.ScrollToCaret();
        }
    }
}