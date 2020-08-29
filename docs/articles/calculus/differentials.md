# Differential Equations

Differential equations are equations that relate one or more functions to their associated derivatives. Such equations are common in engineering, physics, economics and biology. Ordinary differential equations are differential equations that only have a single independent variable. Partial differential equations have many independent variables. 

$$
\frac{dy}{dt} = f(t),
\frac{dy}{dt} = f(t, y)
$$

## Initial Value Problems

Initial value problems are attempts to determine the value of a function from a differential equation knowing only the initial conditions of the function.

$$
\frac{dy}{dt} = f(t, y), a \leq t \leq b, y(a) = y0
$$

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

$$
\frac{d^2y}{dt^2} = p(t)\frac{dy}{dt} + q(t)y + r(t), a \leq t \leq b, y(a) = y0, y(b) = y1
$$

### Linear Shooting
```cs
DoubleRange t = new DoubleRange(0, Math.PI/4, increment: 0.1);
double y0 = -2;
double ypi_4 = 10;

// y`` = 0*y` -4*y + 0
var de = new SecondOrderDE<double>(new DoubleFunction((x) => 0), new DoubleFunction((x) => -4), new DoubleFunction((x) => 0));

var solver = new LinearShootingBvpSolver();
var (y, dy) = solver.SolveBoundary(de, t, y0, ypi_4);
```

<!-- KaTeX -->
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/katex@0.12.0/dist/katex.min.css" integrity="sha384-AfEj0r4/OFrOo5t7NnNe46zW/tFgW6x/bCJG8FqQCEo3+Aro6EYUG4+cU+KJWu/X" crossorigin="anonymous">
<script defer src="https://cdn.jsdelivr.net/npm/katex@0.12.0/dist/katex.min.js" integrity="sha384-g7c+Jr9ZivxKLnZTDUhnkOnsh30B4H0rpLUpJ4jAIKs4fnJI+sEnkvrMWph2EDg4" crossorigin="anonymous"></script>
<script defer src="https://cdn.jsdelivr.net/npm/katex@0.12.0/dist/contrib/auto-render.min.js" integrity="sha384-mll67QQFJfxn0IYznZYonOWZ644AWYC+Pt2cHqMaRhXVrursRwvLnLaebdGIlYNa" crossorigin="anonymous"
    onload="renderMathInElement(document.body, { delimiters: [{left: '$$', right: '$$', display: true}, {left: '$', right: '$', display: false}] });"></script>