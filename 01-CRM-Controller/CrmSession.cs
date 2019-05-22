using Microsoft.AspNetCore.Http;

namespace CRM {

    public interface ICrmSession {
        string TimeCreated { get; set; }
        string UserId { get; set; }
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

        public string TimeCreated {
            get => Session.GetString(timeKey);
            set => Session.SetString(timeKey, value);
        }

        public string UserId {
            get => Session.GetString(userKey);
            set => Session.SetString(userKey, value);
        }

        public void Clear()
        {
            Session.Clear();
        }
    }
}