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

        List<viewmodels.PlanetViewModel> planets = new List<viewmodels.PlanetViewModel>();



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

            //Instância
            HttpClient client = new HttpClient();

            System.Diagnostics.Debug.WriteLine(0);
            while (URL_PLANETAS != null)
            {
                System.Diagnostics.Debug.WriteLine(1);
                var responseHTTP =  client.GetAsync(URL_PLANETAS).GetAwaiter().GetResult();
                System.Diagnostics.Debug.WriteLine(2);
                string response = responseHTTP.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                viewmodels.APIResults<viewmodels.PlanetViewModel> r = new viewmodels.APIResults<viewmodels.PlanetViewModel>();
                r = JsonConvert.DeserializeObject<viewmodels.APIResults<viewmodels.PlanetViewModel>>(response);
                System.Diagnostics.Debug.WriteLine(3);
                this.planets.AddRange(r.Results);
                System.Diagnostics.Debug.WriteLine(4);
                this.URL_PLANETAS = r.Next;
            }

            //criar objeto APIResults
            

            //Desserializa o JSON e o envia para esse lugar
            


            //Alguns planetas estavam com população "unknown", então mudei para 0, quando isso ocorre. Isto evita erros de inserção no banco de dados
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
            System.Diagnostics.Debug.WriteLine("Iniciada a sincronização das naves");
            HttpClient client = new HttpClient();
            string response = await client.GetStringAsync(URL_NAVES);

            viewmodels.APIResults<viewmodels.SpaceShipViewModel> spaceShips = new viewmodels.APIResults<viewmodels.SpaceShipViewModel>();

            var ships = JsonConvert.DeserializeObject<viewmodels.APIResults<viewmodels.SpaceShipViewModel>>(response);



            //Formatação para se adaptar ao banco
            for(int i = 0; i < ships.Results.Count; i++)
            {
                if(ships.Results[i].Passengers== "n/a")
                {
                    ships.Results[i].Passengers = "0";
                }
                if(ships.Results[i].Passengers== "843,342"){
                    ships.Results[i].Passengers = 843342.ToString();
                }
            }

            InsertSpaceShips op = new InsertSpaceShips(ships);

            System.Diagnostics.Debug.WriteLine(op.getMessage());

           
        }
        
    }
}
