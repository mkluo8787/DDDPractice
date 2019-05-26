
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace CRM.Apps {

    public interface ICrmApp {
        
        JObject Authenticate(JObject userData);
        bool IsLoggedIn();
        JObject GetServices();
    }
}