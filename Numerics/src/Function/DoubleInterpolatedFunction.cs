using System;
using System.Linq;
using System.Collections.Generic;

namespace Qkmaxware.Numerics {

/// <summary>
/// Interpolated function using doubles
/// </summary>
public class DoubleInterpolatedFunction : BaseInterpolatedFunction<double> {
    public DoubleInterpolatedFunction (IEnumerable<Point2<double>> samples) : base(DoubleCalculator.Instance, samples) {}

    public DoubleInterpolatedFunction(double[] xs, double[] ys) : base(DoubleCalculator.Instance, xs, ys) {}
}

}