using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Domain {

    public class User : Entity {

        public User(
            Id id,
            Username username,
            PasswordHash passwordHash,
            ByteArray passwordSalt
        ) {
            values = new Dictionary<string, ValueType> { 
                { "Id", id },
                { "Username", username },
                { "PasswordHash", passwordHash },
                { "PasswordSalt", passwordSalt }
            };
        }
    }

    public class LegacyUser : Entity {

        public LegacyUser(
            Id id,
            Username username,
            Password password
        ) {
            values = new Dictionary<string, ValueType> { 
                { "Id", id },
                { "Username", username },
                { "Password", password }
            };
        }
    }
}