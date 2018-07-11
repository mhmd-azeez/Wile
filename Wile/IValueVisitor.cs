namespace Wile
{
    public interface IValueVisitor<T>
    {
        T VisitNull(JNull value);
        T VisitBoolean(JBoolean boolean);
        T VisitString(JString text);
        T VisitNumber(JNumber number);
        T VisitObject(JObject jObject);
        T VisitArray(JArray array);
    }
}
