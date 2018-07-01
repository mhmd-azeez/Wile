using System;
using System.Collections.Generic;
using System.Text;

namespace Wile
{
    public interface IExpressionVisitor<T>
    {
        T VisitLiteral(Literal literal);
        T VisitObject(JObject jObject);
    }
}
