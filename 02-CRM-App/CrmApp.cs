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

        public IAppOutput Authenticate(JObject userData) {
            try {
                var user = new Forms.User(userData);
                Domain.Name? name = null;
                switch (user.type) {
                    case Forms.User.Type.Default:
                        name = core.Authenticate(
                            new Domain.Username(user.username),
                            new Domain.Password(user.password)
                        ); break;
                    case Forms.User.Type.Legacy:
                        name = core.AuthenticateLegacy(
                            new Domain.Username(user.username),
                            new Domain.Password(user.password)
                        ); break;
                }

                if (name != null) {
                    session.Username = user.username;
                    session.TimeCreated = DateTime.Now.ToString();
                    return new Views.LoginInfo($"Welcome to CRM 2019, {name.Str}.");
                } else {
                    session.Clear();
                    return new Views.LoginInfo("Login failed!");
                }
            } catch {
                session.Clear();
                return new Views.LoginInfo("Exception encountered during login!");
            }
        }

        public bool IsLoggedIn() => session.Username != null;

        public IAppOutput GetServices() {
            var userId = session.Username;
            return new Views.LoginInfo("...");
        }

        public IAppOutput GetServiceInfo(JObject serviceCode) {
            var code = new Forms.ServiceCode(serviceCode);
            return new Views.LoginInfo("...");
        }
    }
}