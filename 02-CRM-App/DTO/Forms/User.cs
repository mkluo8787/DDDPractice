using System;
using Newtonsoft.Json.Linq;

namespace CRM.Apps.Forms {

    public class User : Form<object> {

        public string? username;
        public string? password;
        public bool legacy;

        public User() { }
        public User(JObject json) : base(json) {
            username = json["username"]?.ToString();
            password = json["password"]?.ToString();
            legacy = json["legacy"]?.ToString() == "true";
        }
    }
}
