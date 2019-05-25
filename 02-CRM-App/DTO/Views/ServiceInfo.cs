using Newtonsoft.Json.Linq;

namespace CRM.Apps.Views {

    public class ServiceInfo : View<Domain.Service> {
        // TODO: Enum
        public string? id;
        public string? group;
        public string? name;

        public ServiceInfo() {}
        public ServiceInfo(Domain.Service entity) : base(entity)
        {
            throw new System.NotImplementedException();
        }
    }
}