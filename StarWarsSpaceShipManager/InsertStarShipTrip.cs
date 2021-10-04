using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace StarWarsSpaceShipManager
{
    class InsertStarShipTrip
    {
        ConnectionDB conn =new ConnectionDB();
        SqlCommand cmd = new SqlCommand();
        

        private string message = "";

        public InsertStarShipTrip(string PilotID, string ShipId)
        {
            try
            {
                cmd.Connection = conn.connect();

                cmd.CommandText = string.Format("INSERT INTO [dbo].[HistoricoViagens] ([IdPiloto],[IdNave]) VALUES ('{0}','{1}')", PilotID, ShipId);

                cmd.ExecuteNonQuery();

                conn.disconnect();
                this.message = "Viagem iniciada com sucesso!";
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
