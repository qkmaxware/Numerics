using System;
using System.Text;

namespace Qkmaxware.Numerics {

/// <summary>
/// Generic matrix class of type T
/// </summary>
/// <typeparam name="T">element type</typeparam>
public class Matrix<T> : ICalculationHelper<T> {

    private T[,] elements;
    public ICalculator<T> Calculator {get; private set;}
    /// <summary>
    /// Number of elements
    /// </summary>
    public int Elements => elements.Length;
    /// <summary>
    /// Number of rows
    /// </summary>
    public int Rows => elements.GetLength(0);
    /// <summary>
    /// Number of columns
    /// </summary>
    public int Columns => elements.GetLength(1);
    /// <summary>
    /// Get the element at the given row and column
    /// </summary>
    public T this [int row, int column] => elements[row, column];
    /// <summary>
    /// Extract a row matrix over the provided columns
    /// </summary>
    public Matrix<T> this [int row, Range columns] => this[new Range(new Index(row), new Index(row + 1)), columns];
    /// <summary>
    /// Extract a column matrix over the provided rows
    /// </summary>
    public Matrix<T> this [Range rows, int column] => this[rows, new Range(new Index(column), new Index(column + 1))];
    /// <summary>
    /// Extract a submatrix with the provided rows and columns
    /// </summary>
    public Matrix<T> this [Range rows, Range columns] {
        get {
            var rStart = rows.Start.GetOffset(this.Rows);
            var rEnd = rows.End.GetOffset(this.Rows);
            var cStart = columns.Start.GetOffset(this.Columns);
            var cEnd = columns.End.GetOffset(this.Columns);

            var rowCount = Math.Abs(rEnd - rStart);
            var columnCount = Math.Abs(cEnd - cStart);
            var rowStep = rEnd - rStart < 0 ? -1 : 1;
            var columnStep = cEnd - cStart < 0 ? -1 : 1;

            if (rowCount <= 0 || columnCount <= 0)
                return new Matrix<T>(this.Calculator, 0, 0);

            var mx = new Matrix<T>(this.Calculator, rowCount, columnCount);
            var oldRow = rStart;
            var newRow = 0;
            while (oldRow != rEnd) {
                var oldCol = cStart;
                var newCol = 0;
                while (oldCol != cEnd) {
                    mx.elements[newRow, newCol] = this[oldRow, oldCol];

                    oldCol += columnStep;
                    newCol++;
                }
                oldRow += rowStep;
                newRow++;
            }

            return mx;
        }
    }

    /// <summary>
    /// Test if the matrix is square
    /// </summary>
    public bool IsSquare => Rows == Columns;
    /// <summary>
    /// Test if the matrix is a row matrix
    /// </summary>
    public bool IsRowMatrix => Rows == 1;
    /// <summary>
    /// Test if the matrix is a column matrix
    /// </summary>
    public bool IsColumnMatrix => Columns == 1;
    /// <summary>
    /// Create a new matrix
    /// </summary>
    /// <param name="calculator">arithmetic calculator for the given type</param>
    /// <param name="rows">number of rows</param>
    /// <param name="columns">number of columns</param>
    public Matrix(ICalculator<T> calculator, int rows, int columns) {
        this.Calculator = calculator;
        this.elements = new T[rows, columns];
    }
    /// <summary>
    /// Create a new matrix
    /// </summary>
    /// <param name="calculator">arithmetic calculator for the given type</param>
    /// <param name="elements">matrix elements</param>
    public Matrix(ICalculator<T> calculator, T[,] elements) {
        this.Calculator = calculator;
        this.elements = elements;
    }

    // FILLS -----------------------------------------------------------------------------
    #region Fills
    public static Matrix<T> Fill(ICalculator<T> calc, int rows, int columns, T value) {
        var ts = new T[rows, columns];
        for (var row = 0; row < ts.GetLength(0); row++) {
            for (var col = 0; col < ts.GetLength(1); col++) {
                ts[row, col] = value;
            }
        }
        return new Matrix<T>(calc, ts);
    }
    public static Matrix<T> Identity(ICalculator<T> calc, int rows, int columns) {
        var ts = new T[rows, columns];
        for (var i = 0; i < ts.GetLength(1); i++) {
            ts[i,i] = calc.Unit();
        }
        return new Matrix<T>(calc, ts);
    }

    public static Matrix<T> Random(ICalculator<T> calc, int rows, int columns, IValueGenerator<T> rng) {
        var ts = new T[rows, columns];
        for (var row = 0; row < ts.GetLength(0); row++) {
            for (var col = 0; col < ts.GetLength(1); col++) {
                ts[row, col] = rng.Next();
            }
        }
        return new Matrix<T>(calc, ts);
    }

