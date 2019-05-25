
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace CRM.Apps {

    public interface ICrmApp {
        
        bool Authenticate(JObject userData);
        bool IsLoggedIn();
        JObject GetServices();
    }
}