using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wile
{
    public class Generator : IValueVisitor<string>
    {
        private readonly bool _beautify = true;
        private int _indent = 0;

        public Generator(bool beautify = true)
        {
            _beautify = beautify;
        }

        public string VisitArray(JArray array)
        {
            var fancy = array.Values.Any(v => v.Type == JValueType.Array || v.Type == JValueType.Object);

            var builder = new StringBuilder();
            builder.Append("[");
            _indent++;

            if (_beautify && fancy)
            {
                builder.AppendLine();
            }

            for (int i = 0; i < array.Values.Count; i++)
            {
                builder.Append(Indent(fancy) + array.Values[i].Accept(this));

                if (i < array.Values.Count - 1)
                    builder.Append(", ");

                if (_beautify && fancy)
                    builder.AppendLine();
            }

            _indent--;
            builder.Append("]");

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
            _indent++;

            if (_beautify)
            {
                builder.AppendLine();
            }

            var last = jObject.Members.LastOrDefault();
            foreach (var member in jObject.Members)
            {
                builder.Append($"{Indent()}\"{member.Key}\": {member.Value.Accept(this)}");

                if (member.Key != last.Key && member.Value != last.Value)
                    builder.Append(", ");

                if (_beautify)
                    builder.AppendLine();
            };

            _indent--;
            builder.Append("}");

            return builder.ToString();
        }

        public string VisitString(JString text)
        {
            return $"\"{text.Value}\"";
        }

        private string Indent(bool fancy = true)
        {
            if (!fancy || !_beautify)
                return string.Empty;

            string indent = string.Empty;
            for (int i = 0; i < _indent; i++)
            {
                indent += "  ";
            }

            return indent;
        }
    }
}
