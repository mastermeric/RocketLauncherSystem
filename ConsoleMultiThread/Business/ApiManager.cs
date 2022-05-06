using ConsoleMultiThread.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleMultiThread.Business
{
    public static class ApiManager
    {
        
        public static async Task<List<RocketModel>> RoketleriGetir()
        {
            try
            {
                bool isApiOK = false;
                int apiTryCounter = 1;
                List<RocketModel> _roketler = new List<RocketModel>();

                while (!isApiOK)
                {                    
                    HttpClient client1;
                    client1 = new HttpClient();
                    client1.DefaultRequestHeaders.Add("x-api-key", "API_KEY_1");
                    var _request = await client1.GetAsync("http://localhost:5000/rockets");

                    if (_request.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var jsonString = await _request.Content.ReadAsStringAsync();
                        _roketler = JsonConvert.DeserializeObject<List<RocketModel>>(jsonString);
                        isApiOK = true;

                        Console.WriteLine("Roket API " + apiTryCounter + ". Deneme OK. " + _roketler.Count + " Adet Roket Tespit Edildi..");
                        Console.WriteLine(" ");
                    }
                    else
                    {
                        Console.WriteLine("Roket API " + apiTryCounter + ". Denemede HATA ! Tekrar Deneneniyor...");
                        Thread.Sleep(2000);
                        apiTryCounter++;
                    }
                }

                return _roketler;
            }
            catch (Exception ex)
            {
                Console.WriteLine(" Beklenmeyen HATA ! ");
                Console.WriteLine(ex.Message);                
                return null;
            }
        }


        public static async Task MeteorolojiRaporlama()
        {
            try
            {             

                bool isApiOK = false;       
                int apiTryCounter = 1;
                

                while (!isApiOK)
                {
                    WeatherModel _weatherData = new WeatherModel();
                    HttpClient client1;

                    client1 = new HttpClient();
                    client1.DefaultRequestHeaders.Add("x-api-key", "API_KEY_1");
                    var _request = await client1.GetAsync("http://localhost:5000/weather");

                    if (_request.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var jsonString = await _request.Content.ReadAsStringAsync();
                        _weatherData = JsonConvert.DeserializeObject<WeatherModel>(jsonString);
                        isApiOK = true;

                        
                        //METEROLOJI VERILERINI YAYINLA PUSH..
                        TCPManager _TCPManager = new TCPManager();
                        _TCPManager.PushRealtimeData("-------------------METEROLOJI VERILERI", "-------------------");
                        Thread.Sleep(500);
                        _TCPManager.PushRealtimeData("SICAKLIK ", _weatherData.temperature);
                        _TCPManager.PushRealtimeData("NEM ", _weatherData.humidity);
                        _TCPManager.PushRealtimeData("RÜZGAR YÖNÜ ", _weatherData.wind.direction);
                        _TCPManager.PushRealtimeData("RÜZGAR HIZI ", _weatherData.wind.speed);
                        _TCPManager.PushRealtimeData("RÜZGAR AÇISI ", _weatherData.wind.angle);                        
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        Console.WriteLine("Meteroloji API " + apiTryCounter + ". Denemede HATA ! Meteroloji Verisi Tekrar Aranıyor...");
                        Thread.Sleep(2000);
                        apiTryCounter++;
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Meteroloji Verisi - Beklenmeyen HATA ! ");
                Console.WriteLine(ex.Message);                
            }
        }
    }
}
