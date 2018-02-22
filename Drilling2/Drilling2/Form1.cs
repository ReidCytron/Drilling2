// Notes
// Atan needs to be converted afterwards
// Sin needs to be converted before
// If I = I_Last, Radius of curvature will divide by zero; change to AA or T on vertical and hold sections

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Drilling2
{
    /// <summary>
    /// This section is the instructions for the form
    /// </summary>

    public partial class WellboreTrajectoryForm : Form
    {
        public WellboreTrajectoryForm()
        {
            InitializeComponent();
        }

        private void W_Method_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Z_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Z_Target_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox10_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void X_Surface_TextChanged(object sender, EventArgs e)
        {

        }

        private void Done_Button_Click(object sender, EventArgs e)
        {
            // The units call a dictionary that will convert the inputs to feet
            string Method = Convert.ToString(Dictionary.Method()[W_Method.Text]);
            string Path = Save_File_Dialogue.Text;
            double SurfaceUnit = Dictionary.Length()[Surface_Unit.Text];
            double TargetUnit = Dictionary.Length()[Target_Unit.Text];
            double KOPUnit = Dictionary.Length()[KOP_Length_Unit.Text];
            double BuildRateUnit = Dictionary.Rate()[Build_Rate_Unit.Text];
            double DropRateUnit = Dictionary.Rate()[Drop_Rate_Unit.Text];
            double TurnRateUnit = Dictionary.Rate()[Turn_Rate_Unit.Text];
            double IncrementUnit = Dictionary.Length()[Iteration_Increment_Unit.Text];
            double AngleOutUnit = Dictionary.Angle()[Angle_Output_Unit.Text];
            double LengthOutUnit = Dictionary.LengthOut()[Length_Output_Unit.Text];
            double ToleranceUnit = Dictionary.Length()[Tolerance_Unit.Text];
            // These are the values that are input by the user
            double SX = Convert.ToDouble(X_Surface.Text) * SurfaceUnit;
            double SY = Convert.ToDouble(Y_Surface.Text) * SurfaceUnit;
            double SZ = Convert.ToDouble(Z_Surface.Text) * SurfaceUnit;
            double TX = Convert.ToDouble(X_Target.Text) * TargetUnit;
            double TY = Convert.ToDouble(Y_Target.Text) * TargetUnit;
            double TZ = Convert.ToDouble(Z_Target.Text) * TargetUnit;
            double KOP = Convert.ToDouble(KOP_Length.Text) * KOPUnit;
            double BuildRate = Convert.ToDouble(Build_Rate.Text) * BuildRateUnit;
            double DropRate = Convert.ToDouble(Drop_Rate.Text) * DropRateUnit;
            double TurnRate = Convert.ToDouble(Turn_Rate.Text) * TurnRateUnit;
            double Tol = Convert.ToDouble(Tolerance.Text) * ToleranceUnit * LengthOutUnit;
            double Increment = Convert.ToDouble(Iteration_Increment.Text) * IncrementUnit;
            // These are other parameters that the program needs
            double BuildRadius = (180.0 / 3.14159265359) * (1 / BuildRate);
            double DropRadius = (180.0 / 3.14159265359) * (1 / DropRate);
            double TurnRadius = (180.0 / 3.14159265359) * (1.0 / TurnRate);
            double HD = Math.Sqrt(((TY - SY) * (TY - SY)) + ((TX - SX) * (TX - SX)));
            double TVD = (TZ - SZ);
            double TurnAngle = 2.0 * RadianToDegree(Math.Asin((HD * 0.5) / TurnRadius));
            double Length = TurnAngle * (3.14159265359 * TurnRadius / 180.0);
            double ThetaMax = MaxIncAngle(HD, KOP, BuildRadius, DropRadius, TVD);  // Length vs HD
            double Ai = InitialAzimuth(TX, TY, SX, SY, HD);
            double Lead = Ai + (Length * TurnRate) * 0.1;  // The initial Azimuth is Lead plus the Azimuth Function; check accuracy
            double IB_Inc = BuildRate * Increment;
            double ID_Inc = DropRate * Increment;
            double A_Inc = TurnRate * Increment;
            double A = 0.0;
            string Af;
            double I = 0.0;
            double A_Last = 0.0;
            double I_Last = 0.0;
            double X = 0.0;
            double Y = 0.0;
            double Z = 0.0;
            string Hit = " ";
            string[] Results = new string[3];
            string Result = " ";
            string[,] ResultMatrix = new string[1000000, 5];
            double Hold_end = Length - (DropRadius - DropRadius * RadianToDegree(Math.Cos(ThetaMax)));
            int j = 0;
            int i = 0;
            double check = 0;
            // Result Matrix Columns
            ResultMatrix[0, 0] = "Iteration";
            ResultMatrix[0, 1] = "X";
            ResultMatrix[0, 2] = "Y";
            ResultMatrix[0, 3] = "Z";
            ResultMatrix[0, 4] = "MD";
            // Result Matrix Row 2
            ResultMatrix[1, 0] = "1";
            ResultMatrix[1, 1] = "0";
            ResultMatrix[1, 2] = "0";
            ResultMatrix[1, 3] = "0";
            ResultMatrix[1, 4] = "0";
            
            // //
            // // General Error Handling
            // //
            if (KOP >= (TX - SX))
            {
                MessageBox.Show("The KOP is too deep");
            }
            if (Increment < 1.0)
            {
                MessageBox.Show("The increment size is too small");
            }

            // //
            // // Beginning of the Loop
            // //

                // // vertical section // //
            Z = SZ * LengthOutUnit;
            X = SX * LengthOutUnit;
            Y = SY * LengthOutUnit;
            i = 2;
            do{
                ResultMatrix[i, 0] = Convert.ToString(i);
                ResultMatrix[i, 1] = Convert.ToString(SX * LengthOutUnit);
                ResultMatrix[i, 2] = Convert.ToString(SY * LengthOutUnit);
                ResultMatrix[i, 3] = Convert.ToString(Z * LengthOutUnit);
                ResultMatrix[i, 4] = Convert.ToString(Z * LengthOutUnit);
                i = i + 1;
                Z = Z + Increment;
                //Console.WriteLine("{0} {1} {2} {3}\t" + Length_Output_Unit.Text, i, X, Y, Z);
            } while ( Z < SZ + KOP);

            
            
                // // Build Section // //
            I_Last = 0.0;
            A_Last = 0.0;
            A = Lead;
            I = IB_Inc;
            do{
                switch (Method)
                {
                    case "1":
                        Result = AngleAveraging(I, A, I_Last, A_Last, Increment);
                        break;
                    case "2":
                        Result = MinimumCurvature(I, A, I_Last, A_Last, Increment);
                        break;
                    case "3":
                        Result = Tangential(I, A, I_Last, A_Last, Increment);
                        break;
                    case "4":
                        Result = RadiusOfCurvature(I, A, I_Last, A_Last, Increment);
                        break;
                }
                Result = Tangential(I, A, I_Last, A_Last, Increment);
                Results = Result.Split(' ');
                X = X + Convert.ToDouble(Results[0]);
                Y = Y + Convert.ToDouble(Results[1]);
                Z = Z + Convert.ToDouble(Results[2]);
                ResultMatrix[i, 0] = Convert.ToString(i);
                ResultMatrix[i, 1] = Convert.ToString(X * LengthOutUnit);
                ResultMatrix[i, 2] = Convert.ToString(Y * LengthOutUnit);
                ResultMatrix[i, 3] = Convert.ToString(Z * LengthOutUnit);
                ResultMatrix[i, 4] = Convert.ToString(Z * LengthOutUnit);
                //Console.WriteLine("{0} {1} {2} {3}\t" + Length_Output_Unit.Text, i, X * LengthOutUnit, Y * LengthOutUnit, Z * LengthOutUnit); // debugging
                I_Last = I;
                A_Last = A;
                I = I + IB_Inc;
                A = A - A_Inc * 0.05;
                i = i + 1;
                } while( I <= ThetaMax);
                
                
                    // // Hold Section // //
                I_Last = I;
                do{
                    switch (Method) // Will use T instead of MC and RC to prevent divide by zero
                    {
                        case "1":
                            Result = AngleAveraging(I, A, I_Last, A_Last, Increment);
                            break;
                        case "2":
                            Result = Tangential(I, A, I_Last, A_Last, Increment);
                            break;
                        case "3":
                            Result = Tangential(I, A, I_Last, A_Last, Increment);
                            break;
                        case "4":
                            Result = Tangential(I, A, I_Last, A_Last, Increment);
                            break;
                    }
                    Result = Tangential(I, A, I_Last, A_Last, Increment);
                    Results = Result.Split(' ');
                    X = X + Convert.ToDouble(Results[0]);
                    Y = Y + Convert.ToDouble(Results[1]);
                    Z = Z + Convert.ToDouble(Results[2]);
                    ResultMatrix[i, 0] = Convert.ToString(i);
                    ResultMatrix[i, 1] = Convert.ToString(X * LengthOutUnit);
                    ResultMatrix[i, 2] = Convert.ToString(Y * LengthOutUnit);
                    ResultMatrix[i, 3] = Convert.ToString(Z * LengthOutUnit);
                    ResultMatrix[i, 4] = Convert.ToString(Z * LengthOutUnit);
                    //Console.WriteLine("{0} {1} {2} {3}\t" + Length_Output_Unit.Text, i, X * LengthOutUnit, Y * LengthOutUnit, Z * LengthOutUnit); // debugging
                    A_Last = A;
                    A = A - A_Inc * 0.05;
                    i = i + 1;
                } while (Z < SZ + ((TZ - SZ) - DropRadius * Math.Sin(DegreeToRadian(ThetaMax)))); // Simplify
                
                
                    // // Drop Section // //
                do
                {
                    switch (Method)
                    {
                        case "1":
                            Result = AngleAveraging(I, A, I_Last, A_Last, Increment);
                            break;
                        case "2":
                            Result = MinimumCurvature(I, A, I_Last, A_Last, Increment);
                            break;
                        case "3":
                            Result = Tangential(I, A, I_Last, A_Last, Increment);
                            break;
                        case "4":
                            Result = RadiusOfCurvature(I, A, I_Last, A_Last, Increment);
                            break;
                    }
                    Result = Tangential(I, A, I_Last, A_Last, Increment);
                    Results = Result.Split(' ');
                    X = X + Convert.ToDouble(Results[0]);
                    Y = Y + Convert.ToDouble(Results[1]);
                    Z = Z + Convert.ToDouble(Results[2]);
                    ResultMatrix[i, 0] = Convert.ToString(i);
                    ResultMatrix[i, 1] = Convert.ToString(X * LengthOutUnit);
                    ResultMatrix[i, 2] = Convert.ToString(Y * LengthOutUnit);
                    ResultMatrix[i, 3] = Convert.ToString(Z * LengthOutUnit);
                    ResultMatrix[i, 4] = Convert.ToString(Z * LengthOutUnit);
                    //Console.WriteLine("{0} {1} {2} {3}\t" + Length_Output_Unit.Text, I, X * LengthOutUnit, Y * LengthOutUnit, Z * LengthOutUnit); // debugging
                    I_Last = I;
                    A_Last = A;
                    I = I - ID_Inc;
                    A = A - A_Inc * 0.05;
                    i = i + 1;
                    check = Math.Sqrt((X - TX) * (X - TX) + (Y - TY) * (Y - TY) + (Z - TZ) * (Z - TZ)) * LengthOutUnit;
                    if(check <= Tol)
                    {
                        Hit = "Success";
                        continue;
                    }
                    else
                    {
                        Hit = "Failure";
                    }
                } while (I >= 0.0);
            

            // //
            // // Angle Output Units (Compass, Radians, Degrees)
            // //


            if (Angle_Output_Unit.Text == "Compass")
            {
                Af = AzimuthToCompass(A);
                Convert.ToString(A);
            }
            else if(Angle_Output_Unit.Text == "Radians")
            {
                Af = Convert.ToString(DegreeToRadian(A));
            }
            else
            {
                Af = Convert.ToString(A);
            }

            //Console.WriteLine("Tol = {0}  "+Length_Output_Unit.Text+"\nDistance From Target = {1} "+Length_Output_Unit.Text+"\n"+Hit+"\nA = {2}\nThetaMax = {3}\n", Tol, check, Af, ThetaMax);
            //Console.ReadLine();
            


            // //
            // // for debugging purposes
            // //



            // //
            // // Text file print
            // //

            
            int h = 0;
            //string path = @"D:\Drilling II\Trajectory.txt";
            if (!File.Exists(Path))
            {
                File.Create(Path);
                TextWriter txt = new StreamWriter(Path);
                do
                {
                    txt.WriteLine(ResultMatrix[h, 0] + " " + ResultMatrix[h, 1] + " " + ResultMatrix[h, 2] + " " + ResultMatrix[h, 3] + " " + ResultMatrix[h, 4] + "\n");
                    h++;
                } while (h <= i);
                txt.Close();
            }
            else if (File.Exists(Path))
            {
                using (var txt = new StreamWriter(Path, true))
                {
                    do {
                        txt.WriteLine(ResultMatrix[h, 0] + " " + ResultMatrix[h, 1] + " " + ResultMatrix[h, 2] + " " + ResultMatrix[h, 3] + " " + ResultMatrix[h, 4] + "\n");
                        h++;
                    } while (h <= i);
                txt.Close();
                }
            }

            MessageBox.Show("A = "+A+"\n"+"MD = "+Convert.ToString(Z)+"\n"+"Tol = "+Convert.ToString(Tol)+"\n"+"Distance From Target = "+Convert.ToString(check)+"\n");



        }
        private void WellboreTrajectoryForm_Click(object sender, EventArgs e)
        {

        }
        private void label15_Click(object sender, EventArgs e)
        {

        }
        private void Build_Rate_TextChanged(object sender, EventArgs e)
        {
            
        }
        /// <summary>
        /// The below functions are for random uses
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        /// 
        private static double DegreeToRadian(double angle)
        {
            return angle * ( 3.14159265359 / 180.0);
        }
        private static double RadianToDegree(double radians)
        {
            return radians * (180.0 / 3.14159265359);
        }
        public static double MaxIncAngle(double HD, double KOP, double BuildRadius, double DropRadius, double TVD) // Problem Here
        {
            double A, B, C, D, theta;
            double VD = TVD - KOP;
            
            if ((BuildRadius + DropRadius) > HD)
            {
                A = RadianToDegree(Math.Atan(((VD) / (BuildRadius + DropRadius - HD))));
                B = Math.Sin(DegreeToRadian(A));
                C = ((BuildRadius + DropRadius) / VD) * B;
                D = RadianToDegree(Math.Acos((C)));
                theta = A - D;
            }
            else
            {
                A = RadianToDegree(Math.Atan(VD / (HD - (BuildRadius + DropRadius))));
                B = ((BuildRadius + DropRadius) / VD);
                C = Math.Sin(RadianToDegree(A));
                D = RadianToDegree(Math.Acos(C * B));
                theta = 180.0 - A - D;
            }
            return theta;
        }
        public static double InitialAzimuth(double TX, double TY, double SX, double SY, double HD)
        {
            double Y = TY - SY;
            double X = TX - SY;
            double Azimuth;
            if ( Y > 0.0 && X > 0.0 )
            {
                Azimuth = RadianToDegree(Math.Asin(Y / HD));
            }
            else if ( Y > 0.0 && X < 0.0 )
            {
                Azimuth = 90.0 + RadianToDegree(Math.Asin(X / HD));
            }
            else if ( Y < 0.0 && X < 0.0 )
            {
                Azimuth = 180.0 + RadianToDegree(Math.Asin(Y / HD));
            }
            else if (Y < 0.0 && X > 0.0)
            {
                Azimuth = 270.0 + RadianToDegree(Math.Asin(X / HD));
            }
            else if (Y == 0.0 && X > 0.0)
            {
                Azimuth = 0;
            }
            else if (Y == 0.0 && X < 0.0)
            {
                Azimuth = 180.0;
            }
            else if (Y < 0.0 && X == 0.0)
            {
                Azimuth = 270.0;
            }
            else
            {
                Azimuth = 90.0;
            }
            return Azimuth;
        }
        
        private string AzimuthToCompass(double Angle)
        {
            string Compass;

            if (Angle == 0)
            {
                Compass = "N0E";
            }
            else if(Angle == 90)
            {
                Compass = "E0S";
            }
            else if(Angle == 180)
            {
                Compass = "S0W";
            }
            else if(Angle == 270)
            {
                Compass = "W0N";
            }
            else if(Angle == 360)
            {
                Compass = "N0E";
            }
            else if(Angle > 0 & Angle < 90)
            {
                Compass = "N" + Angle + "E";
            }
            else if(Angle > 90 & Angle < 180)
            {
                Compass = "E" + (Angle - 90) + "S";
            }
            else if(Angle > 180 & Angle < 270)
            {
                Compass = "S" + (Angle - 180) + "W";
            }
            else
            {
                Compass = "W" + (Angle - 270) + "N";
            }
            return Compass;
        }
        
        /// <summary>
        /// Below are the functions for the trajectory methods.  I need to learn to return strings.
        /// Ib = I2 and I = I1
        /// </summary>
        /// <returns></returns>

        private string Tangential(double I, double A, double I_Last, double A_Last, double Increment)
        {
            double X, Y, Z;

            X = Increment * Math.Sin(DegreeToRadian(I)) * Math.Cos(DegreeToRadian(A));
            Y = Increment * Math.Sin(DegreeToRadian(I)) * Math.Sin(DegreeToRadian(A));
            Z = Increment * Math.Cos(DegreeToRadian(I));

            return Convert.ToString(X+" "+Y+" "+Z);
        }
        private string AngleAveraging(double I, double A, double I_Last, double A_Last, double Increment)
        {
            double X, Y, Z;

            X = Increment * Math.Sin(DegreeToRadian(I + I_Last)) * Math.Cos(DegreeToRadian(A + A_Last));
            Y = Increment * Math.Sin(DegreeToRadian(I + I_Last)) * Math.Sin(DegreeToRadian(A + A_Last));
            Z = Increment * Math.Cos(DegreeToRadian(I + I_Last));

            return Convert.ToString(X + " " + Y + " " + Z);
        }
        private string MinimumCurvature(double I, double A, double I_Last, double A_Last, double Increment)
        {
            double X, Y, Z; // There is an F term but it is neglected for small radius of curvature

            X = (Increment / 2.0) * (Math.Sin(DegreeToRadian(I)) * Math.Sin(DegreeToRadian(A)) + Math.Sin(DegreeToRadian(I_Last)) * Math.Sin(DegreeToRadian(A_Last)));
            Y = (Increment / 2.0) * (Math.Sin(DegreeToRadian(I)) * Math.Cos(DegreeToRadian(A)) + Math.Sin(DegreeToRadian(I_Last)) * Math.Cos(DegreeToRadian(A_Last)));
            Z = (Increment / 2.0) * (Math.Cos(DegreeToRadian(I)) + Math.Cos(DegreeToRadian(I_Last)));

            return Convert.ToString(X + " " + Y + " " + Z);
        }
        private string RadiusOfCurvature(double I, double A, double I_Last, double A_Last, double Increment)
        {
            double X, Y, Z;

            X = Increment * (Math.Cos(DegreeToRadian(I)) * Math.Cos(DegreeToRadian(I_Last)) + Math.Sin(DegreeToRadian(A)) * Math.Sin(DegreeToRadian(A_Last))) / ((I - I_Last) * (A - A_Last));
            Y = Increment * (Math.Cos(DegreeToRadian(I)) * Math.Cos(DegreeToRadian(I_Last)) + Math.Cos(DegreeToRadian(A)) * Math.Cos(DegreeToRadian(A_Last))) / ((I - I_Last) * (A - A_Last));
            Z = Increment * (Math.Sin(DegreeToRadian(I)) - Math.Sin(DegreeToRadian(I_Last))) / (I - I_Last);

            return Convert.ToString(X + " " + Y + " " + Z);
        }

        private void Angle_Output_Unit_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void WellboreTrajectoryForm_Load(object sender, EventArgs e)
        {

        }
    }
    /// <summary>
    /// Below are the dictionaries for conversions; Make sure they are accurate
    /// </summary>
    /// 
    public class Dictionary
    {
        public static Dictionary<string, double> LengthOut()
        {
            Dictionary<string, double> Length = new Dictionary<string, double>();

            Length.Add("Meters", 0.3048);
            Length.Add("Feet", 1);
            Length.Add("Aggies", 1 / 5.5);
            Length.Add("Inches", 12);
            Length.Add("Yards", 1 / 3);
            Length.Add("Kilometers", 0.0003048);
            Length.Add("Bohr Radius", 5759884813.1885);

            return Length;
        }

        public static Dictionary<string, double> Length()
        {
            Dictionary<string, double> Length = new Dictionary<string, double>();

            Length.Add("Meters", 1 / 0.3048);
            Length.Add("Feet", 1);
            Length.Add("Inches", 1 / 12);
            Length.Add("Yards", 1 / 3);
            Length.Add("Kilometers", 1/ 0.0003048);

            return Length;
        }

        public static Dictionary<string, double> Rate()
        {
            Dictionary<string, double> Rate = new Dictionary<string, double>();

            Rate.Add("Degrees/100feet", 0.01);
            Rate.Add("Degrees/100Meters", 1 / 0.003048);

            return Rate;
        }

        public static Dictionary<string, string> Method()
        {
            Dictionary<string, string> Method = new Dictionary<string, string>();

            Method.Add("Angle Averaging", "1");
            Method.Add("Minimum Curvature", "2");
            Method.Add("Tangential", "3");
            Method.Add("Radius of Curvature", "4");

            return Method;
        }

        public static Dictionary<string, double> Angle()
        {
            Dictionary<string, double> Angle = new Dictionary<string, double>();

            Angle.Add("Degrees", 1);
            Angle.Add("Radians", 3.14159265359 / 180);
            Angle.Add("Compass", 0.0);
            return Angle;
        }
    }
}
