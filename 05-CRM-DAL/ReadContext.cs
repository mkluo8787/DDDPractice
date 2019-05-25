using System;
using System.Collections.Generic;
using System.Linq;

namespace CRM.Domain {

    // FIXME: Cluttering interfaces and implemetations here!
    // FIXME: Remember to organize after testing!    

    public class ReadContext<T, TD> : IReadContext<T> where T : Entity {

        DAL.IRepository<TD> Repo { get; }
        DAL.IReadMapper<T, TD> Mapper { get; }

        public ReadContext(DAL.IRepository<TD> repo, DAL.IReadMapper<T, TD> mapper) {
            Repo = repo;
            Mapper = mapper;
        }

        public IEnumerable<T> QueryAll() {
            return Repo.All().Select(td => Mapper.Map(td));
        }
    }
}
