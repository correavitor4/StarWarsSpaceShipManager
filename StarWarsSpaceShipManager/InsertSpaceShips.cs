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
        public InsertSpaceShips(viewmodels.APIResults<viewmodels.SpaceShipViewModel> ships)
        {
            try
            {
                cmd.Connection = conn.connect();
                string textComando = "INSERT INTO [dbo].[Naves] " +
                    "([Nome],[Modelo],[Passageiros],[Carga],[Classe]) VALUES ";

                for(int i = 0; i < ships.Results.Count; i++)
                {
                    textComando += string.Format("('{0}','{1}','{2}','{3}','{4}')",
                        ships.Results[i].Name, ships.Results[i].Model, ships.Results[i].Passengers, ships.Results[i].Cargo_Capacity, ships.Results[i].Starship_Class);
                
                    if(i != ships.Results.Count - 1)
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
