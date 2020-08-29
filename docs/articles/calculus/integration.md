# Integration
Whereas differentiation describes the rate of change of a curve, integration describes the displacement area of a curve. Integration is also sometimes referred to an anti-derivative in its indefinite form as it can act as an inverse of differentiation. 

<canvas id="c1"></canvas>

## Definite Integration
In definite integration, one computes the area under the curve between a given start and end point.

### Composite Methods
Composite methods for computing the definite integral involve splitting the domain into several equal slices. The area under the curve for each slice is approximated and then all the areas are summed to provide an approximation of the area within the domain. 

#### Trapezoidal Rule
$$
\int_{x_0}^{x_1} f(x) dx = \frac{h}{2} [f(x_0) + f(x_1)] - \frac{h^3}{12}f″(\xi)
$$

```cs
var integrator = new TrapezoidalIntegrator();
DoubleFunction fn = new DoubleFunction((x) => Math.Pow(x, 2));
DoubleRange domain = 1..3;
var area = integrator.Integrate(fn, domain);
```

#### Simpson's Rule
$$
\int_{x_0}^{x_2} f(x) dx = \frac{h}{3} [f(x_0) + 4f(x_1) + f(x_2)] - \frac{h^5}{90}f^{(4)}(\xi)
$$

```cs
var integrator = new SimpsonIntegrator();
DoubleFunction fn = new DoubleFunction((x) => Math.Pow(x, 2));
DoubleRange domain = 1..3;
var area = integrator.Integrate(fn, domain);
```

### Adaptive Methods
Adaptive methods for computing the definite integral involve a recursive processes in which parts of the curve that have more variation are split into smaller slices for more accurate approximations whereas smoother parts that have less variation can use less precise approximations. 

#### Simpson's Rule

```cs
var integrator = new SimpsonAdaptiveQuadratureIntegrator(tolerance: 0.01);
DoubleFunction func = new DoubleFunction((x) => Math.Pow(x, 2));
DoubleRange domain = new DoubleRange(-3, 3);
var estimatedIntegral = integrator.Integrate(func, domain);
```

## Indefinite Integration
In indefinite integration, the area is not directly computed. Instead, an anti-derivative function is created that can be evaluated between any two points to compute the definite integral.

$$
F(x) = \int f(x)dx
$$
$$
\int_{a}^{b}f(x)dx = [F(x)]_{a}^{b} = F(b) - F(a)
$$

Currently indefinite integration is **not yet supported**. 

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
    var domain = [-5, 5];
    var elements = domain[1] - domain[0];
    var xs = Array.from(Array(elements + 1), (_, i) => i + domain[0]);
    var ys = xs.map((x) => func(x));
    var options = {};
    var data = {
        labels: xs,
        datasets: [
            {
                type: 'line',
                label: 'y',
                data: ys,
                backgroundColor: 'rgba(41, 128, 185, 0.5)'
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