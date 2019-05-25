// FIXME: Cluttering interfaces and implemetations here!
// FIXME: Remember to organize after testing!

namespace CRM.DAL {
    using Domain;

    public interface IReadMapper<T, TD> where T : Domain.Entity {
        T Map(TD data);
    }

    public class UserMapper : IReadMapper<Domain.User, USER> {
        public Domain.User Map(USER data) {
            throw new System.NotImplementedException();
        }
    }

    public class LegacyUserMapper : IReadMapper<Domain.LegacyUser, BWApuser> {
        public Domain.LegacyUser Map(BWApuser data) {
            return new Domain.LegacyUser(
                new Id(data.Apuserid),
                new Username(data.Apuserlogin),
                new Password(data.Apuserpassword)
            );
        }
    }
}