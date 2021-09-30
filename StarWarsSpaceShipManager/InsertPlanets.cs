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

        private string message = "";
        public InsertPlanets(List<viewmodels.PlanetViewModel> planets)
        {
            

            try
            {
                cmd.Connection = conn.connect();

                string textComando = "INSERT   INTO [dbo].[Planetas] ([Nome],[Rotacao],[Orbita],[Diametro],[Clima],[Populacao]) VALUES";

                for (int i = 0; i < planets.Count; i++)
                {


                    

                    textComando += string.Format("('{0}','{1}','{2}','{3}','{4}','{5}')",
                        planets[i].Name, planets[i].Rotation_Period, planets[i].Orbital_Period,
                        planets[i].Diameter, planets[i].Climate,planets[i].Population);
                    
                    
                    //Apenas a formatação de separar os valores a serem inseridos por vírgula
                    if (i != planets.Count - 1)
                    {
                        textComando += ",";
                    }
                }

                cmd.CommandText = textComando;

                cmd.ExecuteNonQuery();

                conn.disconnect();

                this.message = "Inserção de planetas no banco executada com sucesso!";
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
