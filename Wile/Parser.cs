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
        public JObject Parse()
        {
            if (Match(TokenType.LeftParanthesis))
            {
                var root = new JObject();

                while (!Match(TokenType.RightParenthesis))
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
            else
            {
                throw new Exception($"Expected '{{', Got: {Peek()}");
            }
        }

        private KeyValuePair<string, Expression> Pair()
        {
            if (Match(TokenType.String))
            {
                var key = Previous().Literal as string;

                if (Peek().Type == TokenType.Colon)
                    Advance();
                else
                    throw new Exception($"Expected :, Got: {Peek()}");

                var value = Expression();

                return new KeyValuePair<string, Expression>(key, value);
            }

            throw new Exception($"Expected \", Got: {Previous()}");
        }

        private Expression Expression()
        {
            if (Match(TokenType.String, TokenType.Double, TokenType.Integer))
            {
                return new Literal(Previous().Literal);
            }
            else if (Match(TokenType.True, TokenType.False, TokenType.Null))
            {
                return new Literal(_keywords[Previous().Type]);
            }
            else if (Match(TokenType.LeftParanthesis, TokenType.LeftBracket))
            {
                throw new NotImplementedException();
            }
            else
            {
                throw new NotImplementedException();
            }
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

        private bool Match(params TokenType[] types)
        {
            foreach (var type in types)
            {
                if (Check(type))
                {
                    Advance();
                    return true;
                }
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
