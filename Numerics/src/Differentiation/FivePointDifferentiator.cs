using System;
using System.Linq;

namespace Qkmaxware.Numerics {

/// <summary>
/// Midpoint five point numerical derivative Solver
/// </summary>
public class CentredFivePointDifferentiator : INumericDifferentiator<double> {
    public double Differentiate (IFunction<double> f, double x, double h = double.Epsilon) {
        return ( f.Evaluate(x - 2 * h) - 8 * f.Evaluate(x - h) + 8 * f.Evaluate(x + h) - f.Evaluate(x + 2 * h) ) / (12 * h);
    }

    public IFunction<double> Differentiate(IFunction<double> f, Range<double> range) {
        var h = range.Increment * 0.5f;
        var xs = range.All.ToArray();
        var ys = xs.Select(x => Differentiate(f, x, h)).ToArray();
        return new DoubleInterpolatedFunction(xs, ys);
    }
}

/// <summary>
/// Endpoint five point numerical derivative solver
/// </summary>
public class EndpointFivePointDifferentiator : INumericDifferentiator<double> {
    public double Differentiate (IFunction<double> f, double x, double h = double.Epsilon) {
        return (-25 * f.Evaluate(x) + 48 * f.Evaluate(x + h) - 36 * f.Evaluate(x + 2 * h) + 16 * f.Evaluate(x + 3 * h) - 3 * f.Evaluate(x + 4 * h) ) / (12 * h);
    }

    public IFunction<double> Differentiate(IFunction<double> f, Range<double> range) {
        var h = range.Increment * 0.5f;
        var xs = range.All.ToArray();
        var ys = xs.Select(x => Differentiate(f, x, h)).ToArray();
        return new DoubleInterpolatedFunction(xs, ys);
    }
}

}