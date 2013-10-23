using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace com.fswire.rest.api.wrapper
{
        internal static class Stream
        {
            public static List<Message> MessageHistory(com.fswire.rest.api.Stream stream, string lastMessageId, bool onlyMessagesWithUrls)
            {
                byte[] result;
                using (var webClient = new System.Net.WebClient())
                {

                    result = webClient.DownloadData(String.Format("http://{0}/{1}/streams/{2}/history.json?auth_token={3}",
                                                            Settings.Default.StreamingHostName,
                                                            Settings.Default.ApiRoot, 
                                                            stream.id, stream.client.AuthToken));
                }


                string data = System.Text.Encoding.UTF8.GetString(result);
                var template = new { disclaimer = String.Empty, license = String.Empty, timestamp = String.Empty, response = new ResponseMessagesObject() };
                var dataAsObj = JsonConvert.DeserializeAnonymousType(data, template);
                return dataAsObj.response.messages;
            }
            public static List<com.fswire.rest.api.Stream> AllStreams(Client client)
            {
                byte[] result;
                using (var webClient = new System.Net.WebClient())
                {

                    result = webClient.DownloadData(String.Format("http://{0}/{1}/streams.json?auth_token={2}",
                                                            Settings.Default.StreamingHostName,
                                                            Settings.Default.ApiRoot, client.AuthToken));
                }


                string data = System.Text.Encoding.UTF8.GetString(result);
                var template = new { disclaimer = String.Empty, license = String.Empty, timestamp = String.Empty, response = new ResponseStreamsObject() };
                var dataAsObj = JsonConvert.DeserializeAnonymousType(data, template);

                List<com.fswire.rest.api.Stream> allStreams = dataAsObj.response.streams;

                foreach (com.fswire.rest.api.Stream s in allStreams)
                {
                    s.client = client;
                }
                return allStreams;
            }
            private static dynamic buildTemplate(object responseObjectTemplate)
            {
               return new { disclaimer = String.Empty, license = String.Empty, timestamp = String.Empty, response = responseObjectTemplate };
            }
        }

        internal class ResponseStreamsObject
        {
            public List<com.fswire.rest.api.Stream> streams { get; set; }
        }
        internal class ResponseMessagesObject
        {
            public List<Message> messages { get; set; }
        }
}
