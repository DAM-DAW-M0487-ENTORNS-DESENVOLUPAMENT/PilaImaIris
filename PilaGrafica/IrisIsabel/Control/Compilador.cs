using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IrisIsabel.Control;

namespace IrisIsabel
{
    public class Compilador
    {
        public bool Validar(string expressio)
        {
            Pila<string> pila = new Pila<string>();
            bool res = true;

            foreach (char c in expressio)
            {
                string simbol = c.ToString();
                if (Obre(simbol))
                {
                    pila.Push(simbol);
                }
                else if (Tanca(simbol))
                {
                    if (pila.Count == 0)
                        res = false;
                    string top = pila.Pop();
                    if (!Coincideixen(top, simbol))
                        res = false;
                }
            }
            return res;
        }
        private bool Obre(string simbol)
        {
            bool s;
            if (simbol == "(" || simbol == "{" || simbol == "[")
                s = true;
            else
                s = false;
            return s;
        }
        private bool Tanca(string simbol)
        {
            bool s;
            if (simbol == ")" || simbol == "}" || simbol == "]")
                s = true;
            else
                s = false;
            return s;
        }
        private bool Coincideixen(string sObre, string sTanca)
        {
            bool con;
            if (sObre == "(" && sTanca == ")" || sObre == "{" && sTanca == "}" || sObre == "[" && sTanca == "]")
                con = true;
            else
                con = false;
            return con;
        }

    }
}
