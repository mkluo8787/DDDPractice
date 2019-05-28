using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace CRM.Apps {

    public interface ICrmApp {

        IAppOutput Authenticate(JObject userData);
        bool IsLoggedIn();
        IAppOutput GetServices();
    }

    public enum OutputType {
        LoginInfo,
        ServiceInfo
    }

    public interface IAppOutput {
        OutputType Type { get; }
        object Payload { get; }
        JObject ToJson() => JObject.FromObject(new {
            Type = Type.ToString(),
            Payload
        });
    }
}