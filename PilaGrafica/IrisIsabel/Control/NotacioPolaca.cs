using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IrisIsabel.Control;

namespace IrisIsabel
{
    public class NotacioPolaca
    {
        private static int Precedencia(char op)
        {
            if (op == '+' || op == '-') return 1;
            if (op == '*' || op == '/') return 2;
            return 0;
        }

        public static string InfijoAPostfijo(string infijo)
        {
            Pila<string> pila = new Pila<string>();
            StringBuilder postfix = new StringBuilder();
            StringBuilder numero = new StringBuilder(); 

            for (int i = 0; i < infijo.Length; i++)
            {
                char c = infijo[i];

                if (char.IsWhiteSpace(c))
                {
                    continue;
                }

                if (char.IsDigit(c))
                {
                    numero.Append(c); 
                }
                else
                {
                    if (numero.Length > 0)
                    {
                        postfix.Append(numero.ToString() + " ");
                        numero.Clear(); 
                    }

                    if (c == '(')
                    {
                        pila.Push(c.ToString());
                    }
                    else if (c == ')')
                    {
                        while (!pila.IsEmpty && pila.Peek() != "(")
                        {
                            postfix.Append(pila.Pop() + " ");
                        }

                        if (!pila.IsEmpty && pila.Peek() == "(")
                        {
                            pila.Pop();
                        }
                    }
                    else if (c == '+' || c == '-' || c == '*' || c == '/')
                    {
                        while (!pila.IsEmpty && Precedencia(pila.Peek()[0]) >= Precedencia(c))
                        {
                            postfix.Append(pila.Pop() + " ");
                        }
                        pila.Push(c.ToString());
                    }
                }
            }

            if (numero.Length > 0)
            {
                postfix.Append(numero.ToString() + " ");
            }

            while (!pila.IsEmpty)
            {
                postfix.Append(pila.Pop() + " ");
            }

            return postfix.ToString().Trim();
        }
    }
}
