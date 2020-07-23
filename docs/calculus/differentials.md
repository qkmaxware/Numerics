# Differential Equations

Differential equations are equations that relate one or more functions to their associated derivatives. Such equations are common in engineering, physics, economics and biology. Ordinary differential equations are differential equations that only have a single independent variable. Partial differential equations have many independent variables. 

```math
\frac{dy}{dt} = f(t),
\frac{dy}{dt} = f(t, y)
```

## Initial Value Problems

Initial value problems are attempts to determine the value of a function from a differential equation knowing only the initial conditions of the function.

```math
\frac{dy}{dt} = f(t, y), a \leq t \leq b, y(a) = y0
```

### Euler's Method
```cs
DoubleRange t = new DoubleRange(0, 2);
double y0 = 0.5;
DoubleFunction2 dydt = new DoubleFunction2((t,y) => (y - t * t + 1));

var solver = new EulerIvpSolver();
var y = solver.Solve(dydt, t, y0); // Approximates y = (t + 1)^2 - 0.5 * e^t
```

### Fourth Order Runge-Kutta
```cs
DoubleRange t = new DoubleRange(0, 2);
double y0 = 0.5;
DoubleFunction2 dydt = new DoubleFunction2((t,y) => (y - t * t + 1));

var solver = new RungeKuttaIvpSolver();
var y = solver.Solve(dydt, t, y0); // Approximates y = (t + 1)^2 - 0.5 * e^t
```

## Boundary Value Problems