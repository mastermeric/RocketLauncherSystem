namespace ConsoleMultiThread
{    

    public class Payload
    {
        public string description { get; set; }
        public string weight { get; set; }
    }

    public class Telemetry
    {
        public string host { get; set; }
        public string port { get; set; }
    }

    public class Timestamps
    {
        public object launched { get; set; }
        public object deployed { get; set; }
        public object failed { get; set; }
        public object cancelled { get; set; }
    }

    public class RocketModel
    {
        public string id { get; set; }
        public string model { get; set; }
        public string mass { get; set; }
        public Payload payload { get; set; }
        public Telemetry telemetry { get; set; }
        public string status { get; set; }
        public Timestamps timestamps { get; set; }

        
        public string altitude { get; set; }
        public string speed { get; set; }
        public string acceleration { get; set; }
        public string thrust { get; set; }
        public string temperature { get; set; }
    }
}
