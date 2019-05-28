using System;
using Newtonsoft.Json.Linq;

namespace CRM.Apps.Forms {

    public class User : Form<object> {

        public enum Type {
            Default,
            Legacy
        }

        public string? username;
        public string? password;
        public Type type;

        public User() { }
        public User(JObject json) : base(json) {
            username = json["username"]?.ToString();
            password = json["password"]?.ToString();
            type = json["legacy"]?.ToString() == "true" ? Type.Legacy : Type.Default;
        }
    }
}
