using System;
using System.Collections.Generic;

namespace Qkmaxware.Numerics {

/// <summary>
/// Generic range of values
/// </summary>
/// <typeparam name="T">numeric type</typeparam>
public class Range<T> : ICalculationHelper<T>{
    /// <summary>
    /// Start of the range
    /// </summary>
    public T Start {get; private set;}
    /// <summary>
    /// End of the range
    /// </summary>
    public T End {get; private set;}
    /// <summary>
    /// Change between values within the range
    /// </summary>
    public T Increment {get; private set;}

    public ICalculator<T> Calculator {get; private set;}

    /// <summary>
    /// All values within the range
    /// </summary>
    public IEnumerable<T> All {
        get {
            var current = Start;
            bool flipComparison = Calculator.Compare(Start, End) > 0 ? false : true;

            do {
                yield return current;
                current = Calculator.Add(current, Increment);
            } while (
                !flipComparison 
                ? Calculator.Compare(current, End) <= 0
                : Calculator.Compare(current, End) >= 0
            );
        }
    }

    public Range(ICalculator<T> calculator, T start, T end, T increment) {
        this.Calculator = calculator;
        this.Start = start;
        this.End = end;
        this.Increment = increment;
    }

    /// <summary>
    /// Range to string
    /// </summary>
    public override string ToString() {
        return $"{this.Start}:{this.Increment}:{this.End}";
    }

    public override bool Equals(object obj) {
        return obj switch {
            Range<T> rng => this.Calculator.Compare(this.Start, rng.Start) == 0 
                            && this.Calculator.Compare(this.End, rng.End) == 0
                            && this.Calculator.Compare(this.Increment, rng.Increment) == 0,
            _ => base.Equals(obj)
        };
    }

    public override int GetHashCode() {
        return HashCode.Combine(this.Start, this.End, this.Increment);
    }
}


/// <summary>
/// Range of integers
/// </summary>
public class IntRange : Range<int> {
    public IntRange(int start, int end, int increment = 1) : base(IntCalculator.Instance, start, end, increment) {}

    /// <summary>
    /// Convert a system range implicitly to a integer range
    /// </summary>
    /// <param name="range">range to convert</param>
    public static implicit operator IntRange (Range range) {
        return new IntRange(range.Start.Value, range.End.Value, 1);
    }
}

/// <summary>
/// Range of doubles
/// </summary>
public class DoubleRange : Range<double> {
    public DoubleRange(double start, double end, double increment = 1) : base(DoubleCalculator.Instance, start, end, increment) {}

    /// <summary>
    /// Convert an integer range implicitly to a double range
    /// </summary>
    /// <param name="range">range to convert</param>
    public static implicit operator DoubleRange (IntRange range) {
        return new DoubleRange(range.Start, range.End, range.Increment);
    }

    /// <summary>
    /// Convert a system range implicitly to a integer range
    /// </summary>
    /// <param name="range">range to convert</param>
    public static implicit operator DoubleRange (Range range) {
        return new DoubleRange(range.Start.Value, range.End.Value, 1);
    }
}
    
}