namespace Wile
{
    public class JLiteral : Expression
    {
        public JLiteral(object value)
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
