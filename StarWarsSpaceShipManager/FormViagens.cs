using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace StarWarsSpaceShipManager
{
    public partial class FormViagens : Form
    {
        public FormViagens()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty || textBox1.Text == null)
            {
                MessageBox.Show("A caixa de pesquisa de pilotos está vazia");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("A caixa de pesquisa de naves está vazia");
            }
        }
    }
}
