using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CRM.Domain;
using NHibernate;
using NHibernate.Criterion;

namespace CRM.DAL {

    // TODO: Is repostory really needed?
    // If it's needed, will it downgrade the performance of LINQ in NHibernate?

    public class Repository<TD> : IRepository<TD> where TD : class {

        ISessionFactory Factory { get; }
        public Repository(ISessionFactory factory) {
            this.Factory = factory;
        }

        public IEnumerable<TD> Query(Expression<Func<TD, bool>> criteria) {
            using var session = Factory.OpenSession();
            var query = session.QueryOver<TD>().Where(criteria);
            // TODO: max?
            // if (query.RowCountInt64() > max)
            //     throw 
            return query.List();
        }
    }
}