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

        // Funció per convertir infix a postfix
        public static string InfijoAPostfijo(string infijo)
        {
            Pila<string> pila = new Pila<string>();
            StringBuilder postfix = new StringBuilder();
            StringBuilder numero = new StringBuilder(); // Acumulador de números

            for (int i = 0; i < infijo.Length; i++)
            {
                char c = infijo[i];

                // Si es un espacio, lo ignoramos
                if (char.IsWhiteSpace(c))
                {
                    continue;
                }

                // Si es un dígito, lo agregamos al número
                if (char.IsDigit(c))
                {
                    numero.Append(c);  // Acumulamos el dígito
                }
                else
                {
                    // Si había un número acumulado, lo añadimos al postfix
                    if (numero.Length > 0)
                    {
                        postfix.Append(numero.ToString() + " ");
                        numero.Clear(); // Limpiamos el acumulador de números
                    }

                    // Si es un paréntesis izquierdo, lo añadimos a la pila
                    if (c == '(')
                    {
                        pila.Push(c.ToString());
                    }
                    // Si es un paréntesis derecho, vaciamos la pila hasta encontrar el paréntesis izquierdo
                    else if (c == ')')
                    {
                        while (!pila.IsEmpty && pila.Peek() != "(")
                        {
                            postfix.Append(pila.Pop() + " ");
                        }

                        // Asegurarnos de que no estamos intentando hacer un Pop() sobre una pila vacía
                        if (!pila.IsEmpty && pila.Peek() == "(")
                        {
                            pila.Pop(); // Quitamos el '(' de la pila
                        }
                    }
                    // Si es un operador, manejamos la precedencia
                    else if (c == '+' || c == '-' || c == '*' || c == '/')
                    {
                        // Controlamos la precedencia y vaciamos la pila correctamente
                        while (!pila.IsEmpty && Precedencia(pila.Peek()[0]) >= Precedencia(c))
                        {
                            postfix.Append(pila.Pop() + " ");
                        }
                        pila.Push(c.ToString());
                    }
                }
            }

            // Si quedó un número pendiente, lo añadimos al postfix
            if (numero.Length > 0)
            {
                postfix.Append(numero.ToString() + " ");
            }

            // Vaciar la pila y añadir a postfix el resto de los operadores
            while (!pila.IsEmpty)
            {
                postfix.Append(pila.Pop() + " ");
            }

            return postfix.ToString().Trim();
        }
    }
}
