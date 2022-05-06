using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMultiThread
{
    public class TelemetryTCPModel
    {
        public TelemetryTCPModel()
        {
            //Gerekli startup islemleri..
        }

        public string ip { get; set; }
        public int port { get; set; }
    }
}
