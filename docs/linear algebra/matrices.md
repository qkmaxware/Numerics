# Matrices

A matrix is a rectangular array of numbers, symbols, or expressions which are arranged into rows and columns. Typically matrices are referred to by their number of rows first then by the number of columns. An $`M \times N`$ matrix will have $`M`$ rows and $`N`$ columns. 

```math
\begin{bmatrix}
a_{11} & a_{12} & \cdots & a_{1n} \\
a_{11} & a_{12} & \cdots & a_{1n} \\
\vdots & \vdots & \ddots & \vdots \\
a_{m1} & a_{m2} & \cdots & a_{mn} \\
\end{bmatrix}
```

The base class for representing matrices in this library is the `Matrix<T>` class where `T` is the data-type used for the matrix elements. Like other parts of this library, the matrix class requires that a Calculator be provided so it knows how to perform element wise operations. The sub-classes `IntMatrix`, `DoubleMatrix`, and `ComplexMatrix` will set the appropriate calculator automatically. However, these sub-classes should rarely be used as method arguments or class properties; instead, use the templated base class. For instance, instead of `DoubleMatrix` as a method argument, use `Matrix<double>` instead to make the argument not specific to that subclass. This is important because all of the matrix operations return instances of the base class rather than of the specialized subclass.

This library supports all the basic matrix arithmetic operations as well as some other common matrix operations, some of which are listed below. 

## Addition

```cs
IntMatrix xs = new int[2,2]{
    {2, 2},
    {2, 2}
};
IntMatrix ys = new int[2,2]{
    {1, 2},
    {3, 4}
};
var zs = xs + ys;
```

## Subtraction

```cs
IntMatrix xs = new int[2,2]{
    {2, 2},
    {2, 2}
};
IntMatrix ys = new int[2,2]{
    {1, 2},
    {3, 4}
};
var zs = xs - ys;
```

## Scalar Multiplication
```cs
IntMatrix xs = new int[2,2]{
    {2, 2},
    {2, 2}
};
var xs4 = xs * 4;
```

## Multiplication

```cs
IntMatrix xs = new int[2,2]{
    {2, 2},
    {2, 2}
};
IntMatrix ys = new int[2,2]{
    {1, 2},
    {3, 4}
};
var zs = xs * ys;
```

## Trace

```cs
IntMatrix xs = new int[2,2]{
    {2, 2},
    {2, 2}
};
var trace = xs.Trace();
```

## Inverse

```cs
DoubleMatrix m = new double[2,2]{
    {1,2},
    {3,4}
}
var inverse = m.Inverse();
```

## Determinant

```cs
DoubleMatrix m2 = new double[3,3]{
    {2,9,4},
    {7,5,3},
    {6,1,8}
};
var det = m2.Determinant();
```