using System;

public class Dictionary
{
    public static Dictionary<string, double> Conversions()
    {
        Dictionary<string, double> Length = new Dictionary<string, double>();

        Length.Add("Meters", 0.3048);
        Length.Add("Feet", 1);

        return Length;
    }
}