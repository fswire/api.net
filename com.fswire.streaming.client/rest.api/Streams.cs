using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using com.fswire.rest.api;

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
                if (null == _allStreams || _allStreams.Count == 0)
                {
                    _allStreams = com.fswire.rest.api.wrapper.Stream.AllStreams(this._client);
                }
                return _allStreams; 
            } 
        }
    }    
}
