using Newtonsoft.Json.Linq;

namespace CRM.Apps.Views {

    public class LoginInfo : IAppOutput {

        public OutputType Type => OutputType.LoginInfo;
        public object Payload => new { Messege };

        public string? Messege { get; }
        public LoginInfo(string messege) {
            this.Messege = messege;
        }
    }
}