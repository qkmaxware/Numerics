using System.Collections.Generic;

namespace Qkmaxware.Numerics {

/// <summary>
/// Generic interface for interpolating a value between two points
/// </summary>
public interface IInterpolator<T> {
    /// <summary>
    /// Interpolate between start and end points
    /// </summary>
    /// <param name="previous">last value in the queue</param>
    /// <param name="start">range to start interpolating from</param>
    /// <param name="end">range to end interpolating to</param>
    /// <param name="next">next value in the queue</param>
    /// <param name="factor">interpolation factor</param>
    /// <returns>interpolate value between start and end</returns>
    T Interpolate (T previous, T start, T end, T next, T factor);
}

}