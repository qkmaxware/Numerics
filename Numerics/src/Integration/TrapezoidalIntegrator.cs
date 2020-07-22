using System;
using System.Linq;

namespace Qkmaxware.Numerics {

/// <summary>
/// Integration using the trapezoidal rule
/// </summary>
public class TrapezoidalIntegrator : IDefiniteIntegrator<double> {

    private double TrapezoidalRule(double x1, double x0, double y1, double y0) {
        var h = x1 - x0;
        var value = (h/2) * (y0 + y1);// - ((h * h * h) / 12) * err;
        return value;
    }

    public double Integrate(IFunction<double> fx, Range<double> range) {
        var x0 = range.Start;
        var sum = 0d;
        foreach (var x1 in range.All.Skip(1)) {
            sum += TrapezoidalRule(x1, x0, fx.Evaluate(x1), fx.Evaluate(x0));
            x0 = x1;
        }
        return sum;
    }
}

}