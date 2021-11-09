using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4_Ciphers
{
    /// <summary>
    /// Шифр Марк
    /// </summary>
    class Mark : ICipher
    {
        private static string PrimaryString = "сеновал";
        private static string[] SecondaryStrings =
        {
            "бгджзийкм",
            "ртуфхцчшщ",
            "ыьэюя.а" // 'а' в этой строке не будет искаться, поэтому мы поставим её здесь вместо '/' для упрощения кода
        };

        private static int charToNumber(char c)
            => c == '0' ? 10 : (c - '0');
        private static char numberToChar(int c)
            => c == 10 ? '0' : (char)(c + '0');

        public string Decode(string s)
        {
            StringBuilder result = new();
            bool numeric = false;

            for(int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                if (numeric)
                {
                    if (c == '8' && result.Length > 0 && result[^1] == '0')
                    {
                        result.Remove(result.Length - 1, 1);
                        numeric = false;
                    }
                    else if ('0' <= c && c <= '9')
                        result.Append(c);
                    else
                        throw new Exception("Invalid message: non-digit in numeric array");
                }
                else if ('1' <= c && c <= '7')
                    result.Append(PrimaryString[c - '1']);
                else if ('0' <= c && c <= '9')
                {
                    if (i + 1 < s.Length)
                    {
                        char c2 = s[i + 1];
                        if ('0' <= c2 && c2 <= '9' && c2 != '1')
                        {
                            char r = SecondaryStrings[charToNumber(c) - 8][charToNumber(s[i + 1]) - 2];
                            if (r == 'а')
                                numeric = true;
                            else
                                result.Append(r);
                            i++;
                        }
                        else
                            throw new Exception("Invalid message: invalid char after 8|9|0");
                    }
                    else
                        throw new Exception("Invalid message: unclosed char 8|9|0");
                }
                else
                    result.Append(c);
            }

            if (numeric)
                throw new Exception("Invalid message: Unclosed numeric array");
            return result.ToString();
        }

        public string Encode(string s)
        {
            StringBuilder result = new();
            bool numeric = false;
            foreach(var c in s)
            {
                if('0' <= c && c <= '9')
                {
                    if(!numeric)
                    {
                        result.Append("08");
                        numeric = true;
                    }
                    result.Append(c);
                }
                else
                {
                    if(numeric)
                    {
                        result.Append("08");
                        numeric = false;
                    }

                    if (PrimaryString.Contains(c))
                        result.Append(numberToChar(PrimaryString.IndexOf(c) + 1));
                    else
                    {
                        bool found = false;
                        for(int i = 0; i < SecondaryStrings.Length; i++)
                            if(SecondaryStrings[i].Contains(c))
                            {
                                result.Append(numberToChar(i + 8));
                                result.Append(numberToChar(SecondaryStrings[i].IndexOf(c) + 2));
                                found = true;
                            }
                        if (!found)
                            result.Append(c);
                    }
                }
            }
            if(numeric)
                result.Append("08");
            return result.ToString();
        }
    }
}
