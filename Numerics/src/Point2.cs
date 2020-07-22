namespace Qkmaxware.Numerics {

/// <summary>
/// X,Y Coordinate pair
/// </summary>
/// <typeparam name="T">type of coordinate</typeparam>
public class Point2<T> {
    /// <summary>
    /// X coordinate
    /// </summary>
    public T X {get; private set;} 
    /// <summary>
    /// Y coordinate
    /// </summary>
    public T Y {get; private set;}

    /// <summary>
    /// Create a new coordinate pair
    /// </summary>
    /// <param name="x">x coordinate</param>
    /// <param name="y">y coordinate</param>
    public Point2(T x, T y) {
        this.X = x;
        this.Y = y;
    }
}

}