using System.Text;

namespace Wile
{
    public class PrintVisitor : IValueVisitor<string>
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

        public string VisitBoolean(JBoolean boolean)
        {
            return $"[boolean] => {boolean.Value}";
        }

        public string VisitNull(JNull value)
        {
            return $"[null]";
        }

        public string VisitNumber(JNumber number)
        {
            return $"[number] => {number.Value}";
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

        public string VisitString(JString text)
        {
            throw new System.NotImplementedException();
        }
    }
}
