using System;
using System.Numerics;

namespace Qkmaxware.Numerics {

/// <summary>
/// Interface for a calculator that can compute definite integrals
/// </summary>
/// <typeparam name="T">numeric type</typeparam>
public interface IDefiniteIntegrator<T> {
    /// <summary>
    /// Integrate the given function between the range
    /// </summary>
    /// <param name="fx">function to integrate</param>
    /// <param name="range">range to integrate over</param>
    /// <returns>definite integral</returns>
    T Integrate(IFunction<T> fx, Range<T> range);
}

}