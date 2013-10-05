using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmutableRope.Unicode
{
    public class CodePointList : IReadOnlyList<TaggedCodePoint>
    {
        const int GuessedInitCap = 20;
        const float InitialThreshold = 1.2f;
        const float MissedThreshold = 1.3f;
        const float WasteFactor = 1.25f;

        private readonly TaggedCodePoint[] _codePoints;
        private readonly int _numberOfChars;
        private readonly int _surrogateCount;

        public CodePointList(string content)
        {
            _codePoints = new TaggedCodePoint[(int)(content.Length * InitialThreshold) + 1];
            _numberOfChars = CopyFromIterator(new UnicodeEnumerator(content), ref _codePoints, out _surrogateCount);
        }

        public CodePointList(IEnumerable<TaggedCodePoint> content, int length = GuessedInitCap)
        {
            _codePoints = new TaggedCodePoint[length];
            _numberOfChars = CopyFromIterator(content, ref _codePoints, out _surrogateCount);
        }

        private static int CopyFromIterator(IEnumerable<TaggedCodePoint> iterator, ref TaggedCodePoint[] array, out int surrogateCount)
        {
            surrogateCount = 0;
            var index = 0;
            foreach (var codePoint in iterator)
            {
                if (index == array.Length)
                    Array.Resize(ref array, (int)(index * MissedThreshold) + 1);

                if (codePoint.UnicodePlane > 0)
                    surrogateCount++;

                array[index++] = codePoint;
            }

            if (index * WasteFactor > array.Length)
                Array.Resize(ref array, index);

            return index;
        }

        public char[] ToCharArray()
        {
            var chars = new char[_numberOfChars + _surrogateCount];
            if (_surrogateCount == 0)
            {
                for (var i = 0; i < _numberOfChars; i++)
                    chars[i] = _codePoints[i].Value;
            }
            else
            {
                for (var i = 0; i < _numberOfChars; i++)
                {
                    var codePoint = _codePoints[i];
                    var plane = codePoint.UnicodePlane;
                    if (plane > 0)
                    {
                        var utf32 = char.ConvertFromUtf32((plane << 16) | codePoint.Value);
                        chars[i] = utf32[0];
                        chars[++i] = utf32[1];
                    }
                    else
                    {
                        chars[i] = _codePoints[i].Value;
                    }                    
                }
            }

            return chars;
        }

        public override string ToString()
        {
            return new string(ToCharArray());
        }

        public TaggedCodePoint this[int index]
        {
            get { return _codePoints[index]; }
        }

        public int Count
        {
            get { return _numberOfChars; }
        }

        public IEnumerator<TaggedCodePoint> GetEnumerator()
        {
            return (IEnumerator<TaggedCodePoint>)_codePoints.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