    #endregion

    // MATRIX ASSERTIONS -----------------------------------------------------------------
    #region Assertions
    public static void AssertSquare(Matrix<T> mtx) {
        if(!mtx.IsSquare) {
            throw new DimensionMismatchException();
        }
    }

    public static void AssertSameDimensions(Matrix<T> m1, Matrix<T> m2) {
        if(m1.Rows != m2.Rows || m1.Columns != m2.Columns) {
            throw new DimensionMismatchException();
        }
    }

    public static void AssertCanMultiply(Matrix<T> m1, Matrix<T> m2) {
        if(m1.Columns != m2.Rows) {
            throw new DimensionMismatchException();
        }
    }

    public static void AssertValidColumn(Matrix<T> m1, int column) {
        if(m1.Columns < column) {
            throw new IndexOutOfRangeException();
        }
    }

    public static void AssertValidRow(Matrix<T> m1, int row) {
        if(m1.Rows < row) {
            throw new IndexOutOfRangeException();
        }
    }

    protected static void AssertValidColumn(T[,] m1, int column) {
        if(m1.GetLength(1) < column) {
            throw new IndexOutOfRangeException();
        }
    }

    protected static void AssertValidRow(T[,] m1, int row) {
        if(m1.GetLength(0) < row) {
            throw new IndexOutOfRangeException();
        }
    }
    #endregion

    // OTHER OPERATIONS -----------------------------------------------------------------
    /// <summary>
    /// Perform element wise operation
    /// </summary>
    /// <param name="op">operation</param>
    /// <returns>new matrix with altered elements</returns>
    public Matrix<T> Map (Func<T, T> op) {
        var rs = new T[this.Rows, this.Columns];
        for (var row = 0; row < this.Rows; row++) {
            for (var col = 0; col < this.Columns; col++) {
                rs[row, col] = op(this[row, col]);
            }
        }
        return new Matrix<T>(this.Calculator, rs);
    }
    /// <summary>
    /// Convert from one element type to another
    /// </summary>
    /// <param name="calculator">calculator for new element arithmetic</param>
    /// <param name="convert">conversion function</param>
    /// <typeparam name="K">new type</typeparam>
    /// <returns>new matrix with the new type and the altered elements</returns>
    public Matrix<K> Map<K> (ICalculator<K> calculator, Func<T, K> convert) {
        var rs = new K[this.Rows, this.Columns];
        for (var row = 0; row < this.Rows; row++) {
            for (var col = 0; col < this.Columns; col++) {
                rs[row, col] = convert(this[row, col]);
            }
        }
        return new Matrix<K>(calculator, rs);
    }
    /// <summary>
    /// Compute the trace of the matrix
    /// </summary>
    /// <returns>trace</returns>
    public T Trace () {
        AssertSquare(this);

        #nullable disable
        T value = default(T);
        for(int i = 0; i < this.Rows; i++) {
            if(i == 0) {
                value = this[i,i];
            } else {
                value = Calculator.Add(value, this[i,i]);
            }
        }
        return value;
        #nullable restore
    }
    /// <summary>
    /// Compute the tranposition of this matrix
    /// </summary>
    /// <returns>transposition of this matrix</returns>
    public Matrix<T> Transpose () {
        T[,] res = new T[this.Columns, this.Rows];
        for (int i = 0; i < this.Rows; i++)
            for (int j = 0; j < this.Columns; j++)
                res[j,i] = this[i,j];

        return new Matrix<T>(this.Calculator, res);
    }

    private void SwapRows(T[,] values, int r1, int r2) {
        AssertValidRow(values, r1);
        AssertValidRow(values, r2);
        T temp;
        for(int i = 0; i < this.Columns; i++) {
            temp = values[r1, i];
            values[r1,i] = values[r2,i];
            values[r2,i] = temp;
        }
    }

    private T[,] Pivot(out uint exchanges) {
        // Only square matrices
        AssertSquare(this);

        // Prepare inputs
        int n = this.Rows; // Rows and columns are the same
        T[,] im = new T[n,n];
        for (var i = 0; i < n; i++) {
            im[i,i] = Calculator.Unit(); // Make into the identity matrix
        }
        exchanges = 0;
        
        // Run
        for (var j = 0; j < n; j++) {
            T max = this[j, j];
            int row = j;
            for (var i = row; i < n; i++) {
                if (Calculator.Compare(this[i,j], max) > 0) {
                    max = this[i, j];
                    row = i;
                }
            }
            if (j != row) {
                // Swap rows
                exchanges++;
                SwapRows(im, j, row);
            }
        }
    
        // Done
        return im;
    }

