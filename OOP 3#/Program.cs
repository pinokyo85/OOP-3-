using System;

public class Calculator
{

    static void Main(string[] args)
    {

        Calculator calc = new Calculator();
        Console.WriteLine($"2 + 3 = {calc.Add(2, 3)}");
        Console.WriteLine($"2 + 3 + 4 = {calc.Add(2, 3, 4)}");
        Console.WriteLine($"2.5 + 3.7 = {calc.Add(2.5, 3.7)}");


        Rectangle rect1 = new Rectangle();
        Rectangle rect2 = new Rectangle(5, 10);
        Rectangle rect3 = new Rectangle(7);

        ComplexNumber c1 = new ComplexNumber(1.5, 2.5);
        ComplexNumber c2 = new ComplexNumber(2.5, 3.5);
        ComplexNumber sum = c1 + c2;
        ComplexNumber diff = c1 - c2;


        Employee emp = new Employee();
        Manager mgr = new Manager();
        emp.Work();
        mgr.Work();


        BaseClass b = new BaseClass();
        DerivedClass1 d1 = new DerivedClass1();
        DerivedClass2 d2 = new DerivedClass2();
        b.DisplayMessage();
        d1.DisplayMessage();
        d2.DisplayMessage();


        Duration duration1 = new Duration(1, 10, 15);
        Duration duration2 = new Duration(3600);
        Duration duration3 = new Duration(7800);
        Duration duration4 = new Duration(666);

        Console.WriteLine(duration1);
        Console.WriteLine(duration2);
        Console.WriteLine(duration3);
        Console.WriteLine(duration4);

        Duration sumDuration = duration1 + duration2;
        Duration sumWithInt = duration1 + 7800;
        Duration preIncrement = ++duration1;
        Duration postDecrement = --duration2;

        if (duration1 > duration2)
        {
            Console.WriteLine("Duration1 is greater than Duration2");
        }

        DateTime dt = (DateTime)duration1;
    }


    public int Add(int a, int b)
    {
        return a + b;
    }

    public int Add(int a, int b, int c)
    {
        return a + b + c;
    }

    public double Add(double a, double b)
    {
        return a + b;
    }
}

public class Rectangle
{
    private int width;
    private int height;

    public Rectangle()
    {
        width = 0;
        height = 0;
    }

    public Rectangle(int width, int height)
    {
        this.width = width;
        this.height = height;
    }

    public Rectangle(int size)
    {
        this.width = size;
        this.height = size;
    }
}

public class ComplexNumber
{
    private double real;
    private double imaginary;

    public ComplexNumber(double real, double imaginary)
    {
        this.real = real;
        this.imaginary = imaginary;
    }

    public static ComplexNumber operator +(ComplexNumber a, ComplexNumber b)
    {
        return new ComplexNumber(a.real + b.real, a.imaginary + b.imaginary);
    }

    public static ComplexNumber operator -(ComplexNumber a, ComplexNumber b)
    {
        return new ComplexNumber(a.real - b.real, a.imaginary - b.imaginary);
    }
}

public class Employee
{
    public virtual void Work()
    {
        Console.WriteLine("Employee is working");
    }
}

public class Manager : Employee
{
    public override void Work()
    {
        base.Work();
        Console.WriteLine("Manager is managing");
    }
}

public class BaseClass
{
    public virtual void DisplayMessage()
    {
        Console.WriteLine("Message from BaseClass");
    }
}

public class DerivedClass1 : BaseClass
{
    public override void DisplayMessage()
    {
        Console.WriteLine("Message from DerivedClass1");
    }
}

public class DerivedClass2 : BaseClass
{
    public new void DisplayMessage()
    {
        Console.WriteLine("Message from DerivedClass2");
    }
}

public class Duration
{
    private int hours;
    private int minutes;
    private int seconds;

    public Duration(int hours, int minutes, int seconds)
    {
        this.hours = hours;
        this.minutes = minutes;
        this.seconds = seconds;
        Normalize();
    }

    public Duration(int totalSeconds)
    {
        hours = totalSeconds / 3600;
        totalSeconds %= 3600;
        minutes = totalSeconds / 60;
        seconds = totalSeconds % 60;
    }

    private void Normalize()
    {
        if (seconds >= 60)
        {
            minutes += seconds / 60;
            seconds %= 60;
        }
        if (minutes >= 60)
        {
            hours += minutes / 60;
            minutes %= 60;
        }
    }

    public override string ToString()
    {
        if (hours > 0)
            return $"Hours: {hours}, Minutes: {minutes}, Seconds: {seconds}";
        return $"Minutes: {minutes}, Seconds: {seconds}";
    }

    public override bool Equals(object obj)
    {
        if (obj is Duration other)
        {
            return ToSeconds() == other.ToSeconds();
        }
        return false;
    }

    public override int GetHashCode()
    {
        return ToSeconds().GetHashCode();
    }

    private int ToSeconds()
    {
        return hours * 3600 + minutes * 60 + seconds;
    }

    public static Duration operator +(Duration a, Duration b)
    {
        return new Duration(a.ToSeconds() + b.ToSeconds());
    }

    public static Duration operator +(Duration a, int seconds)
    {
        return new Duration(a.ToSeconds() + seconds);
    }

    public static Duration operator +(int seconds, Duration a)
    {
        return a + seconds;
    }

    public static Duration operator ++(Duration d)
    {
        return new Duration(d.ToSeconds() + 60);
    }

    public static Duration operator --(Duration d)
    {
        return new Duration(d.ToSeconds() - 60);
    }

    public static Duration operator -(Duration a, Duration b)
    {
        int totalSeconds = a.ToSeconds() - b.ToSeconds();
        return totalSeconds >= 0 ? new Duration(totalSeconds) : new Duration(0); // Avoid negative durations
    }

    public static bool operator >(Duration a, Duration b)
    {
        return a.ToSeconds() > b.ToSeconds();
    }

    public static bool operator <(Duration a, Duration b)
    {
        return a.ToSeconds() < b.ToSeconds();
    }

    public static bool operator >=(Duration a, Duration b)
    {
        return a.ToSeconds() >= b.ToSeconds();
    }

    public static bool operator <=(Duration a, Duration b)
    {
        return a.ToSeconds() <= b.ToSeconds();
    }

    public static bool operator ==(Duration a, Duration b)
    {
        if (a is null && b is null) return true;
        if (a is null || b is null) return false;
        return a.ToSeconds() == b.ToSeconds();
    }

    public static bool operator !=(Duration a, Duration b)
    {
        return !(a == b);
    }

    public static bool operator true(Duration d)
    {
        return d.ToSeconds() > 0;
    }

    public static bool operator false(Duration d)
    {
        return d.ToSeconds() <= 0;
    }

    public static explicit operator DateTime(Duration d)
    {
        return new DateTime().AddSeconds(d.ToSeconds());
    }
}
