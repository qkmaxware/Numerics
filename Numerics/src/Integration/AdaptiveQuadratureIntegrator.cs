using System;
using System.Linq;
using System.Collections.Generic;

namespace Qkmaxware.Numerics {

/// <summary>
/// Base class for integrations methods using an adaptive quadrature scheme
/// </summary>
public abstract class BaseAdaptiveQuadratureIntegrator : IDefiniteIntegrator<double> {
    /// <summary>
    /// Max recursive depth before algorithm stops
    /// </summary>
    public int MaxDepth {get; private set;}
    /// <summary>
    /// Desired error tolerance
    /// </summary>
    public double Tolerance {get; private set;}

    public BaseAdaptiveQuadratureIntegrator(double tolerance = 0.01, int maxDepth = 100) {
        this.Tolerance = tolerance;
        this.MaxDepth = maxDepth;
    }

    /// <summary>
    /// Estimate the integral of the function between the two endpoints
    /// </summary>
    /// <param name="fx">function to integrate</param>
    /// <param name="x0">start point</param>
    /// <param name="x2">end point</param>
    /// <returns>estimated area</returns>
    protected abstract double EstimateIntegralOver(IFunction<double> fx, double x0, double x2);
    /// <summary>
    /// Check if the error of an estimated area is less than the desired tolerance
    /// </summary>
    /// <param name="fx">function</param>
    /// <param name="estimate">integral estimate</param>
    /// <param name="x0">start range</param>
    /// <param name="x2">end range</param>
    /// <returns>true if the error is larger than the tolerance; false otherwise</returns>
    protected abstract bool IsErrorLargerThanTolerance (IFunction<double> fx, double estimate, double x0, double x2);

    public double Integrate(IFunction<double> fx, Range<double> range) {
        var invalidRanges = new Queue<(int, Range<double>)>();
        invalidRanges.Enqueue((0, range));
        var validRanges = new List<double>();

        while (invalidRanges.Count > 0) {
            var current = invalidRanges.Dequeue();
            var currentdepth = current.Item1;
            var currentRange = current.Item2;

            // Compute the integral and the error term
            var integral = EstimateIntegralOver(fx, currentRange.Start, currentRange.End);
            var error = IsErrorLargerThanTolerance(fx, integral, currentRange.Start, currentRange.End);

            // If the integral is good, use it
            if (error || currentdepth > MaxDepth) {
                validRanges.Add(integral);
            } 
            // Else divide the range and try again
            else {
                var midPoint = range.Start + (range.End - range.Start) * 0.5;
                invalidRanges.Enqueue((currentdepth + 1, new DoubleRange(currentRange.Start, midPoint)));
                invalidRanges.Enqueue((currentdepth + 1, new DoubleRange(midPoint, currentRange.End)));
            }
        }

        return validRanges.Sum();
    }
}

/// <summary>
/// Adaptive quadrature integration using Simpson's method
/// </summary>
public class SimpsonAdaptiveQuadratureIntegrator : BaseAdaptiveQuadratureIntegrator {

    public SimpsonAdaptiveQuadratureIntegrator(double tolerance = 0.01, int maxDepth = 100) : base(tolerance, maxDepth) {}

    protected override double EstimateIntegralOver (IFunction<double> fx, double x0, double x2) {
        // Simpson's method
        var H = x2 - x0;
        var h = 0.5 * H;
        var x1 = x0 + h;
        var fx0 = fx.Evaluate(x0);
        var fx1 = fx.Evaluate(x1);
        var fx2 = fx.Evaluate(x2); 

        return (h / 3) * (fx0 + 4*fx1 + fx2);
    }

    protected override bool IsErrorLargerThanTolerance (IFunction<double> fx, double estimate, double x0, double x2) {
        // Error is the difference between the integral and a more precise integral
        var midPoint = x0 + (x2 - x0) * 0.5;
        var error = Math.Abs( (EstimateIntegralOver(fx, x0, midPoint) + EstimateIntegralOver(fx, midPoint, x2)) - estimate );
        return error < 15 * this.Tolerance;
    }
}

}