using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMultiThread.Models
{        
    public class Precipitation
    {
        public string probability { get; set; }
        public string rain { get; set; }
        public string snow { get; set; }
        public string sleet { get; set; }
        public string hail { get; set; }
    }

    public class Wind
    {
        public string direction { get; set; }
        public string angle { get; set; }
        public string speed { get; set; }
    }

    public class WeatherModel
    {
        public string temperature { get; set; }
        public string humidity { get; set; }
        public string pressure { get; set; }
        public Precipitation precipitation { get; set; }
        public DateTime time { get; set; }
        public Wind wind { get; set; }
    }


}