    /// <summary>
    /// Perform LU Decomposition 
    /// </summary>
    /// <returns>LU decomposition</returns>
    public LUPSet<T> Decompose () {
        // Only square matrices
        AssertSquare(this);

        // Create empty matrices
        #nullable disable

        int n = this.Rows;
        T[,] L = new T[n,n];
        T[,] U = new T[n,n];
        uint pexchanges;
        T[,] P = this.Pivot(out pexchanges);
        Matrix<T> A2 = new Matrix<T>(this.Calculator, P) * (this);
    
        for (int j = 0; j < n; j++) {
            L[j,j] = Calculator.Unit();
            for (int i = 0; i < j +1; i++) {
                T s1 = default(T);
                for (int k = 0; k < i; k++) {
                    s1 = Calculator.Add(
                        s1,
                        Calculator.Multiply(U[k,j], L[i,k])
                    );
                }
                U[i,j] = Calculator.Subtract(A2[i,j], s1);
            }
            for (int i = j; i < n; i++) {
                T s2 = default(T);
                for (int k = 0; k < j; k++) {
                    s2 = Calculator.Add(
                        s2,
                        Calculator.Multiply(U[k,j], L[i,k])
                    );
                }
                L[i,j] = Calculator.Divide(
                    Calculator.Subtract(A2[i,j], s2), 
                    U[j,j]
                );
            }
        }

        #nullable restore

        return new LUPSet<T>(L, U, P, pexchanges);
    }

    /// <summary>
    /// Compute the determinate of this matrix
    /// </summary>
    /// <returns>determinate</returns>
    public T Determinant() {
        // TODO deal with 1x1 and 2x2 matrices
        //http://lampx.tugraz.at/~hadley/num/ch2/2.3a.php
        // Only square matrices
        AssertSquare(this);
        // det(P) * det(A) = det(L) * det(U)
        // det(A) = det(L) * det(U) / det(P)
        // det(A) = 1 * Pi{Uii | i in 0..N} / (-1^n)
        LUPSet<T> lup = this.Decompose();
        T d = Calculator.Unit();

        //Product of the diagonal elements of the LU matrix
        for(int i = 0; i < this.Rows; i++) {
            d = Calculator.Multiply(d, lup.U[i,i]); 
        }

        // Multiply by the determinate of the P matrix (-1^n | n = number of exchanges)
        // if n is even, -1^n = 1, if n is odd -1^n = -1
        if (lup.Sign > 0) {
            return d;
        } else {
            return Calculator.Negate(d);
        }
    }

    private Matrix<T> submatrix (int row, int column) {
        AssertValidColumn(this, column);
        AssertValidRow(this, row);
        var newColumns = this.Columns - 1;
        var newRows = this.Rows - 1;
        if (newColumns < 0)
            newColumns = 0;
        if (newRows < 0)
            newRows = 0;

        var sub = new Matrix<T>(this.Calculator, newRows, newColumns);
        var x = 0;
        for (var icol = 0; icol < this.Columns; icol++) {
            if (icol == column)
                continue;
            
            int y = 0;
            for (int irow = 0; irow < this.Rows; irow++) {
                if (irow == row)
                    continue;
                
                sub.elements[y, x] = this[irow, icol];
                y++;
            }

            x++;
        }

        return sub;
    }

    /// <summary>
    /// Compute the matrix of minors
    /// </summary>
    /// <returns>matrix of minors</returns>
    public Matrix<T> Minors() {
        var minors = new Matrix<T>(this.Calculator, this.Rows, this.Columns);

        for (var row = 0; row < this.Rows; row++) {
            for (var col = 0; col < this.Columns; col++) {
                var submatrix = this.submatrix(row, col);
                var det = submatrix.Determinant();
                minors.elements[row, col] = det;
            }
        }

        return minors;
    }
    /// <summary>
    /// Compute the cofactor matrix
    /// </summary>
    /// <returns>matrix of cofactors</returns>
    public Matrix<T> Cofactor() {
        Matrix<T> co = new Matrix<T>(this.Calculator, this.Columns, this.Rows);
        var minors = Minors();

        for (var row = 0; row < this.Rows; row++) {
            for (var col = 0; col < this.Columns; col++) {
                // Apply checkerboard of +/- 
                // + - + -  // EVEN row
                // - + - +  // ODD row
                // + - + -

                bool negate = false;
                if (row % 2 != 0) {
                    // ODD Row
                    if (!(col % 2 != 0)) {
                        // EVEN column
                        negate = true;
                    }
                } else {
                    // EVEN row
                    if ((col % 2 != 0)) {
                        // ODD column
                        negate = true;
                    }
                }
                co.elements[row, col] = negate ? Calculator.Negate(minors[row, col]) : minors[row, col];
            }
        }

        return co;
    }

