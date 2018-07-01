namespace Wile
{
    public class Literal : Expression
    {
        public Literal(object value)
        {
            Value = value;
        }

        public object Value { get; }

        public override T Accept<T>(IExpressionVisitor<T> visitor)
        {
            return visitor.VisitLiteral(this);
        }
    }
}
