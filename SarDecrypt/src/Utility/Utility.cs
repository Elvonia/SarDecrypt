using System;
//using SarDecrypt.Compression;

namespace SarDecrypt
{
    static class Utility
    {
        public static byte[] byteSwap(this byte[] data)
        {
            var swapped = new byte[data.Length];
            for (var i = 0; i < data.Length; i += 4)
            {
                swapped[i] = data[i + 3];
                swapped[i + 1] = data[i + 2];
                swapped[i + 2] = data[i + 1];
                swapped[i + 3] = data[i];
            }
            return swapped;
        }

        public static byte[] xor(byte[] data)
        {
            byte xor = 0x95;

            for (int i = 0; i < data.Length; i++)
            {
                data[i] = data[i] ^= xor;
            }

            return data;
        }

        public static byte[] dStrip(this byte[] data)
        {
            byte[] stripped = new byte[data.Length - 4];

            Buffer.BlockCopy(data, 4, stripped, 0, stripped.Length);

            return stripped;
        }

        public static byte[] dReplace(byte[] data, byte[] strip)
        {
            byte[] merge = new byte[strip.Length + 4];

            Buffer.BlockCopy(data, 0, merge, 0, 4);
            Buffer.BlockCopy(strip, 0, merge, 4, strip.Length);

            return merge;
        }
    }
}
