using Newtonsoft.Json.Linq;

namespace CRM.Apps.Views {

    public struct ServiceInfo : IView<Domain.ServiceInfo> {
        public string id;
        public string group;
        public string name;

        public void Decompose(Domain.ServiceInfo entity)
        {
            throw new System.NotImplementedException();
        }

        public JObject Serialize()
        {
            throw new System.NotImplementedException();
        }
    }
}