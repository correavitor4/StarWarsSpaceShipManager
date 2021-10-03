using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StarWarsSpaceShipManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            label2.Visible = true;
            progressBar1.Visible = true;
            progressBar1.Value = 25;
            SyncData sc = new SyncData();
           
            progressBar1.Value = 70;
            sc.syncronize().Wait();
            sc.syncDbAditionalData();
            progressBar1.Value = 100;

            FormViagens f = new FormViagens();
            f.Show();
            this.Hide();

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
    }
}
