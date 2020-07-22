using System;
using System.Linq;
using System.Collections.Generic;

namespace Qkmaxware.Numerics {

/// <summary>
/// Function interpolated using cubic-interpolation
/// </summary>
public class CubicInterpolation : IInterpolator<double> {
    public double Interpolate(double last, double start, double end, double next, double t) {
        var t2 = t * t;
        var a0 = next - end - last + start;
        var a1 = last - start - a0;
        var a2 = end - last;
        var a3 = start;

        return(a0 * t * t2 + a1 * t2 + a2 * t + a3);
    }
}

}