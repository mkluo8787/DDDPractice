using System.Collections.Generic;
using System.Linq;

namespace CRM.Domain {

    public class CompactString : ValueType {

        public string Str { get; }

        public CompactString(string? str) {
            this.Str = str ??
                throw new InvalidValueException();
        }

        public override bool Equals(object obj) {
            if (!base.Equals(obj))
                return false;
            else
                return Str == (obj as CompactString)?.Str;
        }

        public override int GetHashCode() =>
            Str.GetHashCode();
    }

    public class Username : CompactString {
        public Username(string? str) : base(str) { }
    }

    public class Password : CompactString {
        public Password(string? str) : base(str) { }
    }
}