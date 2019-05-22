using System;
using System.Collections.Generic;
using System.Linq;

namespace CRM.Domain {

    public interface ICrmContext {
        IEnumerable<Order> Orders { get; }
    }

    public class CrmContext : ICrmContext {

        readonly DAL.ICrmRepo repo;

        public CrmContext(DAL.ICrmRepo repo) {
            this.repo = repo;
        }

        // readonly List<Order> orders;
        // public IEnumerable<IOrder> Orders => repo;
        public IEnumerable<Order> Orders => throw new NotImplementedException();
    }
}