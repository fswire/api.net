using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace com.fswire.streaming.api
{
    public class HttpAuthorizer: IAuthorizer
    {
        private Uri _authEndpoint;
        public HttpAuthorizer(string authEndpoint)
        {
            _authEndpoint = new Uri(authEndpoint);
        }

        public string Authorize(string channelName, string socketId)
        {
            string authToken = null;

            using (var webClient = new System.Net.WebClient())
            {
                webClient.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                authToken = webClient.UploadString( String.Format("{0}&channel_name={1}&socket_id={2}", _authEndpoint, channelName, socketId), "POST", "");
            }

            return authToken;
        }
    }
}
