using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Otus.CompressionAlgorithms.RLE.Logic
{
    public class RunLengthEncoding
    {
        public string Encode(char[] characters)
        {
            return EncodeInternal(characters);
        }

        public char[] Decode(string compressedString)
        {
            return DecodeInternal(compressedString);
        }

        public string EncodeFile(string fileName)
        {
            var initialText = File.ReadAllText(fileName);

            var encodedString = EncodeInternal(initialText);

           return WriteToOutputFile(fileName, "encoded", encodedString);
        }

        public string DecodeFile(string fileName)
        {
            var initialText = File.ReadAllText(fileName);

            var decodedString = new string(DecodeInternal(initialText));

            return WriteToOutputFile(fileName, "decoded", decodedString);
        }


        #region Support Methods

        private string EncodeInternal(IEnumerable<char> characters)
        {
            var result = new StringBuilder();

            var counter = 0;
            var currentCharacter = characters.ElementAtOrDefault(0);

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

        public char[] DecodeInternal(string compressedString)
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

        private string WriteToOutputFile(string initialFileName, string newFileNamePrefix, string encodedString)
        {
            var fileNameParts = initialFileName.Split('.');
            var outputFileName = $"{fileNameParts[0]}_{newFileNamePrefix}.{fileNameParts[1]}";
            using (var writer = new StreamWriter(outputFileName))
            {
                writer.Write(encodedString);
            }

            return outputFileName;
        }

        #endregion
    }
}
