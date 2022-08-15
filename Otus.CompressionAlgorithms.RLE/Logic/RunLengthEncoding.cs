using System;
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

        public string EncodeViaImprovedAlgorithm(IEnumerable<char> characters)
        {
            var encodedString = EncodeInternal(characters);

            var result = new StringBuilder();
            var singleCharacters = new StringBuilder();
            var singleCharactersCounter = 0;
            for (var i = 0; i < encodedString.Length; i = i + 2)
            {
                var localCounter = encodedString[i];
                var localCharacter = encodedString[i + 1];

                if (localCounter == '1')
                {
                    singleCharactersCounter--;
                    singleCharacters.Append(localCharacter);
                }
                else
                {
                    var combinedSingleCharacters = singleCharactersCounter == 0 ? string.Empty : $"{singleCharactersCounter}{singleCharacters}";
                    result
                        .Append(combinedSingleCharacters)
                        .Append(localCounter).Append(localCharacter);
                    
                    singleCharactersCounter = 0;
                    singleCharacters.Clear();
                }
            }

            var lastSingleCharacters = singleCharactersCounter == 0 ? string.Empty : $"{singleCharactersCounter}{singleCharacters}";

            return result.Append(lastSingleCharacters).ToString();
        }

        public char[] DecodeViaImprovedAlgorithm(string compressedString)
        {
            var result = new List<char>();

            for (var i = 0; i < compressedString.Length; i++)
            {
                var character = compressedString[i];

                if (char.IsNumber(character))
                {
                    var number = int.Parse(character.ToString());
                    for (var j = 0; j < number; j++)
                    {
                        result.Add(compressedString[i + 1]);
                    }
                }
                else if (character == '-')
                {
                    var singleCharactersLength = int.Parse(compressedString[i + 1].ToString());
                    var endOfSingleCharacters = i + 2 + singleCharactersLength;
                    for (var j = i + 2; j < endOfSingleCharacters; j++)
                    {
                        result.Add(compressedString[j]);
                    }

                    i = endOfSingleCharacters - 1;
                }
            }

            return result.ToArray();
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

            for (var i = 0; i < compressedString.Length; i = i + 2)
            {
                var counter = compressedString[i];
                var character = compressedString[i + 1];

                if (char.IsNumber(counter))
                {
                    for (var j = 0; j < int.Parse(counter.ToString()); j++)
                    {
                        result.Add(character);
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
