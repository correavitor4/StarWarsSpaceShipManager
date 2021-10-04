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
        /*List<string> Id;
        List<string> Name;
        List<string> BornDate;
        List<string> IdPlanet;*/

        SelectInPilots pilots = new SelectInPilots(null);
        SelectInStarShips starShips = new SelectInStarShips(null);

        public FormViagens()
        {
            InitializeComponent();
            updateListView1();
            updateListView2();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty || textBox1.Text == null)
            {
                this.pilots = new SelectInPilots(null);
                updateListView1();
            }
            else
            {
                this.pilots = new SelectInPilots(textBox1.Text);

                updateListView1();
            }
        }

        private void updateListView1()
        {
            //Limpa a listview
            if (listView1.Items.Count > 0)
            {
                for(int i = listView1.Items.Count - 1; i >= 0; i--)
                {
                    listView1.Items.Remove(listView1.Items[i]);
                }
            }

            for(int i = 0; i < this.pilots.Id.Count;i++)
            {
                listView1.Items.Add(this.pilots.Name[i]);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                this.starShips = new SelectInStarShips(null);
                updateListView2();
            }
            else
            {
                this.starShips = new SelectInStarShips(textBox2.Text);
                updateListView2();
            }
        }

        private void updateListView2()
        {
            if (listView2.Items.Count > 0)
            {
                for (int i = listView2.Items.Count - 1; i >= 0; i--)
                {
                    listView2.Items.Remove(listView2.Items[i]);
                }
            }

            for (int i = 0; i < this.starShips.Id.Count; i++)
            {
                listView2.Items.Add(this.starShips.Name[i]);
            }
        }
    }
}
