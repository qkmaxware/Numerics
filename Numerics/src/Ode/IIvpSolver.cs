namespace Qkmaxware.Numerics {

/// <summary>
/// Solver for initial value problem ordinary differential equations
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IIvpSolver<T> {
    /// <summary>
    /// Solve the initial value problem where y` = f(t, y), a <= t <= b, y(a) = y0
    /// </summary>
    /// <param name="f">function of two arguments (t,y)</param>
    /// <param name="tRange">range (a,b) of values for t</param>
    /// <param name="y0">value of y at a</param>
    /// <returns>approximation for the function y</returns>
    IFunction<T> Solve(IFunction2<T> expr, Range<T> tRange, T y0);
}

}