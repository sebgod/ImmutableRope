using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ImmutableRope.Unicode
{
    enum PropertiesAndHigherBytes : byte
    {

    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TaggedCodePoint
    {
        const byte HighPlaneCharMask = 0x3f;

        private readonly CodeBlock _codeBlock;
        private readonly PropertiesAndHigherBytes _propsAndhigh;
        private readonly char _bmpValue;

        
        public TaggedCodePoint(string text, int index = 0)
            : this((uint)char.ConvertToUtf32(text, index))
        {
            if (text == null) throw new ArgumentNullException("text");
            // calling TaggedCodePoint(uint codePoint)
        }
        public TaggedCodePoint(char high, char low)
            : this((uint)char.ConvertToUtf32(high, low))
        {
            // calling TaggedCodePoint(uint codePoint)
        }

        public TaggedCodePoint(char basicPlaneChar)
        {
            if (char.IsSurrogate(basicPlaneChar))
                throw new ArgumentOutOfRangeException("basicPlaneChar", "Is a surrogate char, expected basic plane char: " + basicPlaneChar);

            _bmpValue = basicPlaneChar;
            _propsAndhigh = 0;
            _codeBlock = CodeBlock.Invalid;
        }
        public TaggedCodePoint(uint codePoint)
        {
            if (codePoint > 0x10ffff)
                throw new ArgumentOutOfRangeException("codePoint", "The maximum assignable Unicode codepoint is: 0x10ffff, but you provided: " + codePoint);

            _bmpValue = (char)(codePoint & 0xff);
            _propsAndhigh = (PropertiesAndHigherBytes)((codePoint >> 16) & HighPlaneCharMask);
            _codeBlock = CodeBlock.Invalid;
        }

        public override string ToString()
        {
            var _highPlaneValue = (byte) ((byte) _propsAndhigh & HighPlaneCharMask);
            if (_highPlaneValue == 0)
            {
                return new string(_bmpValue, 1);
            }
            else
            {
                return char.ConvertFromUtf32((_highPlaneValue << 16) & _bmpValue);
            }
        }

        public static implicit operator TaggedCodePoint(char bmp)
        {
            return new TaggedCodePoint(bmp);
        }
        public static implicit operator TaggedCodePoint(int codePoint)
        {
            if (codePoint < 0)
                throw new ArgumentOutOfRangeException("codePoint", "the codepoint is negative: " + codePoint);

            return new TaggedCodePoint((uint)codePoint);
        }
        public static implicit operator TaggedCodePoint(uint codePoint)
        {
            return new TaggedCodePoint(codePoint);
        }
        public static implicit operator uint(TaggedCodePoint @this)
        {
            return (((uint)@this._propsAndhigh & HighPlaneCharMask) << 16) | @this._bmpValue;
        }
        public static explicit operator char(TaggedCodePoint @this)
        {
            if ((byte)((byte)@this._propsAndhigh & HighPlaneCharMask) > 0)
                throw new ArgumentOutOfRangeException("this", "Not a basic multilingual plane char: " + @this);

            return (char)@this._bmpValue;
        }
    }
}
