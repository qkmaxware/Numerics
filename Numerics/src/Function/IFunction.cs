using System;
using System.Numerics;

namespace Qkmaxware.Numerics {

/// <summary>
/// Generic interface for all functions
/// </summary>
/// <typeparam name="Arg">input type</typeparam>
/// <typeparam name="T">result type</typeparam>
public interface IFunction<Arg, T> : ICalculationHelper<T> {
    /// <summary>
    /// Shortcut for the Evaluate method
    /// </summary>
    /// <returns>y value at the given xs</returns>
    T this[Arg xs] => Evaluate(xs);

    /// <summary>
    /// Evaluate the function y = f(xs)
    /// </summary>
    /// <param name="xs">function arguments</param>
    /// <returns>y value</returns>
    T Evaluate(Arg xs);
}

/// <summary>
/// Generic interface for a function with 1 parametre
/// </summary>
public interface IFunction<T> : ICalculationHelper<T>, IFunction<T, T> {}

/// <summary>
/// Generic interface for a function with 2 parametres
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IFunction2<T> : ICalculationHelper<T>, IFunction<ValueTuple<T,T>, T> {}

/// <summary>
/// Generic interface for a function with 3 parametres
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IFunction3<T> : ICalculationHelper<T>, IFunction<ValueTuple<T,T,T>, T> {}

/// <summary>
/// Generic interface for a function with 4 parametres
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IFunction4<T> : ICalculationHelper<T>, IFunction<ValueTuple<T,T,T,T>, T> {}

/// <summary>
/// Generic interface for a function with 5 parametres
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IFunction5<T> : ICalculationHelper<T>, IFunction<ValueTuple<T,T,T,T,T>, T> {}

/// <summary>
/// Generic interface for a function with 6 parametres
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IFunction6<T> : ICalculationHelper<T>, IFunction<ValueTuple<T,T,T,T,T,T>, T> {}

/// <summary>
/// Generic interface for a function with 7 parametres
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IFunction7<T> : ICalculationHelper<T>, IFunction<ValueTuple<T,T,T,T,T,T,T>, T> {}

/// <summary>
/// Constant function for the default value of the given data-type
/// </summary>
/// <typeparam name="T">type</typeparam>
public class ConstantDefaultFunction<T> : IFunction<T> {
    #nullable disable
    public ICalculator<T> Calculator => null;

    public T Evaluate(T xs) {
        return default(T);
    }
    #nullable restore
}

public class NativeFunction<Arg, T> : IFunction<Arg, T> {
    public ICalculator<T> Calculator {get; private set;}
    public Func<Arg, T> Lambda {get; private set;}

    public NativeFunction(ICalculator<T> calculator, Func<Arg, T> fn) {
        this.Calculator = calculator;
        this.Lambda = fn;
    }

    public T Evaluate(Arg xs) {
        return checked(Lambda(xs));
    }
}

public class NativeFunction<T> : NativeFunction<T,T>, IFunction<T> {
    public NativeFunction(ICalculator<T> calculator, Func<T, T> fn) : base(calculator, fn) {}
}
public class NativeFunction2<T> : NativeFunction<(T,T), T>, IFunction2<T> {
    public NativeFunction2(ICalculator<T> calculator, Func<(T,T), T> fn) : base(calculator, fn) {}
}
public class NativeFunction3<T> : NativeFunction<(T,T,T), T>, IFunction3<T> {
    public NativeFunction3(ICalculator<T> calculator, Func<(T,T,T), T> fn) : base(calculator, fn) {}
}
public class NativeFunction4<T> : NativeFunction<(T,T,T,T), T>, IFunction4<T> {
    public NativeFunction4(ICalculator<T> calculator, Func<(T,T,T,T), T> fn) : base(calculator, fn) {}
}
public class NativeFunction5<T> : NativeFunction<(T,T,T,T,T), T>, IFunction5<T> {
    public NativeFunction5(ICalculator<T> calculator, Func<(T,T,T,T,T), T> fn) : base(calculator, fn) {}
}
public class NativeFunction6<T> : NativeFunction<(T,T,T,T,T,T), T>, IFunction6<T> {
    public NativeFunction6(ICalculator<T> calculator, Func<(T,T,T,T,T,T), T> fn) : base(calculator, fn) {}
}
public class NativeFunction7<T> : NativeFunction<(T,T,T,T,T,T,T), T>, IFunction7<T> {
    public NativeFunction7(ICalculator<T> calculator, Func<(T,T,T,T,T,T,T), T> fn) : base(calculator, fn) {}
}

/// <summary>
/// Native integer function
/// </summary>
public class IntFunction : NativeFunction<int> {
    public IntFunction(Func<int, int> fn) : base(IntCalculator.Instance, fn) { }
}

/// <summary>
/// Native double function
/// </summary>
public class DoubleFunction : NativeFunction<double> {
    public DoubleFunction(Func<double, double> fn) : base(DoubleCalculator.Instance, fn) {}
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

    public double Evaluate(ValueTuple<double, double> xs) {
        return checked(Lambda(xs.Item1, xs.Item2));
    }
}

/// <summary>
/// Native double function of three variables
/// </summary>
public class DoubleFunction3 : IFunction3<double> {
    public ICalculator<double> Calculator => DoubleCalculator.Instance;
    public Func<double, double, double, double> Lambda {get; set;}

    public DoubleFunction3(Func<double, double, double, double> fn) {
        this.Lambda = fn;
    }

    public double Evaluate(ValueTuple<double, double, double> xs) {
        return checked(Lambda(xs.Item1, xs.Item2, xs.Item3));
    }
}

/// <summary>
/// Native complex function
/// </summary>
public class ComplexFunction : NativeFunction<Complex> {
    public ComplexFunction(Func<Complex, Complex> fn) : base(ComplexCalculator.Instance, fn) {}
}

}