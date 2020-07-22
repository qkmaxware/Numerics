using System;
using System.Linq;

namespace Qkmaxware.Numerics {

/// <summary>
/// Midpoint three point numerical derivative solver
/// </summary>
public class CentredThreePointDifferentiator : INumericDifferentiator<double> {

    public double Differentiate (IFunction<double> f, double x, double h = double.Epsilon) {
        return f.Evaluate(x + h) - f.Evaluate(x - h) / (2 * h);
    }

    public IFunction<double> Differentiate(IFunction<double> f, Range<double> range) {
        var h = range.Increment * 0.5f;
        var xs = range.All.ToArray();
        var ys = xs.Select(x => Differentiate(f, x, h)).ToArray();
        return new DoubleInterpolatedFunction(xs, ys);
    }
    
}

/// <summary>
/// Endpoint three point numerical derivative solver
/// </summary>
public class EndpointThreePointDifferentiator: INumericDifferentiator<double> {
    
    public double Differentiate (IFunction<double> f, double x, double h = double.Epsilon) {
        return (-3 * f.Evaluate(x) + 4 * f.Evaluate(x + h) - f.Evaluate(x + 2 * h)) / (2 * h);
    }

    public IFunction<double> Differentiate(IFunction<double> f, Range<double> range) {
        var h = range.Increment * 0.5f;
        var xs = range.All.ToArray();
        var ys = xs.Select(x => Differentiate(f, x, h)).ToArray();
        return new DoubleInterpolatedFunction(xs, ys);
    }

}

}