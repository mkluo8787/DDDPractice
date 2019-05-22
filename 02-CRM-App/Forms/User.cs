using Newtonsoft.Json.Linq;

namespace CRM.Apps.Forms {

    public struct User : IForm<Domain.User> {
        public string id;

        public User(string id) {
            this.id = id;
        }        

        public void Deserialize(JObject json) {
            
        }

        public Domain.User Compose()
        {
            throw new System.NotImplementedException();
        }        
    }
}