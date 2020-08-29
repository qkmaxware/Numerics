using System;
using System.Collections.Generic;

namespace Qkmaxware.Numerics {

/// <summary>
/// Solve boundary value problems with linear shooting
/// </summary>
public class LinearShootingBvpSolver : IBvpSolver<double> {
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

    private int M = 1000;
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
        var p = de.Px;
        var q = de.Qx;
        var r = de.Rx;

        // Step 1
        var h = (b - a) / N;
        var u1 = new double[N + 1];
        var u2 = new double[N + 1];
        var v1 = new double[N + 1];
        var v2 = new double[N + 1];

        u1[0] = y0;
        u2[0] = 0d;
        v1[0] = 0d;
        v2[0] = 1d;

        var xs = new double[N + 1];
        var w1 = new double[N + 1];
        var w2 = new double[N + 1];

        // Step 2
        for (var i = 0; i <= N - 1; i++) {
            // Step 3
            var x = a + i * h;

            // Step 4
            // Solve for approximation to 'y'
            var k11 = h * u2[i];
            var k12 = h * (p[x] * u2[i] + q[x]*u1[i] + r[x]);
            var k21 = h * (u2[i] + 0.5 * k12);
            var k22 = h * (p[x + h/2]*(u2[i] + 0.5 * k12) + q[x + h/2]*(u1[i] + 0.5 * k11) + r[x + h/2]);
            var k31 = h * (u2[i] + 0.5 * k22);
            var k32 = h * (p[x + h/2]*(u2[i] + 0.5 *k22) + q[x + h/2]*(u1[i] + 0.5*k21) + r[x + h/2]);
            var k41 = h * (u2[i] + k32);
            var k42 = h * (p[x + h]*(u2[i] + k32) + q[x + h]*(u1[i] + k31) + r[x + h]);

            u1[i+1] = u1[i] + (1f/6f) * (k11 + 2 * k21 + 2 * k31 + k41);
            u2[i+1] = u2[i] + (1f/6f) * (k12 + 2 * k22 + 2 * k32 + k42);

            // Solve for approximation to 'y`'
            var kp11 = h * v2[i];
            var kp12 = h * (p[x] * v2[i] + q[x]*v1[i] + r[x]);
            var kp21 = h * (v2[i] + 0.5 * kp12);
            var kp22 = h * (p[x + h/2]*(v2[i] + 0.5 * kp12) + q[x + h/2]*(v1[i] + 0.5 * kp11) + r[x + h/2]);
            var kp31 = h * (v2[i] + 0.5 * kp22);
            var kp32 = h * (p[x + h/2]*(v2[i] + 0.5 *kp22) + q[x + h/2]*(v1[i] + 0.5*kp21) + r[x + h/2]);
            var kp41 = h * (v2[i] + kp32);
            var kp42 = h * (p[x + h]*(v2[i] + kp32) + q[x + h]*(v1[i] + kp31) + r[x + h]);

            v1[i + 1] = v1[i] + (1f/6f) * (kp11 + 2 * kp21 + 2 * kp31 + kp41);
            v2[i + 1] = v2[i] + (1f/6f) * (kp12 + 2 * kp22 + 2 * kp32 + kp42);
        } 

        // Step 5
        w1[0] = y0;
        w2[0] = (y1 - u1[N]) / v1[N];
        xs[0] = a;

        // Step 6
        for (var i = 1; i <= N; i++) {
            w1[i] = u1[i] + w2[0]*v1[i];
            w2[i] = u2[i] + w2[0]*v2[i];
            xs[i] = a + i * h;
        }

        // Step 7
        return (new DoubleInterpolatedFunction(xs, w1), new DoubleInterpolatedFunction(xs, w2));
    }
}

}