namespace Qkmaxware.Numerics {
    
/// <summary>
/// Interface for tools that deliver their own calculator
/// </summary>
/// <typeparam name="T">calculator type</typeparam>
public interface ICalculationHelper<T> {
    /// <summary>
    /// Obtain the calculator
    /// </summary>
    ICalculator<T> Calculator {get;}
}

}