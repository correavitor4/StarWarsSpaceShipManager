using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace StarWarsSpaceShipManager
{
    class SelectInPilots
    {
        ConnectionDB conn = new ConnectionDB();
        SqlCommand cmd = new SqlCommand();


        //listas com conteúdos das colunas
        public List<string> Id = new List<string>();
        public List<string> Name = new List<string>();
        public List<string> BornDate = new List<string>();
        public List<string> IdPlanet = new List<string>();

        public SelectInPilots(string where)
        {
            try
            {
                cmd.Connection = conn.connect();
                if (string.IsNullOrEmpty(where))
                {
                    cmd.CommandText = "SELECT * FROM [dbo].[Pilotos]";
                    
                }
                else
                {
                    cmd.CommandText = string.Format("SELECT * FROM [dbo].[Pilotos] WHERE [Nome] like '%{0}%'", where);
                }

                SqlDataReader reader = cmd.ExecuteReader();
                
                while (reader.Read())
                {
                    //System.Diagnostics.Debug.WriteLine(reader["IdPiloto"].ToString());
                    this.Id.Add(reader["IdPiloto"].ToString());
                    this.Name.Add(reader["Nome"].ToString());
                    this.BornDate.Add(reader["AnoNascimento"].ToString());
                    this.IdPlanet.Add(reader["IdPlaneta"].ToString());
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
