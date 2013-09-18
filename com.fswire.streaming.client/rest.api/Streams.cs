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
        private Stream _firehose;
        public Streams(Client client)
        {
            _client = client;
            _firehose = new Stream();
            _firehose.client = client;
            _firehose.id = "0";
            _firehose.name = "Firehose";
            _firehose.stream_name = "presence-firehose";
            _firehose.level = 0;
        }
        public Stream Firehose {
            get { return _firehose; } 
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
            var template = new { disclaimer = String.Empty, license = String.Empty, timestamp = String.Empty, response = new ResponseStreamsObject() };
            var dataAsObj = JsonConvert.DeserializeAnonymousType(data, template);

            _allStreams = dataAsObj.response.streams;

            foreach( Stream s in _allStreams)
            {
                s.client = this._client;
            }
        }

    }
    public class ResponseStreamsObject
    {
        public List<Stream> streams { get; set; }
    }
    public class Stream
    {
        internal Client client {get; set;}
        public string id { get; set; }
        public string name { get; set; }
        public string ticker { get; set; }
        public string stream_name { get; set; }
        public int level { get; set; }
        public Stream parent {get; set;}
        
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

    }
}
