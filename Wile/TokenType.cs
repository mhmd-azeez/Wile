using System;
using System.Collections.Generic;
using System.Text;

namespace Wile
{
    public enum TokenType
    {
        /// <summary>
        /// {
        /// </summary>
        LeftParanthesis,

        /// <summary>
        /// }
        /// </summary>
        RightParenthesis,

        /// <summary>
        /// [
        /// </summary>
        LeftBracket,

        /// <summary>
        /// ]
        /// </summary>
        RightBracket,

        /// <summary>
        /// ,
        /// </summary>
        Comma,

        /// <summary>
        /// :
        /// </summary>
        Colon,

        /// <summary>
        /// true
        /// </summary>
        True,

        /// <summary>
        /// false
        /// </summary>
        False,

        /// <summary>
        /// null
        /// </summary>
        Null,

        /// <summary>
        /// \
        /// </summary>
        Backslash,

        /// <summary>
        /// /
        /// </summary>
        Forewardslash,

        /// <summary>
        /// End of file
        /// </summary>
        EOF,

        /// <summary>
        /// 1234.123
        /// </summary>
        Double,

        /// <summary>
        /// The quick brown fox jumps over the lazy dog.
        /// </summary>
        String,
    }
}
