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
    }
}