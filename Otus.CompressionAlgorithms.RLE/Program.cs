using System;
using Otus.CompressionAlgorithms.RLE.Logic;

namespace Otus.CompressionAlgorithms.RLE
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Mode: 1 - encode, 2 - decode");
            var mode = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine(@"File name:");
            var fileName = Console.ReadLine();

            string outputFileName;
            var worker = new RunLengthEncoding();
            switch (mode)
            {
                case 1:
                    outputFileName = worker.EncodeFile(fileName);
                    break;

                case 2:
                    outputFileName = worker.DecodeFile(fileName);
                    break;

                default:
                    throw new ArgumentException("Unknown mode value");
            }

            Console.WriteLine($"Result is in file '{outputFileName}'");

            Console.ReadKey();
        }
    }
}
