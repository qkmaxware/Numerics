# Calculators

Sometimes it is preferred to use data-types other than C#'s `double` to describe certain mathematical operations. Floats, integers, complex numbers, or even boolean values for masks all have their uses. In order to make this possible within the capabilities of the C# language, calculators are employed through all aspects of this library. Calculators are objects that describe how basic arithmetic works for specific data-types. For the most part, calculators for primitive data-types such as double, or int will simply use the built in arithmetic operators such as `+` and `-`. However, the calculator abstraction can be used to create more unique arithmetic systems, or provide additional safety checks if that is so desired.

The base interface for a calculator object is the `ICalculator<T>` interface where `T` is the data-type whose operations are being described by the calculator. Calculators describe each following arithmetic operations as well as describing what value is used for the constant `1`:

- addition
- subtraction
- multiplication
- division
- negation
- comparison
- constant '1'

The following calculator classes come pre-built with this library.

- IntCalculator : ICalculator< int >
- DoubleCalculator : ICalculator< double >
- ComplexCalculator : ICalculator< Complex >



