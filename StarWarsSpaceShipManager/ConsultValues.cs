using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace StarWarsSpaceShipManager
{
    class ConsultValues
    {
        ConnectionDB conn = new ConnectionDB();
        SqlCommand cmd = new SqlCommand();


        public List<string[]> objectData = new List<string[]>();
        private string message { get; set; }

        public ConsultValues(string tableName, string[] columnNames)
        {
            
            try
            {
                //recebe o endeereço da conexão
                cmd.Connection = conn.connect();

                string commandText = "SELECT";

                for (int i = 0; i < columnNames.Length; i++)
                {
                    commandText += "[" + columnNames[i] + "]";
                    if (i != columnNames.Length - 1)
                    {
                        commandText += ",";
                    }
                }

                commandText += "FROM [" + tableName + "]";

               

                cmd.CommandText = commandText;
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string[] results = new string[columnNames.Length];
                    for(int i = 0; i < columnNames.Length; i++)
                    {
                        results[i] = reader[i].ToString();
                    }
                    this.objectData.Add(results);
                    
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
