# Differentiation
Often mathematical problems involve finding tangent lines to a curve, or physics problems ask for the rate of change for a physical property such as position or velocity. These problems are related in that they can both be solved using derivatives. Derivatives describe the slope of a curve at any point. The meaning of these slopes is determined only by the context of the problem being asked. Derivatives are analogous to computing the slope between two points on a curve and taking the limit as those two points approach each other.

```math
f'(x) = \lim_{h \rightarrow 0}\frac{f(x + h) - f(x)}{h}
```

The chart below shows the function $`y = x^{2}`$ as well as its derivative $`y' = 2x`$. For any point $`x`$ on the function, its slope can be determined by evaluating the derivative at the same $`x`$.

<canvas id="c1"></canvas>

In numerical differentiation the derivative of a function is not solved symbolically like above. Instead numerical techniques only compute the derivatives of a few specific points and interpolation is used to approximate the slope in-between those values. As such, the derivatives at the sample points will have the highest computational accuracy whereas the interpolated values can have substantially lower accuracy. Correctly choosing the right points to sample the derivative at increases the quality of the resulting approximation. 

## Three-Point Formulae

### Midpoint Method
```math
f'(x_0) = \frac{1}{2h}[f(x_0 + h) - f(x_0 - h)]
```

```cs
DoubleRange domain = new DoubleRange(start: 0, end: Math.PI, increment: 0.01);
DoubleFunction f = new DoubleFunction((x) => Math.Sin(x));
var differentiator = new CentredThreePointDifferentiator(); // Midpoint method
var fPrime = differentiator.Differentiate(f, domain);
```

### Endpoint Method
```math
f'(x_0) = \frac{1}{2h}[-3f(x_0) + 4f(x_0 + h) - f(x_0 + 2h)]
```

```cs
DoubleRange domain = new DoubleRange(start: 0, end: Math.PI, increment: 0.01);
DoubleFunction f = new DoubleFunction((x) => Math.Sin(x));
var differentiator = new EndpointThreePointDifferentiator(); // Endpoint method
var fPrime = differentiator.Differentiate(f, domain);
```

## Five-Point Formulae

### Midpoint Method
```math
f'(x_0) = \frac{1}{12h}[f(x_0 - 2h) - 8f(x_0 - h) + 8f(x_0 + h) - f(x_0 + 2h)]
```

```cs
DoubleRange domain = new DoubleRange(start: 0, end: Math.PI, increment: 0.01);
DoubleFunction f = new DoubleFunction((x) => Math.Sin(x));
var differentiator = new CentredFivePointDifferentiator(); // Midpoint method
var fPrime = differentiator.Differentiate(f, domain);
```

### Endpoint Method
```math
f'(x_0) = \frac{1}{12h}[-25f(x_0) + 48f(x_0 + h) - 36f(x_0 + 2h) + 16f(x_0 + 3h) -3f(x_0 + 4h)]
```

```cs
DoubleRange domain = new DoubleRange(start: 0, end: Math.PI, increment: 0.01);
DoubleFunction f = new DoubleFunction((x) => Math.Sin(x));
var differentiator = new EndpointFivePointDifferentiator(); // Endpoint method
var fPrime = differentiator.Differentiate(f, domain);
```

<script src="https://cdn.jsdelivr.net/npm/chart.js@2.9.3/dist/Chart.min.js"></script>
<script src="c1.js"></script>