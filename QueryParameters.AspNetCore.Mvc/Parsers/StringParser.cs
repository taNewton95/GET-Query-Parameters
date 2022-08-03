using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryParameters.AspNetCore.Mvc.Parsers
{
    internal class StringParser
    {

        public readonly string InputString;

        private int IterationIndex = 0;

        /// <summary>
        /// The string consisting of the data between the last match and the current match.
        /// </summary>
        public string CurrentString { get; private set; }

        /// <summary>
        /// The last character that matched the supplied delimiters.
        /// </summary>
        public char? MatchedDelimiter { get; private set; }

        public StringParser(string inputString)
        {
            InputString = inputString;
        }

        /// <summary>
        /// <inheritdoc cref="Next(HashSet{char}, bool)" path="/summary"/>
        /// </summary>
        /// <param name="delimiter"><inheritdoc cref="Next(HashSet{char}, bool)" path="/param[@name='delimiters']"/></param>
        /// <param name="includeFinal"><inheritdoc cref="Next(HashSet{char}, bool)" path="/param[@name='includeFinal']"/></param>
        /// <returns></returns>
        public bool Next(char delimiter, bool includeFinal = true)
        {
            return Next(new HashSet<char>(new[] { delimiter }), includeFinal: includeFinal);
        }

        /// <summary>
        /// Forward the parser to the next instance of a delimiter.
        /// </summary>
        /// <param name="delimiters">Delimiters in the <see cref="InputString"/>.</param>
        /// <param name="includeFinal">Whether to return <see langword="true"/> when the end of the string is reached and there is data remaining.</param>
        /// <returns></returns>
        public bool Next(HashSet<char> delimiters, bool includeFinal = true)
        {
            if (IterationIndex >= InputString.Length) return false;

            // Track whether the current iteration is contained within a string
            bool inString = false;

            // Build the current matched string. Needs to be a builder rather than using indexes as some characters are ignored in the input string
            var matchBuilder = new StringBuilder();

            while (IterationIndex < InputString.Length)
            {
                var currentChar = InputString[IterationIndex];

                // Handle potential string declarations
                if (currentChar == '\'')
                {
                    if (inString)
                    {
                        // Handle apostrophes escaped within a string with a double declaration
                        if (IterationIndex + 1 < InputString.Length && InputString[IterationIndex + 1] == '\'')
                        {
                            IterationIndex ++;
                        }
                        else
                        {
                            inString = false;
                        }
                    }
                    else
                    {
                        inString = true;
                    }
                }

                IterationIndex++;

                if (inString)
                {
                    matchBuilder.Append(currentChar);
                    continue;
                }

                if (delimiters.Contains(currentChar))
                {
                    MatchedDelimiter = currentChar;
                    CurrentString = matchBuilder.ToString();
                    matchBuilder.Clear();

                    return true;
                }

                matchBuilder.Append(currentChar);
            }

            MatchedDelimiter = null;

            if (matchBuilder.Length == 0)
            {
                CurrentString = null;
            }
            else
            {
                CurrentString = matchBuilder.ToString();

                if (includeFinal) return true;
            }

            return false;
        }

    }
}
