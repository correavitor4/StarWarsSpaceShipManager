using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace StarWarsSpaceShipManager
{
    class SelectInPilotsShipsTable
    {

        public List<string> PilotsId = new List<string>();
        public List<string> ShipsId = new List<string>();
        public List<string> AuthorizedFlag = new List<string>();

        ConnectionDB conn = new ConnectionDB();
        SqlCommand cmd = new SqlCommand();

        public SelectInPilotsShipsTable(string whereIdPiloto, string whereIdNave)
        {
            cmd.Connection = conn.connect();

            try
            {
                if(string.IsNullOrEmpty(whereIdPiloto) && string.IsNullOrEmpty(whereIdNave))
                {
                    cmd.CommandText = "SELECT * FROM [dbo].[PilotosNaves]";
                }
                else
                {
                    if (string.IsNullOrEmpty(whereIdPiloto))
                    {
                        cmd.CommandText = string.Format("SELECT * FROM [dbo].[PilotosNaves] WHERE [IdNave]='{0}'",whereIdNave);
                    }
                    else if (string.IsNullOrEmpty(whereIdNave))
                    {
                        cmd.CommandText = string.Format("SELECT * FROM [dbo].[PilotosNaves] Where [IdPiloto]='{0}'", whereIdPiloto);
                    }
                    else{
                        cmd.CommandText = string.Format("SELECT * FROM [dbo].[PilotosNaves] Where [IdPiloto]='{0}' AND [IdNave]='{1}'", whereIdPiloto,whereIdNave);
                    }
                }

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    this.PilotsId.Add(reader["IdPiloto"].ToString());
                    this.ShipsId.Add(reader["IdNave"].ToString());
                    this.AuthorizedFlag.Add(reader["FlagAutorizado"].ToString());

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
