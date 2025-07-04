namespace task04;

public interface ISpaceship
{
    void MoveForward();      // Движение вперед
    void Rotate(int angle);  // Поворот на угол (градусы)
    void Fire();             // Выстрел ракетой
    int Speed { get; }       // Скорость корабля
    int FirePower { get; }   // Мощность выстрела
}

public class Cruiser : ISpaceship
{
    public int Speed { get; } = 50;
    public int FirePower { get; } = 100;
    public int Damage { get; private set; } = 0;
    public int X { get; private set; } = 0;
    public int Y { get; private set; } = 0;
    public int Angle { get; private set; } = 0;

    public void MoveForward()
    {
        double radians = Angle * Math.PI / 180.0;
        X += (int)(Speed * Math.Cos(radians));
        Y += (int)(Speed * Math.Sin(radians));
    }

    public void Rotate(int angle)
    {
        Angle = (Angle + angle) % 360;
        if (Angle < 0) Angle += 360;
    }

    public void Fire()
    {
        Damage += FirePower;
    }
}

public class Fighter : ISpaceship
{
    public int Speed { get; } = 100;
    public int FirePower { get; } = 50;
    public int Damage { get; private set; } = 0;
    public int X { get; private set; } = 0;
    public int Y { get; private set; } = 0;
    public int Angle { get; private set; } = 0;

    public void MoveForward()
    {
        double radians = Angle * Math.PI / 180.0;
        X += (int)(Speed * Math.Cos(radians));
        Y += (int)(Speed * Math.Sin(radians));
    }

    public void Rotate(int angle)
    {
        Angle = (Angle + angle) % 360;
        if (Angle < 0) Angle += 360;
    }

    public void Fire()
    {
        Damage += FirePower;
    }
}
