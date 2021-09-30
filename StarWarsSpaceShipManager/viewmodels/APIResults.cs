using System;
using System.Collections.Generic;
using System.Text;


namespace StarWarsSpaceShipManager.viewmodels
{
    public class APIResults<ViewModel>
    {
       public string Next { set; get; }
        public List<ViewModel> Results { set; get; }
        
    }
}
