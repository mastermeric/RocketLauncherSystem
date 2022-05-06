using ConsoleMultiThread.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMultiThread
{
    public class TCPManager
    {
        private string _ip { get; set; }
        private int _port { get; set; }
        RocketModel _model;
        public TCPManager(RocketModel model)
        {
            _model = model;
            _ip = model.telemetry.host;
            _port = Convert.ToInt32(model.telemetry.port);
        }

        public TCPManager()
        {
            
        }

        public async void StartContinuousRealtimePush()
        {            
            try
            {

                IPAddress localAddress = IPAddress.Parse(_ip);
                TcpListener listener = new TcpListener(localAddress, _port);
                Console.WriteLine(_ip + ":" + _port + " -> TCP Soket Baslatildi. OK.");
                listener.Start();

                PushRealtimeData(_ip + ":" + _port, "Model: " + _model.model + " |  Mass: " + _model.mass + " | Status: " + _model.status);

                while (true)
                {
                    // Accept incoming connection with IP/Port 
                    TcpClient client = await listener.AcceptTcpClientAsync();

                    if (client.Connected)
                    {
                        // Get the stream of data send by the Rocket and create a buffer of data we can read
                        NetworkStream stream = client.GetStream();
                        byte[] buffer = new byte[client.ReceiveBufferSize];

                        int bytesRead = stream.Read(buffer, 0, client.ReceiveBufferSize);
                        string data = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                        Console.WriteLine("Recieved Data: " + data);                        
                        
                        try
                        {
                            PushRealtimeData(_ip + ":" + _port, data);                            
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        
                        //---------------------------------------------------------------
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\n Press any key to continue...");
            Console.ReadKey();
        }


        public async void PushRealtimeData(string sender, string data)
        {
            HubConnection connection;
            try
            {            

                connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:44990/NotificationHub")
                .Build();

                //------------  SignalR startup  -------------------------------------------------
                //SignalR retry lojigi..
                connection.Closed += async (error) =>
                {
                    await Task.Delay(new Random().Next(0, 5) * 1000);
                    await connection.StartAsync();
                };

                //START SignalR 
                try
                {
                    await connection.StartAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("SignalR ERROR :" + ex.Message);
                }

                // SignalR ile Push (TEST islemi) -------------------------------                 
                try
                {
                    await Task.Run(() =>
                    {
                        connection.InvokeAsync("SendMessageToAll", sender , data);
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                //---------------------------------------------------------------                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }            
        }

    }
}
