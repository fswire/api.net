using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using com.fswire;
using com.fswire.streaming.api;
using System.Net;
using System.Collections.Specialized;
using ExampleClient.Properties;
namespace ExampleClient
{
    class Program
    {
        static Channel _streamChannel = null;
        static Client _fswireClient = null;

        static void Main(string[] args)
        {
            String email = String.Empty;
            String password = String.Empty;
            while (email.Length == 0 || password.Length == 0)
            {
                Console.Write("Pleae enter email address: ");
                email = Console.ReadLine();
                Console.Write("Pleae enter password: ");
                password = Console.ReadLine();
            }

            _fswireClient = new com.fswire.Client();
            if (_fswireClient.Authenticate(email, password))
            {
                
                _fswireClient.StreamingConnection.Connected += _fswireClient_Connected;
                _fswireClient.StreamingConnection.ConnectionStateChanged += _fswireClient_ConnectionStateChanged;
                _fswireClient.StreamingConnection.Connect();


                // Read input in loop
                string line;

                do
                {
                    line = Console.ReadLine();

                    if (line == "quit")
                        break;
                    

                } while (line != null);

                _fswireClient.StreamingConnection.Disconnect();
            }
        }
        static void _fswireClient_ConnectionStateChanged(object sender, ConnectionState state)
        {
            Console.WriteLine("Connection state: " + state.ToString());
        }

        static void _fswireClient_Connected(object sender)
        {
            // Setup private channel
            try
            {
                //_streamChannel = _fswireClient.Streams.AllStreams.First(s => s.id.Equals("1") ).Subscribe();
                _streamChannel = _fswireClient.Streams.Firehose.Subscribe();
                _streamChannel.Subscribed += _streamChannelSubscribed;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static void _streamChannelSubscribed(object sender)
        {

             _streamChannel.Bind("send_message", (dynamic data) =>
            {
                Console.WriteLine(data.message);
            });

             Console.WriteLine("Connected to Stream");
        }
    }
}
