using System;

public class Functions
{
    //Angle Averaging
    //Minimum Curvature
    //Tangential
    //Radius of Curvature

	public string Tangential(double I, double A, double Increment)
	{
        double X, Y, Z;

        X = Increment * Math.Sin(I) * Math.Cos(A);
        Y = Increment * Math.Sin(I) * Math.Sin(A);
        Z = Increment * Math.Cos(I);

        return "{X} {Y} {Z}"; // learn how to return shit
	}

    public string AngleAveraging(double I, double A, double Increment)
	{
        double X, Y, Z;

        X = Increment * Math.Sin(I) * Math.Cos(A);
        Y = Increment * Math.Sin(I) * Math.Sin(A);
        Z = Increment * Math.Cos(I);

        return "{X} {Y} {Z}"; // learn how to return shit
	}

    public string MinimumCurvature(double I, double A, double Increment)
	{
        double X, Y, Z;

        X = Increment * Math.Sin(I) * Math.Cos(A);
        Y = Increment * Math.Sin(I) * Math.Sin(A);
        Z = Increment * Math.Cos(I);

        return "{X} {Y} {Z}"; // learn how to return shit
	}

    public string RadiusOfCurvature(double I, double A, double Increment)
	{
        double X, Y, Z;

        X = Increment * Math.Sin(I) * Math.Cos(A);
        Y = Increment * Math.Sin(I) * Math.Sin(A);
        Z = Increment * Math.Cos(I);

        return "{X} {Y} {Z}"; // learn how to return shit
	}
}
