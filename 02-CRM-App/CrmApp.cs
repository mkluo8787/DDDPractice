using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace CRM.Apps {

    using Forms;
    using Views;

    public class CrmApp : ICrmApp {

        readonly ICrmSession session;
        readonly Services.CrmService sercice;

        public CrmApp(ICrmSession session, Domain.ICrmContext context) {
            this.session = session;
            this.sercice = new Services.CrmService(context);
        }

        public bool Authenticate(JObject userData) {
            // TODO: Forms.User(userData).username/password
            // var user = sercice.Authenticate();
            // TODO: Mock!

            if (userData.GetValue("username").ToString() == "MKLUO") {
                session.UserId = "1234";
                session.TimeCreated = DateTime.UtcNow.ToString();
                return true;
            } else {
                session.Clear();
                return false;
            }

        }

        public JArray GetServices(string userId) {
            User user = new User(userId);
            return JArray.Parse("[]");
        }

        // public DetailedOrderDto GetOrders() {        
        //     return new DetailedOrderDto(sercice.Orders.First());
        // }

    }
}