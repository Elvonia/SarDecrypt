using SarDecrypt.Crypto;

namespace SarDecrypt.FileFormat
{
    class SarFile
    {
        private Cipher cipher;

        private readonly string key = "0907c12b";
        private readonly byte cByte = 0x84;

        private bool compressed;

        public byte[] data;

        public SarFile(byte[] data)
        {
            this.data = data;
            cipher = new Cipher(key);
            if (data[3] == cByte)
            {
                compressed = true;
            }
        }

        public void Decrypt()
        {
            data = cipher.Decipher(data, compressed);
        }

        public void Encrypt()
        {
            //data = cipher.Encipher(data);
        }

        public string getKey()
        {
            return key;
        }

    }
}
