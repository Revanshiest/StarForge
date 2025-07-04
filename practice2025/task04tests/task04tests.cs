using Xunit;
using Moq;

public class SpaceshipTests
{
    [Fact]
    public void Cruiser_ShouldHaveCorrectStats()
    {
        ISpaceship cruiser = new Cruiser();
        Assert.Equal(50, cruiser.Speed);
        Assert.Equal(100, cruiser.FirePower);
    }

    [Fact]
    public void Fighter_ShouldBeFasterThanCruiser()
    {
        var fighter = new Fighter();
        var cruiser = new Cruiser();
        Assert.True(fighter.Speed > cruiser.Speed);
    }

    [Fact]
    public void Fighter_ShouldHaveCorrectStats()
    {
        ISpaceship fighter = new Fighter();
        Assert.Equal(100, fighter.Speed);
        Assert.Equal(50, fighter.FirePower)
    }

    [Fact]
    public void Fighter_FirePower_ShouldBeLessThanCruiser()
    {
        var fighter = new Fighter();
        var cruiser = new Cruiser();
        Assert.True(fighter.Speed > cruiser.Speed);
    }

    [Fact]
    public void Cruiser_Move_ShouldChangePosition()
    {
        var cruiser = new Cruiser();
        var startX = cruiser.X;
        var startY = cruiser.Y;

        cruiser.MoveForward();

        Assert.True(cruiser.X != startX || cruiser.Y != startY);
    }

    [Fact]
    public void Cruiser_Rotate_ShouldChangeAngle()
    {
        var cruiser = new Cruiser();
        var startAngle = cruiser.Angle;
        cruiser.Rotate(45);
        Assert.NotEqual(startAngle, cruiser.Angle);
    }

    [Theory]
    [InlineData(370, 10)]
    [InlineData(-30, 330)]
    [InlineData(720, 0)]
    [InlineData(450, 90)]
    [InlineData(0,0)]
    public void Cruiser_Rotate_AngleAlwaysInRange0To359(int rotateBy, int expectedAngle)
    {
        var cruiser = new Cruiser();
        cruiser.Rotate(rotateBy);
        Assert.Equal(expectedAngle, cruiser.Angle);
    }  

    [Fact]
    public void Cruiser_Fire_IncreasesDamageByFirePower()
    {
        var cruiser = new Cruiser();
        var initialDamage = cruiser.Damage;
        cruiser.Fire();
        Assert.Equal(initialDamage + cruiser.FirePower, cruiser.Damage);
    }

    [Fact]
    public void Fighter_Move_ShouldChangePosition()
    {
        var fighter = new Fighter();
        var startX = fighter.X;
        var startY = fighter.Y;
    
        fighter.MoveForward();

        Assert.True(fighter.X != startX || fighter.Y != startY);
    }

    [Fact]
    public void Fighter_Rotate_ShouldChangeAngle()
    {
        var fighter = new Fighter();
        var startAngle = fighter.Angle;
        fighter.Rotate(45);
        Assert.NotEqual(startAngle, fighter.Angle);
    }

    [Theory]
    [InlineData(370, 10)]
    [InlineData(-30, 330)]
    [InlineData(720, 0)]
    [InlineData(450, 90)]
    public void Fighter_Rotate_AngleAlwaysInRange0To359(int rotateBy, int expectedAngle)
    {
        var fighter = new Fighter();
        fighter.Rotate(rotateBy);
        Assert.Equal(expectedAngle, fighter.Angle);
    }  

    [Fact]
    public void Fighter_Fire_IncreasesDamageByFirePower()
    {
        var fighter = new Fighter();
        var initialDamage = fighter.Damage;
        cruiser.Fire();
        Assert.Equal(initialDamage + fighter.FirePower, fighter.Damage);
    }
}
