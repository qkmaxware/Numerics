using System;
using System.Collections.Generic;

namespace Qkmaxware.Numerics {

/// <summary>
/// Solve boundary value problems with Nonlinear Shooting and Newton's method
/// </summary>
public class NewtonNonlinearShootingBvpSolver : IBvpSolver<double> {
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

    private int M = 100;
    /// <summary>
    /// Max number of iterations
    /// </summary>
    public int Iterations {
        get {
            return M;
        } set {
            M = Math.Max(value, 1);
        }
    }

    private double tolerance = 0.01;
    /// <summary>
    /// Accuracy tolerance level
    /// </summary>
    public double Tolerance {
        get {
            return tolerance;
        } set {
            this.Tolerance = Math.Abs(tolerance);
        }
    }

    /// <summary>
    /// Solve the boundary value problem where y`` = f(t, y, y`), a <= t <= b, y(a) = y0, y(b) = y1
    /// </summary>
    /// <param name="de">function of three arguments (t,y,y`)</param>
    /// <param name="tRange">range (a,b) of values for t</param>
    /// <param name="y0">value of y at a</param>
    /// <param name="y1">value of y at b</param>
    /// <returns>approximation for the function y and y`</returns>
    public (IFunction<double> y, IFunction<double> dy) SolveBoundary(SecondOrderDE<double> de, Range<double> tRange, double y0, double y1) {
        // INPUTS: endpoints a and b for t, boundary conditions y0 and y1, number of subintervals N
        var a = tRange.Start; var b = tRange.End;
        var _1 = 0; var _2 = 1;
        var f = de.GetEquationForD2y();
        var p = de.Px;
        var q = de.Qx;
        var r = de.Rx;
        List<Point2<double>> yPoints = new List<Point2<double>>();
        List<Point2<double>> yPrimePoints = new List<Point2<double>>();
        var w = new double[2,N + 1];

        // Step 1
        var h = (b - a) / N;
        var k = 1.0;
        var TK = (y1 - y0) / (b - a);

        // Step 2
        while (k <= M) {
            // Step 3
            w[_1,0] = y0;
            w[_2,0] = TK;
            var u1 = 0.0;
            var u2 = 1.0;

            // Step 4
            for (var i = 1; i <= N; i++) {
                // Step 5
                var x = a + (i - 1) * h;

                // Step 6
                var k11 = h * w[_2,i -1];
                var k12 = h * f.Evaluate((x, w[_1,i-1], w[_2,i-1]));
                var k21 = h * ( w[_2,i-1] + 0.5 * k12 );
                var k22 = h * f.Evaluate((x + h * 0.5, w[_1,i-1] + 0.5 * k11, w[_2,i-1] + 0.5 * k12));
                var k31 = h * ( w[_2,i-1] + 0.5 * k22 );
                var k32 = h * f.Evaluate((x + h * 0.5, w[_1,i-1] + 0.5 * k21, w[_2,i-1] + 0.5 * k22));
                var k41 = h * ( w[_2,i-1] + k32 );
                var k42 = h * f.Evaluate((x + h, w[_1,i-1] + k31, w[_2,i-1] + k32));

                var next_w1i = w[_1,i-1] + (k11 + 2*k21 + 2*k31 + k41) / 6;
                var next_w2i = w[_2,i-1] + (k12 + 2*k22 + 2*k32 + k42) / 6;

                var kp11 = h * u2;
                var kp12 = h * (p.Evaluate(x)*u1 + q.Evaluate(x)*u2); 
                var kp21 = h * (u2 * 0.5*kp12);
                var kp22 = h * (p.Evaluate(x + h/2)*(u1 + 0.5*kp11) + q.Evaluate(x + h/2)*(u2 + 0.5*kp12));
                var kp31 = h * (u2 + 0.5 *kp22);
                var kp32 = h * (p.Evaluate(x + h/2)*(u1 + 0.5*kp21) + q.Evaluate(x + h/2)*(u2 + 0.5*kp22));
                var kp41 = h * (u2 + kp32);
                var kp42 = h * (p.Evaluate(x + h)*(u1 + k31) + q.Evaluate(x + h)*(u2 + kp32));

                u1 = u1 + (1.0/6.0) * (kp11 + 2*kp21 + 2*kp31 + kp41);
                u2 = u2 + (1.0/6.0) * (kp12 + 2*kp22 + 2*kp32 + kp42); 
            }

            // Step 7
            if (Math.Abs(w[_1,N] - y1) <= tolerance) {
                // Step 8
                for (var i = 0; i <= N; i++) {
                    var x = a + i * h;
                    // w2i is an approximation to y`(x)
                    yPrimePoints.Add(new Point2<double>(
                        x: x,
                        y: w[_2,i]
                    ));
                    // w1i is an approximation to y(x)
                    yPoints.Add(new Point2<double>(
                        x: x,
                        y: w[_1,i]
                    ));
                }
                // Step 9
                // OUTPUT: Approximations w to y(x) and y`(x) for each 0..N
                return (y: new DoubleInterpolatedFunction(yPoints), dy: new DoubleInterpolatedFunction(yPrimePoints));
            }

            // Step 10
            TK = TK - (w[_1,N] - y1) / u1;
            k = k + 1;
        }

        // Step 11 (unsuccessful)
        throw new ArithmeticException("Maximum number of iterations exceeded");
    }
}

}