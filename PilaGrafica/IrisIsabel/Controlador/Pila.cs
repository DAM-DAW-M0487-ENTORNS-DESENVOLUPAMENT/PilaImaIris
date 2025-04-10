using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisIsabel.Controlador
{
    public class Pila<T> : IEnumerable<T>, ICollection<T>
    {

        private T[] data;
        private int top = -1;
        const int DEFAULT_SIZE = 5;
        private int size;

        #region Constructors
        /// <summary>
        /// Inicialitza una nova instància de la pila amb la mida predeterminada.
        /// </summary>
        public Pila()
        {
            data = new T[DEFAULT_SIZE];
        }

        /// <summary>
        /// Inicialitza una nova instància de la pila amb la mida especificada pel paràmetre <paramref name="size"/>.
        /// </summary>
        /// <param name="size">La capacitat inicial de la pila.</param>
        public Pila(int size)
        {
            this.size = size;
            data = new T[size];
        }

        /// <summary>
        /// Inicialitza una nova instància de la pila a partir d'una col·lecció de tipus <see cref="IEnumerable{T}"/>.
        /// Afegeix cada element de la col·lecció a la pila.
        /// </summary>
        /// <param name="collection">Una col·lecció d'elements per inicialitzar la pila.</param>
        /// <exception cref="ArgumentNullException">Llança una excepció si la col·lecció és null.</exception>
        public Pila(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException("La col·lecció no pot ser null");

            size = collection.Count();
            data = new T[size];
            top = -1;

            foreach (T item in collection)
            {
                Push(item);
            }
        }
        #endregion

        #region Propietats
        /// <summary>
        /// Indica si la pila està plena (no es poden afegir més elements).
        /// </summary>
        public bool IsFull
        { get { return top == data.Length - 1; } }

        /// <summary>
        /// Indica si la pila està buida (no hi ha elements).
        /// </summary>
        public bool IsEmpty
        { get { return top == -1; } }

        /// <summary>
        /// Permet l'accés a un element de la pila per mitjà d'un índex.
        /// </summary>
        /// <param name="index">L'índex de l'element que es vol accedir.</param>
        /// <returns>L'element a l'índex especificat.</returns>
        /// <exception cref="IndexOutOfRangeException">Llança una excepció si l'índex està fora de límits.</exception>
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                    throw new IndexOutOfRangeException("L'índex està fora dels límits");
                return data[index];
            }
        }

        /// <summary>
        /// Obtén el nombre d'elements actuals a la pila.
        /// </summary>
        public int Count
        { get { return top + 1; } }

        /// <summary>
        /// Obtén la capacitat total de la pila (el nombre màxim d'elements que es poden emmagatzemar).
        /// </summary>
        public int Capacity
        { get { return data.Length; } }

        /// <summary>
        /// Indica si la pila és de només lectura.
        /// </summary>
        public bool IsReadOnly => throw new NotImplementedException();

        #endregion

        #region Metodes
        /// <summary>
        /// Afegeix un element a la pila. Com que tenim push, es crida aquest a l'hora d'afegir nous elements
        /// </summary>
        /// <param name="item">L'element a afegir.</param>
        public void Add(T item) => Push(item);

        /// <summary>
        /// Elimina tots els elements de la pila.
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i <= top; i++)
            {
                data[i] = default;
            }
            top = -1;
        }

        /// <summary>
        /// Determina si un element està contingut a la pila
        /// </summary>
        /// <param name="item">L'element que es vol cercar.</param>
        /// <returns>True si l'element existeix a la pila i false si no existeix.</returns>
        public bool Contains(T item)
        {
            bool found = false;
            for (int i = 0; i <= top; i++)
            {
                if (data[i].Equals(item))
                {
                    found = true;
                }
            }
            return found;
        }

        /// <summary>
        /// Copia els elements de la pila a un array a partir d'un índex especificat.
        /// </summary>
        /// <param name="array">L'array on es copiaran els elements.</param>
        /// <param name="arrayIndex">L'índex d'inici a l'array on començar a copiar.</param>
        /// <exception cref="ArgumentNullException">Llança una excepció si l'array és null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Llança una excepció si l'índex és menor que 0.</exception>
        /// <exception cref="ArgumentException">Llança una excepció si l'array no té prou espai per als elements.</exception>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array is null) throw new ArgumentNullException("Array es null");
            else if (arrayIndex < 0) throw new ArgumentOutOfRangeException("Esta fora del rang permes");
            else if (arrayIndex + Count > array.Length) throw new ArgumentException("L'array no té prou espai per a copiar els elements.");

            foreach (T item in this)
            {
                array[arrayIndex++] = item;
            }
        }

        /// <summary>
        /// Elimina un element de la pila. No implementat ja que tenim els metodes de la nested class que ho poden fer.
        /// </summary>
        /// <param name="item">L'element que es vol eliminar.</param>
        /// <returns>True si l'element s'ha eliminat correctament, false en cas contrari.</returns>
        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Obté un enumerador que permet iterar sobre els elements de la pila.
        /// </summary>
        /// <returns>Un enumerador per iterar a través de la pila.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return new EnumeradorPila(this);
        }

        /// <summary>
        /// Classe interna per implementar el comportament d'un enumerador per la pila.
        /// </summary>
        private class EnumeradorPila : IEnumerator<T>
        {
            private Pila<T> pila;
            private int index;

            /// <summary>
            /// Inicialitza una nova instància de l'enumerador.
            /// </summary>
            /// <param name="pila">La pila que es vol iterar.</param>
            public EnumeradorPila(Pila<T> pila)
            {
                this.pila = pila;
                this.index = pila.Count;
            }

            /// <summary>
            /// Obté l'element actual de la pila durant la iteració.
            /// </summary>
            /// <exception cref="IndexOutOfRangeException">Llança una excepció si l'índex està fora dels límits.</exception>
            public T Current
            {
                get
                {
                    if (index < 0 || index >= pila.Count)
                        throw new IndexOutOfRangeException("L'index esta fora dels limits");
                    return pila[index];

                }
            }

            /// <summary>
            /// Obté l'objecte actual de la pila durant la iteració (implementació per a la interfície IEnumerator).
            /// </summary>
            object IEnumerator.Current => Current;

            /// <summary>
            /// Allibera els recursos utilitzats per l'enumerador.
            /// En aquest cas, no cal fer res específic, però és necessari implementar-ho per a la interfície.
            /// </summary>
            public void Dispose()
            {
                throw new NotImplementedException();
            }

            /// <summary>
            /// Passa a l'element següent durant la iteració.
            /// </summary>
            /// <returns>True si hi ha un següent element a la pila, False si s'ha arribat al final de la pila.</returns>
            public bool MoveNext()
            {
                index--;
                return index >= 0;
            }

            /// <summary>
            /// Restaura l'índex de l'enumerador al seu valor inicial (la part superior de la pila).
            /// </summary>
            public void Reset()
            {
                index = pila.Count;
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Elimina i retorna l'element superior de la pila.
        /// </summary>
        /// <returns>L'element eliminat de la pila.</returns>
        /// <exception cref="InvalidOperationException">Llança una excepció si la pila està buida.</exception>
        public T Pop()
        {
            if (top == -1) throw new InvalidOperationException("No hi ha res a l'stack");
            T item = data[top];
            top--;
            data[top] = default;  // Esborrem la referència de l'element
            return item;
        }

        /// <summary>
        /// Retorna l'element superior de la pila sense eliminar-lo.
        /// </summary>
        /// <returns>L'element superior de la pila.</returns>
        /// <exception cref="InvalidOperationException">Llança una excepció si la pila està buida.</exception>
        public T Peek()
        {
            if (top == -1) throw new InvalidOperationException("No hi ha res a l'stack");
            return data[top];
        }

        /// <summary>
        /// Afegeix un element a la pila.
        /// </summary>
        /// <param name="item">L'element a afegir.</param>
        /// <exception cref="StackOverflowException">Llança una excepció si la pila està plena.</exception>
        public void Push(T item)
        {
            if (top == data.Length - 1) throw new StackOverflowException("La pila esta plena");
            data[++top] = item; // s'incrementa top abans d'afegir l'element
        }

        /// <summary>
        /// Converteix la pila en un array.
        /// </summary>
        /// <returns>Un array amb els elements de la pila.</returns>
        public T[] ToArray()
        {
            T[] array = new T[Count];
            int index = 0;

            foreach (T item in this)
            {
                array[index++] = item;
            }
            return array;
        }

        /// <summary>
        /// Assegura que la pila té una capacitat mínima per allotjar un nombre especificat d'elements.
        /// </summary>
        /// <param name="newCapacity">La nova capacitat desitjada.</param>
        /// <returns>La nova capacitat de la pila.</returns>
        public int EnsureCapacity(int newCapacity)
        {
            int newSize = data.Length;

            if (newSize < newCapacity)
            {
                if (newCapacity > newSize * 2)
                {
                    newSize = newCapacity;
                }
                else
                {
                    newSize = newSize * 2;
                }

                T[] newData = new T[newSize];
                Array.Copy(data, newData, Count);
                data = newData;
            }
            return data.Length;
        }

        /// <summary>
        /// Retorna una representació en cadena dels elements de la pila.
        /// </summary>
        /// <returns>Una cadena que representa els elements de la pila.</returns>
        public override string ToString()
        {
            IEnumerator<T> enumerator = GetEnumerator();
            StringBuilder strbuild = new StringBuilder();

            strbuild.Append("[");

            bool first = true;

            while (enumerator.MoveNext())
            {
                if (!first)
                {
                    strbuild.Append(", ");
                }
                strbuild.Append(enumerator.Current.ToString());
                first = false;
            }

            strbuild.Append("]");

            return strbuild.ToString();
        }

        public override bool Equals(object? obj)
        {
            if (obj is null || obj is not Pila<T> altraPila || altraPila.Count != this.Count)
                return false;

            for (int i = 0; i <= top; i++)
                if (!this[i].Equals(altraPila[i]))
                    return false;

            return true;
        }
        #endregion

    }
}
