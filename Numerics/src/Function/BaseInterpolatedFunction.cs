using System;
using System.Linq;
using System.Collections.Generic;

namespace Qkmaxware.Numerics {

/// <summary>
/// Base class for a function that is interpolated between sample points
/// </summary>
public abstract class BaseInterpolatedFunction<T> : IInterpolatedFunction<T> where T:IComparable {

    private Point2<T>[] points;
    
    public IEnumerable<Point2<T>> Samples => Array.AsReadOnly(this.points);
    public IInterpolator<T> InterpolationMethod {get; set;}

    public ICalculator<T> Calculator {get; private set;}

    public BaseInterpolatedFunction (ICalculator<T> calculator, IEnumerable<Point2<T>> samples) {
        this.Calculator = calculator;
        this.points = samples.ToArray();
        this.InterpolationMethod = new LinearInterpolation<T>(calculator);
        Array.Sort(this.points, (a, b) => a.X.CompareTo(b.X)); // sort this in terms of x
    }

    public BaseInterpolatedFunction(ICalculator<T> calculator, T[] xs, T[] ys) {
        this.Calculator = calculator;
        this.points = new Point2<T>[Math.Min(xs.Length, ys.Length)];
        this.InterpolationMethod = new LinearInterpolation<T>(calculator);
        for (var i = 0; i < this.points.Length; i++) {
            this.points[i] = new Point2<T>(x: xs[i], y: ys[i]);
        }
        Array.Sort(this.points, (a, b) => a.X.CompareTo(b.X)); // sort this in terms of x
    }

    protected bool IsInRange(T x) {
        // Not enough points
        if (points.Length < 2)
            return false;
        // Out of range
        if (LessThan(x, points.First().X)) {
            return false;
        }
        if (GreaterThan(x, points.Last().X)) {
            return false;
        }
        return true;
    }

    private bool LessThan(T first, T second) {
        return first.CompareTo(second) < 0;
    }

    private bool LessEqualThan(T first, T second) {
        return LessThan(first, second) || Equal(first, second);
    }

    private bool GreaterThan(T first, T second) {
        return first.CompareTo(second) > 0;
    }

    private bool GreaterEqualThan(T first, T second) {
        return GreaterThan(first, second) || Equal(first, second);
    }

    private bool Equal (T first, T second) {
        return first.CompareTo(second) == 0;
    }

    public T Evaluate (T x) {
        // Check that x is in range, if not return the endpoints (no restriction on the values we can send it)
        if (points.Length < 1)
            throw new IndexOutOfRangeException(x?.ToString() ?? string.Empty);
        var firstIndex = 0;
        var lastIndex = points.Length - 1;
        var first = points[firstIndex];
        var last  = points[lastIndex ];
        if (LessEqualThan(x, first.X))
            return first.Y;
        if (GreaterEqualThan(x, last.X))
            return last.Y;

        // Get interpolation factor (there is not officially at least 2 points)
        int lhs;
        int rhs = 1;
        for (lhs = 0; lhs < points.Length; lhs++, rhs++) {
            var lx = points[lhs].X;
            var rx = points[rhs].X;

            if (Equal(x, lx))
                return points[lhs].Y;
            if (Equal(x, rx))
                return points[rhs].Y;

            if (LessEqualThan(lx, x) && GreaterEqualThan(rx, x))
                break;
        }
        var lastPoint = lhs > 0 ? points[lhs - 1].Y : points[lhs].Y;                    // prior point or current
        var nextPoint = rhs < points.Length - 1 ? points[rhs + 1].Y : points[rhs].Y;    // next point or current

        var T = Calculator.Divide(
            Calculator.Subtract(x, points[lhs].X),
            Calculator.Subtract(points[rhs].X, points[lhs].X)
        );                                                                              // T is between 0 and 1

        // Get interpolated value
        return InterpolationMethod.Interpolate(
            lastPoint,
            points[lhs].Y,
            points[rhs].Y,
            nextPoint,
            T
        );
    }
}

/// <summary>
/// Interpolated function using doubles
/// </summary>
public class DoubleInterpolatedFunction : BaseInterpolatedFunction<double> {
    public DoubleInterpolatedFunction (IEnumerable<Point2<double>> samples) : base(DoubleCalculator.Instance, samples) {}

    public DoubleInterpolatedFunction(double[] xs, double[] ys) : base(DoubleCalculator.Instance, xs, ys) {}
}

}