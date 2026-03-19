using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace ThreadKitchen
{
    public partial class Form1 : Form
    {
        private ProgressBar[] _pbchef;
        private Label[] _lblperChef;
        private readonly Random _rnd = new Random();

        public Form1()
        {
            InitializeComponent();
            button1.Click += Button1_Click; // Tasto Avvia
            button2.Click += Button2_Click; // Tasto Reset
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button2.Enabled = false;
            button1.Enabled = true;
            richTextBox1.ReadOnly = true;

            // Inizializzo i controlli dinamici
            impostaLabelCuchi();

            var chefs = Chef.ChefsList;
            // Assicurati che questi nomi corrispondano a quelli nel Designer
            labelProgres1.Text = $"{chefs[0].Name} - {chefs[0].Dish}";
            labelProgres2.Text = $"{chefs[1].Name} - {chefs[1].Dish}";
            labelProgres3.Text = $"{chefs[2].Name} - {chefs[2].Dish}";
            labelProgres4.Text = $"{chefs[3].Name} - {chefs[3].Dish}";

            LogMessage("🍳 Cucina pronta. Premi «Avvia cucina» per iniziare.");
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = true;

            // Nota: _lblperChef[0] viene creato in impostaLabelCuchi
            LogMessage("Avvio preparazione di " + Chef.ChefsList[0].Name + "....");

            Thread treadMario = new Thread(() => cookDish(0));
            treadMario.IsBackground = true;
            treadMario.Start();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            LogMessage("↺ Reset eseguito. La cucina è pronta per un nuovo turno.");
            richTextBox1.Clear();
        }

        private void aggiornaCuchi()
        {
            // Uso Invoke per aggiornare la UI dal thread secondario
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(aggiornaCuchi));
                return;
            }

            for (int i = 0; i < Chef.ChefsList.Count; i++)
            {
                if (_pbchef[i] != null)
                    _pbchef[i].Value = Chef.ChefsList[i].Progress;

                if (_lblperChef[i] != null)
                    _lblperChef[i].Text = $"{Chef.ChefsList[i].Progress}%";
            }
        }

        private void LogMessage(string msg)
        {
            // Thread-safe log
            if (richTextBox1.InvokeRequired)
            {
                richTextBox1.Invoke(new Action<string>(LogMessage), msg);
                return;
            }
            richTextBox1.AppendText($"{DateTime.Now:HH:mm:ss} - {msg}\n");
            richTextBox1.ScrollToCaret();
        }

        private void cookDish(int chefIndex)
        {
            Chef chef = Chef.ChefsList[chefIndex];
            Stopwatch sw = Stopwatch.StartNew();

            while (chef.Progress < 100)
            {
                Thread.Sleep(_rnd.Next(100, 501));
                int incremento = _rnd.Next(1, 8);
                chef.Progress = Math.Min(100, chef.Progress + incremento);

                //AGGIORNA DATI NELLA FORM
              /*  _pbchef[chefIndex].Value = chef.Progress;
                _lblperChef[chefIndex].Text = $"{chef.Progress}%";*/
                //aggiornaCuchi();
            }

            sw.Stop();
            chef.ElapsedSeconds = sw.Elapsed.TotalSeconds;
            chef.IsFinished = true;
            //preparziobne terminata scivo il log 
            LogMessage($"✅ {chef.Name} ha terminato il piatto {chef.Dish} in {chef.ElapsedSeconds:F2}s");
        }

        private void impostaLabelCuchi()
        {
            string[] icona = { "🍳", "🍲", "🍝", "🍰" };
            _pbchef = new ProgressBar[4];
            _lblperChef = new Label[4];

            for (int i = 0; i < 4; i++)
            {
                // Inizializzazione Label
                _lblperChef[i] = new Label
                {
                    Text = $"{icona[i]} 0%",
                    Location = new System.Drawing.Point(20, 30 + i * 50),
                    AutoSize = true,
                    Name = $"lblChef{i}"
                };
                this.Controls.Add(_lblperChef[i]); // Aggiunta al Form

                // Inizializzazione ProgressBar (mancava nel tuo codice)
                _pbchef[i] = new ProgressBar
                {
                    Location = new System.Drawing.Point(120, 30 + i * 50),
                    Size = new System.Drawing.Size(200, 20),
                    Maximum = 100
                };
                this.Controls.Add(_pbchef[i]); // Aggiunta al Form
            }
        }
    }
}