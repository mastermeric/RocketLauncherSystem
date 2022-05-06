using ConsoleMultiThread.Business;
using ConsoleMultiThread.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleMultiThread
{    
    public class Program
    {
        
        public static void DoWork(object prm)
        {
            
            RocketModel model = (RocketModel)prm;
            TCPManager _TCPManager = new TCPManager(model);            
            _TCPManager.StartContinuousRealtimePush();
        }

        static async Task MainAsync()
        {
            //Meteorolojik Raporlama yap..
            await ApiManager.MeteorolojiRaporlama();

            //Roket Listesini Getir..
            List<RocketModel> _roketler = await ApiManager.RoketleriGetir();

            TCPManager _TCPManager = new TCPManager();
            _TCPManager.PushRealtimeData("-------------------ROKET VERILERI", "-------------------");
            Thread.Sleep(500);

            if (_roketler != null)
            {
                Console.WriteLine("-----------------------------------------------------");                                

                foreach (RocketModel item in _roketler)
                {                    
                    //Console.WriteLine("Model: " + item.model+ " |  Mass: " + item.mass + " | Status: " + item.status);
                    //Console.WriteLine(item.payload.description);
                    //Console.WriteLine("..........");

                    IPAddress _ipAddress = IPAddress.Parse(item.telemetry.host);
                    int _port = int.Parse(item.telemetry.port);

                    //TelemetryTCPModel telemetryModel = new TelemetryTCPModel();
                    //telemetryModel.ip = item.telemetry.host;
                    //telemetryModel.port = _port;

                    WaitCallback waitCallback = new WaitCallback(DoWork);
                    ThreadPool.QueueUserWorkItem(waitCallback, item);
                    //ThreadPool.QueueUserWorkItem(waitCallback, telemetryModel);                    
                }
                Console.WriteLine("-----------------------------------------------------");                
            }
        }


        static void Main(string[] args)
        {
            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine("SERVER Started.. Lütfen bekleyin");
            Console.WriteLine("-----------------------------------------------------");            
            Console.WriteLine("");
            Thread.Sleep(5000); // 5 sn icinde baslat.. 
            MainAsync();
            Console.ReadLine();
        }
    }
}
