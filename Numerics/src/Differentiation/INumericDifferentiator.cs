using System;

namespace Qkmaxware.Numerics {

/// <summary>
/// Interface for any class that can calculate the derivative of a function
/// </summary>
/// <typeparam name="T">numeric type</typeparam>
public interface INumericDifferentiator<T>  {
    /// <summary>
    /// Compute the derivative of the function over the range
    /// </summary>
    /// <param name="f">function</param>
    /// <param name="range">range of 'x' values</param>
    /// <returns>derivative function</returns>
    IFunction<T> Differentiate(IFunction<T> f, Range<T> range);
}

}