using System;
using System.IO;
using System.IO.Compression;

namespace CAStreamDecorator
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var stream = File.Create("data.bin"))
            {
                using (var ds = new DeflateStream(stream, CompressionMode.Compress))
                {
                    ds.WriteByte(65);
                    ds.WriteByte(66);
                }
            }

            using (var stream = File.OpenRead("data.bin"))
            {
                using (var ds = new DeflateStream(stream, CompressionMode.Decompress))
                {
                    for (int i = 0; i < stream.Length; i++)
                    {
                        Console.WriteLine(ds.ReadByte());
                    }
                }
            }

            Console.ReadKey();
        }
    }
}
