using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class EntityInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
    }

    public class EntityOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<Entity> entity_type { get; set; }
    }

    public class Entity
    {
        public string id { get; set; }
        public string name { get; set; }

    }
}

