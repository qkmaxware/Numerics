namespace Qkmaxware.Numerics {
    /// <summary>
    /// Interface representing any object that can generate values
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IValueGenerator<T> {
        /// <summary>
        /// Generate the next value in the sequence
        /// </summary>
        T Next();
        /// <summary>
        /// Generate the next value in the sequence between to endpoints
        /// </summary>
        /// <param name="min">min endpoint</param>
        /// <param name="max">max endpoint</param>
        T Next(T min, T max);
    }

}