using System.Collections.Generic;

namespace CRM.Domain {
    public interface IReadContext<T> where T : Entity {
        IEnumerable<T> QueryAll();
    }
}