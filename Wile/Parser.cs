using System;
using System.Collections.Generic;

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
        public Value Parse()
        {
            if (Match(TokenType.LeftParanthesis))
            {
                return Object();
            }
            else if (Match(TokenType.LeftBracket))
            {
                return Array();
            }
            else if (Match(TokenType.String, TokenType.Number, TokenType.True, TokenType.False, TokenType.Null))
            {
                return Literal();
            }
            else
            {
                throw new WileConfusedException(Peek().Line, Peek().Character, $"Unexpected token: {Peek().Lexeme}");
            }
        }

        private JArray Array()
        {
            Advance(); // Consume the right bracket

            var array = new JArray();

            while (!MatchAndConsume(TokenType.RightBracket))
            {
                if (IsAtEnd())
                    throw new WileConfusedException(Peek().Line, Peek().Character, "Unterminated array.");

                array.Values.Add(Parse());

                if (Peek().Type == TokenType.Comma)
                    Advance();
            }

            return array;
        }

        private JObject Object()
        {
            Advance(); // Consume the right parenthesis

            var root = new JObject();

            while (!MatchAndConsume(TokenType.RightParenthesis))
            {
                if (IsAtEnd())
                    throw new WileConfusedException(Peek().Line, Peek().Character, "Unterminated Object.");

                var member = Pair();
                root.Members.Add(member.Key, member.Value);

                if (Peek().Type == TokenType.Comma)
                    Advance();
            }

            return root;
        }

        private KeyValuePair<string, Value> Pair()
        {
            if (MatchAndConsume(TokenType.String))
            {
                var key = Previous().Literal as string;

                if (Peek().Type == TokenType.Colon)
                    Advance();
                else
                    throw new WileConfusedException(Peek().Line, Peek().Character, $"Expected :\tGot: {Peek().Lexeme}");

                var value = Parse();

                return new KeyValuePair<string, Value>(key, value);
            }

            throw new WileConfusedException(Previous().Line, Previous().Character, $"Expected \"\tGot: {Previous().Lexeme}");
        }

        private JLiteral Literal()
        {
            if (MatchAndConsume(TokenType.String, TokenType.Number))
            {
                return new JLiteral(Previous().Literal);
            }
            else if (MatchAndConsume(TokenType.True, TokenType.False, TokenType.Null))
            {
                return new JLiteral(_keywords[Previous().Type]);
            }

            throw new WileConfusedException(Peek().Line, Peek().Character, "Invalid tokens for a literal value.");
        }

        private bool IsAtEnd() => _tokens.Count == _current;

        private bool Check(TokenType type) => !IsAtEnd() && _tokens[_current].Type == type;

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

        private Token Peek() => _tokens[_current];

        private Token Previous() => _tokens[_current - 1];
        #endregion
    }
}
