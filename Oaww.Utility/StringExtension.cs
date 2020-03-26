using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.IO;

namespace Oaww.Utility
{
    public static class StringExtension
    {
        private const string ENC = "+LoOWf5Fqxjfsa5BM0jq9w==";
        private const string ENCIV = "BTbeVr15PGcMLmty0BTbeVr15PGcMLmty0";
        static readonly Regex YoutubeVideoRegex = new Regex(@"youtu(?:\.be|be\.com)/(?:(.*)v(/|=)|(.*/)?)([a-zA-Z0-9-_]+)", RegexOptions.IgnoreCase);
        /// <summary>
        /// 字串加密(非對稱式)
        /// </summary>
        /// <param name="Source">加密前字串</param>
        /// <param name="CryptoKey">加密金鑰</param>
        /// <returns>加密後字串</returns>
        public static string aesEncryptBase64(this string SourceStr)
        {
            string encrypt = "";
            try
            {
                RijndaelManaged aes = new RijndaelManaged() { BlockSize = 256, KeySize = 256 };
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider();
                byte[] key = sha256.ComputeHash(Encoding.UTF8.GetBytes(ENC));
                byte[] iv = sha256.ComputeHash(Encoding.UTF8.GetBytes(ENCIV));
                aes.Key = key;
                aes.IV = iv;

                byte[] dataByteArray = Encoding.UTF8.GetBytes(SourceStr);
                using (MemoryStream ms = new MemoryStream())
                using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(dataByteArray, 0, dataByteArray.Length);
                    cs.FlushFinalBlock();
                    encrypt = Convert.ToBase64String(ms.ToArray());
                }
            }
            catch (Exception e)
            {

            }
            return encrypt;
        }

        /// <summary>
        /// 字串解密(非對稱式)
        /// </summary>
        /// <param name="Source">解密前字串</param>
        /// <param name="CryptoKey">解密金鑰</param>
        /// <returns>解密後字串</returns>
        public static string aesDecryptBase64(this string SourceStr)
        {
            string decrypt = "";
            try
            {
                RijndaelManaged aes = new RijndaelManaged() { BlockSize = 256, KeySize = 256 };
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider();
                byte[] key = sha256.ComputeHash(Encoding.UTF8.GetBytes(ENC));
                byte[] iv = sha256.ComputeHash(Encoding.UTF8.GetBytes(ENCIV));
                aes.Key = key;
                aes.IV = iv;

                byte[] dataByteArray = Convert.FromBase64String(SourceStr);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(dataByteArray, 0, dataByteArray.Length);
                        cs.FlushFinalBlock();
                        decrypt = Encoding.UTF8.GetString(ms.ToArray());
                    }
                }
            }
            catch (Exception e)
            {

            }
            return decrypt;
        }
        public static string ToNotSet(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return "未設定";
            }
            else
            {
                return value;
            }


        }

        public static string ToEmpty(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return "";
            }
            else
            {
                return value;
            }


        }

        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static string IsNullOrEmpty(this string value, string defaultValue)
        {
            return string.IsNullOrWhiteSpace(value) ? defaultValue : "";
        }

        public static string NullTrim(this string value)
        {
            if (value == null)
            {
                return "";
            }
            else
            {
                return value.Trim();
            }
        }

        public static string ZeroValue(this string value)
        {
            return string.IsNullOrEmpty(value) || value == "--" ? "--" : double.Parse(value) == 0 ? "--" : string.Format("{0:0.000}", double.Parse(value));
        }

        public static string NameBeautiful(this string value)
        {
            if (value != null && value.Length > 2)
            {
                return value.Substring(0, 1) + "**";
            }
            else
            {
                return "***";
            }
        }

        public static string PhoneBeautiful(this string value)
        {
            if (value != null && value.Length > 6)
            {
                return value.Substring(0, 3) + "*".PadLeft(value.Length - 6, '*') + value.Substring(value.Length - 3, 3);
            }
            else
            {
                return value;
            }
        }
        public static string EmailBeautiful(this string value)
        {
            int index = value.IndexOf('@');

            if (index > 0)
            {
                string replacestring = value.Substring(index - 3, 3);
                return value.Replace(replacestring, "***");
            }
            else
            {
                return value;
            }
        }

        public static string ToChristianDateString(this string taiwainDate, string format)
        {
            if (string.IsNullOrEmpty(taiwainDate.Trim())) return "";

            int year = 0;
            int month = 0;
            int day = 0;
            string token = "/-";
            char[] separator = token.ToCharArray();
            string[] result = taiwainDate.Split(separator);

            if (result.Length == 3)
            {
                year = int.Parse(result[0]);
                month = int.Parse(result[1]);
                day = int.Parse(result[2]);
            }
            else
            {
                switch (taiwainDate.Length)
                {
                    case 7://yyyMMdd
                        year = int.Parse(taiwainDate.Substring(0, 3));
                        month = int.Parse(taiwainDate.Substring(3, 2));
                        day = int.Parse(taiwainDate.Substring(5, 2));
                        break;
                    case 6://yyMMdd
                        year = int.Parse(taiwainDate.Substring(0, 2));
                        month = int.Parse(taiwainDate.Substring(2, 2));
                        day = int.Parse(taiwainDate.Substring(4, 2));
                        break;
                    default:
                        return string.Empty;
                }
            }

            year += 1911;

            if (format == "yyyyMMdd")
                return (year.ToString("D4") + month.ToString("D2") + day.ToString("D2"));
            else
                return (year.ToString("D4") + "/" + month.ToString("D2") + "/" + day.ToString("D2"));
        }


        public static string StripHTML(this string input)
        {
            if (input.IsNullOrEmpty())
            {
                return "";
            }
            else
            return Regex.Replace(input, "<.*?>", String.Empty);
        }

        public static string UrlToEmbedCode(this string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                var youtubeMatch = YoutubeVideoRegex.Match(url);

                if (youtubeMatch.Success)
                {
                    return getYoutubeEmbedCode(youtubeMatch.Groups[youtubeMatch.Groups.Count - 1].Value);
                }

            }

            return url;
        }

        const string youtubeEmbedFormat = @"https://www.youtube.com/embed/{0}";

        private static string getYoutubeEmbedCode(string youtubeId)
        {
            return string.Format(youtubeEmbedFormat, youtubeId);
        }
    }
}
