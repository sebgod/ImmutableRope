using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImmutableRope.Unicode
{
    public static class StringBuilderHelper
    {
        /// <summary>
        /// Replaces all chars with the given string,
        /// no new allocation of StringBuilder
        /// </summary>
        /// <param name="this"></param>
        /// <param name="replaceWith"></param>
        /// <returns></returns>
        public static StringBuilder CopyFrom(this StringBuilder @this, string replaceWith)
        {
            if (replaceWith.Length < 20)
            {
                @this.Length = replaceWith.Length;
                for (var i = 0; i < replaceWith.Length; i++)
                    @this[i] = replaceWith[i];

            }
            else
            {
                @this.Clear();
                @this.Insert(0, replaceWith);
            }
            return @this;
        }

        public static StringBuilder AppendFormatLine(this StringBuilder @this, string format, params object[] args)
        {
            return @this.AppendFormat(format + Environment.NewLine, args);
        }

        /// <summary>
        /// Add Text with proper intentation and max width (using word breaker)
        /// TODO: proper indentation
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="text"></param>
        /// <param name="followLineSpace"></param>
        /// <param name="space"></param>
        /// <returns></returns>
        public static StringBuilder AppendWithLineBreaking(this StringBuilder builder, string text, int followLineSpace, int space)
        {
            return builder.Append(text);
        }
    }
}
