using System.Collections.Generic;
using System.Linq;

namespace CRM.DAL {

    public interface IRepository<TD> {
        IEnumerable<TD> All();
    }
}