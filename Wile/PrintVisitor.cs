using System;
using System.Collections.Generic;
using System.Text;

namespace Wile
{
    public class PrintVisitor : IExpressionVisitor<string>
    {
        public string VisitLiteral(Literal literal)
        {
            return $"[literal] => {literal.Value ?? "null"}";
        }

        public string VisitObject(JObject jObject)
        {
            var builder = new StringBuilder();

            builder.AppendLine("[object] => ");
           
            foreach(var pair in jObject.Members)
            {
                builder.AppendLine($"\t {pair.Key} => {pair.Value.Accept(this)}");
            }

            return builder.ToString();
        }
    }
}
