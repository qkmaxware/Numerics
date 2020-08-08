using System;
using System.Collections.Generic;

namespace Qkmaxware.Numerics {

/// <summary>
/// Solve initial value problems with Fourth Order Runge-Kutta method
/// </summary>
public class RungeKuttaIvpSolver : IIvpSolver<double> {

    private int N = 50;
    /// <summary>
    /// Number of subintervals
    /// </summary>
    public int Subintervals {
        get {
            return N;
        } set {
            N = Math.Max(value, 2);
        }
    }

    /// <summary>
    /// Solve the initial value problem where y` = f(t, y), a <= t <= b, y(a) = y0
    /// </summary>
    /// <param name="de">first order differential equation</param>
    /// <param name="tRange">range (a,b) of values for t</param>
    /// <param name="y0">value of y at a</param>
    /// <returns>approximation for the function y</returns>
    public IFunction<double> SolveInitial(FirstOrderDE<double> de, Range<double> tRange, double y0) {
        // INPUT endpoints a & b from xRange, range subdivisions N, initial condition y(a) = y0
        // OUTPUT approximation of y at the values in xRange
        var f = de.GetEquationForDy();
        var a = Math.Min(tRange.Start, tRange.End);   var b = Math.Max(tRange.Start, tRange.End);
        var h = (b - a) / N;
        var t = a; 
        var w = y0;

        var points = new List<Point2<double>>(N);
        points.Add(new Point2<double>(t, w));

        for (var i = 1; i <= N; i++) {
            var k1 = h * f.Evaluate((t, w));
            var k2 = h * f.Evaluate((t + h/2, w + k1/2));
            var k3 = h * f.Evaluate((t + h/2, w + k2/2));
            var k4 = h * f.Evaluate((t + h, w + k3));

            w = w + (k1 + 2 * k2 + 2 * k3 + k4) / 6;    // Compute wi
            t = a + i * h;                              // Compute ti

            points.Add(new Point2<double>(t, w));
        }

        return new DoubleInterpolatedFunction(points);
    }
}

}