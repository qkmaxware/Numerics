using Qkmaxware.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;

namespace Testing.Numerics {

public class DerivativeFunctionTest {
    protected static void SaveCsv(string name, IFunction<double> approx, Func<double, double> real) {
        if (!Directory.Exists(".data"))
            Directory.CreateDirectory(".data");

        using (var writer = new StreamWriter(Path.Combine(".data", $"{name}.xy.csv"))) {
            writer.WriteLine("x, y, y`");
            for (var x = -10d; x <= 10d; x += 0.1d) {
                writer.WriteLine($"{x},{real(x)},{approx.Evaluate(x)}");
            }
        }
    }
}

}