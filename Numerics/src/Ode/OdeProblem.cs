namespace Qkmaxware.Numerics {

/// <summary>
/// First order differential equation
/// </summary>
/// <typeparam name="T">variable type</typeparam>
public class FirstOrderDE<T> {
    private IFunction2<T> Fty;

    /// <summary>
    /// Create a first order differential equation of the form y` = F(t,y)
    /// </summary>
    /// <param name="Fty"></param>
    public FirstOrderDE(IFunction2<T> Fty) {
        this.Fty = Fty;
    }

    /// <summary>
    /// Linear Differential Equation of the form y` = P(t)y + G(t)
    /// </summary>
    /// <param name="Pt">function of t to multiply with y</param>
    /// <param name="Gt">function of t to add to result</param>
    /// <returns>differential equation</returns>
    public static FirstOrderDE<T> Linear(IFunction<T> Pt, IFunction<T> Gt) {
        var calc = Pt.Calculator ?? Gt.Calculator;
        return new FirstOrderDE<T>(
            new NativeFunction2<T>(calc, ((T, T) args) => {
                var x = args.Item1;
                var y = args.Item2;
                return calc.Subtract(
                    calc.Multiply(Pt.Evaluate(x), y),
                    Gt.Evaluate(x)
                );
            })
        );
    }

    /// <summary>
    /// Separable Differential Equation of the form y` = M(t)/N(y)
    /// </summary>
    /// <param name="Ny">function of y multiplied to y`</param>
    /// <param name="Mt">function of t</param>
    /// <returns>differential equation</returns>
    public static FirstOrderDE<T> Separable (IFunction<T> Ny, IFunction<T> Mt) {
        var calc = Ny.Calculator ?? Mt.Calculator;
        return new FirstOrderDE<T>(
            new NativeFunction2<T>(calc, ((T, T) args) => {
                var x = args.Item1;
                var y = args.Item2;
                return calc.Divide(
                    Mt.Evaluate(x),
                    Ny.Evaluate(y)
                );
            })
        );
    }
    
    
    /// <summary>
    /// Create the function y' = f(t, y)
    /// </summary>
    /// <returns>Rearranged function for y` with the first argument being the independent variable</returns>
    public IFunction2<T> GetEquationForDy() => Fty;

    public override string ToString() {
        return $"y` = f(t,y)";
    }
}

/// <summary>
/// Second order differential equation
/// </summary>
/// <typeparam name="T"></typeparam>
public class SecondOrderDE<T> : ICalculationHelper<T> {

    public IFunction<T> Px {get; private set;}
    public IFunction<T> Qx {get; private set;}
    public IFunction<T> Rx{get; private set;}

    public ICalculator<T> Calculator => Px.Calculator ?? Qx.Calculator ?? Rx.Calculator;

    public SecondOrderDE(IFunction<T> Pdy, IFunction<T> Qy, IFunction<T> R) {
        this.Px = Pdy;
        this.Qx = Qy;
        this.Rx = R;
    }

    public IFunction3<T> GetEquationForD2y() {
        return new NativeFunction3<T>(
            Calculator,
            // args = (x, y, y`)
            (args) => (
                Calculator.Add(
                    Calculator.Add(
                        Calculator.Multiply(Px.Evaluate(args.Item1), args.Item3),   //P(x)*y`
                        Calculator.Multiply(Qx.Evaluate(args.Item1), args.Item2)    //Q(x)*y
                    ),
                    Rx.Evaluate(args.Item1)                                         //R(x)
                )
            )
        );
    }

    public override string ToString() {
        return $"y`` = P(x)y` + Q(x)y + R(x)";
    }
}

}