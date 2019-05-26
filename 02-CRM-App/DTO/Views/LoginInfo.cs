using Newtonsoft.Json.Linq;

namespace CRM.Apps.Views {

    public class LoginInfo {
        public string? Messege { get; }

        public LoginInfo(string messege) {
            this.Messege = messege;
        }
    }
}