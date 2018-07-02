namespace Wile
{
    public abstract class Value
    {
        public abstract T Accept<T>(IValueVisitor<T> visitor);
    }
}
