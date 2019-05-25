namespace CRM.Domain {

    public class Id : ValueType {

        public int Value { get; }

        public Id(int? id) {
            this.Value = id ??
                throw new InvalidValueException();
        }

        public override bool Equals(object obj) {
            if (!base.Equals(obj))
                return false;
            else
                return Value == (obj as Id)?.Value;
        }

        public override int GetHashCode() =>
            Value.GetHashCode();

        public override string ToString() => Value.ToString();
    }
}