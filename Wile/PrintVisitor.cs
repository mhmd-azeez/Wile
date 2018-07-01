using System;
using System.Collections.Generic;
using System.Text;

namespace Wile
{
    public class PrintVisitor : IExpressionVisitor<string>
    {
        public string VisitArray(JArray array)
        {
            var builder = new StringBuilder();

            builder.AppendLine("[array] => ");

            foreach (var value in array.Values)
            {
                builder.AppendLine($"\t {value.Accept(this)}");
            }

            return builder.ToString();
        }

        public string VisitLiteral(JLiteral literal)
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
