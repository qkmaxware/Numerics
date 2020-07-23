using Qkmaxware.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;

namespace Testing.Numerics {

[TestClass]
public class LinearInterpolatedFunctionTest : DerivativeFunctionTest{
    [TestMethod]
    public void TestFunction() {
        Func<double,double> fn = (x) => Math.Pow(x, 2); // x ^ 2

        // Generated range is -10 to 10 incrementing by 1
        var xs = new double[21];
        var ys = new double[21];
        for (var i = 0; i < xs.Length; i++) {
            xs [i] = -10 + i;
            ys [i] = fn(xs[i]);
        }

        var fnPrime = new DoubleInterpolatedFunction(xs, ys);
        fnPrime.InterpolationMethod = new LinearInterpolation<double>(DoubleCalculator.Instance);

        SaveCsv("x2.linear", fnPrime, fn);
    }
}

}