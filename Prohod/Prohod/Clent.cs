using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prohod
{
    class Clent
    {
        string le;

        public Clent(string m)
        {
            le = m;
        }

        public string Repl(string m, int key) //замена символа m на символ со смещением
        {
            int pos = le.IndexOf(m);
            if (pos == -1) return ""; //символ в этой ленте не найден 
            pos = (pos + key) % le.Length; //если смещение больше одного круга 
            if (pos < 0) pos += le.Length;
            return le.Substring(pos, 1);
        }
    }
}