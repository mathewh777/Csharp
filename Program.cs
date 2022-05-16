using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace c_source
{
    class check 
    {
        public string Decrypt(byte[] text_, byte[] key, byte[] iv)
        {
             AesManaged _cipher = MainCip();
             _cipher.Key = key;
             _cipher.IV = iv;
            ICryptoTransform transform_t = _cipher.CreateDecryptor();
            byte[] decText = transform_t.TransformFinalBlock(text_, 0, text_.Length);
            string data = Convert.ToBase64String(decText);
            string bytedata = BitConverter.ToString(decText);
            return bytedata;
        }
        public (byte[], byte[], byte[]) Encrypt(byte[] Text)
        {
            AesManaged _cipher = initcip();
            _cipher.GenerateIV();
            byte[] iv = _cipher.IV;
            //Console.WriteLine(BitConverter.ToString(Text));
            ICryptoTransform transform = _cipher.CreateEncryptor();
            byte[] cipherText = transform.TransformFinalBlock(Text, 0, Text.Length);
            //string dataToSave = Convert.ToBase64String(cipherText);
            //string encr = BitConverter.ToString(Convert.FromBase64String(dataToSave));
            return (cipherText, iv, _cipher.Key);
        }
        private AesManaged CreateCip() //string key, string IV)
        {
            AesManaged cipher = new AesManaged(); // new object
            cipher.Mode = CipherMode.CBC;
            cipher.Padding = PaddingMode.Zeros;
            cipher.BlockSize = 128;
            cipher.KeySize = 256;
            return cipher;
        }
       private AesManaged MainCip()
        {
             AesManaged cipher = CreateCip();
             cipher.GenerateKey();
             return cipher;
        }
        public AesManaged initcip()
        {
            AesManaged _cipher = null;
            _cipher = MainCip();
            return _cipher;
        }

    }
    class Program
    {
        //copyed from github <<EO
        public static byte[] FromHex(string hex)
        {
        hex = hex.Replace("-", "");
        byte[] raw = new byte[hex.Length / 2];
        for (int i = 0; i < raw.Length; i++)
        {
            raw[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
        }
            return raw;
        }
        //EO
        static void Main(string[] args)
        {
            check check1 = new check();
            var value = check1.Encrypt(Encoding.UTF8.GetBytes("Text To Encrypt"));
            var encr_msg = value.Item1;
            //just use bitconvert then fromhex, acsi to see hex
            Console.WriteLine(Encoding.ASCII.GetString(FromHex(BitConverter.ToString(encr_msg))));
            //Decrypt Text 
            var iv = FromHex(BitConverter.ToString(value.Item2));
            var key = value.Item3;
            var dec_msg = check1.Decrypt(encr_msg, key, iv);
            Console.WriteLine(Encoding.ASCII.GetString(FromHex(dec_msg))); 
            Console.ReadKey();
        }
    }
}
