using System.Collections.Generic;
using System.Linq;

namespace CRM.Domain {

    public class String : ValueType, IValue<string> {

        public string Str { get; }

        public String(string? str) {
            this.Str = str ??
                throw new InvalidValueException();
        }

        public virtual string Value => Str;

        public override bool Equals(object obj) {
            if (!base.Equals(obj))
                return false;
            else
                return Str == (obj as String)?.Str;
        }

        public override int GetHashCode() => Str.GetHashCode();
    }

    public class Name : String {
        public Name(string? str) : base(str) { }
    }

    public class CompactString : String {
        public CompactString(string? str) : base(str) { }
    }

    public class Username : CompactString {
        public Username(string? str) : base(str) { }
    }

    public class Password : CompactString {
        public Password(string? str) : base(str) { }
    }
}