/************************************
 * 描述：尚未添加描述
 * 作者：吴毅
 * 日期：2015/11/18 11:05:03  
*************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WY.RMS.Component.Tools.helpers
{
    /// <summary>
    /// 加密算法辅助类
    /// </summary>
    public class EncryptionHelper
    {
        /// <summary>
        /// 32位MD5加密
        /// </summary>
        /// <param name="input">未加密前的密码</param>
        /// <returns>加密后的密码</returns>
        public static string GetMd5Hash(string input)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }



        public static string GetMd5Hash(MD5 md5Hash, string input)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
        //用法
        //MD5 md5Hash = MD5.Create();
        //string source = "str";
        //string hash = GetMd5Hash(md5Hash, source);
    }
}
