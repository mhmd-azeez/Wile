using System.Collections.Generic;

namespace Wile
{
    public class JObject : Value
    {
        public Dictionary<string, Value> Members { get; } = new Dictionary<string, Value>();

        public override T Accept<T>(IValueVisitor<T> visitor)
        {
            return visitor.VisitObject(this);
        }
    }
}
