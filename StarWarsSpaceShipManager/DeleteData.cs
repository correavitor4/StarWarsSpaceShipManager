using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace StarWarsSpaceShipManager
{
    class DeleteData
    {
        ConnectionDB conn = new ConnectionDB();
        SqlCommand cmd = new SqlCommand();

        public DeleteData()
        {
            cmd.Connection = conn.connect();
            try
            {
                cmd.CommandText = "delete from HistoricoViagens;delete from PilotosNaves;delete from Naves;delete from Pilotos;delete from Planetas ";


                cmd.ExecuteNonQuery();





            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            conn.disconnect();
        }
    }
}
