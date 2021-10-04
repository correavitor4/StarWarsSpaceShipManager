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

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                textBox3.Text = listView1.SelectedItems[0].Text;
            }
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count > 0)
            {
                textBox4.Text = listView2.SelectedItems[0].Text;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox4.Text))
            {
                MessageBox.Show("Você deve selecionar um piloto e uma nave antes");
            }
            else
            {

                string pilotId = null;
                string shipId = null;

                //Os dois "for" abaixo são responsável por preencher as duas variáveis acima, para que sejam usadas na consulta do db
                for (int i = 0; i < this.pilots.Name.Count; i++)
                {
                    if (this.pilots.Name[i] == textBox3.Text)
                    {
                        pilotId = this.pilots.Id[i];
                    }
                }
                for (int i = 0; i < this.starShips.Name.Count; i++)
                {
                    if (this.starShips.Name[i] == textBox4.Text)
                    {
                        shipId = this.starShips.Id[i];
                    }
                }
                
                
                SelectInPilotsShipsTable pilotsShips = new SelectInPilotsShipsTable(pilotId, shipId);
                
                
                
                if (pilotsShips.PilotsId.Count == 0)
                {
                    MessageBox.Show("Esse piloto não está autorizado a voar com essa nave");
                }
                else
                {
                    InsertStarShipTrip op = new InsertStarShipTrip(pilotId, shipId);
                    System.Diagnostics.Debug.WriteLine(op.getMessage());

                    MessageBox.Show("Nova viagem iniciada com sucesso");
                }

            }
        }
    }
}
