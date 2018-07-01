using System.Collections.Generic;

namespace Wile
{
    public class JObject : Expression
    {
        public Dictionary<string, Expression> Members { get; } = new Dictionary<string, Expression>();

        public override T Accept<T>(IExpressionVisitor<T> visitor)
        {
            return visitor.VisitObject(this);
        }
    }
}
