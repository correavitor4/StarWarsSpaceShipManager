using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace StarWarsSpaceShipManager
{
    class InsertPlanets
    {
        ConnectionDB conn = new ConnectionDB();
        SqlCommand cmd = new SqlCommand();
        public InsertPlanets(viewmodels.APIResults planets)
        {
            

            try
            {
                cmd.Connection = conn.connect();

                string textComando = "INSERT   INTO [dbo].[Planetas] ([Nome],[Rotacao],[Orbita],[Diametro],[Clima],[Populacao]) VALUES";

                for (int i = 0; i < planets.Results.Count; i++)
                {


                    System.Diagnostics.Debug.WriteLine(planets.Results[i].Population);

                    textComando += string.Format("('{0}','{1}','{2}','{3}','{4}','{5}')",
                        planets.Results[i].Name, planets.Results[i].Rotation_Period, planets.Results[i].Orbital_Period,
                        planets.Results[i].Diameter, planets.Results[i].Climate,planets.Results[i].Population);
                    if (i != planets.Results.Count - 1)
                    {
                        textComando += ",";
                    }
                }

                cmd.CommandText = textComando;

                cmd.ExecuteNonQuery();

                conn.disconnect();
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }


        }
    }
}
