using Qkmaxware.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Testing.Numerics {

[TestClass]
public class TestTrapezoidalIntegrator {
    [TestMethod]
    public void TestFunctionIntegration() {
        var integrator = new TrapezoidalIntegrator();
        DoubleFunction fn = new DoubleFunction((x) => Math.Pow(x, 2)); // real integral is ~8.7
        var area = integrator.Integrate(fn, (DoubleRange)(1..3));

        var trapezoid12 = 2.5;
        var trapezoid23 = 6.5;

        Assert.AreEqual(trapezoid12 + trapezoid23, area);
    }
}

}