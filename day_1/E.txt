class Program
{
    static void Main()
    {
        double r = double.Parse(Console.ReadLine());
        const double pi = 3.141592653;
        double area = pi * r * r;
        Console.WriteLine($"{area:F9}");
    }
}