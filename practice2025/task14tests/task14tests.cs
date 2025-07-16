using System;
using Xunit;
using task14;

public class DefiniteIntegralTests
{
    [Fact]
    public void Test_Integral_Of_X_From_Minus1_To_1()
    {
        Func<double, double> X = x => x;
        double result = DefiniteIntegral.Solve(-1, 1, X, 1e-4, 2);
        Assert.Equal(0, result, 4);
    }

    [Fact]
    public void Test_Integral_Of_Sin_From_Minus1_To_1()
    {
        Func<double, double> SIN = x => Math.Sin(x);
        double result = DefiniteIntegral.Solve(-1, 1, SIN, 1e-5, 8);
        Assert.Equal(0, result, 4);
    }

    [Fact]
    public void Test_Integral_Of_X_From_0_To_5()
    {
        Func<double, double> X = x => x;
        double result = DefiniteIntegral.Solve(0, 5, X, 1e-6, 8);
        Assert.Equal(12.5, result, 5);
    }
}
