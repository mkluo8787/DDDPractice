using System.Collections.Generic;
using System.Linq;

// NOTE: Here is the paradise of pure Business/Domain logic!
// Keep in mind that no App/DAL logic belongs here.
// Remember to factor out repeatitive functionality into separate class/files.

namespace CRM.Core {
    using System;
    using Domain;

    public interface ICrmCore {
        Name? Authenticate(Username username, Password password);
        Name? AuthenticateLegacy(Username username, Password password);
    }
    public class CrmCore : ICrmCore {

        // TODO: More Contexts to be added. (e.g. Order, Customer, ... )
        IReadContext<User> UserContext { get; }
        IReadContext<LegacyUser> LegacyUserContext { get; }

        public CrmCore(
            IReadContext<User> userContext,
            IReadContext<LegacyUser> legacyUserContext) {
            this.UserContext = userContext;
            this.LegacyUserContext = legacyUserContext;
        }

        public Name? Authenticate(Username username, Password password) {
            // var user = UserContext.Query(
            //     (User user) =>
            //     username.Equals(user["Username"])
            // ).Single();

            // if (!(user["PasswordSalt"] is ByteArray salt))
            //     throw new InvalidSaltException();
            // if (!PasswordHash.Hash(password, salt).Equals(user["PasswordHash"]))
            //     return null;

            // return user["Name"] as Name;
            throw new System.NotImplementedException();
        }

        public Name? AuthenticateLegacy(Username username, Password password) {
            // var user = LegacyUserContext.Query(
            //     (LegacyUser user) =>
            //     username.Equals(user["Username"])
            // ).Single();

            // TODO: Operator ==

            var user = LegacyUserContext.QueryExpr(
                user => user["Username"] == username
            ).Single();

            if (!password.Equals(user["Password"]))
                return null;

            return user["Name"] as Name;
        }

        public class InvalidSaltException : System.Exception { }
        // public class DuplicatedUsernameException : System.Exception { }
    }
}