using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace CRM.Apps {

    public class CrmApp : ICrmApp {

        readonly ICrmSession session;
        readonly Core.ICrmCore core;

        public CrmApp(ICrmSession session, Core.ICrmCore core) {
            this.session = session;
            this.core = core;
        }

        public bool Authenticate(JObject userData) {
            try {
                var user = new Forms.User(userData);
                Domain.Id? userId;
                if (!user.legacy)
                    userId = core.Authenticate(
                        new Domain.Username(user.username),
                        new Domain.Password(user.password)
                    );
                else
                    userId = core.AuthenticateLegacy(
                        new Domain.Username(user.username),
                        new Domain.Password(user.password)
                    );

                if (userId != null) {
                    session.UserId = userId.ToString();
                    session.TimeCreated = DateTime.Now.ToString();
                    return true;
                } else {
                    session.Clear();
                    return false;
                }
            } catch {
                session.Clear();
                return false;
            }
        }

        public bool IsLoggedIn() => session.UserId != null;

        public JObject GetServices() {
            var userId = session.UserId;
            return JObject.Parse("{}");
        }

        // // TODO: Mock!
        // List<Views.ServiceInfo> serviceInfos =
        //     new List<Views.ServiceInfo> {
        //         new Views.ServiceInfo { id = "200", group = "B", name = "測試功能01" },
        //         new Views.ServiceInfo { id = "201", group = "B", name = "測試功能02" },
        //         new Views.ServiceInfo { id = "203", group = "B", name = "測試功能033" }
        //     };

        public JObject GetServiceInfo(JObject serviceCode) {
            var code = new Forms.ServiceCode(serviceCode);
            return JObject.Parse("{}");
        }
    }
}