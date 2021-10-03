using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace StarWarsSpaceShipManager
{
    class InsertInShipsPilotsTable
    {
        ConnectionDB conn = new ConnectionDB();
        SqlCommand cmd = new SqlCommand();
        private string message { get; set; }

        

        public InsertInShipsPilotsTable(List<string[]> items)
        {
            try
            {
                cmd.Connection = conn.connect();
                string com ="INSERT INTO [dbo].[PilotosNaves] (IdPiloto,IdNave) VALUES ";

                for(int i = 0; i < items.Count; i++)
                {
                    com += string.Format("('{0}','{1}')", items[i][0], items[i][1]);
                    if (i != items.Count - 1)
                    {
                        com += ",";
                    }
                }

                cmd.CommandText = com;
                cmd.ExecuteNonQuery();

                this.message = "Tabela PilotosNaves preenchida com sucesso";

                conn.disconnect();
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
        }

        public string getMessage()
        {
            return this.message;
        }
    }
}
