using System;
using System.Linq;

namespace Qkmaxware.Numerics {

/// <summary>
/// Integration using Simpson's method
/// </summary>
public class SimpsonIntegrator : IDefiniteIntegrator<double> {

    public double Integrate(IFunction<double> fx, Range<double> range) {
        var x0 = range.Start;
        var sum = 0d;
        foreach (var x2 in range.All.Skip(1)) {
            var h = x2 - x0;
            var x1 = x0 + 0.5 * h;
            var fx0 = fx.Evaluate(x0);
            var fx1 = fx.Evaluate(x1);
            var fx2 = fx.Evaluate(x2); 

            sum += (h / 6) * (fx0 + 4*fx1 + fx2);
            x0 = x2;
        }
        return sum;
    }
}

}