    /// <summary>
    /// Compute the adjugate matrix
    /// </summary>
    /// <returns>adjugate matrix</returns>
    public Matrix<T> Adjugate() {
        return this.Cofactor().Transpose();
    }

    /// <summary>
    /// Compute the inverse of this matrix
    /// </summary>
    /// <returns>matrix inverse</returns>
    public Matrix<T> Inverse() {
        return this.Adjugate() / this.Determinant();
    }

    /// <summary>
    /// Perform element wise operations
    /// </summary>
    /// <param name="lhs">first matrix</param>
    /// <param name="rhs">second matrix</param>
    /// <param name="function">element wise operation</param>
    /// <returns>transformed matrix</returns>
    public static Matrix<T> Operate(Matrix<T> lhs, Matrix<T> rhs, Func<T,T,T> function) {
        AssertSameDimensions(lhs, rhs);
        var rs = new T[lhs.Rows,lhs.Columns];
        var calc = lhs.Calculator ?? rhs.Calculator;

        for (var row = 0; row < lhs.Rows; row++) {
            for (var col = 0; col < lhs.Columns; col++) {
                rs[row, col] = function(lhs[row, col], rhs[row, col]);
            }
        }

        return new Matrix<T>(calc, rs);
    }

    // MATRIX OPERATORS -----------------------------------------------------------------
    #region  Operators
    /// <summary>
    /// Matrix addition
    /// </summary>
    /// <param name="lhs">first</param>
    /// <param name="rhs">second</param>
    /// <returns>sum of the matrices</returns>
    public static Matrix<T> operator + (Matrix<T> lhs, Matrix<T> rhs) {
        AssertSameDimensions(lhs, rhs);
        var calc = lhs.Calculator ?? rhs.Calculator;

        T[,] rs = new T[lhs.Rows, lhs.Columns];
        for(int i = 0; i < lhs.Rows; i++) {
            for(int j = 0; j < lhs.Columns; j++) {
                rs[i,j] = calc.Add(lhs[i,j], (rhs[i,j]));
            }
        }

        return new Matrix<T>(calc, rs);
    }
    /// <summary>
    /// Matrix subtraction
    /// </summary>
    /// <param name="lhs">first</param>
    /// <param name="rhs">second</param>
    /// <returns>difference of the matrices</returns>
    public static Matrix<T> operator - (Matrix<T> lhs, Matrix<T> rhs) {
        AssertSameDimensions(lhs, rhs);
        var calc = lhs.Calculator ?? rhs.Calculator;

        T[,] rs = new T[lhs.Rows, lhs.Columns];
        for(int i = 0; i < lhs.Rows; i++) {
            for(int j = 0; j < lhs.Columns; j++) {
                rs[i,j] = calc.Subtract(lhs[i,j], (rhs[i,j]));
            }
        }
        
        return new Matrix<T>(calc, rs);
    }
    /// <summary>
    /// Scalar division
    /// </summary>
    /// <param name="lhs">first</param>
    /// <param name="rhs">second</param>
    /// <returns>element wise division</returns>
    public static Matrix<T> operator / (Matrix<T> lhs, T rhs) {
        var calc = lhs.Calculator;

        T[,] rs = new T[lhs.Rows, lhs.Columns];
        for(int i = 0; i < lhs.Rows; i++) {
            for(int j = 0; j < lhs.Columns; j++) {
                rs[i,j] = calc.Divide(lhs[i,j], rhs);
            }
        }
        
        return new Matrix<T>(calc, rs);
    }
    /// <summary>
    /// Scalar multiplication
    /// </summary>
    /// <param name="lhs">first</param>
    /// <param name="rhs">second</param>
    /// <returns>element wise multiplication</returns>
    public static Matrix<T> operator * (Matrix<T> lhs, T rhs) {
        var calc = lhs.Calculator;

        T[,] rs = new T[lhs.Rows, lhs.Columns];
        for(int i = 0; i < lhs.Rows; i++) {
            for(int j = 0; j < lhs.Columns; j++) {
                rs[i,j] = calc.Multiply(lhs[i,j], rhs);
            }
        }
        
        return new Matrix<T>(calc, rs);
    }
    /// <summary>
    /// Scalar multiplication
    /// </summary>
    /// <param name="lhs">first</param>
    /// <param name="rhs">second</param>
    /// <returns>element wise multiplication</returns>
    public static Matrix<T> operator * (T lhs, Matrix<T> rhs) {
        var calc = rhs.Calculator;

        T[,] rs = new T[rhs.Rows, rhs.Columns];
        for(int i = 0; i < rhs.Rows; i++) {
            for(int j = 0; j < rhs.Columns; j++) {
                rs[i,j] = calc.Multiply(lhs, rhs[i,j]);
            }
        }
        
        return new Matrix<T>(calc, rs);
    }
    /// <summary>
    /// Matrix multiplication
    /// </summary>
    /// <param name="lhs">first</param>
    /// <param name="rhs">second</param>
    /// <returns>matrix multiplication</returns>
    public static Matrix<T> operator * (Matrix<T> lhs, Matrix<T> rhs) {
        AssertCanMultiply(lhs, rhs);
        var calc = lhs.Calculator ?? rhs.Calculator;

        #nullable disable
        int rows = lhs.Rows;
        int columns = rhs.Columns;
        T[,] rs = new T[rows,columns];
        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < columns; j++) {
                T sum = default(T);
                for (int k = 0; k < lhs.Columns; k++) {
                    if (k == 0) {
                        sum = calc.Multiply(lhs[i,k], (rhs[k,j]));
                    } else {
                        sum = calc.Add(
                            sum, 
                            calc.Multiply(lhs[i,k], (rhs[k,j]))
                        ); 
                    }
                }
                rs[i,j] = sum;
            }
        }
        #nullable restore

