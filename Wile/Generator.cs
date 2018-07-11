using System;
using System.Collections.Generic;
using System.Text;

namespace Wile
{
    public class Generator : IValueVisitor<string>
    {
        public string VisitArray(JArray array)
        {
            var builder = new StringBuilder();
            builder.Append('[');

            for (int i = 0; i < array.Values.Count; i++)
            {
                if (i > 0)
                    builder.Append(", ");

                builder.Append(array.Values[i].Accept(this));
            }

            builder.Append(']');

            return builder.ToString();
        }

        public string VisitBoolean(JBoolean boolean)
        {
            return boolean.Value ? "true" : "false";
        }

        public string VisitNull(JNull value)
        {
            return "null";
        }

        public string VisitNumber(JNumber number)
        {
            return number.Value.ToString();
        }

        public string VisitObject(JObject jObject)
        {
            var builder = new StringBuilder();
            builder.Append("{");

            int i = 0;
            foreach (var member in jObject.Members)
            {
                if (i++ > 0)
                    builder.Append(", ");

                builder.Append($"\"{member.Key}\":{member.Value.Accept(this)}");
            };

            builder.Append("}");

            return builder.ToString();
        }

        public string VisitString(JString text)
        {
            return $"\"{text.Value}\"";
        }
    }
}
