using Qkmaxware.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;

namespace Testing.Numerics {

[TestClass]
public class BvpSolverTest {
    [TestMethod]
    public void Solve() {
        // Example taken from https://tutorial.math.lamar.edu/classes/de/BoundaryValueProblem.aspx
        // y'' + 4y = 0, 0 <= t <= pi/4, y(0) = -2, y(pi/4) = 10
        DoubleRange t = new DoubleRange(0, Math.PI/4, increment: 0.1);
        double y0 = -2;
        double ypi_4 = 10;

        //DoubleFunction3 yPP = new DoubleFunction3((t,y,yP) => (-4 * y));
        // y`` = 0*y` -4*y + 0
        var de = new SecondOrderDE<double>(new DoubleFunction((x) => 0), new DoubleFunction((x) => -4), new DoubleFunction((x) => 0));

        var solver = new LinearShootingBvpSolver();
        var (approxy, _) = solver.SolveBoundary(de, t, y0, ypi_4); //discard approximation to y`
        var exact = new DoubleFunction((t) => -2*Math.Cos(2 * t) + 10 * Math.Sin(2 * t));

        SaveCsv("LinearShootingBVP", approxy, exact, t);
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