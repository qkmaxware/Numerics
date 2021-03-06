using System.Numerics;

/// <summary>
/// Root namespace for the Numerics package
/// </summary>
namespace Qkmaxware.Numerics {

/// <summary>
/// Interface for a class that can perform computations with the given type
/// </summary>
/// <typeparam name="T">numeric type</typeparam>
public interface ICalculator<T> {
    /// <summary>
    /// Add two values
    /// </summary>
    /// <param name="v1">first</param>
    /// <param name="v2">second</param>
    /// <returns>sum of the two values</returns>
    T Add(T v1, T v2);
    /// <summary>
    /// Subtract from a value
    /// </summary>
    /// <param name="v1">first</param>
    /// <param name="v2">second</param>
    /// <returns>difference</returns>
    T Subtract (T v1, T v2);
    /// <summary>
    /// Multiply two values
    /// </summary>
    /// <param name="v1">first</param>
    /// <param name="v2">second</param>
    /// <returns>product</returns>
    T Multiply (T v1, T v2);
    /// <summary>
    /// Multiply two values
    /// </summary>
    /// <param name="v1">first</param>
    /// <param name="v2">second</param>
    /// <returns>division</returns>
    T Divide (T v1, T v2);
    /// <summary>
    /// Negate a value
    /// </summary>
    /// <param name="v1">value</param>
    /// <returns>negated value</returns>
    T Negate (T v1);
    /// <summary>
    /// Get the '1' value for the given type
    /// </summary>
    /// <returns>1</returns>
    T Unit();
    /// <summary>
    /// Compare two values
    /// </summary>
    /// <param name="v1">first</param>
    /// <param name="v2">second</param>
    /// <returns>
    /// Less than zero if first < second
    /// Zero if first == second
    /// Greater than zero if first > second
    /// </returns>
    int Compare(T v1, T v2);
}

/// <summary>
/// Calculator for integer arithmetic
/// </summary>
public class IntCalculator : ICalculator<int> {
    public static readonly ICalculator<int> Instance = new IntCalculator();

    public int Add(int v1, int v2) => checked(v1 + v2);
    public int Subtract (int v1, int v2) => checked(v1 - v2);
    public int Multiply (int v1, int v2) => checked(v1 * v2);
    public int Divide (int v1, int v2) => checked(v1 / v2);
    public int Negate (int v1) => -v1;
    public int Unit() => 1;

    public int Compare(int v1, int v2) => v1.CompareTo(v2);
} 

/// <summary>
/// Calculator for unsigned integer arithmetic
/// </summary>
public class UintCalculator : ICalculator<uint> {
    public static readonly ICalculator<uint> Instance = new UintCalculator();

    public uint Add(uint v1, uint v2) => checked(v1 + v2);
    public uint Subtract (uint v1, uint v2) => checked(v1 - v2);
    public uint Multiply (uint v1, uint v2) => checked(v1 * v2);
    public uint Divide (uint v1, uint v2) => checked(v1 / v2);
    public uint Negate (uint v1) => checked((uint)-v1);
    public uint Unit() => 1;

    public int Compare(uint v1, uint v2) => v1.CompareTo(v2);
} 

/// <summary>
/// Calculator for long integer arithmetic
/// </summary>
public class LongCalculator : ICalculator<long> {
    public static readonly ICalculator<long> Instance = new LongCalculator();

    public long Add(long v1, long v2) => checked(v1 + v2);
    public long Subtract (long v1, long v2) => checked(v1 - v2);
    public long Multiply (long v1, long v2) => checked(v1 * v2);
    public long Divide (long v1, long v2) => checked(v1 / v2);
    public long Negate (long v1) => -v1;
    public long Unit() => 1;

    public int Compare(long v1, long v2) => v1.CompareTo(v2);
} 

/// <summary>
/// Calculator for float arithmetic
/// </summary>

public class FloatCalculator : ICalculator<float> {
    public static readonly ICalculator<float> Instance = new FloatCalculator();

    public float Add(float v1, float v2) => checked(v1 + v2);
    public float Subtract (float v1, float v2) => checked(v1 - v2);
    public float Multiply (float v1, float v2) => checked(v1 * v2);
    public float Divide (float v1, float v2) => checked(v1 / v2);
    public float Negate (float v1) => -v1;
    public float Unit() => 1;

    public int Compare(float v1, float v2) => v1.CompareTo(v2);
}


/// <summary>
/// Calculator for double arithmetic
/// </summary>

public class DoubleCalculator : ICalculator<double> {
    public static readonly ICalculator<double> Instance = new DoubleCalculator();

    public double Add(double v1, double v2) => checked(v1 + v2);
    public double Subtract (double v1, double v2) => checked(v1 - v2);
    public double Multiply (double v1, double v2) => checked(v1 * v2);
    public double Divide (double v1, double v2) => checked(v1 / v2);
    public double Negate (double v1) => -v1;
    public double Unit() => 1;

    public int Compare(double v1, double v2) => v1.CompareTo(v2);
} 

/// <summary>
/// Calculator for complex arithmetic
/// </summary>

public class ComplexCalculator : ICalculator<Complex> {
    public static readonly ICalculator<Complex> Instance = new ComplexCalculator();

    public Complex Add(Complex v1, Complex v2) => checked(v1 + v2);
    public Complex Subtract (Complex v1, Complex v2) => checked(v1 - v2);
    public Complex Multiply (Complex v1, Complex v2) => checked(v1 * v2);
    public Complex Divide (Complex v1, Complex v2) => checked(v1 / v2);
    public Complex Negate (Complex v1) => -v1;
    public Complex Unit() => Complex.One;

    public int Compare(Complex v1, Complex v2) => v1.Magnitude.CompareTo(v2.Magnitude);
} 

}