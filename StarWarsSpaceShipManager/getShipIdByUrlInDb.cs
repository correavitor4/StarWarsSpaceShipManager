using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace StarWarsSpaceShipManager
{
    class getShipIdByUrlInDb
    {
        ConnectionDB conn = new ConnectionDB();
        SqlCommand cmd = new SqlCommand();
        public string result;

        public getShipIdByUrlInDb(string url)
        {
            try
            {
                



                cmd.Connection= conn.connect();

                cmd.CommandText = string.Format("SELECT [IdNave] FROM [dbo].[Naves] WHERE [Url]='{0}'", url);

                SqlDataReader reader =cmd.ExecuteReader();



                while (reader.Read())
                {
                    result = reader["IdNave"].ToString();
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
