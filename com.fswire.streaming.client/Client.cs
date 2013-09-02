using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.fswire.streaming.api;
using System.Net;
using Newtonsoft.Json;
using com.fswire.streaming.api.rest;

namespace com.fswire
{
    public class Client
    {
        static Pusher _pusher;
        Boolean _isAuthenticated = false;
        String _userId;
        String _userEmail;
        String _password;
        String _authToken;
        Streams _streams;
        public Streams Streams { get { return _streams; } }
        public String AuthToken { get { return _authToken; } }
        public Pusher StreamingConnection { get { return _pusher; } }
        public Boolean IsAuthenticated { get { return _isAuthenticated; } }

        public Client()
        {
            _streams = new Streams(this);
        }
        void Connect()
        {
            string authorize_url = String.Format("http://{0}/api/authenticate?user_id={1}&auth_token={2}", Settings.Default.StreamingHostName, _userId, _authToken);

            _pusher = new Pusher("2c33406e0172ac95cecd", new PusherOptions()
            {
                Authorizer = new HttpAuthorizer(authorize_url)
            });
        }
        public Boolean Authenticate(String apiKey)
        {
            throw new NotImplementedException();

            return _isAuthenticated;
        }
        public Boolean Authenticate(String userId, String password)
        {
            string result = PostTest(String.Format("http://{0}/api/users/sign_in.json?user_login[email]={1}&user_login[password]={2}", Settings.Default.StreamingHostName, userId, password), String.Empty);
            var template = new {success = Boolean.FalseString, id = String.Empty, auth_token = String.Empty, email = String.Empty, is_admin = Boolean.FalseString, is_cleaner = Boolean.FalseString };
            var authenticationData = JsonConvert.DeserializeAnonymousType(result, template);
            _isAuthenticated = Boolean.Parse(authenticationData.success);
            _userId = authenticationData.id;
            _userEmail = authenticationData.email;
            _authToken = authenticationData.auth_token;
            _password = password;
            if (_isAuthenticated) 
                Connect();

            return _isAuthenticated;
         }
        private string PostTest(string URI, string Parameters)
        {
            string data;
            using (var webClient = new System.Net.WebClient())
            {
                webClient.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                data = webClient.UploadString(URI, "POST", Parameters);
            }

            return data;
        }
    }
}
