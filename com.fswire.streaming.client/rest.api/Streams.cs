using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace com.fswire.streaming.api.rest
{
    public class Streams
    {
        private Client _client;
        private List<Stream> _allStreams;

        public Streams(Client client)
        {
            _client = client;
        }
        public List<Stream> AllStreams
        { 
            get 
            {
                if (null == _allStreams || _allStreams.Count == 0) _downloadAllStreams();  
                return _allStreams; 
            } 
        }

        private void _downloadAllStreams() 
        {
            byte[] result;
            using (var webClient = new System.Net.WebClient())
            {
                
                result = webClient.DownloadData(String.Format("http://{0}/{1}/streams.json?auth_token={2}", 
                                                        Settings.Default.StreamingHostName, 
                                                        Settings.Default.ApiRoot, _client.AuthToken));
            }


            string data = System.Text.Encoding.UTF8.GetString(result);
            var template = new { disclaimer = String.Empty, license = String.Empty, timestamp = String.Empty, response = new List<Stream>() };
            var dataAsObj = JsonConvert.DeserializeAnonymousType(data, template);

            _allStreams = dataAsObj.response;

            foreach( Stream s in _allStreams)
            {
                s.client = this._client;
            }
        }

    }
    public class Stream
    {
        private StreamEx _chat;

        internal Client client {get; set;}
        public string id { get; set; }
        public string name { get; set; }
        public string ticker { get; set; }
        
        public string stream_name 
        { 
            get 
            {
                if (null == _chat)
                {
                    using (var webClient = new System.Net.WebClient())
                    {
                        byte[] result = webClient.DownloadData(String.Format("http://{0}/{1}/streams/{3}.json?auth_token={2}",
                                                                Settings.Default.StreamingHostName,
                                                                Settings.Default.ApiRoot,
                                                                this.client.AuthToken,
                                                                this.id));

                        string data = System.Text.Encoding.UTF8.GetString(result);
                        var template = new { chat = new StreamEx() };
                        var dataAsObj = JsonConvert.DeserializeAnonymousType(data, template);
                        _chat = dataAsObj.chat;

                    }
                }
                return _chat.channel_presence_name;
            } 
        }
        private Channel _streamChannel;

        public Channel Subscribe()
        {
            _streamChannel = client.StreamingConnection.Subscribe(this.stream_name);
            return _streamChannel;
          
        }
        public void UnSubscribe()
        {
            if ( null != _streamChannel)
                _streamChannel.Unsubscribe();

        }
        private class StreamEx
        {
            public string channel_presence_name { get; set; }
        }
    }
}
