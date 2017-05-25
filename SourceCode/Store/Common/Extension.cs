using System;

namespace Common
{
    /// <summary>
    /// Extension function
    /// </summary>
    public static class Extension
    {
        public static readonly string NUMBER_FORMAT = "#,###.#";

        private static string[] VietNamChar = new string[]
       {
           "aAeEoOuUiIdDyY",
           "áàạảãâấầậẩẫăắằặẳẵ",
           "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
           "éèẹẻẽêếềệểễ",
           "ÉÈẸẺẼÊẾỀỆỂỄ",
           "óòọỏõôốồộổỗơớờợởỡ",
           "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
           "úùụủũưứừựửữ",
           "ÚÙỤỦŨƯỨỪỰỬỮ",
           "íìịỉĩ",
           "ÍÌỊỈĨ",
           "đ",
           "Đ",
           "ýỳỵỷỹ",
           "ÝỲỴỶỸ"
       };

        /// <summary>
        /// Convert date to string
        /// </summary>
        /// <param name="date">datetime</param>
        /// <param name="format">format to string</param>
        /// <returns>string</returns>
        public static string ConvertDateToString(this DateTime date, string format)
        {
            return date.ToString(format);
        }

        /// <summary>
        /// Format double number to string
        /// </summary>
        /// <param name="value">double value</param>
        /// <returns>Format type #,###.#</returns>
        public static string ConvertNumberToString(this double value)
        {
            return value.ToString(NUMBER_FORMAT);
        }

        /// <summary>
        /// Format int value to string
        /// </summary>
        /// <param name="value">Integer value</param>
        /// <returns>Format type #,###.#</returns>
        public static string ConvertNumberToString(this int value)
        {
            return value.ToString(NUMBER_FORMAT);
        }

        /// <summary>
        /// Format float value to string
        /// </summary>
        /// <param name="value">Float value</param>
        /// <returns>Format type #,###.#</returns>
        public static string ConvertNumberToString(this float value)
        {
            return value.ToString(NUMBER_FORMAT);
        }

        /// <summary>
        /// Format decimal value to string
        /// </summary>
        /// <param name="value">Decimal value</param>
        /// <returns>Format type #,###.#</returns>
        public static string ConvertNumberToString(this decimal value)
        {
            return value.ToString(NUMBER_FORMAT);
        }

        /// <summary>
        /// Use for url website
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ConvertToTitleToAlias(this string str)
        {
            return removeSpecialChars(replaceUnicode(str)).Replace(' ', '-');
        }

        /// <summary>
        /// Replace string to unicode
        /// </summary>
        /// <param name="strInput"></param>
        /// <returns></returns>
        public static string replaceUnicode(string strInput)
        {
            for (int i = 1; i < VietNamChar.Length; i++)
            {
                for (int j = 0; j < VietNamChar[i].Length; j++)
                {
                    strInput = strInput.Replace(VietNamChar[i][j], VietNamChar[0][i - 1]);
                }
            }
            return strInput;
        }

        /// <summary>
        /// Remove special characters string.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string removeSpecialChars(string str)
        {
            //string[] chars = new string[] { ",", "-", ".", "/", "!", "@", "#", "$", "%", "^", "&", "*", "'", "\"", ";", " ", "(", ")", ":", "|", "[", "]" };
            string[] chars = new string[] { ",", "-", ".", "/", "!", "@", "#", "$", "%", "^", "&", "*", "'", "\"", ";", "(", ")", ":", "|", "[", "]", "?", "+", "=", "{", "}", "`", "~", "<", ">" };

            for (int i = 0; i < chars.Length; i++)
            {
                if (str.Contains(chars[i]))
                {
                    str = str.Replace(chars[i], "");
                }
            }
            return str.Trim();
        }

        /// <summary>
        /// Convert base64 to byte base on image extension
        /// </summary>
        /// <param name="base64">base64 string</param>
        /// <param name="type">Image type</param>
        /// <returns></returns>
        public static byte[] ConvertBase64ToByte(this string base64, string type)
        {
            if (type == "png")
                return Convert.FromBase64String(base64.Replace("data:image/png;base64,", ""));
            if (type == "jpeg" || type == "jpg")
                return Convert.FromBase64String(base64.Replace("data:image/jpeg;base64,", ""));
            return Convert.FromBase64String(base64.Replace("data:image/png;base64,", ""));

        }
    }
}
