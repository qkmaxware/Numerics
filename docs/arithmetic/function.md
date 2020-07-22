# Functions

## Native Functions
Native functions are functions that can be represented exactly by the evaluation of a C# function. These functions are considered **exact** in that they are expected to work for all valid input parametres. Native functions are classes that implement the function interface abstraction `IFunction`. The example below shows a native C# function which works for all valid parametres of type `double`.

```cs
DoubleFunction func = new DoubleFunction((x) => Math.Sin(x));
```

## Interpolated Functions
Often, functions cannot be represented exactly on a computer. This could be because the function is derived from experimental data points and the actual function is unknown, or because the actual function it too computationally heavy. For these cases, function interpolation is used. Interpolated functions all extend from the `BaseInterpolatedFunction` class. For interpolated functions, a series of sample points are needed. For any value on the interpolated curve, the value is the result of interpolating a value from among the provided sample points. Good choices of the sample points and the interpolation method are a necessity for the quality of the interpolated function. 

A comparison of some of the common interpolation methods can be seen in the figure below. By default, linear interpolation is used unless otherwise stated. However this can be changed by assigning new interpolation functions to the `InterpolationMethod` member of the `BaseInterpolatedFunction` class.

<canvas id="c1"></canvas>

Interpolated functions behave exactly like native functions and can be used anywhere that accepts an `IFunction` as an argument. 

Usually the `double` data-type is preferred for representing real values. The class `DoubleInterpolatedFunction` is a class that extends from `BaseInterpolatedFunction` but is specialized for the `double` data-type.

```cs
var xs = new double[] { ... };
var ys = new double[] { ... };

var function = new DoubleInterpolatedFunction(xs, ys);
```

<script src="https://cdn.jsdelivr.net/npm/chart.js@2.9.3/dist/Chart.min.js"></script>
<script src="c1.js"></script>