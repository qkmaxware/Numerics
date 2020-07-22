using System;
using System.Numerics;

namespace Qkmaxware.Numerics {

/// <summary>
/// Static class for extensions related to complex numbers
/// </summary>
public static class ComplexExtensions {
    /// <summary>
    /// Easy extension method to create an imaginary value
    /// </summary>
    /// <param name="convertible">imaginary value</param>
    /// <returns>complex number</returns>
    public static Complex i(this IConvertible convertible) {
        return new Complex(0, Convert.ToDouble(convertible));
    }
}

}