using System;
using System.Text;
using System.Security.Cryptography;

namespace Struct.Core
{
    public static class Encrypter
    {
        private static string iv = "7dbfd6e0-1276-486a-9553-46fe48ea221c";

        public static byte[] Encrypt(string unEncryptedString, string key)
        {
            // create aes object
            using (AesManaged am = new AesManaged())
            {
                am.Padding = PaddingMode.PKCS7;
                am.BlockSize = 128;
                am.KeySize = 256;
                am.Key = key.ToBytes(32);
                am.IV = iv.ToBytes(16);

                // create the encrytor
                ICryptoTransform encryptor = am.CreateEncryptor();

                try
                {
                    // convert string to bytes
                    byte[] data = Encoding.UTF8.GetBytes(unEncryptedString);

                    // encrypt bytes and return
                    return encryptor.TransformFinalBlock(data, 0, data.Length);
                }
                finally
                {
                    encryptor.Dispose();
                }
            }
        }

        public static string Decrypt(byte[] encryptedBytes, string key)
        {
            // create aes object
            using (AesManaged am = new AesManaged())
            {
                am.Padding = PaddingMode.PKCS7;
                am.BlockSize = 128;
                am.KeySize = 256;
                am.Key = key.ToBytes(32);
                am.IV = iv.ToBytes(16);

                // create the decryptor
                ICryptoTransform decryptor = am.CreateDecryptor();

                try
                {
                    // decrypt and return converted data
                    return Encoding.UTF8.GetString(
                        decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length)
                    );
                }
                finally
                {
                    decryptor.Dispose();
                }
            }
        }

        private static byte[] ToBytes(this string val, int length)
        {
            if (string.IsNullOrWhiteSpace(val))
                throw new ArgumentNullException("val");
            else if (length < 1)
                throw new Exception("Length must be greater than zero.");

            // concat the string out to the desired length or more
            while (val.Length < length)
                val += val;

            // grab the desired length and return converted
            return Encoding.UTF8.GetBytes(val.Substring(0, length));
        }
    }
}
