using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CRM.Domain {

    public class ByteArray : ValueType {

        readonly byte[] data;
        public byte[] Data => data.Clone() as byte[] ??
            throw new InvalidValueException();

        public ByteArray(byte[] ? data) {
            if (data == null)
                throw new InvalidValueException();
            this.data = data.Clone() as byte[] ??
                throw new InvalidValueException();
        }

        public override bool Equals(object obj) {
            if (!base.Equals(obj))
                return false;
            else
                return data.SequenceEqual((obj as ByteArray)?.Data);
        }

        public override int GetHashCode() =>
            data.GetHashCode();
    }

    public class PasswordHash : ByteArray {
        public PasswordHash(byte[] ? data) : base(data) { }

        public static PasswordHash Hash(Password password, ByteArray salt) {
            // TODO:
            var hmac = new HMACSHA512(salt.Data);
            return new PasswordHash(
                hmac.ComputeHash(
                    Encoding.ASCII.GetBytes(password.Str)));
        }
    }
}