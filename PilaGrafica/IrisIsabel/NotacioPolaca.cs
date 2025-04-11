using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisIsabel
{
    internal class NotacioPolaca
    {// Funció per obtenir la precedencia dels operadors
        private static int Precedencia(char op)
        {
            if (op == '+' || op == '-') return 1;
            if (op == '*' || op == '/') return 2;
            return 0;
        }

        // Funció per convertir infix a postfix
        public static string InfijoAPostfijo(string infijo)
        {
            Pila<string> pila = new Pila<string>();  
            StringBuilder postfix = new StringBuilder();

            for (int i = 0; i < infijo.Length; i++)
            {
                char c = infijo[i];

                if (char.IsDigit(c))
                {
                    postfix.Append(c);
                }
                else if (c == '(')
                {
                    pila.Push(c.ToString());  
                }
                
                else if (c == ')')
                {
                    while (!pila.IsEmpty && pila.Peek() != "(")
                    {
                        postfix.Append(pila.Pop());
                    }
                    pila.Pop();
                }
                
                else if (c == '+' || c == '-' || c == '*' || c == '/')
                {
                    while (!pila.IsEmpty && Precedencia(pila.Peek()[0]) >= Precedencia(c))  
                    {
                        postfix.Append(pila.Pop());
                    }
                    pila.Push(c.ToString()); 
                }
            }

            while (!pila.IsEmpty)
            {
                postfix.Append(pila.Pop());
            }

            return postfix.ToString();
        }
    }
}
