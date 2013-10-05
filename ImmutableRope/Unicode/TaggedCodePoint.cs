using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ImmutableRope.Unicode
{
    public enum PropertiesAndUnicodePlane : byte
    {

    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TaggedCodePoint
    {
        public const uint MaxCodePoint = 0x10ffff;
        public const byte HighPlaneCharMask = 0x3f;

        public readonly CodeBlock CodeBlock;
        public readonly PropertiesAndUnicodePlane PropertiesAndPlane;
        public readonly char Value;
                
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

            Value = basicPlaneChar;
            PropertiesAndPlane = 0;
            CodeBlock = CodeBlock.Invalid;
        }
        public TaggedCodePoint(uint codePoint)
        {
            if (codePoint > MaxCodePoint)
                throw new ArgumentOutOfRangeException("codePoint", "The maximum assignable Unicode codepoint is: 0x10ffff, but you provided: " + codePoint);

            Value = (char)(codePoint & char.MaxValue);
            PropertiesAndPlane = (PropertiesAndUnicodePlane)((codePoint >> 16) & HighPlaneCharMask);
            CodeBlock = CodeBlock.Invalid;
        }

        public byte UnicodePlane
        {
            get
            {
                return (byte)((byte)PropertiesAndPlane & HighPlaneCharMask);
            }
        }

        public override string ToString()
        {
            var plane = UnicodePlane;
            return plane == 0 
                ? new string(new []{Value})
                : char.ConvertFromUtf32((plane << 16) | Value);
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
            return (((uint)@this.PropertiesAndPlane & HighPlaneCharMask) << 16) | @this.Value;
        }
        public static explicit operator char(TaggedCodePoint @this)
        {
            var plane = @this.UnicodePlane;
            if (plane > 0)
                throw new ArgumentOutOfRangeException("this", @this + " is not a basic multilingual plane char, but from plane: " + plane);

            return @this.Value;
        }
    }
}
