using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CRM.Domain {

    public interface IReadContext<T> where T : Entity {
        // IEnumerable<T> Query(Func<T, bool> criteria);
        IEnumerable<T> QueryExpr(Expression<Func<T, bool>> criteria);
    }
}