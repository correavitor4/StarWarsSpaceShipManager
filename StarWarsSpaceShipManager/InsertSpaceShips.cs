using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace StarWarsSpaceShipManager
{
    class InsertSpaceShips
    {
        ConnectionDB conn = new ConnectionDB();
        SqlCommand cmd = new SqlCommand();
        private string message = "";
        public InsertSpaceShips(List<viewmodels.SpaceShipViewModel> ships)
        {
            try
            {
                cmd.Connection = conn.connect();
                string textComando = "INSERT INTO [dbo].[Naves] " +
                    "([Nome],[Modelo],[Passageiros],[Carga],[Classe],[Url]) VALUES ";

                for(int i = 0; i < ships.Count; i++)
                {
                    textComando += string.Format("('{0}','{1}','{2}','{3}','{4}','{5}')",
                        ships[i].Name, ships[i].Model, ships[i].Passengers, ships[i].Cargo_Capacity, ships[i].Starship_Class, ships[i].Url);
                
                    if(i != ships.Count - 1)
                    {
                        textComando += ",";
                    }

                
                }

                cmd.CommandText = textComando;
                cmd.ExecuteNonQuery();
                conn.disconnect();

                this.message = "Inserção de spaceships feita com sucesso";

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
