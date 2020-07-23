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
/// Generic interface for a function with 2 parametres
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IFunction2<T> : ICalculationHelper<T> {
    /// <summary>
    /// Evaluate the function at y = f(x1, x2)
    /// </summary>
    /// <param name="x1">first parametre</param>
    /// <param name="x2">second parametre</param>
    /// <returns>y</returns>
    T Evaluate(T x1, T x2);
}

/// <summary>
/// Generic interface for a function with 3 parametres
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IFunction3<T> : ICalculationHelper<T> {
    /// <summary>
    /// Evaluate the function at y = f(x1, x2, x3)
    /// </summary>
    /// <param name="x1">first parametre</param>
    /// <param name="x2">second parametre</param>
    /// <param name="x3">third parametre</param>
    /// <returns>y</returns>
    T Evaluate(T x1, T x2, T x3);
}

/// <summary>
/// Generic interface for a function with 4 parametres
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IFunction4<T> : ICalculationHelper<T> {
    /// <summary>
    /// Evaluate the function at y = f(x1, x2, x3, x4)
    /// </summary>
    /// <param name="x1">first parametre</param>
    /// <param name="x2">second parametre</param>
    /// <param name="x3">third parametre</param>
    /// <param name="x4">four parametre</param>
    /// <returns>y</returns>
    T Evaluate(T x1, T x2, T x3, T x4);
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
}

/// <summary>
/// Native double function of two variables
/// </summary>
public class DoubleFunction2 : IFunction2<double> {
    public ICalculator<double> Calculator => DoubleCalculator.Instance;
    public Func<double, double, double> Lambda {get; set;}

    public DoubleFunction2(Func<double, double, double> fn) {
        this.Lambda = fn;
    }

    public double Evaluate(double x1, double x2) {
        return checked(Lambda(x1, x2));
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
}

}