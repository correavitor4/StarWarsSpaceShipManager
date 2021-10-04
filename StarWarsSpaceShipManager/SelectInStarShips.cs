using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace StarWarsSpaceShipManager
{

    class SelectInStarShips
    {
        ConnectionDB conn = new ConnectionDB();
        SqlCommand cmd = new SqlCommand();

        public List<string> Id= new List<string>();
        public List<string> Name = new List<string>();
        public List<string> Model = new List<string>();
        public List<string> Passengers = new List<string>();
        public List<string> Cargo = new List<string>();
        public List<string> Class = new List<string>();
        public List<string> Url = new List<string>();

        public SelectInStarShips(string where)
        {
            try
            {//

                //recebe o endereço da conexão
                cmd.Connection = conn.connect();
                if (string.IsNullOrEmpty(where))
                {
                    cmd.CommandText = "SELECT * FROM [dbo].[Naves]";

                }
                else
                {
                    cmd.CommandText =string.Format("SELECT * FROM [dbo].[Naves] WHERE [Nome] like '%{0}%'",where);
                }


                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    this.Id.Add(reader["IdNave"].ToString());
                    this.Name.Add(reader["Nome"].ToString());
                    this.Model.Add(reader["Modelo"].ToString());
                    this.Passengers.Add(reader["Passageiros"].ToString());
                    this.Cargo.Add(reader["Carga"].ToString());
                    this.Class.Add(reader["Classe"].ToString());
                    this.Url.Add(reader["Url"].ToString());

                }

                conn.disconnect();

            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
        }

    }
}
