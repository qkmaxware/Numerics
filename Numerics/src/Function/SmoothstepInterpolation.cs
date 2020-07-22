using System;
using System.Linq;
using System.Collections.Generic;

namespace Qkmaxware.Numerics {

/// <summary>
/// Function interpolated using smoothstep
/// </summary>
public class SmoothstepInterpolation : IInterpolator<double> {
    public double Interpolate(double last, double start, double end, double next, double t) {
        var _01 = t * t * t * (t * (t * 6 - 15) + 10); // evaluation between 0(start) and 1(end)
        return _01 * end + (1 - _01) * start;
    }
}

}