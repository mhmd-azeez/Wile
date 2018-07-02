using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

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

        // From: https://stackoverflow.com/a/183909/7003797
        private readonly Regex _unicodeSequenceRegex = new Regex(@"\\[uU]([0-9A-F]{4})");

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

            _tokens.Add(new Token(TokenType.EOF, "", null, _line, _current));
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
                case '-': ScanNumeber(); break;
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
                        throw new WileConfusedException(_line, _current, $"Unexpected token: {c}.");
                    break;
            }
        }

        private void ScanString()
        {
            while ((Peek() != '"' || IsEscaped()) && !IsAtEnd())
            {
                if (Peek() == '\n')
                    _line++;

                Advance();
            }

            if (IsAtEnd())
            {
                throw new WileConfusedException(_line, _current, "Unterminated string.");
            }

            var value = _source.Substring(_start + 1, _current - _start - 1)
                               .Replace("\\n", "\n")
                               .Replace("\\t", "\t")
                               .Replace(@"\\", "\\")
                               .Replace(@"\""", "\"");

            value = _unicodeSequenceRegex.Replace(value, match =>
            {
                try
                {
                    var character = (char)int.Parse(match.Value.Substring(2), NumberStyles.HexNumber);
                    return character.ToString();
                }
                catch (Exception)
                {
                    throw new WileConfusedException(GetLine(match.Index), _current, "Badly formatted Number.");
                }
            });

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
                throw new WileConfusedException(_line, _current, $"Unknown keyword: {keyword}.");
            }
        }

        private bool IsEscaped()
        {
            if (_current >= 2)
            {
                var previousPrevious = _source[_current - 2];
                var previous = _source[_current - 1];
                return previous == '\\' && previousPrevious != '\\';
            }

            return false;
        }

        private int GetLine(int index)
        {
            if (index > _source.Length)
                return -1;

            int line = 0;

            for (int i = 0; i < index; i++)
            {
                if (_source[i] == '\n')
                    line++;
            }

            return line;
        }

        private void ScanNumeber()
        {
            while (IsDigit(Peek()) || Peek() == '.' || Peek() == 'E' ||
                   Peek() == 'e' || Peek() == '-' || Peek() == '+')
            {
                Advance();
            }

            var lexeme = GetCurrentLexeme();
            var value = double.Parse(lexeme, NumberStyles.Any);

            AddToken(TokenType.Number, value);
        }

        private bool IsAlpha(char c) => (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');

        private string GetCurrentLexeme() => _source.Substring(_start, _current - _start);

        private bool IsDigit(char c) => c >= '0' && c <= '9';

        private bool IsAlphaNumeric(char c) => IsAlpha(c) || IsDigit(c);

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

        private void AddToken(TokenType type) => AddToken(type, null);

        private void AddToken(TokenType type, object literal)
        {
            var lexeme = GetCurrentLexeme();
            _tokens.Add(new Token(type, lexeme, literal, _line, _current));
        }

        private char Advance() => _source[_current++];

        private bool IsAtEnd() => _current >= _source.Length;
        #endregion
    }
}
