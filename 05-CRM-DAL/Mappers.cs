// FIXME: Cluttering interfaces and implemetations here!
// FIXME: Remember to organize after testing!

namespace CRM.DAL {
    using System.Linq.Expressions;
    using System;
    using Domain;

    public interface IReadMapper<T, TD> where T : Domain.Entity {
        T Map(TD data);
        Expression MapExpr(ParameterExpression td, string key);
    }

    public class UserMapper : IReadMapper<Domain.User, USER> {
        public Domain.User Map(USER data) {
            throw new System.NotImplementedException();
        }
        public Func<ParameterExpression, MemberExpression> MapExp(string tProp) {
            throw new System.NotImplementedException();
        }

        public Expression MapExpr(ParameterExpression td, string key) {
            throw new NotImplementedException();
        }
    }

    public class LegacyUserMapper : IReadMapper<Domain.LegacyUser, BWApuser> {
        public Domain.LegacyUser Map(BWApuser data) {
            return new Domain.LegacyUser(
                new Id(data.Apuserid),
                new Username(data.Apuserlogin),
                new Password(data.Apuserpassword),
                new Name(data.Apusername)
            );
        }

        public Func<ParameterExpression, MemberExpression> MapExp(string tProp) {
            // Expression<Func<BWApuser, TDpropType>> exp =
            //     (BWApuser user) => user.Apuserlogin as TDpropType;
            switch (tProp) {
                case "Id":
                    return pe => Expression.PropertyOrField(pe, "Apuserid");
                default:
                    throw new ArgumentException();
            }
        }

        public Expression MapExpr(ParameterExpression td, string key) {
            switch (key) {
                case "Username":
                    return Expression.MakeMemberAccess(
                        td, typeof(BWApuser).GetMember("Apuserlogin")[0]
                    );
                default:
                    // TODO:
                    throw new NotImplementedException();
            }
        }
    }

    class DomainExprBdyToOrmExprBdyVisitor<T, TD> : ExpressionVisitor where T : Domain.Entity {
        IReadMapper<T, TD> Mapper { get; }
        ParameterExpression Td { get; }

        public DomainExprBdyToOrmExprBdyVisitor(IReadMapper<T, TD> mapper, ParameterExpression td) {
            this.Mapper = mapper;
            this.Td = td;
        }

        // TODO: Must do massive refactor latter!

        protected override Expression VisitBinary(BinaryExpression node) {
            if ((node.Left is MethodCallExpression call) &&
                (call.Object.Type.Name == typeof(T).Name) &&
                (call.Method.Name == "get_Item") &&
                (call.Arguments.Count == 1) &&
                (call.Arguments[0] is ConstantExpression arg) &&
                (arg.Value is string key))
                return Expression.MakeBinary(
                    node.NodeType,
                    Mapper.MapExpr(Td, key),
                    Expression.MakeMemberAccess(
                        node.Right, node.Right.Type.GetMember("Value")[0]
                    )
                );
            //Mapper.MapExpr(key);

            return base.VisitBinary(node);
        }
    }
}