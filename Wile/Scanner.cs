using System;
using System.Collections.Generic;

namespace Wile
{
    public class Scanner
    {
        #region Private Fields
        private Dictionary<string, TokenType> _keywords = new Dictionary<string, TokenType>
        {
            { "false", TokenType.False },
            { "true", TokenType.True },
            { "null", TokenType.Null }
        };

        private readonly string _source;
        private readonly List<Token> _tokens = new List<Token>();

        private int _start = 0;
        private int _current = 0;
        private int _line = 1;
        #endregion

        #region Constructor
        public Scanner(string source)
        {
            _source = source;
        }
        #endregion

        #region Methods
        public List<Token> ScanTokens()
        {
            while (!IsAtEnd())
            {
                _start = _current;
                ScanToken();
            }

            _tokens.Add(new Token(TokenType.EOF, "", null, _line));
            return _tokens;
        }

        private void ScanToken()
        {
            var c = Advance();

            switch (c)
            {
                case '{': AddToken(TokenType.LeftParanthesis); break;
                case '}': AddToken(TokenType.RightParenthesis); break;
                case '[': AddToken(TokenType.LeftBracket); break;
                case ']': AddToken(TokenType.RightBracket); break;
                case ',': AddToken(TokenType.Comma); break;
                case ':': AddToken(TokenType.Colon); break;
                case '-': AddToken(TokenType.Minus); break;
                case '"': ScanString(); break;

                case ' ':
                case '\r':
                case '\t':
                    break; // Ignore whitespace

                case '\n':
                    _line++;
                    break;

                default:
                    if (IsAlpha(c))
                        ScanKeyword();
                    else if (IsDigit(c))
                        ScanNumeber();
                    else
                        throw new ScannerException(_line, $"Unexpected character: {c} at line {_line}");
                    break;
            }
        }

        private void ScanString()
        {
            while (Peek() != '"' && !IsAtEnd())
            {
                if (Peek() == '\n')
                    _line++;

                Advance();
            }

            if (IsAtEnd())
            {
                throw new ScannerException(_line, "Unterminated string.");
            }

            var value = _source.Substring(_start + 1, _current - _start - 1)
                               .Replace("\\n", "\n")
                               .Replace("\\t", "\t");

            Advance();

            AddToken(TokenType.String, value);
        }

        private void ScanKeyword()
        {
            while (IsAlpha(Peek()))
                Advance();

            var keyword = GetCurrentLexeme();
            if (_keywords.ContainsKey(keyword))
            {
                AddToken(_keywords[keyword]);
            }
            else
            {
                throw new ScannerException(_line, $"Unknown keyword: {keyword}.");
            }
        }

        private void ScanNumeber()
        {
            while (IsDigit(Peek()) || Peek() == '.')
                Advance();

            var lexeme = GetCurrentLexeme();
            var type = lexeme.IndexOf('.') >= 0 ? TokenType.Double : TokenType.Integer;
            var value = type == TokenType.Double ? (object)double.Parse(lexeme) : int.Parse(lexeme);

            AddToken(type, value);
        }

        private bool IsAlpha(char c)
        {
            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
        }

        private string GetCurrentLexeme()
        {
            return _source.Substring(_start, _current - _start);
        }

        private bool IsDigit(char c)
        {
            return c >= '0' && c <= '9';
        }

        private bool IsAlphaNumeric(char c)
        {
            return IsAlpha(c) || IsDigit(c);
        }

        private char PeekNext()
        {
            if (_current + 1 == _source.Length) return '\0';
            return _source[_current + 1];
        }

        private char Peek()
        {
            if (IsAtEnd()) return '\0';
            return _source[_current];
        }

        private void AddToken(TokenType type)
        {
            AddToken(type, null);
        }

        private void AddToken(TokenType type, object literal)
        {
            var lexeme = GetCurrentLexeme();
            _tokens.Add(new Token(type, lexeme, literal, _line));
        }

        private char Advance()
        {
            return _source[_current++];
        }

        private bool IsAtEnd() => _current >= _source.Length;
        #endregion
    }
}
