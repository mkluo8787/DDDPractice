using Newtonsoft.Json.Linq;

namespace CRM.Apps.Forms {

    public class ServiceCode : Form<object> {

        public string? id;

        public ServiceCode() {}
        public ServiceCode(JObject json) : base(json) {
            id = json["id"].ToString();
        }

        public override object Compose() => new object();
    }
}