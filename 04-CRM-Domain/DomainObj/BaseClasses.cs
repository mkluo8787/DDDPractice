using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CRM.Domain {

    public abstract class ValueType {

        public new virtual bool Equals(object obj) =>
            !(obj == null || GetType() != obj.GetType());

        public new virtual int GetHashCode() =>
            base.GetHashCode();

        public class InvalidValueException : System.Exception { }
    }

    public abstract class Entity {

        protected Dictionary<string, ValueType> values =
            new Dictionary<string, ValueType> { };

        public virtual ValueType this [string key] => values[key];

        public virtual List<string> GetEntries() =>
            values.Keys.ToList();
    }
}