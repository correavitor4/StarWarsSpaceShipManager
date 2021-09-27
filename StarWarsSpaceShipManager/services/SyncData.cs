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

            //response = formatPlanetsJsonString(response);

            System.Diagnostics.Debug.WriteLine(response);

            viewmodels.APIResults pns = new viewmodels.APIResults();
            pns = JsonConvert.DeserializeObject<viewmodels.APIResults>(response);

            System.Diagnostics.Debug.WriteLine(pns.Results[0].Name);

            /*

            viewmodels.APIResults<viewmodels.PlanetViewModel> apir = new viewmodels.APIResults<viewmodels.PlanetViewModel>();

            
            string response = await client.GetStringAsync(URL_PLANETAS) ;
            

            apir = JsonConvert.DeserializeObject<viewmodels.APIResults<viewmodels.PlanetViewModel>>(response);
            

            System.Diagnostics.Debug.WriteLine(apir.Results);

            this.planets.Add(planets);
            System.Diagnostics.Debug.WriteLine("Concluída a sincronização dos planetas");
            System.Diagnostics.Debug.WriteLine(planets[0].Name);
            */

            

            
            


            
           
        }

        private string formatPlanetsJsonString(string response)
        {
            int initIndex = response.IndexOf(":[")+2;
            int lengthForMethod = response.Length - initIndex -2;
            return ("["+response.Substring(initIndex,lengthForMethod)+"]");
        }
    }
}
