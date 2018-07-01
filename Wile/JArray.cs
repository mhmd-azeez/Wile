using System;
using System.Collections.Generic;
using System.Text;

namespace Wile
{
    public class JArray : Expression
    {
        public List<Expression> Values { get; } = new List<Expression>();

        public override T Accept<T>(IExpressionVisitor<T> visitor)
        {
            return visitor.VisitArray(this);
        }
    }
}
