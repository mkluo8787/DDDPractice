using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.AspNetCore.Http;

namespace CRM.Apps {

    public interface ICrmSession {
        string? TimeCreated { get; set; }
        string? UserId { get; set; }
        void Clear();
    }

    public class CrmSession : ICrmSession {
        static readonly string timeKey = "session.time";
        static readonly string userKey = "session.user";

        readonly IHttpContextAccessor httpContextAccessor;

        public CrmSession(IHttpContextAccessor httpContextAccessor) =>
            this.httpContextAccessor = httpContextAccessor;

        ISession Session =>
            httpContextAccessor.HttpContext.Session;

        public string? TimeCreated {
            get => Session.GetString(timeKey);
            set => Session.SetString(timeKey, value);
        }

        public string? UserId {
            get => Session.GetString(userKey);
            set => Session.SetString(userKey, value);
        }

        // public Domain.User User {
        //     get => Deserialize<Domain.User>(Session.Get(userKey));
        //     set => Session.Set(userKey, Serialize(value));
        // }

        public void Clear() {
            Session.Clear();
        }

        // byte[] Serialize(object obj) {
        //     BinaryFormatter bf = new BinaryFormatter();
        //     using MemoryStream ms = new MemoryStream();
        //     bf.Serialize(ms, obj);
        //     return ms.ToArray();            
        // }

        // T Deserialize<T>(byte[] byteArray) {
        //     BinaryFormatter bf = new BinaryFormatter();
        //     using MemoryStream ms = new MemoryStream(byteArray);
        //     return (T)bf.Deserialize(ms);           
        // }
    }
}