using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace StarWarsSpaceShipManager
{
    class InsertPilots
    {
        ConnectionDB conn = new ConnectionDB();
        SqlCommand cmd = new SqlCommand();

        private string message;

        public InsertPilots(List<viewmodels.PilotsViewModel> pilots)
        {

            try
            {
                cmd.Connection = conn.connect();

                string textCommand = "INSERT INTO [dbo].[Pilotos] (Nome,AnoNascimento,IdPlaneta) VALUES ";

                for(int i = 0; i < pilots.Count; i++)
                {
                    textCommand += string.Format("('{0}','{1}','{2}')",pilots[i].Name,pilots[i].Birth_Year,pilots[i].PlanetId);

                    if (i != pilots.Count - 1)
                    {
                        textCommand += ",";
                    }
                }

                cmd.CommandText = textCommand;
                cmd.ExecuteNonQuery();

                this.message = "Inserção de pilotos no DB executada com sucesso!";

                conn.disconnect();

            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            cmd.Connection = conn.connect();

            

        }

        public string getMessage()
        {
            return this.message;
        }

    }
}
