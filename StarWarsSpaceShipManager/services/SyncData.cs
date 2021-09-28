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
        private const string URL_PLANETAS = "http://swapi.dev/api/planets/";
        private const string URL_NAVES = "http://swapi.dev/api/starships/";
        private const string URL_PILOTOS = "http://swapi.dev/api/people/";

        List<viewmodels.PlanetViewModel> planets = new List<viewmodels.PlanetViewModel>();



        public Task syncronize()
        {

            return syncPlanets();
        }

        private async Task syncPlanets()
        {
            System.Diagnostics.Debug.WriteLine("Iniciada a sincronização dos planetas");

            //Instância
            HttpClient client = new HttpClient();

            //resposta do server
            string response = await client.GetStringAsync(URL_PLANETAS);


            //criar objeto APIResults
            viewmodels.APIResults<viewmodels.PlanetViewModel> pns = new viewmodels.APIResults<viewmodels.PlanetViewModel>();

            //Desserializa o JSON e o envia para esse lugar
            pns = JsonConvert.DeserializeObject<viewmodels.APIResults<viewmodels.PlanetViewModel>>(response);


            //Alguns planetas estavam com população "unknown", então mudei para 0, quando isso ocorre. Isto evita erros de inserção no banco de dados
            for(int i = 0; i < pns.Results.Count; i++)
            {
                if (pns.Results[i].Population == "unknown")
                {
                    pns.Results[i].Population = 0.ToString();
                }
            }
            
            //instancia a classe responsável por armazenar os dados no banco
            InsertPlanets op = new InsertPlanets(pns);
            
        }

        private async Task syncSpaceShips()
        {
            System.Diagnostics.Debug.WriteLine("Iniciada a sincronização das naves");
            HttpClient client = new HttpClient();
            string response = await client.GetStringAsync(URL_NAVES);

            viewmodels.APIResults<viewmodels.SpaceShipViewModel> spaceShips = new viewmodels.APIResults<viewmodels.SpaceShipViewModel>();

            var ships = JsonConvert.DeserializeObject<viewmodels.APIResults<viewmodels.SpaceShipViewModel>>(response);




           
        }
        
    }
}
