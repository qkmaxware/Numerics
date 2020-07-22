using System;
using System.Linq;
using System.Collections.Generic;

namespace Qkmaxware.Numerics {

/// <summary>
/// Function interpolated using cosine-interpolation
/// </summary>
public class CosineInterpolation : IInterpolator<double> {
    public double Interpolate(double last, double start, double end, double next, double t) {
        var t2 = (1 - Math.Cos(t * Math.PI)) / 2;
        return start * (1 - t2) + end * t2;
    }
}

}