using System;
using System.Collections.Generic;

namespace ImmutableRope.Unicode
{
    public static class UnicodeEx
    {
        public static IEnumerable<TaggedCodePoint> GetCodePointEnumerator(this string @this)
        {
            return (UnicodeEnumerator) @this;
        }
    }
}