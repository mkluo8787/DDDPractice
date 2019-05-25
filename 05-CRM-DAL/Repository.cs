using System.Collections.Generic;
using System.Linq;
using CRM.Domain;
using NHibernate;
using NHibernate.Linq;

namespace CRM.DAL {

    // TODO: Is repostory really needed?
    // If it's needed, will it downgrade the performance of LINQ in NHibernate?

    public class Repository<TD> : IRepository<TD> {

        ISessionFactory Factory { get; }
        public Repository(ISessionFactory factory) {
            this.Factory = factory;
        }

        public IEnumerable<TD> All() {
            using var session = Factory.OpenSession();
            var query = session.Query<TD>();
            return query.ToList();
        }
    }
}
