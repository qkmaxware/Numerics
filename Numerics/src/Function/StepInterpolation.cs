using System;
using System.Linq;
using System.Collections.Generic;

namespace Qkmaxware.Numerics {

/// <summary>
/// Function interpolated using stepped-interpolation
/// </summary>
public class StepInterpolation : IInterpolator<double> {

    public StepInterpolation() {}

    public double Interpolate(double last, double start, double end, double next, double t) {
        return (t > 0.5) ? end : start;
    }
}

}