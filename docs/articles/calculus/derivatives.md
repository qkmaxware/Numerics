# Differentiation
Often mathematical problems involve finding tangent lines to a curve, or physics problems ask for the rate of change for a physical property such as position or velocity. These problems are related in that they can both be solved using derivatives. Derivatives describe the slope of a curve at any point. The meaning of these slopes is determined only by the context of the problem being asked. Derivatives are analogous to computing the slope between two points on a curve and taking the limit as those two points approach each other.

$$
f'(x) = \lim_{h \rightarrow 0}\frac{f(x + h) - f(x)}{h}
$$

The chart below shows the function $y = x^{2}$ as well as its derivative $y' = 2x$. For any point $x$ on the function, its slope can be determined by evaluating the derivative at the same $x$.

<canvas id="c1"></canvas>

In numerical differentiation the derivative of a function is not solved symbolically like above. Instead numerical techniques only compute the derivatives of a few specific points and interpolation is used to approximate the slope in-between those values. As such, the derivatives at the sample points will have the highest computational accuracy whereas the interpolated values can have substantially lower accuracy. Correctly choosing the right points to sample the derivative at increases the quality of the resulting approximation. 

## Three-Point Formulae

### Midpoint Method
$$
f'(x_0) = \frac{1}{2h}[f(x_0 + h) - f(x_0 - h)]
$$

```cs
DoubleRange domain = new DoubleRange(start: 0, end: Math.PI, increment: 0.01);
DoubleFunction f = new DoubleFunction((x) => Math.Sin(x));
var differentiator = new CentredThreePointDifferentiator(); // Midpoint method
var fPrime = differentiator.Differentiate(f, domain);
```

### Endpoint Method
$$
f'(x_0) = \frac{1}{2h}[-3f(x_0) + 4f(x_0 + h) - f(x_0 + 2h)]
$$

```cs
DoubleRange domain = new DoubleRange(start: 0, end: Math.PI, increment: 0.01);
DoubleFunction f = new DoubleFunction((x) => Math.Sin(x));
var differentiator = new EndpointThreePointDifferentiator(); // Endpoint method
var fPrime = differentiator.Differentiate(f, domain);
```

## Five-Point Formulae

### Midpoint Method
$$
f'(x_0) = \frac{1}{12h}[f(x_0 - 2h) - 8f(x_0 - h) + 8f(x_0 + h) - f(x_0 + 2h)]
$$

```cs
DoubleRange domain = new DoubleRange(start: 0, end: Math.PI, increment: 0.01);
DoubleFunction f = new DoubleFunction((x) => Math.Sin(x));
var differentiator = new CentredFivePointDifferentiator(); // Midpoint method
var fPrime = differentiator.Differentiate(f, domain);
```

### Endpoint Method
$$
f'(x_0) = \frac{1}{12h}[-25f(x_0) + 48f(x_0 + h) - 36f(x_0 + 2h) + 16f(x_0 + 3h) -3f(x_0 + 4h)]
$$

```cs
DoubleRange domain = new DoubleRange(start: 0, end: Math.PI, increment: 0.01);
DoubleFunction f = new DoubleFunction((x) => Math.Sin(x));
var differentiator = new EndpointFivePointDifferentiator(); // Endpoint method
var fPrime = differentiator.Differentiate(f, domain);
```

<!-- KaTeX -->
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/katex@0.12.0/dist/katex.min.css" integrity="sha384-AfEj0r4/OFrOo5t7NnNe46zW/tFgW6x/bCJG8FqQCEo3+Aro6EYUG4+cU+KJWu/X" crossorigin="anonymous">
<script defer src="https://cdn.jsdelivr.net/npm/katex@0.12.0/dist/katex.min.js" integrity="sha384-g7c+Jr9ZivxKLnZTDUhnkOnsh30B4H0rpLUpJ4jAIKs4fnJI+sEnkvrMWph2EDg4" crossorigin="anonymous"></script>
<script defer src="https://cdn.jsdelivr.net/npm/katex@0.12.0/dist/contrib/auto-render.min.js" integrity="sha384-mll67QQFJfxn0IYznZYonOWZ644AWYC+Pt2cHqMaRhXVrursRwvLnLaebdGIlYNa" crossorigin="anonymous"
    onload="renderMathInElement(document.body, { delimiters: [{left: '$$', right: '$$', display: true}, {left: '$', right: '$', display: false}] });"></script>
<!-- ChartJS -->
<script src="https://cdn.jsdelivr.net/npm/chart.js@2.9.3/dist/Chart.min.js"></script>
<script>
    (function(divId){
    var func = (x) => Math.pow(x, 2);
    var funcPrime = (x) => 2*x;
    var domain = [-5, 5];
    var elements = domain[1] - domain[0];
    var xs = Array.from(Array(elements + 1), (_, i) => i + domain[0]);
    var ys = xs.map((x) => func(x));
    var ysPrime = xs.map((x) => funcPrime(x));
    var options = {};
    var data = {
        labels: xs,
        datasets: [
            {
                type: 'line',
                label: 'y',
                data: ys,
                fill: false,
                borderColor: 'rgba(41, 128, 185, 1)'
            },
            {
                type: 'line',
                label: 'y`',
                data: ysPrime,
                fill: false,
                borderColor: 'rgba(137, 207, 240, 1)'
            }
        ]
    };
    var div = document.getElementById(divId);
    if (!div)
        return;   
    var ctx = div.getContext("2d");
    var chart = new Chart(ctx, {
        type: 'bar',
        data: data,
        options: options
    });
})("c1");
</script>