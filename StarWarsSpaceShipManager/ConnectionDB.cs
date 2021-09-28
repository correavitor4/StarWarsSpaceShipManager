using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace StarWarsSpaceShipManager
{
    class ConnectionDB
    {
        //instância à classe SqlConnection
        SqlConnection connection = new SqlConnection();

        //Método construtor
        public ConnectionDB()
        {
            connection.ConnectionString = @"Server=localhost\SQLEXPRESS;Database=EstrelaDaMorte;Trusted_Connection=True;";
        }


        //método de conexão
        public SqlConnection connect()
        {
            //verifica se a conexão ainda não está aberta
            if(connection.State == System.Data.ConnectionState.Closed)
            {
                this.connection.Open();
            }

            return this.connection;
        }


        //método de desconexão
        public void disconnect()
        {
            if(connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }

        }

    } 
}
