using Qkmaxware.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;

namespace Testing.Numerics {

[TestClass]
public class EulerIvpSolverTest {
    [TestMethod]
    public void Solve() {
        // y' = y - t^2 + 1, 0 <= t <= 2, y(0) = 0.5
        DoubleRange t = new DoubleRange(0, 2, increment: 0.1);
        double y0 = 0.5;
        DoubleFunction y = new DoubleFunction((t) => (t + 1)* (t + 1) - 0.5 * Math.Pow(Math.E, t));
        DoubleFunction2 yP = new DoubleFunction2((t,y) => (y - t * t + 1));

        var solver = new EulerIvpSolver();
        var approxy = solver.Solve(yP, t, y0);

        SaveCsv("EulerIVP", approxy, y, t);
    }

    protected static void SaveCsv(string name, IFunction<double> approx, IFunction<double> real, Range<double> range) {
        if (!Directory.Exists(".data"))
            Directory.CreateDirectory(".data");

        using (var writer = new StreamWriter(Path.Combine(".data", $"{name}.xy.csv"))) {
            writer.WriteLine("x, y, ~y");
            foreach (var x in range.All) {
                writer.WriteLine($"{x},{real.Evaluate(x)},{approx.Evaluate(x)}");
            }
        }
    }
}

}