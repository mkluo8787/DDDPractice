using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CRM.Domain {

    // FIXME: Cluttering interfaces and implemetations here!
    // FIXME: Remember to organize after testing!    

    public class ReadContext<T, TD> : IReadContext<T> where T : Entity {

        DAL.IRepository<TD> Repo { get; }
        DAL.IReadMapper<T, TD> Mapper { get; }

        public ReadContext(
            DAL.IRepository<TD> repo,
            DAL.IReadMapper<T, TD> mapper) {
            Repo = repo;
            Mapper = mapper;
        }

        // public IEnumerable<T> Query(Func<T, bool> criteria) {
        //     return Repo.Query(td => criteria(Mapper.Map(td)))
        //         .Select(td => Mapper.Map(td));
        // }

        public IEnumerable<T> QueryExpr(Expression<Func<T, bool>> criteria) {

            ParameterExpression td = Expression.Parameter(typeof(TD));

            var visitor = new DAL.DomainExprBdyToOrmExprBdyVisitor<T, TD>(Mapper, td);
            var transformedBody = visitor.Visit(criteria.Body);

            Expression<Func<TD, bool>> transformedCriteria =
                Expression.Lambda<Func<TD, bool>>(
                    transformedBody,
                    new List<ParameterExpression> { td }
                );
                
            return Repo.Query(transformedCriteria)
                .Select(td => Mapper.Map(td));
        }
    }
}