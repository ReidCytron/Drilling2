using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//////  I need a function that will convert degrees to compass //////

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
            double SX = Convert.ToDouble(X_Surface.Text);
            double SY = Convert.ToDouble(Y_Surface.Text);
            double SZ = Convert.ToDouble(Z_Surface.Text);
            double TX = Convert.ToDouble(X_Target.Text);
            double TY = Convert.ToDouble(Y_Target.Text);
            double TZ = Convert.ToDouble(Z_Target.Text);
            double KOP = Convert.ToDouble(KOP_Length.Text);
            double BuildRate = Convert.ToDouble(Build_Rate.Text);
            double DropRate = Convert.ToDouble(Drop_Rate.Text);
            double TurnRate = Convert.ToDouble(Turn_Rate.Text);
            double Increment = Convert.ToDouble(Iteration_Increment.Text);

            double Method = Dictionary.Method()[W_Method.Text];
            double SurfaceUnit = Dictionary.Length()[Surface_Unit.Text];
            double TargetUnit = Dictionary.Length()[Target_Unit.Text];
            double KOPUnit = Dictionary.Length()[KOP_Length_Unit.Text];
            double BuildRateUnit = Dictionary.Rate()[Build_Rate_Unit.Text];
            double DropRateUnit = Dictionary.Rate()[Drop_Rate_Unit.Text];
            double TurnRateUnit = Dictionary.Rate()[Turn_Rate_Unit.Text];
            double IncrementUnit = Dictionary.Length()[Iteration_Increment_Unit.Text];
            double AngleOutUnit = Dictionary.Angle()[Angle_Output_Unit.Text];
            double LengthOutUnit = Dictionary.Length()[Length_Output_Unit.Text];

            double BuildRadius, DropRadius; // Eqns.




            //BuildRadius = (1 / BuildRadius * Dictionary.Rate()[""]);

            double A = Dictionary.Length()["Meters"];
            MessageBox.Show(Convert.ToString(SX));

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
    }

    /// <summary>
    /// Below are the dictionaries for conversions
    /// </summary>

    public class Dictionary
    {
        public static Dictionary<string, double> Length()
        {
            Dictionary<string, double> Length = new Dictionary<string, double>();

            Length.Add("Meters", 0.3048);
            Length.Add("Feet", 1);

            return Length;
        }

        public static Dictionary<string, double> Rate()
        {
            Dictionary<string, double> Rate = new Dictionary<string, double>();

            Rate.Add("Degrees/100feet", 0.01);
            Rate.Add("Degrees/100Meters", 0.003048);

            return Rate;
        }

        public static Dictionary<string, double> Method()
        {
            Dictionary<string, double> Method = new Dictionary<string, double>();

            Method.Add("Angle Averaging", 1);
            Method.Add("Minimum Curvature", 2);
            Method.Add("Tangential", 3);
            Method.Add("Radius of Curvature", 4);

            return Method;
        }

        public static Dictionary<string, double> Angle()
        {
            Dictionary<string, double> Angle = new Dictionary<string, double>();

            Angle.Add("Degrees", 1);
            Angle.Add("Radians", 3.1415926 / 180);
            return Angle;
        }
    }

}
