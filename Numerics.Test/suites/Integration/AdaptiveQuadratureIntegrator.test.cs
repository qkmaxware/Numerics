using Qkmaxware.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Testing.Numerics {

[TestClass]
public class TestAdaptiveQuadratureIntegrator {
    [TestMethod]
    public void TestAdaptiveSimpson() {
        var integrator = new SimpsonAdaptiveQuadratureIntegrator(tolerance: 0.01);
        DoubleFunction func = new DoubleFunction((x) => Math.Pow(x, 2));
        DoubleRange range = new DoubleRange(-3, 3);
        
        var estimatedIntegral = integrator.Integrate(func, range);
        var realIntegral = 18; // -3 .. 3

        var difference = Math.Abs(realIntegral - estimatedIntegral);
        var smaller = difference < integrator.Tolerance;
        Assert.AreEqual(true, smaller);
    }
}

}