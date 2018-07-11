using System.Collections.Generic;

namespace Wile
{
    public abstract class JValue
    {
        public abstract JValueType Type { get; }
        public abstract T Accept<T>(IValueVisitor<T> visitor);
    }

    public class JObject : JValue
    {
        public Dictionary<string, JValue> Members { get; } = new Dictionary<string, JValue>();

        public override JValueType Type => JValueType.Object;

        public override T Accept<T>(IValueVisitor<T> visitor) => visitor.VisitObject(this);
    }

    public class JArray : JValue
    {
        public List<JValue> Values { get; } = new List<JValue>();

        public override JValueType Type => JValueType.Array;

        public override T Accept<T>(IValueVisitor<T> visitor) => visitor.VisitArray(this);
    }

    public class JNull : JValue
    {
        public override JValueType Type => JValueType.Null;

        public override T Accept<T>(IValueVisitor<T> visitor) => visitor.VisitNull(this);
    }

    public class JNumber : JValue
    {
        public JNumber(double number) => Value = number;

        public override JValueType Type => JValueType.Number;
        public double Value { get; }

        public override T Accept<T>(IValueVisitor<T> visitor) => visitor.VisitNumber(this);
    }

    public class JBoolean : JValue
    {
        public JBoolean(bool value) => Value = value;

        public override JValueType Type => JValueType.Boolean;
        public bool Value { get; }

        public override T Accept<T>(IValueVisitor<T> visitor) => visitor.VisitBoolean(this);
    }

    public class JString : JValue
    {
        public JString(string value) => Value = value;

        public string Value { get; }

        public override JValueType Type => JValueType.String;

        public override T Accept<T>(IValueVisitor<T> visitor) => visitor.VisitString(this);
    }
}
