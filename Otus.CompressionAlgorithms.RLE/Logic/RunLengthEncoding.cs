using System;
using System.Collections.Generic;
using System.Text;

namespace Otus.CompressionAlgorithms.RLE.Logic
{
    public class RunLengthEncoding
    {
        public string Encode(char[] characters)
        {
            var result = new StringBuilder();

            var counter = 0;
            var currentCharacter = characters[0];

            foreach (var character in characters)
            {
                if (currentCharacter == character)
                {
                    counter++;
                }
                else
                {
                    result.Append($"{counter}{currentCharacter}");
                    currentCharacter = character;
                    counter = 1;
                }
            }

            return result.Append($"{counter}{currentCharacter}").ToString();
        }

        public char[] Decode(string compressedString)
        {
            var result = new List<char>();

            for (var i = 0; i < compressedString.Length; i++)
            {
                var character = compressedString[i];

                if (char.IsNumber(character))
                {
                    for (var j = 0; j < int.Parse(character.ToString()); j++)
                    {
                        result.Add(compressedString[i + 1]);
                    }
                }
            }

            return result.ToArray();
        }


        #region Support Methods



        #endregion
    }
}
