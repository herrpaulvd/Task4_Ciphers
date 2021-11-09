using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4_Ciphers
{
    /// <summary>
    /// Интерфейс для шифра
    /// </summary>
    interface ICipher
    {
        string Encode(string s);
        string Decode(string s);
    }
}
