using System;
using System.Collections.Generic;
using System.Text;

namespace Wile
{
    public interface IExpressionVisitor<T>
    {
        T VisitLiteral(JLiteral literal);
        T VisitObject(JObject jObject);
        T VisitArray(JArray array);
    }
}
