using System.IO;
using NUnit.Framework;
using Otus.CompressionAlgorithms.RLE.Logic;

namespace Tests
{
    public class Tests
    {
        [Test]
        public void Can_Encode_Array()
        {
            var initial = new[]
            {
                'a', 'a', 'b', 'a', 'b', 'c', 'c'
            };

            var result = new RunLengthEncoding().Encode(initial);

            Assert.That(result, Is.EqualTo("2a1b1a1b2c"));
        }

        [Test]
        public void Can_Decode_Array()
        {
            var result = new RunLengthEncoding().Decode("2a1b1a1b2c");


            var expectedResult = new[]
            {
                'a', 'a', 'b', 'a', 'b', 'c', 'c'
            };
            Assert.That(result, Is.EqualTo(expectedResult));
        }


        [Test]
        public void Can_Encode_Text_In_File()
        {
            var resultFileName = new RunLengthEncoding().EncodeFile(@"TestData\test_for_encode.txt");
            
            var text = File.ReadAllText(resultFileName);
            Assert.That(text, Is.EqualTo("2a1b1a1b2c"));
        }

        [Test]
        public void Can_Decode_Text_In_File()
        {
            var resultFileName = new RunLengthEncoding().DecodeFile(@"TestData\test_for_decode.txt");

            var text = File.ReadAllText(resultFileName);
            Assert.That(text, Is.EqualTo("aababcc"));
        }


        [Test]
        public void Can_Encode_Array_Via_Improved_Algorithm()
        {
            var initial = new[]
            {
                'a', 'a', 'b', 'a', 'b', 'c', 'c', 'd', 'e', 'f', 'f'
            };

            var result = new RunLengthEncoding().EncodeViaImprovedAlgorithm(initial);

            Assert.That(result, Is.EqualTo("2a-3bab2c-2de2f"));
        }

        [Test]
        public void Can_Decode_Array_Via_Improved_Algorithm()
        {
            var result = new RunLengthEncoding().DecodeViaImprovedAlgorithm("2a-3bab2c-2de2f");


            var expectedResult = new[]
            {
                'a', 'a', 'b', 'a', 'b', 'c', 'c', 'd', 'e', 'f', 'f'
            };
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}