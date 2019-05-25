using System.Collections.Generic;
using System.Linq;

// NOTE: Here is the paradise of pure Business/Domain logic!
// Keep in mind that no App/DAL logic belongs here.
// Remember to factor out repeatitive functionality into separate class/files.

namespace CRM.Core {
    using System;
    using Domain;

    public interface ICrmCore {
        Id? Authenticate(Username username, Password password);
        Id? AuthenticateLegacy(Username username, Password password);
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

        public Id? Authenticate(Username username, Password password) {
            var user = UserContext.QueryAll().Where(
                (User user) => username.Equals(user["Username"])
            ).Single();

            var salt = user["PasswordSalt"] as ByteArray ??
                throw new InvalidSaltException();
            if (!PasswordHash.Hash(password, salt).Equals(user["PasswordHash"]))
                return null;

            return user["Id"] as Id;
        }

        public Id? AuthenticateLegacy(Username username, Password password) {
            var user = LegacyUserContext.QueryAll().Where(
                (LegacyUser user) => username.Equals(user["Username"])
            ).Single();

            if (!(user["Password"] is Password pwd))
                return null;
            if (!password.Equals(pwd))
                return null;

            return user["Id"] as Id;
        }

        public class InvalidSaltException : System.Exception { }
        // public class DuplicatedUsernameException : System.Exception { }
    }
}