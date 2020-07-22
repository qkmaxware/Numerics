using System;

namespace Qkmaxware.Numerics {
    /// <summary>
    /// Matrix dimension mismatch 
    /// </summary>
    public class DimensionMismatchException : ArithmeticException {
        public DimensionMismatchException() : base() {}
    }
}