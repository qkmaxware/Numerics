using System;
using System.Numerics;

namespace Qkmaxware.Numerics {

/// <summary>
/// Interface for a calculator that can compute indefinite integrals
/// </summary>
/// <typeparam name="T">numeric type</typeparam>
public interface IIndefiniteIntegrator<T> {
    /// <summary>
    /// Integrate the given function between the range
    /// </summary>
    /// <param name="fx">function to integrate</param>
    /// <param name="range">range to integrate over</param>
    /// <returns>integral function</returns>
    IFunction<T> Integrate(IFunction<T> fx, Range<T> range);
}

}