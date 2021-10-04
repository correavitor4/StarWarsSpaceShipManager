using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace StarWarsSpaceShipManager
{
    class SelectInStarShipTripHistoric
    {
        ConnectionDB conn = new ConnectionDB();
        SqlCommand cmd = new SqlCommand();

        public List<string> ShipId = new List<string>();
        public List<string> PilotId = new List<string>();
        public List<string> ExitDate = new List<string>();
        public List<string> ArrivalDate = new List<string>();

        public SelectInStarShipTripHistoric(bool flyingNow)
        {
            cmd.Connection = conn.connect();

            try
            {
                if (flyingNow == true)
                {
                    cmd.CommandText = string.Format("SELECT * FROM [dbo].[HistoricoViagens] Where [DtChegada] = {0}", null);
                }
                else
                {
                    cmd.CommandText = "SELECT * FROM [dbo].[HistoricoViagens]";
                }

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    this.ShipId.Add(reader["IdNave"].ToString());
                    this.PilotId.Add(reader["IdPiloto"].ToString());
                    this.ExitDate.Add(reader["DtSaida"].ToString());
                    this.ShipId.Add(reader["DtChegada"].ToString());
                }

            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }

            conn.disconnect();
        }
    }
}
