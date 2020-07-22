using System.Collections.Generic;

namespace Qkmaxware.Numerics {

/// <summary>
/// Generic interface for a function with 1 parametre that is interpolated between the given sample points
/// </summary>
public interface IInterpolatedFunction<T> : IFunction<T> {
    /// <summary>
    /// Samples used for function interpolation
    /// </summary>
    IEnumerable<Point2<T>> Samples {get;}
    /// <summary>
    /// Interpolation method to use
    /// </summary>
    IInterpolator<T> InterpolationMethod {get; set;}
}

}