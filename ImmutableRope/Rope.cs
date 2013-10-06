using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImmutableRope
{
    using Unicode;

    public class Rope : IEquatable<Rope>, IEnumerable<TaggedCodePoint>
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

        /// <summary>
        /// TODO: Optimize!
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return _content.ToString().GetHashCode();
        }

        public override bool Equals(object other)
        {
            if (other is Rope)
                return Equals(other as Rope);

            return false;
        }

        /// <summary>
        /// TODO: Optimize!
        /// </summary>
        /// <returns></returns>
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

        #region Implements IEnumerable<CodePointList>
        public IEnumerator<TaggedCodePoint> GetEnumerator()
        {
            return _content.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}
