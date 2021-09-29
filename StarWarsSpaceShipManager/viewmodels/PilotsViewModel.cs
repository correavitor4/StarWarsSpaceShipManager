using System;
using System.Collections.Generic;
using System.Text;

namespace StarWarsSpaceShipManager.viewmodels
{
    class PilotsViewModel
    {
        public string Name { get; set; }
        public string Birth_Year { get; set; }
        public string Homeworld { get; set; }
        public List<string> Starships { get; set; }
        public string Url { get; set; }

        public List<int> ShipsId = new List<int>();
    }
}
