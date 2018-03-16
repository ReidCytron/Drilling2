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
using System.Text.RegularExpressions;

namespace Drilling2
{
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
            string Method = Convert.ToString(Dictionary.Method()[W_Method.Text]);
            string Path = Save_File_WT.Text+@"\datafile.txt";
            double SurfaceUnit = Dictionary.Length()[Surface_Unit.Text];
            double TargetUnit = Dictionary.Length()[Target_Unit.Text];
            double KOPUnit = Dictionary.Length()[KOP_Length_Unit.Text];
            double BuildRateUnit = Dictionary.Rate()[Build_Rate_Unit.Text];
            double DropRateUnit = Dictionary.Rate()[Drop_Rate_Unit.Text];
            double TurnRateUnit = Dictionary.Rate()[Turn_Rate_Unit.Text];
            double IncrementUnit = Dictionary.Length()[Iteration_Increment_Unit.Text];
            double AngleOutUnit = Dictionary.Angle()[Angle_Output_Unit.Text];
            double LengthOutUnit = Dictionary.Length()[Length_Output_Unit.Text];
            double ToleranceUnit = Dictionary.Length()[Tolerance_Unit.Text];
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
            double Tol = Convert.ToDouble(Tolerance.Text) * ToleranceUnit / LengthOutUnit;
            double Increment = Convert.ToDouble(Iteration_Increment.Text) * IncrementUnit;
            double BuildRadius = (180.0 / 3.14159265359) * (1 / BuildRate);
            double DropRadius = (180.0 / 3.14159265359) * (1 / DropRate);
            double TurnRadius = (180.0 / 3.14159265359) * (1.0 / TurnRate);
            double HD = Math.Sqrt(((TY - SY) * (TY - SY)) + ((TX - SX) * (TX - SX)));
            double TVD = (TZ - SZ);
            double TurnAngle = 2.0 * RadianToDegree(Math.Asin((HD * 0.5) / TurnRadius));
            double Length = TurnAngle * (3.14159265359 * TurnRadius / 180.0);
            if (TurnRate == 0) { Length = HD; };
            double ThetaMax = MaxIncAngle(Length, KOP, BuildRadius, DropRadius, TVD);
            double Ai = InitialAzimuth(TX, TY, SX, SY, HD);
            double Lead = Ai + (Length * TurnRate) * 0.1;
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
            ResultMatrix[0, 0] = "Group";
            ResultMatrix[0, 1] = "X";
            ResultMatrix[0, 2] = "Y";
            ResultMatrix[0, 3] = "Z";
            ResultMatrix[0, 4] = "Units";
            if (KOP >= (TZ - SZ)){
                MessageBox.Show("The KOP is too deep");
                return;
            }
            if (Increment > 25){
                MessageBox.Show("The increment size is too large");
                return;
            }
            if (Increment < 0.05){
                MessageBox.Show("The target is outside of the boundary");
                return;
            }
                    Z = SZ / LengthOutUnit;
                    X = SX / LengthOutUnit;
                    Y = SY / LengthOutUnit;
                    i = 1;
                    do
                    {
                        ResultMatrix[i, 0] = "Group-5";
                        ResultMatrix[i, 1] = Convert.ToString(Math.Round(SX / LengthOutUnit, 3));
                        ResultMatrix[i, 2] = Convert.ToString(Math.Round(SY / LengthOutUnit, 3));
                        ResultMatrix[i, 3] = Convert.ToString(Math.Round(Z / LengthOutUnit, 3));
                        ResultMatrix[i, 4] = Length_Output_Unit.Text;
                        i = i + 1;
                        Z = Z + Increment;
                    } while (Z <= SZ + KOP);
                    I_Last = 0.0;
                    A_Last = 0.0;
                    A = Lead;
                    I = IB_Inc;
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
                            case "5":
                                Result = IAAM(I, A, I_Last, A_Last, Increment);
                                break;
                            case "6":
                                Result = SM(I, A, I_Last, A_Last, Increment);
                                break;
                            case "7":
                                Result = HAM(I, A, I_Last, A_Last, Increment);
                                break;
                            case "8":
                                Result = CircularArcMethod(I, A, I_Last, A_Last, Increment);
                                break;
                        }
                        Result = Tangential(I, A, I_Last, A_Last, Increment);
                        Results = Result.Split(' ');
                        X = X + Convert.ToDouble(Results[0]);
                        Y = Y + Convert.ToDouble(Results[1]);
                        Z = Z + Convert.ToDouble(Results[2]);
                        ResultMatrix[i, 0] = "Group-5";
                        ResultMatrix[i, 1] = Convert.ToString(Math.Round(X / LengthOutUnit, 3));
                        ResultMatrix[i, 2] = Convert.ToString(Math.Round(Y / LengthOutUnit, 3));
                        ResultMatrix[i, 3] = Convert.ToString(Math.Round(Z / LengthOutUnit, 3));
                        ResultMatrix[i, 4] = Length_Output_Unit.Text;
                        I_Last = I;
                        A_Last = A;
                        I = I + IB_Inc;
                        A = A - A_Inc * 0.15;
                        i = i + 1;
                    } while (I <= ThetaMax);
                    I_Last = I;
                    do
                    {
                        switch (Method)
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
                            case "5":
                                Result = IAAM(I, A, I_Last, A_Last, Increment);
                                break;
                            case "6":
                                Result = SM(I, A, I_Last, A_Last, Increment);
                                break;
                            case "7":
                                Result = HAM(I, A, I_Last, A_Last, Increment);
                                break;
                            case "8":
                                Result = Tangential(I, A, I_Last, A_Last, Increment);
                                break;
                        }
                        Result = Tangential(I, A, I_Last, A_Last, Increment);
                        Results = Result.Split(' ');
                        X = X + Convert.ToDouble(Results[0]);
                        Y = Y + Convert.ToDouble(Results[1]);
                        Z = Z + Convert.ToDouble(Results[2]);
                        ResultMatrix[i, 0] = "Group-5";
                        ResultMatrix[i, 1] = Convert.ToString(Math.Round(X / LengthOutUnit, 3));
                        ResultMatrix[i, 2] = Convert.ToString(Math.Round(Y / LengthOutUnit, 3));
                        ResultMatrix[i, 3] = Convert.ToString(Math.Round(Z / LengthOutUnit, 3));
                        ResultMatrix[i, 4] = Length_Output_Unit.Text;
                        A_Last = A;
                        A = A - A_Inc * 0.05;
                        i = i + 1;
                    } while (Z < SZ + ((TZ - SZ) - DropRadius * Math.Sin(DegreeToRadian(ThetaMax))));
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
                            case "5":
                                Result = IAAM(I, A, I_Last, A_Last, Increment);
                                break;
                            case "6":
                                Result = SM(I, A, I_Last, A_Last, Increment);
                                break;
                            case "7":
                                Result = HAM(I, A, I_Last, A_Last, Increment);
                                break;
                            case "8":
                                Result = CircularArcMethod(I, A, I_Last, A_Last, Increment);
                                break;
                        }
                        Result = Tangential(I, A, I_Last, A_Last, Increment);
                        Results = Result.Split(' ');
                        X = X + Convert.ToDouble(Results[0]);
                        Y = Y + Convert.ToDouble(Results[1]);
                        Z = Z + Convert.ToDouble(Results[2]);
                        ResultMatrix[i, 0] = "Group-5";
                        ResultMatrix[i, 1] = Convert.ToString(Math.Round(X / LengthOutUnit, 3));
                        ResultMatrix[i, 2] = Convert.ToString(Math.Round(Y / LengthOutUnit, 3));
                        ResultMatrix[i, 3] = Convert.ToString(Math.Round(Z / LengthOutUnit, 3));
                        ResultMatrix[i, 4] = Length_Output_Unit.Text;
                        I_Last = I;
                        A_Last = A;
                        I = I - ID_Inc;
                        A = A - A_Inc * 0.05;
                        i = i + 1;
                        check = Math.Sqrt((X - TX) * (X - TX) + (Y - TY) * (Y - TY) + (Z - TZ) * (Z - TZ)) / LengthOutUnit;
                        if (check <= Tol)
                        {
                            Hit = "Success";
                            continue;
                        }
                        else
                        {
                            Hit = "Failure";
                        }
                    } while (I >= 0.0);
            if (Angle_Output_Unit.Text == "Compass")
            {
                Af = AzimuthToCompass(A);
                Convert.ToString(Math.Round(A, 3));
            }
            else if(Angle_Output_Unit.Text == "Radians")
            {
                Af = Convert.ToString(Math.Round(DegreeToRadian(A), 3));
            }
            else
            {
                Af = Convert.ToString(Math.Round(A, 3));
            }
            int h = 0;
            if (!File.Exists(Path))
            {
                File.Create(Path);
                TextWriter txt = new StreamWriter(Path);
                do
                {
                    txt.Write(ResultMatrix[h, 0] + ", " + ResultMatrix[h, 1] + " , " + ResultMatrix[h, 2] + " , " + ResultMatrix[h, 3] + " , " + ResultMatrix[h, 4] + "\n");
                    h++;
                } while (h < i);
                txt.WriteLine("END");
                txt.Close();
            }
            else if (File.Exists(Path))
            {
                File.WriteAllText(Path, String.Empty);
                using (var txt = new StreamWriter(Path, true))
                {
                    do {
                        txt.WriteLine(ResultMatrix[h, 0] + ", " + ResultMatrix[h, 1] + " , " + ResultMatrix[h, 2] + " , " + ResultMatrix[h, 3] + " , " + ResultMatrix[h, 4] + "\n");
                        h++;
                    } while (h < i);
                    txt.WriteLine("END");
                    txt.Close();
                }
            }
            
            MessageBox.Show("Finished\nAzimuth = "+Af+" "+Angle_Output_Unit.Text+"\n"+"MD = "+Convert.ToString(Math.Round(i * Increment, 3))+" feet\nMax Inclination Angle = "+Math.Round(ThetaMax, 2)+" degrees\nTol = "+
                Convert.ToString(Math.Round(Tol, 2))+" "+Length_Output_Unit.Text+"\n"+"Distance From Target = "+Convert.ToString(Math.Round(check, 3))+" "+Length_Output_Unit.Text+"\n"+Hit);

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

        private static double DegreeToRadian(double angle)
        {
            return angle * ( 3.14159265359 / 180.0);
        }
        private static double RadianToDegree(double radians)
        {
            return radians * (180.0 / 3.14159265359);
        }

        public static double MaxIncAngle(double HD, double KOP, double BuildRadius, double DropRadius, double TVD)
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

        public static double MaxIncAngleBuildHold(double HD, double KOP, double BuildRadius, double DropRadius, double TVD)
        {
            double A, B, C, D, theta;
            double VD = TVD - KOP;

            if (HD < BuildRadius)
            {
                A = RadianToDegree(Math.Atan(VD / (BuildRadius - HD)));
                B = (BuildRadius / VD);
                C = Math.Sin(DegreeToRadian(A));
                D = RadianToDegree(Math.Acos(B * C));
                theta = A - D;
            }
            else
            {
                A = RadianToDegree(Math.Atan(VD / (HD - BuildRadius)));
                B = (BuildRadius / VD);
                C = Math.Sin(DegreeToRadian(A));
                D = RadianToDegree(Math.Acos(B * C));
                theta = 180 - A - D;
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
            Angle = Math.Round(Angle, 3);

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
            double X, Y, Z;

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
        
        private string IAAM(double I, double A, double I_Last, double A_Last, double Increment)
        {
            double X, Y, Z, beta, Ci;

            beta = (1 / Increment) * ((I - I_Last) * (I - I_Last) + Math.Sqrt((Math.Sin(DegreeToRadian((I + I_Last) / 2.0))) * (A - A_Last) * (Math.Sin(DegreeToRadian((I + I_Last) / 2.0))) * (A - A_Last)));

            Ci = (2.0 * Increment * Math.Sin(DegreeToRadian(beta/2.0))) / beta;

            X = Ci * Math.Sin(DegreeToRadian((I + I_Last) / 2.0)) * Math.Sin(DegreeToRadian((A + A_Last) / 2.0));
            Y = Ci * Math.Sin(DegreeToRadian((I+I_Last)/2.0))*Math.Cos(DegreeToRadian((A+A_Last)/2.0));
            Z = Ci * Math.Cos(DegreeToRadian((I+I_Last)/2.0));

            return Convert.ToString(X + " " + Y + " " + Z);
        }

        private string SM(double I, double A, double I_Last, double A_Last, double Increment)
        {
            double X, Y, Z, Vx ,Vy, Vz, Ux, Uy, Uz, phi, mew;

            Ux = Math.Sin(RadianToDegree(A)) * Math.Cos(RadianToDegree(I));
            Uy = Math.Sin(RadianToDegree(A)) * Math.Sin(RadianToDegree(I));
            Uz = Math.Cos(RadianToDegree(I));

            Vx = Math.Sin(RadianToDegree(A_Last)) * Math.Cos(RadianToDegree(I_Last));
            Vy = Math.Sin(RadianToDegree(A_Last)) * Math.Sin(RadianToDegree(I_Last));
            Vz = Math.Cos(RadianToDegree(I_Last));

            phi = DegreeToRadian(Math.Acos(Ux * Vx + Uy * Vy + Uz * Vz));

            mew = Math.Sqrt(2.0 / (1.0 + Math.Cos(RadianToDegree(phi))));

            X = Increment * mew * (Vx + Ux) / 2.0;
            Y = Increment * mew * (Vy + Uy) / 2.0;
            Z = Increment * mew * (Vz + Uz) / 2.0;

            return Convert.ToString(X + " " + Y + " " + Z);
        }

        private string CircularArcMethod(double I, double A, double I_Last, double A_Last, double Increment)
        {
            double X, Y, Z;

            X = (Increment / 2.0) * (Math.Sin(DegreeToRadian(I)) * Math.Sin(DegreeToRadian(A)) + Math.Sin(DegreeToRadian(I_Last)) * Math.Sin(DegreeToRadian(A_Last)));
            Y = (Increment / 2.0) * (Math.Sin(DegreeToRadian(I)) * Math.Cos(DegreeToRadian(A)) + Math.Sin(DegreeToRadian(I_Last)) * Math.Cos(DegreeToRadian(A_Last)));
            Z = (Increment / 2.0) * (Math.Cos(DegreeToRadian(I)) + Math.Cos(DegreeToRadian(I_Last)));

            return Convert.ToString(X + " " + Y + " " + Z);
        }

        private string HAM(double I, double A, double I_Last, double A_Last, double Increment)
        {
            double X, Y, Z, Vx, Vy, Vz, Ux, Uy, Uz, phi, mew;

            Ux = Math.Sin(RadianToDegree(A)) * Math.Cos(RadianToDegree(I));
            Uy = Math.Sin(RadianToDegree(A)) * Math.Sin(RadianToDegree(I));
            Uz = Math.Cos(RadianToDegree(I));

            Vx = Math.Sin(RadianToDegree(A_Last)) * Math.Cos(RadianToDegree(I_Last));
            Vy = Math.Sin(RadianToDegree(A_Last)) * Math.Sin(RadianToDegree(I_Last));
            Vz = Math.Cos(RadianToDegree(I_Last));

            phi = DegreeToRadian(Math.Acos(Ux * Vx + Uy * Vy + Uz * Vz));

            mew = Math.Sqrt(2.0 / (1.0 + Math.Cos(RadianToDegree(phi))));

            X = Increment * mew * (Vx + Ux) / 2.0;
            Y = Increment * mew * (Vy + Uy) / 2.0;
            Z = Increment * mew * (Vz + Uz) / 2.0;

            return Convert.ToString(X + " " + Y + " " + Z);
        }
        

        private void Angle_Output_Unit_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void WellboreTrajectoryForm_Load(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Line;
            string path = Survey_File.Text;
            if (Survey_File.Text != Save_File_WT.Text + @"\datafile.txt") { MessageBox.Show("You did not select the data file."); return; };
            int i;
            double X = 0, Y = 0, Z = 0, X_Last = 0.0, Y_Last = 0.0, Z_Last = 0.0, MD = 0, increment;
            string[] Str = new string[5];
            string[] Strings = new string[1000000];
            i = 0;
            StreamReader ReadSurveyFile = new StreamReader(Survey_File.Text);
            while ((Line = Convert.ToString(ReadSurveyFile.ReadLine())) != null)
            {
                Strings[i] = Line;
                Str = Strings[i].Split(new char[] { ',' }, StringSplitOptions.None);
                //X = Convert.ToDouble(Str[1]);
                //Y = Convert.ToDouble(Str[2]);
                //Z = Convert.ToDouble(Str[3]);
                increment = Math.Sqrt((X - X_Last) * (X - X_Last) + (Y - Y_Last) * (Y - Y_Last) + (Z - Z_Last) * (Z - Z_Last));
                X_Last = X;
                Y_Last = Y;
                Z_Last = Z;
                MD = MD + increment;
                i++;
            }

            string Method = Convert.ToString(Dictionary.Method()[W_Method.Text]);
            string Path = Save_File_WT.Text+@"\surveyfile.txt";
            double SurfaceUnit = Dictionary.Length()[Surface_Unit.Text];
            double TargetUnit = Dictionary.Length()[Target_Unit.Text];
            double KOPUnit = Dictionary.Length()[KOP_Length_Unit.Text];
            double BuildRateUnit = Dictionary.Rate()[Build_Rate_Unit.Text];
            double DropRateUnit = Dictionary.Rate()[Drop_Rate_Unit.Text];
            double TurnRateUnit = Dictionary.Rate()[Turn_Rate_Unit.Text];
            double IncrementUnit = Dictionary.Length()[Iteration_Increment_Unit.Text];
            double AngleOutUnit = Dictionary.Angle()[Angle_Output_Unit.Text];
            double LengthOutUnit = Dictionary.Length()[Length_Output_Unit.Text];
            double ToleranceUnit = Dictionary.Length()[Tolerance_Unit.Text];
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
            double Tol = Convert.ToDouble(Tolerance.Text) * ToleranceUnit / LengthOutUnit;
            double Increment = Convert.ToDouble(Iteration_Increment.Text) * IncrementUnit;
            double BuildRadius = (180.0 / 3.14159265359) * (1 / BuildRate);
            double DropRadius = (180.0 / 3.14159265359) * (1 / DropRate);
            double TurnRadius = (180.0 / 3.14159265359) * (1.0 / TurnRate);
            double HD = Math.Sqrt(((TY - SY) * (TY - SY)) + ((TX - SX) * (TX - SX)));
            double TVD = (TZ - SZ);
            double TurnAngle = 2.0 * RadianToDegree(Math.Asin((HD * 0.5) / TurnRadius));
            double Length = TurnAngle * (3.14159265359 * TurnRadius / 180.0);
            if (TurnRate == 0) { Length = HD; };
            double ThetaMax = MaxIncAngle(Length, KOP, BuildRadius, DropRadius, TVD);
            double Ai = InitialAzimuth(TX, TY, SX, SY, HD);
            double Lead = Ai + (Length * TurnRate) * 0.1;
            double IB_Inc = BuildRate * Increment;
            double ID_Inc = DropRate * Increment;
            double A_Inc = TurnRate * Increment;
            double A = 0.0;
            string Af;
            double I = 0.0;
            double A_Last = 0.0;
            double I_Last = 0.0;
            X = 0.0;
            Y = 0.0;
            Z = 0.0;
            string Hit = " ";
            string[] Results = new string[3];
            string Result = " ";
            string[,] ResultMatrix = new string[1000000, 6];
            double Hold_end = Length - (DropRadius - DropRadius * RadianToDegree(Math.Cos(ThetaMax)));
            int j = 0;
            i = 0;
            double check = 0;
            ResultMatrix[0, 0] = "MD";
            ResultMatrix[0, 1] = "I";
            ResultMatrix[0, 2] = "A";
            ResultMatrix[0, 3] = "X";
            ResultMatrix[0, 4] = "Y";
            ResultMatrix[0, 5] = "Z";
            if (KOP >= (TZ - SZ)){
                MessageBox.Show("The KOP is too deep");
                return;
            }
            if (Increment > 25){
                MessageBox.Show("The increment size is too large");
                return;
            }
            if (Increment < 0.05){
                MessageBox.Show("The target is outside of the boundary");
                return;
            }
                    Z = SZ / LengthOutUnit;
                    X = SX / LengthOutUnit;
                    Y = SY / LengthOutUnit;
                    i = 1;
                    do
                    {
                        ResultMatrix[i, 0] = Convert.ToString(Math.Round(i * Increment / LengthOutUnit, 3));
                        ResultMatrix[i, 1] = Convert.ToString(Math.Round(I / AngleOutUnit, 3));
                        ResultMatrix[i, 2] = Convert.ToString(Math.Round(A / AngleOutUnit, 3));
                        if (Angle_Output_Unit.Text == "Compass") { ResultMatrix[i, 2] = AzimuthToCompass(A); };
                        ResultMatrix[i, 3] = Convert.ToString(Math.Round(SX / LengthOutUnit, 3));
                        ResultMatrix[i, 4] = Convert.ToString(Math.Round(SY / LengthOutUnit, 3));
                        ResultMatrix[i, 5] = Convert.ToString(Math.Round(Z / LengthOutUnit, 3));
                        i = i + 1;
                        Z = Z + Increment;
                    } while (Z <= SZ + KOP);
                    I_Last = 0.0;
                    A_Last = 0.0;
                    A = Lead;
                    I = IB_Inc;
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
                            case "5":
                                Result = IAAM(I, A, I_Last, A_Last, Increment);
                                break;
                            case "6":
                                Result = SM(I, A, I_Last, A_Last, Increment);
                                break;
                            case "7":
                                Result = HAM(I, A, I_Last, A_Last, Increment);
                                break;
                            case "8":
                                Result = CircularArcMethod(I, A, I_Last, A_Last, Increment);
                                break;
                        }
                        Result = Tangential(I, A, I_Last, A_Last, Increment);
                        Results = Result.Split(' ');
                        X = X + Convert.ToDouble(Results[0]);
                        Y = Y + Convert.ToDouble(Results[1]);
                        Z = Z + Convert.ToDouble(Results[2]);
                        ResultMatrix[i, 0] = Convert.ToString(Math.Round(i * Increment / LengthOutUnit, 3));
                        ResultMatrix[i, 1] = Convert.ToString(Math.Round(I / AngleOutUnit, 3));
                        ResultMatrix[i, 2] = Convert.ToString(Math.Round(A, 3));
                        if (Angle_Output_Unit.Text == "Compass") { ResultMatrix[i, 2] = AzimuthToCompass(A); };
                        ResultMatrix[i, 3] = Convert.ToString(Math.Round(SX / LengthOutUnit, 3));
                        ResultMatrix[i, 4] = Convert.ToString(Math.Round(SY / LengthOutUnit, 3));
                        ResultMatrix[i, 5] = Convert.ToString(Math.Round(Z / LengthOutUnit, 3));
                        I_Last = I;
                        A_Last = A;
                        I = I + IB_Inc;
                        A = A - A_Inc * 0.15;
                        i = i + 1;
                    } while (I <= ThetaMax);
                    I_Last = I;
                    do
                    {
                        switch (Method)
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
                            case "5":
                                Result = IAAM(I, A, I_Last, A_Last, Increment);
                                break;
                            case "6":
                                Result = SM(I, A, I_Last, A_Last, Increment);
                                break;
                            case "7":
                                Result = HAM(I, A, I_Last, A_Last, Increment);
                                break;
                            case "8":
                                Result = Tangential(I, A, I_Last, A_Last, Increment);
                                break;
                        }
                        Result = Tangential(I, A, I_Last, A_Last, Increment);
                        Results = Result.Split(' ');
                        X = X + Convert.ToDouble(Results[0]);
                        Y = Y + Convert.ToDouble(Results[1]);
                        Z = Z + Convert.ToDouble(Results[2]);
                        ResultMatrix[i, 0] = Convert.ToString(Math.Round(i * Increment / LengthOutUnit, 3));
                        ResultMatrix[i, 1] = Convert.ToString(Math.Round(I / AngleOutUnit, 3));
                        ResultMatrix[i, 2] = Convert.ToString(Math.Round(A, 3));
                        if (Angle_Output_Unit.Text == "Compass") { ResultMatrix[i, 2] = AzimuthToCompass(A); };
                        ResultMatrix[i, 3] = Convert.ToString(Math.Round(SX / LengthOutUnit, 3));
                        ResultMatrix[i, 4] = Convert.ToString(Math.Round(SY / LengthOutUnit, 3));
                        ResultMatrix[i, 5] = Convert.ToString(Math.Round(Z / LengthOutUnit, 3));
                        A_Last = A;
                        A = A - A_Inc * 0.05;
                        i = i + 1;
                    } while (Z < SZ + ((TZ - SZ) - DropRadius * Math.Sin(DegreeToRadian(ThetaMax))));
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
                            case "5":
                                Result = IAAM(I, A, I_Last, A_Last, Increment);
                                break;
                            case "6":
                                Result = SM(I, A, I_Last, A_Last, Increment);
                                break;
                            case "7":
                                Result = HAM(I, A, I_Last, A_Last, Increment);
                                break;
                            case "8":
                                Result = CircularArcMethod(I, A, I_Last, A_Last, Increment);
                                break;
                        }
                        Result = Tangential(I, A, I_Last, A_Last, Increment);
                        Results = Result.Split(' ');
                        X = X + Convert.ToDouble(Results[0]);
                        Y = Y + Convert.ToDouble(Results[1]);
                        Z = Z + Convert.ToDouble(Results[2]);
                        ResultMatrix[i, 0] = Convert.ToString(Math.Round(i * Increment / LengthOutUnit, 3));
                        ResultMatrix[i, 1] = Convert.ToString(Math.Round(I / AngleOutUnit, 3));
                        ResultMatrix[i, 2] = Convert.ToString(Math.Round(A, 3));
                        if (Angle_Output_Unit.Text == "Compass") { ResultMatrix[i, 2] = AzimuthToCompass(A); };
                        ResultMatrix[i, 3] = Convert.ToString(Math.Round(SX / LengthOutUnit, 3));
                        ResultMatrix[i, 4] = Convert.ToString(Math.Round(SY / LengthOutUnit, 3));
                        ResultMatrix[i, 5] = Convert.ToString(Math.Round(Z / LengthOutUnit, 3));
                        I_Last = I;
                        A_Last = A;
                        I = I - ID_Inc;
                        A = A - A_Inc * 0.05;
                        i = i + 1;
                        check = Math.Sqrt((X - TX) * (X - TX) + (Y - TY) * (Y - TY) + (Z - TZ) * (Z - TZ)) / LengthOutUnit;
                        if (check <= Tol)
                        {
                            Hit = "Success";
                            continue;
                        }
                        else
                        {
                            Hit = "Failure";
                        }
                    } while (I >= 0.0);
            if (Angle_Output_Unit.Text == "Compass")
            {
                Af = AzimuthToCompass(A);
                Convert.ToString(Math.Round(A, 3));
            }
            else if(Angle_Output_Unit.Text == "Radians")
            {
                Af = Convert.ToString(Math.Round(DegreeToRadian(A), 3));
            }
            else
            {
                Af = Convert.ToString(Math.Round(A, 3));
            }
            int h = 0;
            if (!File.Exists(Path))
            {
                File.Create(Path);
                TextWriter txt = new StreamWriter(Path);
                do
                {
                    txt.Write(ResultMatrix[h, 0] + ", " + ResultMatrix[h, 1] + " , " + ResultMatrix[h, 2] + " , " + ResultMatrix[h, 3] + " , " + ResultMatrix[h, 4] + " , " + ResultMatrix[h, 5] + "\n");
                    h++;
                } while (h < i);
                txt.WriteLine("END");
                txt.Close();
            }
            else if (File.Exists(Path))
            {
                File.WriteAllText(Path, String.Empty);
                using (var txt = new StreamWriter(Path, true))
                {
                    do {
                        txt.WriteLine(ResultMatrix[h, 0] + ", " + ResultMatrix[h, 1] + " , " + ResultMatrix[h, 2] + " , " + ResultMatrix[h, 3] + " , " + ResultMatrix[h, 4]+ " , " + ResultMatrix[h, 5] + "\n");
                        h++;
                    } while (h < i);
                    txt.WriteLine("END");
                    txt.Close();
                }
            }
            MessageBox.Show("Finished");
        }

        private void Save_File_Dialogue_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            FolderBrowserDialog SaveFileDialog1 = new FolderBrowserDialog();

            if (SaveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Save_File_WT.Text = SaveFileDialog1.SelectedPath;
            }
        }

        private void Length_Output_Unit_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            OpenFileDialog SurveyFileDialog1 = new OpenFileDialog();

            if (SurveyFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Survey_File.Text = SurveyFileDialog1.FileName;
            }
        }

        private void SurveyFile_TextChanged(object sender, EventArgs e)
        {

        }

        private void Y_Target_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }
        private void Compare_Button_Click(object sender, EventArgs e)
        {
            string Method1 = Convert.ToString(Dictionary.Method()[C_Method_1.Text]);
            string Method2 = Convert.ToString(Dictionary.Method()[C_Method_2.Text]);
            string Path = Save_File_WT.Text + @"\comparefile.txt";
            double SurfaceUnit = Dictionary.Length()[Surface_Unit.Text];
            double TargetUnit = Dictionary.Length()[Target_Unit.Text];
            double KOPUnit = Dictionary.Length()[KOP_Length_Unit.Text];
            double BuildRateUnit = Dictionary.Rate()[Build_Rate_Unit.Text];
            double DropRateUnit = Dictionary.Rate()[Drop_Rate_Unit.Text];
            double TurnRateUnit = Dictionary.Rate()[Turn_Rate_Unit.Text];
            double IncrementUnit = Dictionary.Length()[Iteration_Increment_Unit.Text];
            double AngleOutUnit = Dictionary.Angle()[Angle_Output_Unit.Text];
            double LengthOutUnit = Dictionary.Length()[Length_Output_Unit.Text];
            double ToleranceUnit = Dictionary.Length()[Tolerance_Unit.Text];
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
            double Tol = Convert.ToDouble(Tolerance.Text) * ToleranceUnit / LengthOutUnit;
            double Increment = Convert.ToDouble(Iteration_Increment.Text) * IncrementUnit;
            double BuildRadius = (180.0 / 3.14159265359) * (1 / BuildRate);
            double DropRadius = (180.0 / 3.14159265359) * (1 / DropRate);
            double TurnRadius = (180.0 / 3.14159265359) * (1.0 / TurnRate);
            double HD = Math.Sqrt(((TY - SY) * (TY - SY)) + ((TX - SX) * (TX - SX)));
            double TVD = (TZ - SZ);
            double TurnAngle = 2.0 * RadianToDegree(Math.Asin((HD * 0.5) / TurnRadius));
            double Length = TurnAngle * (3.14159265359 * TurnRadius / 180.0);
            if (TurnRate == 0) { Length = HD; };
            double ThetaMax = MaxIncAngle(Length, KOP, BuildRadius, DropRadius, TVD);
            double Ai = InitialAzimuth(TX, TY, SX, SY, HD);
            double Lead = Ai + (Length * TurnRate) * 0.1;
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
            double X2 = 0.0;
            double Y2 = 0.0;
            double Z2 = 0.0;
            string Hit = " ";
            string[] Results = new string[3];
            string Result = " ";
            string[,] ResultMatrix1 = new string[1000000, 3];
            string[,] ResultMatrix2 = new string[1000000, 3];
            double Hold_end = Length - (DropRadius - DropRadius * RadianToDegree(Math.Cos(ThetaMax)));
            int j = 0;
            int i = 0;
            double check = 0;
            ResultMatrix1[0, 0] = "X";
            ResultMatrix1[0, 1] = "Y";
            ResultMatrix1[0, 2] = "Z";
            ResultMatrix2[0, 0] = "X";
            ResultMatrix2[0, 1] = "Y";
            ResultMatrix2[0, 2] = "Z";
            if (KOP >= (TZ - SZ))
            {
                MessageBox.Show("The KOP is too deep");
                return;
            }
            if (Increment > 25)
            {
                MessageBox.Show("The increment size is too large");
                return;
            }
            if (Increment < 0.05)
            {
                MessageBox.Show("The target is outside of the boundary");
                return;
            }
            Z = SZ / LengthOutUnit;
            X = SX / LengthOutUnit;
            Y = SY / LengthOutUnit;
            i = 1;
            do
            {
                ResultMatrix1[i, 0] = Convert.ToString(Math.Round(SX / LengthOutUnit, 3));
                ResultMatrix1[i, 1] = Convert.ToString(Math.Round(SY / LengthOutUnit, 3));
                ResultMatrix1[i, 2] = Convert.ToString(Math.Round(Z / LengthOutUnit, 3));
                ResultMatrix2[i, 0] = Convert.ToString(Math.Round(SX / LengthOutUnit, 3));
                ResultMatrix2[i, 1] = Convert.ToString(Math.Round(SY / LengthOutUnit, 3));
                ResultMatrix2[i, 2] = Convert.ToString(Math.Round(Z2 / LengthOutUnit, 3));
                i = i + 1;
                Z = Z + Increment;
                Z2 = Z2 + Increment;
            } while (Z <= SZ + KOP);
            I_Last = 0.0;
            A_Last = 0.0;
            A = Lead;
            I = IB_Inc;
            do
            {
                switch (Method1)
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
                    case "5":
                        Result = IAAM(I, A, I_Last, A_Last, Increment);
                        break;
                    case "6":
                        Result = SM(I, A, I_Last, A_Last, Increment);
                        break;
                    case "7":
                        Result = HAM(I, A, I_Last, A_Last, Increment);
                        break;
                    case "8":
                        Result = CircularArcMethod(I, A, I_Last, A_Last, Increment);
                        break;
                }
                Result = Tangential(I, A, I_Last, A_Last, Increment);
                Results = Result.Split(' ');
                X = X + Convert.ToDouble(Results[0]);
                Y = Y + Convert.ToDouble(Results[1]);
                Z = Z + Convert.ToDouble(Results[2]);
                ResultMatrix1[i, 0] = Convert.ToString(Math.Round(X / LengthOutUnit, 3));
                ResultMatrix1[i, 1] = Convert.ToString(Math.Round(Y / LengthOutUnit, 3));
                ResultMatrix1[i, 2] = Convert.ToString(Math.Round(Z / LengthOutUnit, 3));
                switch (Method2)
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
                    case "5":
                        Result = IAAM(I, A, I_Last, A_Last, Increment);
                        break;
                    case "6":
                        Result = SM(I, A, I_Last, A_Last, Increment);
                        break;
                    case "7":
                        Result = HAM(I, A, I_Last, A_Last, Increment);
                        break;
                    case "8":
                        Result = CircularArcMethod(I, A, I_Last, A_Last, Increment);
                        break;
                }
                Result = Tangential(I, A, I_Last, A_Last, Increment);
                Results = Result.Split(' ');
                X2 = X2 + Convert.ToDouble(Results[0]);
                Y2 = Y2 + Convert.ToDouble(Results[1]);
                Z2 = Z2 + Convert.ToDouble(Results[2]);
                ResultMatrix2[i, 0] = Convert.ToString(Math.Round(X2 / LengthOutUnit, 3));
                ResultMatrix2[i, 1] = Convert.ToString(Math.Round(Y2 / LengthOutUnit, 3));
                ResultMatrix2[i, 2] = Convert.ToString(Math.Round(Z2 / LengthOutUnit, 3));
                I_Last = I;
                A_Last = A;
                I = I + IB_Inc;
                A = A - A_Inc * 0.15;
                i = i + 1;
            } while (I <= ThetaMax);
            I_Last = I;
            do
            {
                switch (Method1)
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
                    case "5":
                        Result = IAAM(I, A, I_Last, A_Last, Increment);
                        break;
                    case "6":
                        Result = SM(I, A, I_Last, A_Last, Increment);
                        break;
                    case "7":
                        Result = HAM(I, A, I_Last, A_Last, Increment);
                        break;
                    case "8":
                        Result = Tangential(I, A, I_Last, A_Last, Increment);
                        break;
                }
                Result = Tangential(I, A, I_Last, A_Last, Increment);
                Results = Result.Split(' ');
                X = X + Convert.ToDouble(Results[0]);
                Y = Y + Convert.ToDouble(Results[1]);
                Z = Z + Convert.ToDouble(Results[2]);
                ResultMatrix1[i, 0] = Convert.ToString(Math.Round(X / LengthOutUnit, 3));
                ResultMatrix1[i, 1] = Convert.ToString(Math.Round(Y / LengthOutUnit, 3));
                ResultMatrix1[i, 2] = Convert.ToString(Math.Round(Z / LengthOutUnit, 3));
                switch (Method2)
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
                    case "5":
                        Result = IAAM(I, A, I_Last, A_Last, Increment);
                        break;
                    case "6":
                        Result = SM(I, A, I_Last, A_Last, Increment);
                        break;
                    case "7":
                        Result = HAM(I, A, I_Last, A_Last, Increment);
                        break;
                    case "8":
                        Result = Tangential(I, A, I_Last, A_Last, Increment);
                        break;
                }
                Result = Tangential(I, A, I_Last, A_Last, Increment);
                Results = Result.Split(' ');
                X2 = X2 + Convert.ToDouble(Results[0]);
                Y2 = Y2 + Convert.ToDouble(Results[1]);
                Z2 = Z2 + Convert.ToDouble(Results[2]);
                ResultMatrix2[i, 0] = Convert.ToString(Math.Round(X2 / LengthOutUnit, 3));
                ResultMatrix2[i, 1] = Convert.ToString(Math.Round(Y2 / LengthOutUnit, 3));
                ResultMatrix2[i, 2] = Convert.ToString(Math.Round(Z2 / LengthOutUnit, 3));
                A_Last = A;
                A = A - A_Inc * 0.05;
                i = i + 1;
            } while (Z < SZ + ((TZ - SZ) - DropRadius * Math.Sin(DegreeToRadian(ThetaMax))));
            do
            {
                switch (Method1)
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
                    case "5":
                        Result = IAAM(I, A, I_Last, A_Last, Increment);
                        break;
                    case "6":
                        Result = SM(I, A, I_Last, A_Last, Increment);
                        break;
                    case "7":
                        Result = HAM(I, A, I_Last, A_Last, Increment);
                        break;
                    case "8":
                        Result = CircularArcMethod(I, A, I_Last, A_Last, Increment);
                        break;
                }
                Result = Tangential(I, A, I_Last, A_Last, Increment);
                Results = Result.Split(' ');
                X = X + Convert.ToDouble(Results[0]);
                Y = Y + Convert.ToDouble(Results[1]);
                Z = Z + Convert.ToDouble(Results[2]);
                ResultMatrix1[i, 0] = Convert.ToString(Math.Round(X / LengthOutUnit, 3));
                ResultMatrix1[i, 1] = Convert.ToString(Math.Round(Y / LengthOutUnit, 3));
                ResultMatrix1[i, 2] = Convert.ToString(Math.Round(Z / LengthOutUnit, 3));
               
                switch (Method1)
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
                    case "5":
                        Result = IAAM(I, A, I_Last, A_Last, Increment);
                        break;
                    case "6":
                        Result = SM(I, A, I_Last, A_Last, Increment);
                        break;
                    case "7":
                        Result = HAM(I, A, I_Last, A_Last, Increment);
                        break;
                    case "8":
                        Result = CircularArcMethod(I, A, I_Last, A_Last, Increment);
                        break;
                }
                Result = Tangential(I, A, I_Last, A_Last, Increment);
                Results = Result.Split(' ');
                X2 = X2 + Convert.ToDouble(Results[0]);
                Y2 = Y2 + Convert.ToDouble(Results[1]);
                Z2 = Z2 + Convert.ToDouble(Results[2]);
                ResultMatrix2[i, 0] = Convert.ToString(Math.Round(X2 / LengthOutUnit, 3));
                ResultMatrix2[i, 1] = Convert.ToString(Math.Round(Y2 / LengthOutUnit, 3));
                ResultMatrix2[i, 2] = Convert.ToString(Math.Round(Z2 / LengthOutUnit, 3));
                I_Last = I;
                A_Last = A;
                I = I - ID_Inc;
                A = A - A_Inc * 0.05;
                i = i + 1;
            } while (I >= 0.0);
            if (Angle_Output_Unit.Text == "Compass")
            {
                Af = AzimuthToCompass(A);
                Convert.ToString(Math.Round(A, 3));
            }
            else if (Angle_Output_Unit.Text == "Radians")
            {
                Af = Convert.ToString(Math.Round(DegreeToRadian(A), 3));
            }
            else
            {
                Af = Convert.ToString(Math.Round(A, 3));
            }
            int h = 0;
            if (!File.Exists(Path))
            {
                File.Create(Path);
                TextWriter txt = new StreamWriter(Path);
                txt.WriteLine(C_Method_1.Text + "\t\t" + C_Method_2.Text + "\n");
                do
                {
                    txt.WriteLine(ResultMatrix1[h, 0] + " , " + ResultMatrix1[h, 1] + " , " + ResultMatrix1[h, 2] + "\t\t" + ResultMatrix2[h, 0] + " , " + ResultMatrix2[h, 1] + " , " + ResultMatrix2[h, 2] + "\n");
                    h++;
                } while (h < i);
                txt.WriteLine("END");
                txt.Close();
            }
            else if (File.Exists(Path))
            {
                File.WriteAllText(Path, String.Empty);
                using (var txt = new StreamWriter(Path, true))
                {
                    txt.WriteLine(C_Method_1.Text + "\t\t" + C_Method_2.Text + "\n");
                    do
                    {
                        txt.WriteLine(ResultMatrix1[h, 0] + " , " + ResultMatrix1[h, 1] + " , " + ResultMatrix1[h, 2] + "\t\t" + ResultMatrix2[h, 0] + " , " + ResultMatrix2[h, 1] + " , " + ResultMatrix2[h, 2] + "\n");
                        h++;
                    } while (h < i);
                    txt.WriteLine("END");
                    txt.Close();
                }
            }
            MessageBox.Show("Finished");
        }
    }
    public class Dictionary
    {

        public static Dictionary<string, double> Length()
        {
            Dictionary<string, double> Length = new Dictionary<string, double>();

            Length.Add("Meters", 1.0 / 0.3048);
            Length.Add("Feet", 1.0);
            Length.Add("Inches", 1.0 / 12.0);
            Length.Add("Yards", 3.0);
            Length.Add("Kilometers", 1.0 / 0.0003048);
            Length.Add("Aggies", 5.5);
            Length.Add("Centimeters", 1.0 / 30.48);
            Length.Add("Bohr Radius", 1.0 / 5759884813.1885);
            Length.Add("Nautical League", 18228.3);

            return Length;
        }

        public static Dictionary<string, double> Rate()
        {
            Dictionary<string, double> Rate = new Dictionary<string, double>();

            Rate.Add("Degrees/100feet", 0.01);
            Rate.Add("Degrees/100Meters", 0.0003048);
            Rate.Add("Radians/100feet", 0.01 * 180.0 / 3.14159265359);
            Rate.Add("Radians/100Meters", 0.0003048 * 180.0 / 3.14159265359);

            return Rate;
        }

        public static Dictionary<string, string> Method()
        {
            Dictionary<string, string> Method = new Dictionary<string, string>();

            Method.Add("Angle Averaging", "1"); 
            Method.Add("Minimum Curvature", "2");
            Method.Add("Tangential", "3");
            Method.Add("Radius of Curvature", "4");
            Method.Add("Improved Angle Averaging Method", "5");
            Method.Add("Secant Method", "6");
            Method.Add("Helical Arc Method", "7");
            Method.Add("Circular Arc Method", "8");
            
            return Method;
        }

        public static Dictionary<string, double> Angle()
        {
            Dictionary<string, double> Angle = new Dictionary<string, double>();

            Angle.Add("Degrees", 1);
            Angle.Add("Radians", 3.14159265359 / 180.0);
            Angle.Add("Compass", 1.0);

            return Angle;
        }
    }
}


