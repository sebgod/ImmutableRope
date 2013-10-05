using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImmutableRope
{
    public class Rope : IEquatable<Rope>
    {
        private readonly Rope _left;
        private readonly Rope _right;
        private readonly byte _depth;
        private readonly Unicode.TaggedCodePoint[] _content;

        private Rope(string content)
        {

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
            return "";
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        
        public bool Equals(Rope other)
        {
            return true;
        }
    }
}
