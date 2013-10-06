using System;
using System.Collections.Generic;

namespace ImmutableRope.Unicode
{
    class UnicodeEnumerator : IEnumerable<TaggedCodePoint>
    {
        private readonly string _text;

        public UnicodeEnumerator(string text)
        {
            _text = text;
        }

        public IEnumerator<TaggedCodePoint> GetEnumerator()
        {
            var length = _text.Length;
            for (var i = 0; i < length; i++)
            {
                var current = _text[i];
                if (char.IsHighSurrogate(current))
                {
                    if (i + 1 == length)
                        throw new InvalidOperationException(string.Format("Premature end of string; last char was a high surrogate: {0:x}", current));

                    yield return char.ConvertToUtf32(current, _text[++i]);
                }
                else
                    yield return current;
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
