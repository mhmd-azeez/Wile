using System.Collections.Generic;

namespace Wile
{
    public class JArray : Value
    {
        public List<Value> Values { get; } = new List<Value>();

        public override T Accept<T>(IValueVisitor<T> visitor)
        {
            return visitor.VisitArray(this);
        }
    }
}
