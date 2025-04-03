using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisIsabel.Controlador
{
    /// <summary>
    /// Representa una llista genèrica que implementa les interfícies IEnumerable, ICollection i IList.
    /// </summary>
    /// <typeparam name="T">Tipus dels elements de la llista.</typeparam>
    public class TaulaLlista<T> : IEnumerable<T>, ICloneable, ICollection<T>, IList<T>
    {
        //Atributs
        private T[] dades;  //esta apuntant a null, no te valors per ara
        private int nElem;
        private const int MAX = 100000;
        private const int DEFAULT_SIZE = 100000;

        /// <summary>
        /// Obté el nombre d'elements de la llista.
        /// </summary>
        public int Count
        { get { return nElem; } }
        /// <summary>
        /// Indica si la llista és de només lectura. En aquest cas, sempre retorna false.
        /// </summary>
        public bool IsReadOnly
        { get { return false; } }

        #region Propietats no Interficie

        /// <summary>
        /// Indica si la llista està plena (si s'ha arribat a la capacitat màxima).
        /// </summary>
        public bool IsFull => dades.Length == nElem;
        /// <summary>
        /// Obté la capacitat actual de la llista (mida de l'array).
        /// </summary>
        public int Capacity => dades.Length;
        #endregion

        #region Metode publics no interficies
        /// <summary>
        /// Obté tots els elements de la llista com un array.
        /// </summary>
        public T[] ToArray => dades;

        /// <summary>
        /// Retorna l'índex de l'última aparició de l'element a la llista.
        /// </summary>
        /// <param name="item">Element a cercar.</param>
        /// <returns>Índex de l'última aparició o -1 si no el troba.</returns>
        public int LastIndexOf(T item)
        {
            bool trobat = false;
            int i = nElem - 1;
            int index = -1;
            while (!trobat && i >= 0)
            {
                if (dades[i].Equals(item))
                {
                    trobat = true;
                    index = i;
                    i--;
                }
            }
            return index;
        }

        #endregion

        #region Sobreescriptura

        /// <summary>
        /// Sobreescriu el mètode ToString per representar la llista com una cadena.
        /// </summary>
        /// <returns>Cadena que representa els elements de la llista.</returns>
        public override string ToString()
        {
            string linia = $"Dades[0]";
            for (int i = 1; i < nElem; i++)
            {
                linia += $", Dades[{i}]";
            }
            return linia;
        }
        #endregion

        /// <summary>
        /// Permet l'accés als elements de la llista mitjançant un índex.
        /// </summary>
        /// <param name="index">Índex de l'element.</param>
        /// <returns>L'element a la posició indicada.</returns>
        /// <exception cref="IndexOutOfRangeException">Excepció si l'índex està fora dels límits de la llista.</exception>
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= nElem)
                    throw new IndexOutOfRangeException("L'índex està fora dels límits");
                return dades[index];
            }
            set
            {
                if (index < 0 || index >= nElem)
                    throw new IndexOutOfRangeException("L'índex està fora dels límits");
                dades[index] = value;
            }
        }

        #region Constructors
        /// <summary>
        /// Constructor que inicialitza la llista amb una mida específica.
        /// </summary>
        /// <param name="size">Mida inicial de la llista.</param>
        public TaulaLlista(int size)
        {
            nElem = 0;
            dades = new T[size];
        }

        /// <summary>
        /// Constructor que inicialitza la llista amb la mida per defecte.
        /// </summary>
        public TaulaLlista() : this(DEFAULT_SIZE)
        { }

        /// <summary>
        /// Duplicar la capacitat de la llista
        /// </summary>
        private void DuplicarCapacitat()
        {
            dades = new T[nElem];
            if (dades is null) throw new NotSupportedException("Item introduit es null");
            else if (IsReadOnly) throw new NotSupportedException("Llista es nomes de lectura");
            else if (!dades.IsReadOnly)
            {
                T[] dadesAux = new T[dades.Length * 2];
                for (int i = 0; i < nElem; i++)
                {
                    dadesAux[i] = dades[i]; //igualar apunten al mateix, nos da igual

                }
                dades = dadesAux; // dades ara apunta a dedesAux, tots dos apunten al matexi array
                                  // l'array que ja no s'utilitza, el garbage colector l'elimina
                                  //quan s'acaba el metode fa que lo que hi ha dins del metode ya no existeixi
                                  //dadesAux no esta y dades apunta a un array amb el doble de capacittat que abans
            }
        }

        /// <summary>
        /// Constructor que realitza una còpia d'una altra llista.
        /// </summary>
        /// <param name="llista">Llista a copiar.</param>
        public TaulaLlista(TaulaLlista<T> llista)
        {
            dades = new T[llista.dades.Length];
            for (int i = 0; i < nElem; i++)
            {
                this.dades[i] = llista.dades[i];
            }
        }
        #endregion

        #region Metodes

        /// <summary>
        /// Afegeix un element a la llista.
        /// </summary>
        /// <param name="item">Element a afegir.</param>
        /// <exception cref="NotSupportedException">Si l'element és null o la llista és només de lectura.</exception>
        void ICollection<T>.Add(T item)
        {
            if (item is null) throw new NotSupportedException("Item introduit es null");
            else if (IsReadOnly) throw new NotSupportedException("Llista es nomes de lectura");
            else if (nElem == dades.Length) DuplicarCapacitat();

            dades[nElem++] = item;
        }

        /// <summary>
        /// Elimina tots els elements de la llista.
        /// </summary>
        /// <exception cref="NotSupportedException">Si la llista és només de lectura.</exception>
        void ICollection<T>.Clear()
        {
            if (IsReadOnly) throw new NotSupportedException("Llista es nomes de lectura");
            for (int i = 0; i < nElem; nElem++) dades[i] = default(T);
            nElem = 0;
        }

        /// <summary>
        /// Verifica si un element està contingut a la llista.
        /// </summary>
        /// <param name="item">Element a verificar.</param>
        /// <returns>True si l'element està a la llista, de lo contrari false.</returns>
        bool ICollection<T>.Contains(T item)
        {
            return item != null && dades.Contains(item);
        }

        /// <summary>
        /// Copia els elements de la llista a un array començant des de l'índex especificat.
        /// </summary>
        /// <param name="array">Array on copiar els elements.</param>
        /// <param name="arrayIndex">Índex de començament en l'array de destí.</param>
        /// <exception cref="ArgumentNullException">Si l'array és null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Si l'índex està fora dels límits permesos.</exception>
        /// <exception cref="ArgumentException">Si l'array no té espai suficient.</exception>
        void ICollection<T>.CopyTo(T[] array, int arrayIndex)
        {
            if (array is null) throw new ArgumentNullException("Array es null");
            else if (arrayIndex < 0) throw new ArgumentOutOfRangeException("Esta fora del rang permes");
            //et dona una array i un index i has de copiar el teu array a partir d'aquell index
            else if (arrayIndex + nElem > array.Length) throw new ArgumentException("L'array no té prou espai per a copiar els elements.");

            for (int i = 0; i < nElem; i++)
            {
                array[arrayIndex + i] = dades[i];
            }
        }

        /// <summary>
        /// Elimina el primer element que coincideix amb l'element donat.
        /// </summary>
        /// <param name="item">Element a eliminar.</param>
        /// <returns>True si l'element ha estat eliminat, de lo contrari false.</returns>
        /// <exception cref="NotSupportedException">Si l'element és null o la llista és només de lectura.</exception>
        bool ICollection<T>.Remove(T item)
        {
            int index = ((IList<T>)this).IndexOf(item);
            if (item is null) throw new NotSupportedException("Item introduit es null");
            ((IList<T>)this).RemoveAt(index);
            return index != -1;
        }

        /// <summary>
        /// Obté un enumerador per iterar sobre els elements de la llista.
        /// </summary>
        /// <returns>Enumerador de la llista.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return new Enumeradora(this.dades, nElem);
        }

        /// <summary>
        /// Classe interna que implementa IEnumerator per permetre la iteració sobre la llista.
        /// </summary>
        public class Enumeradora : IEnumerator<T>
        {
            private int nElem;
            private int pos;
            private T[] valors;
            private int limit;

            /// <summary>
            /// Constructor que inicialitza l'enumerador.
            /// </summary>
            /// <param name="values">Array de valors a iterar.</param>
            /// <param name="pos">Posició inicial de l'enumerador.</param>
            public Enumeradora(T[] values, int pos)
            {
                this.limit = nElem;
                this.valors = values;
                this.pos = -1;
            }

            /// <summary>
            /// Obté l'element actual de l'enumerador.
            /// </summary>
            public T Current
            {
                get
                {
                    if (pos < 0 || pos >= nElem) throw new ArgumentOutOfRangeException("Estem fora del index");
                    return valors[pos];
                }
            }

            object IEnumerator.Current => Current;

            /// <summary>
            /// Neteja els recursos utilitzats pel enumerador.
            /// </summary>
            public void Dispose()
            {
                valors = null;
            }

            /// <summary>
            /// Moure el punter del enumerador al següent element.
            /// </summary>
            /// <returns>True si es pot moure al següent element, de lo contrari false.</returns>
            public bool MoveNext()
            {
                pos++;
                return pos < limit;
            }

            /// <summary>
            /// Reinicia el punter de l'enumerador.
            /// </summary>
            public void Reset()
            {
                pos = -1;
            }
        }

        /// <summary>
        /// Obté un enumerador per iterar sobre els elements de la llista
        /// </summary>
        /// <returns>Enumerador de la llista.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Retorna l'índex del primer element que coincideix amb l'element donat.
        /// </summary>
        /// <param name="item">Element a cercar.</param>
        /// <returns>Índex de l'element o -1 si no es troba.</returns>
        int IList<T>.IndexOf(T item)
        {
            int index = -1;
            for (int i = 0; i < nElem && index == -1; i++)
            {
                if (dades[i] != null && dades[i].Equals(item))  // Verifica que no sea null antes de llamar a Equals
                {
                    index = i;
                }
            }
            return index;
        }

        /// <summary>
        /// Insereix un element a la llista en una posició específica.
        /// </summary>
        /// <param name="index">Índex on inserir l'element.</param>
        /// <param name="item">Element a inserir.</param>
        /// <exception cref="NotSupportedException">Si la llista és només de lectura.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Si l'índex està fora del rang permès.</exception>
        void IList<T>.Insert(int index, T item)
        {
            if (IsReadOnly) throw new NotSupportedException("Llista es nomes de lectura");
            else if (item is null) throw new NullReferenceException("Item introduit es null");
            else if (index < 0 || index > nElem) throw new ArgumentOutOfRangeException("Esta fora del rang permes");

            if (nElem == dades.Length) DuplicarCapacitat();

            for (int i = nElem; i > index; i--)
            {
                dades[i] = dades[i - 1];
            }
            dades[index] = item;
            nElem++;

        }

        /// <summary>
        /// Elimina un element a la posició especificada.
        /// </summary>
        /// <param name="index">Índex de l'element a eliminar.</param>
        /// <exception cref="ArgumentOutOfRangeException">Si l'índex està fora del rang permès.</exception>
        void IList<T>.RemoveAt(int index)
        {
            if (IsReadOnly) throw new NotSupportedException("Llista es nomes de lectura");
            else if (index < 0 || index >= nElem) throw new ArgumentOutOfRangeException("Esta fora del rang permes");

            for (int i = index; i < nElem - 1; i++)
            {
                dades[i] = dades[i + 1];
            }
            nElem--;

        }

        /// <summary>
        /// Crea una còpia de la llista, duplicant els seus elements.
        /// </summary>
        /// <returns>Una nova instància de la llista amb els mateixos elements.</returns>
        public TaulaLlista<T> Clonar()
        {
            TaulaLlista<T> copia = new TaulaLlista<T>(dades.Length);
            copia.nElem = this.nElem;

            for (int i = 0; i < nElem; i++)
            {
                if (dades[i] is ICloneable)
                {
                    ICloneable clon = (ICloneable)dades[i];
                    copia.dades[i] = (T)clon.Clone();
                }
                else
                {
                    copia.dades[i] = dades[i];
                }
            }

            return copia;
        }

        /// <summary>
        /// Crea una còpia de la llista en el format object
        /// </summary>
        /// <returns>Una còpia de la llista com a objecte.</returns>
        public object Clone()
        {
            return Clonar();
        }

        #endregion
    }
}
