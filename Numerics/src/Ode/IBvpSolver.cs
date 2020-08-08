namespace Qkmaxware.Numerics {

/// <summary>
/// Solver for boundary value problem ordinary differential equations
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IBvpSolver<T> {
    /// <summary>
    /// Solve the boundary value problem where y`` = f(t, y, y`), a <= t <= b, y(a) = y0, y(b) = y1
    /// </summary>
    /// <param name="de">function of three arguments (t,y,y`)</param>
    /// <param name="tRange">range (a,b) of values for t</param>
    /// <param name="y0">value of y at a</param>
    /// <param name="y1">value of y at b</param>
    /// <returns>approximation for the function y and y`</returns>
    (IFunction<T> y, IFunction<T> dy) SolveBoundary(SecondOrderDE<T> de, Range<T> tRange, T y0, T y1);
}

}