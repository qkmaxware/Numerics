using System;
using System.Collections.Generic;

namespace Qkmaxware.Numerics {

/// <summary>
/// Solve initial value problems with Euler's method
/// </summary>
public class EulerIvpSolver : IIvpSolver<double> {
    /// <summary>
    /// Solve the initial value problem where y` = f(t, y), a <= t <= b, y(a) = y0
    /// </summary>
    /// <param name="f">function of two arguments (t,y)</param>
    /// <param name="tRange">range (a,b) of values for t</param>
    /// <param name="y0">value of y at a</param>
    /// <returns>approximation for the function y</returns>
    public IFunction<double> Solve(IFunction2<double> f, Range<double> tRange, double y0) {
        // INPUT endpoints a & b from xRange, range subdivisions N, initial condition y(a) = y0
        // OUTPUT approximation of y at the values in xRange
        var N = 50; // provide as a argument?
        var a = Math.Min(tRange.Start, tRange.End);   var b = Math.Max(tRange.Start, tRange.End);
        var h = (b - a) / N;
        var t = a; 
        var w = y0;

        var points = new List<Point2<double>>(N);
        points.Add(new Point2<double>(t, w));

        for (var i = 1; i <= N; i++) {
            w = w + h * f.Evaluate(t, w);   // compute wi
            t = a + i * h;                  // compute ti

            points.Add(new Point2<double>(t, w));
        }

        return new DoubleInterpolatedFunction(points);
    }
}

}