using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using CloudBreadLib.BAL.Base64;

namespace CloudBreadLib.BAL.Crypto
{
    public class Crypto
    {

        public static string SHA512HashTemp(string Data)
        {
            SHA512 sha = new SHA512Managed();
            byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(Data));
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in hash)
            {
                stringBuilder.AppendFormat("{0:x2}", b);
            }
            return Base64.Base64.Base64Encode(stringBuilder.ToString());
        }

        //sha512
        public static string SHA512Hash(string Phrase)
        {
            try
            {
                SHA512Managed HashTool = new SHA512Managed();
                Byte[] PhraseAsByte = System.Text.Encoding.UTF8.GetBytes(string.Concat(Phrase));
                Byte[] EncryptedBytes = HashTool.ComputeHash(PhraseAsByte);
                HashTool.Clear();
                return Convert.ToBase64String(EncryptedBytes);
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public static string MD5Hash(string input)
        {
            try
            {
                MD5 md5 = System.Security.Cryptography.MD5.Create();
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);     //ASCII
                byte[] hash = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("X2"));      //소문자로 처리 = x2
                }
                return sb.ToString();
            }
            catch (Exception)
            {
                throw;
            }
            
        }


        // 대칭키 AES256
        public static string AES_encrypt(string Input, string key, string IV)
        {
            try
            {
                RijndaelManaged aes = new RijndaelManaged();
                aes.KeySize = 256;
                aes.BlockSize = 128;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = Encoding.UTF8.GetBytes(key);           //UTF8 값이기 때문에 1=49 로 처리됨. string을 byte로 변환해 넘길때 주의
                aes.IV = Encoding.UTF8.GetBytes(IV); 

                var encrypt = aes.CreateEncryptor(aes.Key, aes.IV);
                byte[] xBuff = null;
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encrypt, CryptoStreamMode.Write))
                    {
                        byte[] xXml = Encoding.UTF8.GetBytes(Input);
                        cs.Write(xXml, 0, xXml.Length);
                    }

                    xBuff = ms.ToArray();
                }

                string Output = Convert.ToBase64String(xBuff);
                return Output;
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }

        // 대칭키 AES256
        public static string AES_decrypt(string Input, string key, string IV)
        {
            try
            {
                RijndaelManaged aes = new RijndaelManaged();
                aes.KeySize = 256;
                aes.BlockSize = 128;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = Encoding.UTF8.GetBytes(IV);

                var decrypt = aes.CreateDecryptor();
                byte[] xBuff = null;
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, decrypt, CryptoStreamMode.Write))
                    {
                        byte[] xXml = Convert.FromBase64String(Input);
                        cs.Write(xXml, 0, xXml.Length);
                    }

                    xBuff = ms.ToArray();
                }

                string Output = Encoding.UTF8.GetString(xBuff);
                return Output;
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }

    }
}
