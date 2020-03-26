using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Security;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.IO;

namespace Oaww.Utility
{
    public  static class SecurityExtension
    {
        private const string salt = "+LoOWf5Fqxjfsa5BM0jq9w==";
       
        public static string AntiXss(this string value)
        {
            return Microsoft.Security.Application.Encoder.HtmlEncode(value);
        }

        public static string safeHtmlFragment(this string value)
        {
            string html =  Microsoft.Security.Application.Sanitizer.GetSafeHtmlFragment(value);

            string input = html;
            Match match = Regex.Match(input, "class=\"(x_).+\"", RegexOptions.IgnoreCase);

            if (match.Success)
            {
                string key = match.Groups[1].Value;
                return input.Replace(key, "");
            }
            return html;
        }

        public static string SecureStringToString(this SecureString value)
        {
            var valuePtr = IntPtr.Zero;
            try
            {
               
                 valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);

                return Marshal.PtrToStringUni(valuePtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }

        public static SecureString SecureString(this string value)
        {
            SecureString secureString = new SecureString();
            foreach (char c in value)
            {
                secureString.AppendChar(c);
            }

            return secureString;

            
        }

        public static string GetMD5(this string sstr)
        {
            string salted = "Base";
            byte[] Original = Encoding.Default.GetBytes(sstr);
            byte[] SaltValue = Encoding.Default.GetBytes(salted);
            byte[] ToSalt = new byte[Original.Length + SaltValue.Length];
            Original.CopyTo(ToSalt, 0);
            SaltValue.CopyTo(ToSalt, Original.Length);
            System.Security.Cryptography.MD5 st = System.Security.Cryptography.MD5.Create();
            byte[] SaltMD5 = st.ComputeHash(ToSalt);
            byte[] PWD = new byte[SaltMD5.Length + SaltValue.Length];
            SaltMD5.CopyTo(PWD, 0);
            SaltValue.CopyTo(PWD, SaltMD5.Length);
            return Convert.ToBase64String(PWD);
        }

        public static string NewLineReplace(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return "";
            else
                return value.Replace("\n", "").Replace("\r", "").Replace("\x0a", "").Replace("\x0d", "");
        }

        public static bool PwdRepeat(this string value)
        {
            char[] mypXD = value.Trim().ToCharArray(0, value.Trim().Length);
            char tempChar = '↑';
            int myCount = 0;

            foreach (char everyOne in mypXD)
            {
                if (everyOne == tempChar)
                {
                    myCount++;
                    tempChar = everyOne;
                    if (myCount >= 1)
                        return false;
                }
                else
                {
                    tempChar = everyOne;
                    myCount = 0;
                }
            }

            return true;
        }

        public static bool PwdContinuous(this string value)
        {
            char[] mypXD = value.Trim().ToCharArray(0, value.Trim().Length);
            char tempChar = '↑';
            int myCount = 0;

            foreach (char everyOne in mypXD)
            {
                if (everyOne.CompareTo(tempChar) == 1)
                {
                    myCount++;
                    tempChar = everyOne;
                    if (myCount >= 1)
                        return false;
                }
                else
                {
                    tempChar = everyOne;
                    myCount = 0;
                }
            }

            return true;
        }

        public static string DecryptStringAES(this string value)
        {
            var keybytes = Encoding.UTF8.GetBytes("D*G-KaPdSgVkYp3s");   //自行設定，但要與JavaScript端 一致
            var iv = Encoding.UTF8.GetBytes("D(G+KaPdSgVkYp3s"); // 自行設定，但要與JavaScript端 一致

            var encrypted = Convert.FromBase64String(value);
            var decriptedFromJavascript = DecryptStringFromBytes(encrypted, keybytes, iv);
            return string.Format(decriptedFromJavascript);
        }

        private static string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
        {
            if (cipherText == null || cipherText.Length <= 0)
            {
                throw new ArgumentNullException("cipherText");
            }
            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            string plaintext = null;
            using (var rijAlg = new RijndaelManaged())
            {
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;
                rijAlg.Key = key;
                rijAlg.IV = iv;
                var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);
                try
                {
                    using (var msDecrypt = new MemoryStream(cipherText))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                plaintext = srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
                catch
                {
                    plaintext = "keyError";
                }
            }
            return plaintext;
        }

        public static string EncodePassword(this string pass)
        {


            byte[] bIn = Encoding.Unicode.GetBytes(pass);
            byte[] bSalt = Convert.FromBase64String(salt);
            byte[] bAll = new byte[bSalt.Length + bIn.Length];
            byte[] bRet = null;

            Buffer.BlockCopy(bSalt, 0, bAll, 0, bSalt.Length);
            Buffer.BlockCopy(bIn, 0, bAll, bSalt.Length, bIn.Length);

            using (SHA256 s = SHA256.Create())
            {
                bRet = s.ComputeHash(bAll);
            }

            return Convert.ToBase64String(bRet);
        }

        
    }
}
