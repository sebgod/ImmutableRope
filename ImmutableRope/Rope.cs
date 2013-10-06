using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImmutableRope
{
    using Unicode;

    public class Rope : IEquatable<Rope>
    {
        private readonly Rope _left;
        private readonly Rope _right;
        private readonly byte _depth;
        private readonly CodePointList _content;

        public Rope(string content)
            : this(new CodePointList(content))
        {
            // calling private Rope(CodePointList content)
        }

        public Rope(CodePointList content)
        {
            _content = content;
        }

        public static implicit operator Rope(string content) {
            return new Rope(content);
        }

        public static implicit operator string(Rope rope)
        {
            return rope.ToString();
        }

        public override string ToString()
        {
            return _content.ToString();
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object other)
        {
            if (other is Rope)
                return Equals(other as Rope);

            return false;
        }

        public bool Equals(Rope other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (ReferenceEquals(null, other)) return false;

            return this.ToString() == other.ToString();
        }

        #region relational operator overloading
        public static bool operator ==(Rope a, Rope b)
        {
            return !object.ReferenceEquals(a, null) && a.Equals(b);
        }

        public static bool operator !=(Rope a, Rope b)
        {
            return object.ReferenceEquals(a, null) || !a.Equals(b);
        }
        #endregion
    }
}