        return new Matrix<T>(calc, rs);
    } 
    #endregion

    // STRING CONSTRUCTION -----------------------------------------------------------------
    #region Stringing
    protected enum MatrixItemPrintOrder {
        RowWise, ColumnWise,
    }   

    public string ToWolframString(string format = "{0}") {
        return this.Format(
            MatrixItemPrintOrder.RowWise,
            "{",
                "{",format,",", "}", ",",
            "}"
        );
    }

    public string ToMatlabString(string format = "{0}") {
        return this.Format(
            MatrixItemPrintOrder.RowWise,
            "[",
                string.Empty,format,",", string.Empty, ";",
            "]"
        );
    }

    public string ToMapleString( string format = "{0}") {
        return this.Format(
            MatrixItemPrintOrder.ColumnWise,
            "<",
                "<",format,",", ">", "|",
            ">"
        );
    }

    public string ToLatexString(string format = "{0}") {
        return 
        @"\begin{bmatrix}" + 
        this.Format(
            MatrixItemPrintOrder.RowWise,
            string.Empty,
                string.Empty,format,"&", string.Empty, @" \\",
            string.Empty
        ) + 
        @"\end{bmatrix}";
    }

    public override string ToString() {
        return this.Format(
            MatrixItemPrintOrder.RowWise,
            string.Empty,
                string.Empty,"{0}"," ", string.Empty, System.Environment.NewLine,
            string.Empty
        );
    }

    private string Format(MatrixItemPrintOrder format = MatrixItemPrintOrder.RowWise, string prefix = "[", string itemPrefix = "", string elementFormat = "{0}", string elementSeparator = ",", string itemPostfix = "", string itemSeparator = ";", string postfix = "]") {
        StringBuilder sb = new StringBuilder();
        sb.Append(prefix);
        if (format == MatrixItemPrintOrder.RowWise) {
            // Iterate over rows first
            for(int i = 0; i < this.Rows; i++){
                if (i != 0) {
                    sb.Append(itemSeparator);
                }
                sb.Append(itemPrefix);
                for(int j = 0; j < this.Columns; j++) {
                    if (j != 0) {
                        sb.Append(elementSeparator);
                    }
                    sb.Append(string.Format(elementFormat, this[i,j]));
                }
                sb.Append(itemPostfix);
            }
        } else {
            // Iterate over columns first
            for(int i = 0; i < this.Columns; i++){
                if (i != 0) {
                    sb.Append(itemSeparator);
                }
                sb.Append(itemPrefix);
                for(int j = 0; j < this.Rows; j++) {
                    if (j != 0) {
                        sb.Append(elementSeparator);
                    }
                    sb.Append(string.Format(elementFormat, this[j,i]));
                }
                sb.Append(itemPostfix);
            }
        }
        sb.Append(postfix);
        return sb.ToString();
    }
    #endregion
}

}