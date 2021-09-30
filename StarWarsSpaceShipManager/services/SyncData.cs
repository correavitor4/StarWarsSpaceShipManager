using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;


namespace StarWarsSpaceShipManager
{
    public class SyncData
    {
        private string URL_PLANETAS = "http://swapi.dev/api/planets/";
        private string URL_NAVES = "http://swapi.dev/api/starships/";
        private string URL_PILOTOS = "http://swapi.dev/api/people/";

        



        public Task syncronize()
        {

            syncPlanets().Wait();
            syncPilots();
            return syncSpaceShips();
        }

        private async Task syncPilots()
        {
            System.Diagnostics.Debug.WriteLine("Iniciada a sincronização dos pilotos");
            HttpClient client = new HttpClient();
            string response = await client.GetStringAsync(this.URL_PILOTOS);

        }

        private async Task syncPlanets()
        {
            System.Diagnostics.Debug.WriteLine("Iniciada a sincronização dos planetas");
            List<viewmodels.PlanetViewModel> planets = new List<viewmodels.PlanetViewModel>();
            //Instância
            HttpClient client = new HttpClient();

            
            while (URL_PLANETAS != null)
            {
                
                var responseHTTP =  client.GetAsync(URL_PLANETAS).GetAwaiter().GetResult();
                
                string response = responseHTTP.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                viewmodels.APIResults<viewmodels.PlanetViewModel> r = new viewmodels.APIResults<viewmodels.PlanetViewModel>();
                r = JsonConvert.DeserializeObject<viewmodels.APIResults<viewmodels.PlanetViewModel>>(response);
               
                planets.AddRange(r.Results);
              
                this.URL_PLANETAS = r.Next;
            }

            //criar objeto APIResults
            

            //Desserializa o JSON e o envia para esse lugar
            


            //Tratamento de dados para evitar erros ao inserir no DB
            for(int i = 0; i < planets.Count; i++)
            {
                float r;
                long inteiro;
                if (!float.TryParse(planets[i].Rotation_Period,out r))
                {
                    planets[i].Rotation_Period = 0.ToString();
                }
                if (!float.TryParse(planets[i].Orbital_Period, out r))
                {
                    planets[i].Orbital_Period = 0.ToString();
                }
                if (!float.TryParse(planets[i].Diameter, out r))
                {
                    planets[i].Diameter = 0.ToString();
                }
                if (!long.TryParse(planets[i].Population, out inteiro) )
                {
                    planets[i].Population = 0.ToString();
                }

            }
            
            //instancia a classe responsável por armazenar os dados no banco
            InsertPlanets op = new InsertPlanets(planets);


            System.Diagnostics.Debug.WriteLine(op.getMessage());
            
        }

        private async Task syncSpaceShips()
        {
            List<viewmodels.SpaceShipViewModel> ships = new List<viewmodels.SpaceShipViewModel>();
            System.Diagnostics.Debug.WriteLine("Iniciada a sincronização das naves");
            HttpClient client = new HttpClient();

            while (URL_NAVES != null)
            {
                //recebe o que solicitou à API
                var responseHTTP = client.GetAsync(URL_NAVES).GetAwaiter().GetResult();
                //Converte o valor recebido para string
                string response = responseHTTP.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                viewmodels.APIResults<viewmodels.SpaceShipViewModel> thisApiResults = new viewmodels.APIResults<viewmodels.SpaceShipViewModel>();

                thisApiResults = JsonConvert.DeserializeObject<viewmodels.APIResults<viewmodels.SpaceShipViewModel>>(response);
                ships.AddRange(thisApiResults.Results);
                URL_NAVES = thisApiResults.Next;

            }
           

           



            //Formatação para se adaptar ao banco
            for(int i = 0; i < ships.Count; i++)
            {
                int inteiro;
                float floateiro;
                if(!int.TryParse(ships[i].Passengers,out inteiro))
                {
                    ships[i].Passengers = "0";
                }
                /*if(ships[i].Passengers== "843,342"){
                    ships[i].Passengers = 843342.ToString();
                }*/
                if(!float.TryParse(ships[i].Cargo_Capacity,out floateiro))
                {
                    ships[i].Cargo_Capacity = "0";
                }
            }

            InsertSpaceShips op = new InsertSpaceShips(ships);

            System.Diagnostics.Debug.WriteLine(op.getMessage());

           
        }
        
    }
}
