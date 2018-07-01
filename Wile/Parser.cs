using System;
using System.Collections.Generic;
using System.Text;

namespace Wile
{
    public class Parser
    {
        #region Private Fields
        private readonly List<Token> _tokens;
        private int _current = 0;

        private Dictionary<TokenType, object> _keywords = new Dictionary<TokenType, object>
        {
            { TokenType.True, true },
            { TokenType.False, false },
            { TokenType.Null, null }
        };

        #endregion

        #region Constructor
        public Parser(List<Token> tokens)
        {
            _tokens = tokens;
        }
        #endregion

        #region Methods
        public Expression Parse()
        {
            if (Match(TokenType.LeftParanthesis))
            {
                return Object();
            }
            else if (Match(TokenType.LeftBracket))
            {
                return Array();
            }
            else if (Match(TokenType.String, TokenType.Double, TokenType.True, TokenType.False, TokenType.Null))
            {
                return Literal();
            }
            else
            {
                throw new Exception($"Unexpected token: {Peek()}");
            }
        }

        private JArray Array()
        {
            Advance(); // Consume the bracket

            var array = new JArray();

            while (!MatchAndConsume(TokenType.RightBracket))
            {
                if (IsAtEnd())
                    throw new Exception("Unterminated array.");

                array.Values.Add(Parse());

                if (Peek().Type == TokenType.Comma)
                    Advance();
            }

            return array;
        }

        private JObject Object()
        {
            Advance(); // Consume the parenthesis

            var root = new JObject();

            while (!MatchAndConsume(TokenType.RightParenthesis))
            {
                if (IsAtEnd())
                    throw new Exception("Unterminated Object.");

                var member = Pair();
                root.Members.Add(member.Key, member.Value);

                if (Peek().Type == TokenType.Comma)
                    Advance();
            }

            return root;
        }

        private KeyValuePair<string, Expression> Pair()
        {
            if (MatchAndConsume(TokenType.String))
            {
                var key = Previous().Literal as string;

                if (Peek().Type == TokenType.Colon)
                    Advance();
                else
                    throw new Exception($"Expected :, Got: {Peek()}");

                var value = Parse();

                return new KeyValuePair<string, Expression>(key, value);
            }

            throw new Exception($"Expected \", Got: {Previous()}");
        }

        private JLiteral Literal()
        {
            if (MatchAndConsume(TokenType.String, TokenType.Double))
            {
                return new JLiteral(Previous().Literal);
            }
            else if (MatchAndConsume(TokenType.True, TokenType.False, TokenType.Null))
            {
                return new JLiteral(_keywords[Previous().Type]);
            }

            throw new ArgumentException("Invalid tokens for a literal value.");
        }

        private bool IsAtEnd()
        {
            return _tokens.Count == _current;
        }

        private bool Check(TokenType type)
        {
            if (IsAtEnd()) return false;
            return _tokens[_current].Type == type;
        }

        private bool MatchAndConsume(params TokenType[] types)
        {
            var result = Match(types);
            if (result) Advance();

            return result;
        }

        private bool Match(params TokenType[] types)
        {
            foreach (var type in types)
            {
                if (Check(type))
                    return true;
            }

            return false;
        }

        private Token Advance()
        {
            if (!IsAtEnd())
                _current++;

            return Previous();
        }

        private Token Peek()
        {
            return _tokens[_current];
        }

        private Token Previous()
        {
            return _tokens[_current - 1];
        }
        #endregion
    }
}
