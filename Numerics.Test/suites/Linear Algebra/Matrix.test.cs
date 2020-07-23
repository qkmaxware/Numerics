using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Qkmaxware.Numerics;

namespace Testing.Numerics {
    [TestClass]
    public class Matrix {
        [TestMethod]
        public void IsSquare() {
            IntMatrix xs = new int[1,1];
            IntMatrix ys = new int[1,2];
            IntMatrix zs = new int[2,1];

            Assert.AreEqual(true, xs.IsSquare);
            Assert.AreEqual(false, ys.IsSquare);
            Assert.AreEqual(false, zs.IsSquare);
        }

        [TestMethod]
        public void IsRowMatrix() {
            IntMatrix xs = new int[1,1];
            IntMatrix ys = new int[1,2];
            IntMatrix zs = new int[2,1];

            Assert.AreEqual(true, xs.IsRowMatrix);
            Assert.AreEqual(true, ys.IsRowMatrix);
            Assert.AreEqual(false, zs.IsRowMatrix);
        }
        
        [TestMethod]
        public void IsColumnMatrix() {
            IntMatrix xs = new int[1,1];
            IntMatrix ys = new int[1,2];
            IntMatrix zs = new int[2,1];

            Assert.AreEqual(true, xs.IsColumnMatrix);
            Assert.AreEqual(false, ys.IsColumnMatrix);
            Assert.AreEqual(true, zs.IsColumnMatrix);
        }

        [TestMethod]
        public void Map() {
            IntMatrix xs = new int[2,2]{
                {2, 2},
                {2, 2}
            };

            Matrix<double> zs = xs.Map<double>(DoubleCalculator.Instance, (x) => (double)x);
        }

        [TestMethod]
        public void Transpose() {
            IntMatrix xs = new int[2,1] {
                {2},
                {3}
            };
            var ys = xs.Transpose();

            Assert.AreEqual(1, ys.Rows);
            Assert.AreEqual(2, ys.Columns);
            Assert.AreEqual(2, ys[0,0]);
            Assert.AreEqual(3, ys[0,1]);
        }

        [TestMethod]
        public void Trace() {
            IntMatrix xs = new int[2,2]{
                {2, 2},
                {2, 2}
            };
            Assert.AreEqual(4, xs.Trace());
        }

        [TestMethod]
        public void ScalarMultiplication() {
            IntMatrix xs = new int[2,2]{
                {2, 2},
                {2, 2}
            };
            var ys = xs * 4;

            Assert.AreEqual(8, ys[0,0]);
            Assert.AreEqual(8, ys[0,1]);
            Assert.AreEqual(8, ys[1,0]);
            Assert.AreEqual(8, ys[1,1]);
        }

        [TestMethod]
        public void MatrixAddition() {
            IntMatrix xs = new int[2,2]{
                {2, 2},
                {2, 2}
            };
            IntMatrix ys = new int[2,2]{
                {1, 2},
                {3, 4}
            };
            var zs = xs + ys;

            Assert.AreEqual(3, zs[0,0]);
            Assert.AreEqual(4, zs[0,1]);
            Assert.AreEqual(5, zs[1,0]);
            Assert.AreEqual(6, zs[1,1]);
        }

        [TestMethod]
        public void MatrixSubtraction() {
            IntMatrix xs = new int[2,2]{
                {2, 2},
                {2, 2}
            };
            IntMatrix ys = new int[2,2]{
                {1, 2},
                {3, 4}
            };
            var zs = xs - ys;

            Assert.AreEqual(1, zs[0,0]);
            Assert.AreEqual(0, zs[0,1]);
            Assert.AreEqual(-1, zs[1,0]);
            Assert.AreEqual(-2, zs[1,1]);
        }

        [TestMethod]
        public void MatrixMultiplication() {
            IntMatrix xs = new int[2,2]{
                {2, 2},
                {2, 2}
            };
            IntMatrix ys = new int[2,2]{
                {1, 2},
                {3, 4}
            };
            var zs = xs * ys;

            Assert.AreEqual(8, zs[0,0]);
            Assert.AreEqual(12, zs[0,1]);
            Assert.AreEqual(8, zs[1,0]);
            Assert.AreEqual(12, zs[1,1]);
        }

        [TestMethod]
        public void ElementWise() {
            DoubleMatrix xs = new double[2,2] {
                {1, 2},
                {3, 4}
            };
            DoubleMatrix ys = new double[2,2] {
                {9, 7},
                {5, 3}
            };
            var zs = Matrix<double>.Operate(xs, ys, (x, y) => x + y);

            Assert.AreEqual(10, zs[0,0]);
            Assert.AreEqual(9, zs[0,1]);
            Assert.AreEqual(8, zs[1,0]);
            Assert.AreEqual(7, zs[1,1]);
        }

        [TestMethod]
        public void LUPDecompose() {
            // Test derived from
            // https://rosettacode.org/wiki/LU_decomposition#2D_representation
            DoubleMatrix m = new double[3,3]{
                {1,3,5},
                {2,4,7},
                {1,1,0}
            };

            DoubleMatrix l = new double[3,3]{
                {1, 0, 0},
                {0.5,1,0},
                {0.5,-1,1}
            };

            DoubleMatrix u = new double[3,3]{
                {2,4,7},
                {0,1,1.5},
                {0,0,-2}
            };

            DoubleMatrix p = new double[3,3]{
                {0,1,0},
                {1,0,0},
                {0,0,1}
            };

            LUPSet<double> decomposition = m.Decompose();

            // Using strings so I can see the actual values if it does not match
            Assert.AreEqual(p.ToMatlabString("{0:0.00}"), new DoubleMatrix(decomposition.P).ToMatlabString("{0:0.00}"));
            Assert.AreEqual(l.ToMatlabString("{0:0.00}"), new DoubleMatrix(decomposition.L).ToMatlabString("{0:0.00}"));
            Assert.AreEqual(u.ToMatlabString("{0:0.00}"), new DoubleMatrix(decomposition.U).ToMatlabString("{0:0.00}"));
        }

        [TestMethod]
        public void Determinant() {
            // https://rosettacode.org/wiki/Determinant_and_permanent#Go
            DoubleMatrix m = new double[2,2]{
                {1,2},
                {3,4}
            };
            DoubleMatrix m2 = new double[3,3]{
                {2,9,4},
                {7,5,3},
                {6,1,8}
            };

            Assert.AreEqual(-2, m.Determinant());
            Assert.AreEqual(-360, m2.Determinant());
        }
    }
}