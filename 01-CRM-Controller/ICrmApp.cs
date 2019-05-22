
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace CRM.Apps {

    public interface ICrmApp {
        // string GetUserId(JObject userData);
        bool Authenticate(JObject userData);
        JArray GetServices(string userId);
    }
}