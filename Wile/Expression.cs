using System;
using System.Collections.Generic;
using System.Text;

namespace Wile
{
    public abstract class Expression
    {
        public abstract T Accept<T>(IExpressionVisitor<T> visitor);
    }
}
