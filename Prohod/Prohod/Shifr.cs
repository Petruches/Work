using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prohod
{
    class Shifr : System.Collections.Generic.List<Clent>
    {
            public Shifr()
            { //в конструкторе формируется коллекция лент 
                this.Add(new Clent("abcdefghijklmnopqrstuvwxyz"));
                this.Add(new Clent("ABCDEFGHIJKLMNOPQRSTUVWXYZ"));
                this.Add(new Clent("абвгдеёжзийклмнопрстуфхцчшщъыьэюя"));
                this.Add(new Clent("АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ"));
                this.Add(new Clent("0123456789"));
                this.Add(new Clent("!\"#$%^&*()+=-_'?.,|/`~№:;@[]{}"));
            }

            public string Codeс(string m, int key) //кодирование и декодирование в зависимости от знака ключа 
            {
                string res = "", tmp = "";
                for (int i = 0; i < m.Length; i++)
                {
                    foreach (Clent v in this)
                    {
                        tmp = v.Repl(m.Substring(i, 1), key);
                        if (tmp != "") //нужная лента найдена, замена символу определена 
                        {
                            res += tmp;
                            break; // прерывается foreach (перебор лент) 
                        }
                    }
                    if (tmp == "") res += m.Substring(i, 1); //незнакомый символ оставляется без изменений 
                }
                return res;
            }
    }
}