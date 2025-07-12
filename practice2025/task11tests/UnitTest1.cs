using task11;
using Xunit;

namespace task11tests;

public class UnitTest1
{
    [Fact]
    public void Add_ReturnsCorrectSum()
    {
        var calc = CalculatorClass.CreateCalculator();
        Assert.Equal(5, calc.Add(3, 2));
    }

    [Fact]
    public void Minus_ReturnsCorrectDifference()
    {
        var calc = CalculatorClass.CreateCalculator();
        Assert.Equal(1, calc.Minus(3, 2));
    }

    [Fact]
    public void Mul_ReturnsCorrectProduct()
    {
        var calc = CalculatorClass.CreateCalculator();
        Assert.Equal(6, calc.Mul(2, 3));
    }

    [Fact]
    public void Div_ReturnsCorrectQuotient()
    {
        var calc = CalculatorClass.CreateCalculator();
        Assert.Equal(2, calc.Div(6, 3));
    }
}
