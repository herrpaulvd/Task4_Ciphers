using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4_Ciphers
{
    /// <summary>
    /// Шифр Атбаш
    /// </summary>
    class Atbash : ICipher
    {
        public string Decode(string s)
        {
            return Encode(s);
        }

        private static string abc = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";

        private static char Convert(char c)
        {
            if (abc.Contains(c))
                return abc[abc.Length - 1 - abc.IndexOf(c)];
            return c;
        }

        public string Encode(string s)
        {
            StringBuilder result = new();
            foreach (var c in s)
                result.Append(Convert(c));
            return result.ToString();
        }
    }
}
