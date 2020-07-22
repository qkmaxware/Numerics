using System.Numerics;

namespace Qkmaxware.Numerics {

/// <summary>
/// Integer matrix
/// </summary>
public class IntMatrix : Matrix<int> {
    public IntMatrix (int rows, int columns) : base(IntCalculator.Instance, rows, columns) {}
    public IntMatrix (int[,] elements) : base(IntCalculator.Instance, elements) {}

    public static implicit operator IntMatrix (int[,] elements) {
        return new IntMatrix(elements);
    }
}

/// <summary>
/// Double matrix
/// </summary>
public class DoubleMatrix : Matrix<double> {
    public DoubleMatrix (int rows, int columns) : base(DoubleCalculator.Instance, rows, columns) {}
    public DoubleMatrix (double[,] elements) : base(DoubleCalculator.Instance, elements) {}

    public static implicit operator DoubleMatrix (double[,] elements) {
        return new DoubleMatrix(elements);
    }
}

/// <summary>
/// Complex number matrix
/// </summary>
public class ComplexMatrix : Matrix<Complex> {
    public ComplexMatrix (int rows, int columns) : base(ComplexCalculator.Instance, rows, columns) {}
    public ComplexMatrix (Complex[,] elements) : base(ComplexCalculator.Instance, elements) {}

    public static implicit operator ComplexMatrix (Complex[,] elements) {
        return new ComplexMatrix(elements);
    }
}

}