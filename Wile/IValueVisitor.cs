namespace Wile
{
    public interface IValueVisitor<T>
    {
        T VisitLiteral(JLiteral literal);
        T VisitObject(JObject jObject);
        T VisitArray(JArray array);
    }
}
