using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.fswire.streaming.api;

namespace com.fswire.rest.api
{
    public class Stream
    {
        internal Client client { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string ticker { get; set; }
        public string stream_name { get; set; }
        public int level { get; set; }
        public Stream parent { get; set; }

        private Channel _streamChannel;

        public Channel Subscribe()
        {
            _streamChannel = client.StreamingConnection.Subscribe(this.stream_name);
            return _streamChannel;

        }
        public void UnSubscribe()
        {
            if (null != _streamChannel)
                _streamChannel.Unsubscribe();

        }

        public List<Message> MessageHistory(int beforeLastId)
        {
            return com.fswire.rest.api.wrapper.Stream.MessageHistory(this, beforeLastId.ToString(), false);
        }

    }
}
