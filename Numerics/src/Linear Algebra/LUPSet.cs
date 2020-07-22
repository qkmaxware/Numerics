namespace Qkmaxware.Numerics {
    /// <summary>
    /// LU factorization with full pivoting decomposition
    /// </summary>
    public struct LUPSet<T> {
        /// <summary>
        /// Lower matrix
        /// </summary>
        public T[,] L {get; private set;}
        /// <summary>
        /// Upper matrix
        /// </summary>
        public T[,] U {get; private set;}
        /// <summary>
        /// Pivot matrix
        /// </summary>
        public T[,] P {get; private set;}

        public uint Exchanges {get; private set;}

        public int Sign { 
            get {
                return Exchanges % 2 == 0 ? 1 : -1; //-1^Exchanges
            }
        }

        /// <summary>
        /// Create a new LUP set
        /// </summary>
        /// <param name="l">lower matrix</param>
        /// <param name="u">upper matrix</param>
        /// <param name="p">pivot matrix</param>
        /// <param name="exchanges">number of exchanges</param>
        public LUPSet(T[,] l, T[,] u, T[,] p, uint exchanges) {
            this.L = l;
            this.U = u;
            this.P = p;
            this.Exchanges = exchanges;
        }
    }

}