using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Singular.Core.Encryption
{
    //http://stackoverflow.com/questions/165808/simple-two-way-encryption-for-c-sharp#answer-212707
    /// <summary>
    /// EncryptionHelper
    /// </summary>
    public class EncryptionHelper
    {
        // Change these keys
        private readonly ICryptoTransform _decryptorTransform;
        private readonly ICryptoTransform _encryptorTransform;

        private readonly byte[] _key =
        {
            165, 91, 191, 123, 111, 196, 255, 16, 191, 121, 14, 192, 182, 159, 242, 161, 78, 124, 75,
            44, 73, 53, 156, 94, 112, 13, 190, 136, 162, 163, 45, 181
        };

        private readonly UTF8Encoding _utfEncoder;
        private readonly byte[] _vector = {1, 133, 223, 131, 46, 8, 9, 12, 64, 94, 79, 135, 149, 66, 16, 240};

        public EncryptionHelper()
        {
            //This is our encryption method
            var rm = new RijndaelManaged();

            //Create an encryptor and a decryptor using our encryption method, key, and vector.
            _encryptorTransform = rm.CreateEncryptor(_key, _vector);
            _decryptorTransform = rm.CreateDecryptor(_key, _vector);

            //Used to translate bytes to text and vice versa
            _utfEncoder = new UTF8Encoding();
        }

        /// -------------- Two Utility Methods (not used but may be useful) -----------
        /// Generates an encryption key.
        public static byte[] GenerateEncryptionKey()
        {
            //Generate a Key.
            var rm = new RijndaelManaged();
            rm.GenerateKey();
            return rm.Key;
        }

        /// Generates a unique encryption vector
        public static byte[] GenerateEncryptionVector()
        {
            //Generate a Vector
            var rm = new RijndaelManaged();
            rm.GenerateIV();
            return rm.IV;
        }


        /// ----------- The commonly used methods ------------------------------    
        /// Encrypt some text and return a string suitable for passing in a URL.
        public string EncryptToString(string textValue)
        {
            return ByteArrToString(Encrypt(textValue));
        }

        /// Encrypt some text and return an encrypted byte array.
        public byte[] Encrypt(string textValue)
        {
            //Translates our text value into a byte array.
            var bytes = _utfEncoder.GetBytes(textValue);

            //Used to stream the data in and out of the CryptoStream.
            var memoryStream = new MemoryStream();

            /*
         * We will have to write the unencrypted bytes to the stream,
         * then read the encrypted result back from the stream.
         */

            #region Write the decrypted value to the encryption stream

            var cs = new CryptoStream(memoryStream, _encryptorTransform, CryptoStreamMode.Write);
            cs.Write(bytes, 0, bytes.Length);
            cs.FlushFinalBlock();

            #endregion

            #region Read encrypted value back out of the stream

            memoryStream.Position = 0;
            var encrypted = new byte[memoryStream.Length];
            memoryStream.Read(encrypted, 0, encrypted.Length);

            #endregion

            //Clean up.
            cs.Close();
            memoryStream.Close();

            return encrypted;
        }

        /// The other side: Decryption methods
        public string DecryptString(string encryptedString)
        {
            return Decrypt(StrToByteArray(encryptedString));
        }

        /// Decryption when working with byte arrays.
        public string Decrypt(byte[] encryptedValue)
        {
            #region Write the encrypted value to the decryption stream

            var encryptedStream = new MemoryStream();
            var decryptStream = new CryptoStream(encryptedStream, _decryptorTransform, CryptoStreamMode.Write);
            decryptStream.Write(encryptedValue, 0, encryptedValue.Length);
            decryptStream.FlushFinalBlock();

            #endregion

            #region Read the decrypted value from the stream.

            encryptedStream.Position = 0;
            var decryptedBytes = new Byte[encryptedStream.Length];
            encryptedStream.Read(decryptedBytes, 0, decryptedBytes.Length);
            encryptedStream.Close();

            #endregion

            return _utfEncoder.GetString(decryptedBytes);
        }

        /// Convert a string to a byte array.  NOTE: Normally we'd create a Byte Array from a string using an ASCII encoding (like so).
        //      System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
        //      return encoding.GetBytes(str);
        // However, this results in character values that cannot be passed in a URL.  So, instead, I just
        // lay out all of the byte values in a long string of numbers (three per - must pad numbers less than 100).
        public byte[] StrToByteArray(string str)
        {
            if (str.Length == 0)
                throw new Exception("Invalid string value in StrToByteArray");

            var byteArr = new byte[str.Length/3];
            var i = 0;
            var j = 0;
            do
            {
                var val = byte.Parse(str.Substring(i, 3));
                byteArr[j++] = val;
                i += 3;
            } while (i < str.Length);
            return byteArr;
        }

        // Same comment as above.  Normally the conversion would use an ASCII encoding in the other direction:
        //      System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
        //      return enc.GetString(byteArr);    
        public string ByteArrToString(byte[] byteArr)
        {
            var tempStr = "";
            for (var i = 0; i <= byteArr.GetUpperBound(0); i++)
            {
                var val = byteArr[i];
                if (val < 10)
                    tempStr += "00" + val;
                else if (val < 100)
                    tempStr += "0" + val;
                else
                    tempStr += val.ToString();
            }
            return tempStr;
        }
    }
}