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

        //Listas com objetos buscados na API. Resolvi colocálos fora das tasks para que o acesso aos dados seja facilitado
        List<viewmodels.PilotsViewModel> pilots = new List<viewmodels.PilotsViewModel>();
        List<viewmodels.PlanetViewModel> planets = new List<viewmodels.PlanetViewModel>();
        List<viewmodels.SpaceShipViewModel> ships = new List<viewmodels.SpaceShipViewModel>();



        /*Para preencher a tabela PILOTOS-NAVES
            [0] = piloto
            [1] = nave
        */
        List<string[]> pilotsShipsRelationship = new List<string[]>();


        public Task syncronize()
        {

            syncPlanets();
            syncSpaceShips();
            return syncPilots();  
        }

        private async Task syncPilots()
        {
            
            System.Diagnostics.Debug.WriteLine("Iniciada a sincronização dos pilotos");
            HttpClient client = new HttpClient();


            while (URL_PILOTOS != null)
            {
                var responseHTTP = client.GetAsync(URL_PILOTOS).GetAwaiter().GetResult();
                var response = responseHTTP.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                viewmodels.APIResults<viewmodels.PilotsViewModel> aPIResults = new viewmodels.APIResults<viewmodels.PilotsViewModel>();
                aPIResults = JsonConvert.DeserializeObject<viewmodels.APIResults<viewmodels.PilotsViewModel>>(response);


                //Faz uma verificação para que somente sejam adicionada as pessoas que pilotam naves (os pilotos)
                foreach(var item in aPIResults.Results)
                {
                    if(item.Starships!=null)
                    {
                        pilots.Add(item);
                        /*string[] cl = { "IdNave", "Url" };
                        ConsultValues op2 = new ConsultValues("Naves", cl);
                        foreach(var itUrl in op2.objectData[1])
                        {
                            foreach(var itSt in item.Starships)
                            {
                                if (itSt == itUrl)
                                {
                                    string[] fi = { i, itUrl };
                                    this.pilotsShipsRelationship.Add({itSt,irUrl});
                                }
                            }
                        }*/
                    }
                }
               
                
                URL_PILOTOS = aPIResults.Next;
                


            }

            
            string[] columnNames = new string[2];
            columnNames[0] = "IdPlaneta";
            columnNames[1] = "Url";

            //Consulta dados dos planetas para pegar o id do planeta de onde é o piloto
            ConsultValues op = new ConsultValues("Planetas", columnNames);


            int cou = 0;
            //Foreach responsável por encontar id do planeta e preencher para colocar no objeto piloto
            foreach(var p in pilots)
            {
                for(int i = 0; i < op.objectData.Count; i++)
                {
                    if (op.objectData[i][1] == p.Homeworld)
                    {

                        cou++;
                        
                        p.PlanetId = op.objectData[i][0];
                        
                    }
                    
                }
            }


            InsertPilots op1 = new InsertPilots(pilots);
            System.Diagnostics.Debug.WriteLine(op1.getMessage());


        }

        private async Task syncPlanets()
        {
            System.Diagnostics.Debug.WriteLine("Iniciada a sincronização dos planetas");
            
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
        

        public void syncDbAditionalData()
        {
            foreach(var pil in pilots)
            {
                foreach(var pilotStarships in pil.Starships)
                {
                    foreach(var ships in this.ships)
                    {
                        if (pilotStarships == ships.Url)
                        {
                            string[] pilConsultColumnNames = {"IdPiloto","Nome"};
                            string[] ShipsConsultColumnNames = { "IdNave","Nome"};

                            ConsultValues pilConsult = new ConsultValues("Pilotos", pilConsultColumnNames);
                            ConsultValues shipsConsult = new ConsultValues("Naves", ShipsConsultColumnNames);

                            for(int i = 0; i < pilConsult.objectData.Count; i++)
                            {
                                if(pilConsult.objectData[i][1] == pil.Name)
                                {
                                    for(int j = 0; j < shipsConsult.objectData.Count;j++)
                                    {
                                        if (shipsConsult.objectData[j][1] == ships.Name)
                                        {
                                            string[] im = { pilConsult.objectData[i][0], shipsConsult.objectData[j][0]};
                                            this.pilotsShipsRelationship.Add(im);
                                        }
                                    }
                                }
                            }
                            

                        }
                    }
                }
            }

            InsertInShipsPilotsTable op =new InsertInShipsPilotsTable(this.pilotsShipsRelationship);
            System.Diagnostics.Debug.WriteLine(op.getMessage());
        }
    }
}
