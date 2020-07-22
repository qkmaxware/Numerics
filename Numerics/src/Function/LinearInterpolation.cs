using System;
using System.Linq;
using System.Collections.Generic;

namespace Qkmaxware.Numerics {

/// <summary>
/// Function interpolated using linear-interpolation
/// </summary>
public class LinearInterpolation<T> : IInterpolator<T>, ICalculationHelper<T> {
    public ICalculator<T> Calculator {get;}

    public LinearInterpolation(ICalculator<T> calc) {
        this.Calculator = calc;
    }

    public T Interpolate(T last, T start, T end, T next, T t) {
        return Calculator.Add(
            Calculator.Multiply(t, end),
            Calculator.Multiply(
                Calculator.Subtract(Calculator.Unit(), t),
                start
            )
        );
    }
}

}