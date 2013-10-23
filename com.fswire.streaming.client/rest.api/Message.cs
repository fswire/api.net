using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.fswire.rest.api
{
    public class Message
    {
        public string id { get; set; }
        public string message { get; set; }
        public string source_message_id { get; set; }
        public string sentiment { get; set; }
        public string approved_domains { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public string is_favourite { get; set; }
        public User user { get; set; }
        public List<Url> urls {get; set;}
        public Chat chat { get; set; }

    }
    public class Chat
    {
        public string id { get; set; }
        public string ticker { get; set; }
        public string ticker_level { get; set; }
        public string parent_id { get; set; }
        public string channel { get; set; }

    }
}
