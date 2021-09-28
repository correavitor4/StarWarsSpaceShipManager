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
            HttpClient client = new HttpClient();

            string response = await client.GetStringAsync(URL_PLANETAS);


            viewmodels.APIResults pns = new viewmodels.APIResults();
            pns = JsonConvert.DeserializeObject<viewmodels.APIResults>(response);

            for(int i = 0; i < pns.Results.Count; i++)
            {
                if (pns.Results[i].Population == "unknown")
                {
                    pns.Results[i].Population = 0.ToString();
                }
            }
            
            InsertPlanets op = new InsertPlanets(pns);
            
        }

        /*private string formatPlanetsJsonString(string response)
        {
            int initIndex = response.IndexOf(":[")+2;
            int lengthForMethod = response.Length - initIndex -2;
            return ("["+response.Substring(initIndex,lengthForMethod)+"]");
        }*/
    }
}
