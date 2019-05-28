using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CRM.DAL {

    public interface IRepository<TD> {
        IEnumerable<TD> Query(Expression<Func<TD, bool>> criteria);
    }
}
