using System;
using System.Numerics;

namespace Qkmaxware.Numerics {

/// <summary>
/// Generic interface for a function with 1 parametre
/// </summary>
public interface IFunction<T> : ICalculationHelper<T> {
    /// <summary>
    /// Evaluate the function at y = f(x)
    /// </summary>
    /// <param name="x">x</param>
    /// <returns>y</returns>
    T Evaluate (T x);
}

/// <summary>
/// Native integer function
/// </summary>
public class IntFunction : IFunction<int> {
    public ICalculator<int> Calculator => IntCalculator.Instance;
    public Func<int, int> Lambda {get; set;}

    public IntFunction(Func<int, int> fn) {
        this.Lambda = fn;
    }

    public int Evaluate(int x) {
        return checked(Lambda(x));
    }

    public static implicit operator IntFunction(Func<int, int> fn) {
        return new IntFunction(fn);
    }
}

/// <summary>
/// Native double function
/// </summary>
public class DoubleFunction : IFunction<double> {
    public ICalculator<double> Calculator => DoubleCalculator.Instance;
    public Func<double, double> Lambda {get; set;}

    public DoubleFunction(Func<double, double> fn) {
        this.Lambda = fn;
    }

    public double Evaluate(double x) {
        return checked(Lambda(x));
    }

    public static implicit operator DoubleFunction(Func<double, double> fn) {
        return new DoubleFunction(fn);
    }
}

/// <summary>
/// Native complex function
/// </summary>
public class ComplexFunction : IFunction<Complex> {
    public ICalculator<Complex> Calculator => ComplexCalculator.Instance;
    public Func<Complex, Complex> Lambda {get; set;}

    public ComplexFunction(Func<Complex, Complex> fn) {
        this.Lambda = fn;
    }

    public Complex Evaluate(Complex x) {
        return checked(Lambda(x));
    }

    public static implicit operator ComplexFunction(Func<Complex, Complex> fn) {
        return new ComplexFunction(fn);
    }
}

}