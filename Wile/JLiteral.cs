namespace Wile
{
    public class JLiteral : Value
    {
        public JLiteral(object value)
        {
            Value = value;
        }

        public object Value { get; }

        public override T Accept<T>(IValueVisitor<T> visitor)
        {
            return visitor.VisitLiteral(this);
        }
    }
}
