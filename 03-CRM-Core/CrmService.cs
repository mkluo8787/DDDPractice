
using System.Collections.Generic;
using System.Linq;

namespace CRM.Services {

    // public interface ICrmService {
    //     // Domain.IOrder GetFirstOrder();
    // }

    // public class CrmService : ICrmService {
    public class CrmService {
        
        readonly Domain.ICrmContext context;

        public CrmService(Domain.ICrmContext context){
            this.context = context;
        }

        // TODO: Value type for username and password!
        // public Domain.User Authenticate(string username, string password) {
            
        // }

        // public Domain.IOrder GetOrderBy
    }
}