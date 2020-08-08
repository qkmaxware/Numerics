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
var y = solver.SolveInitial(new FirstOrderDE<double>(dydt), t, y0); // Approximates y = (t + 1)^2 - 0.5 * e^t
```

### Fourth Order Runge-Kutta
```cs
DoubleRange t = new DoubleRange(0, 2);
double y0 = 0.5;
DoubleFunction2 dydt = new DoubleFunction2((t,y) => (y - t * t + 1));

var solver = new RungeKuttaIvpSolver();
var y = solver.SolveInitial(new FirstOrderDE<double>(dydt), t, y0); // Approximates y = (t + 1)^2 - 0.5 * e^t
```

## Boundary Value Problems

Boundary value problems attempt to solve a differential equation together with a set of additional boundary conditions.

```math
\frac{d^2y}{dt^2} = p(t)\frac{dy}{dt} + q(t)y + r(t), a \leq t \leq b, y(a) = y0, y(b) = y1
```

### Nonlinear Shooting
```cs
DoubleRange t = new DoubleRange(0, Math.PI/4, increment: 0.1);
double y0 = -2;
double ypi_4 = 10;

// y`` = 0*y` -4*y + 0
var de = new SecondOrderDE<double>(new DoubleFunction((x) => 0), new DoubleFunction((x) => -4), new DoubleFunction((x) => 0));

var solver = new NewtonNonlinearShootingBvpSolver();
var (y, dy) = solver.SolveBoundary(de, t, y0, ypi_4);
```