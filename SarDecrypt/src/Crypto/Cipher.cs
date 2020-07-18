using System;
using SarDecrypt.Compression;

namespace SarDecrypt.Crypto
{
    class Cipher
    {
        private Blowfish blowfish;
        private readonly string key;

        public Cipher(string key)
        {
            this.key = key;
            this.blowfish = new Blowfish(key);
        }

        public byte[] Decipher(byte[] data, bool compressed)
        {
            int length = (data.Length - 4) / 8;
            byte[] enc = new byte[length * 8];

            for (int i = 0; i < length; i++)
            {
                int os1 = (i * 8) + 4;
                int os2 = i * 8;

                Buffer.BlockCopy(data, os1, enc, os2, 8);
            }

            enc = Utility.byteSwap(enc);
            enc = blowfish.Decrypt_ECB(enc);
            enc = Utility.byteSwap(enc);

            Buffer.BlockCopy(enc, 0, data, 4, enc.Length);

            if (compressed)
            {
                byte[] xor = Utility.xor(Utility.dStrip(data));
                xor = PRS.Decompress(xor);
                xor = Utility.dReplace(data, xor);

                return xor;
            }

            return data;
        }
    }
}
