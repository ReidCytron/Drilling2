namespace Drilling2
{
    partial class WellboreTrajectoryForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.W_Method = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.KOP_Length_Unit = new System.Windows.Forms.ComboBox();
            this.Build_Rate_Unit = new System.Windows.Forms.ComboBox();
            this.Drop_Rate_Unit = new System.Windows.Forms.ComboBox();
            this.Tolerance_Unit = new System.Windows.Forms.ComboBox();
            this.Turn_Rate_Unit = new System.Windows.Forms.ComboBox();
            this.Iteration_Increment_Unit = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.Angle_Output_Unit = new System.Windows.Forms.ComboBox();
            this.Length_Output_Unit = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.Surface_Unit = new System.Windows.Forms.ComboBox();
            this.Target_Unit = new System.Windows.Forms.ComboBox();
            this.X_Surface = new System.Windows.Forms.TextBox();
            this.X_Target = new System.Windows.Forms.TextBox();
            this.Y_Surface = new System.Windows.Forms.TextBox();
            this.Y_Target = new System.Windows.Forms.TextBox();
            this.Z_Surface = new System.Windows.Forms.TextBox();
            this.Z_Target = new System.Windows.Forms.TextBox();
            this.KOP_Length = new System.Windows.Forms.TextBox();
            this.Build_Rate = new System.Windows.Forms.TextBox();
            this.Drop_Rate = new System.Windows.Forms.TextBox();
            this.Tolerance = new System.Windows.Forms.TextBox();
            this.Turn_Rate = new System.Windows.Forms.TextBox();
            this.Iteration_Increment = new System.Windows.Forms.TextBox();
            this.Wellbore_Trajectory_Button = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.Save_File_WT = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.Survey_Button = new System.Windows.Forms.Button();
            this.Save_Button_WT = new System.Windows.Forms.Button();
            this.Survey_File_Path = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.Survey_File = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.C_Method_1 = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.C_Method_2 = new System.Windows.Forms.ComboBox();
            this.Compare_Button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // W_Method
            // 
            this.W_Method.FormattingEnabled = true;
            this.W_Method.Items.AddRange(new object[] {
            "Angle Averaging",
            "Minimum Curvature",
            "Tangential",
            "Radius of Curvature",
            "Improved Angle Averaging Method",
            "Secant Method",
            "Helical Arc Method",
            "Circular Arc Method"});
            this.W_Method.Location = new System.Drawing.Point(203, 12);
            this.W_Method.Name = "W_Method";
            this.W_Method.Size = new System.Drawing.Size(182, 21);
            this.W_Method.TabIndex = 0;
            this.W_Method.Text = "Tangential";
            this.W_Method.SelectedIndexChanged += new System.EventHandler(this.W_Method_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(156, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "X";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(240, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Y";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(325, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Z";
            this.label3.Click += new System.EventHandler(this.Z_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Surface Coordinates";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Target Coordinates";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(182, 129);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Value";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(281, 129);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Units";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 158);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "KOP";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 187);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 13);
            this.label9.TabIndex = 9;
            this.label9.Text = "Build Rate";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 217);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 13);
            this.label10.TabIndex = 10;
            this.label10.Text = "Drop Rate";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 248);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(55, 13);
            this.label11.TabIndex = 11;
            this.label11.Text = "Tolerance";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(13, 275);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(55, 13);
            this.label12.TabIndex = 12;
            this.label12.Text = "Turn Rate";
            this.label12.Click += new System.EventHandler(this.label12_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(13, 303);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(95, 13);
            this.label13.TabIndex = 13;
            this.label13.Text = "Iteration Increment";
            this.label13.Click += new System.EventHandler(this.label13_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(158, 337);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(99, 13);
            this.label14.TabIndex = 14;
            this.label14.Text = "Select Output Units";
            this.label14.Click += new System.EventHandler(this.label14_Click);
            // 
            // KOP_Length_Unit
            // 
            this.KOP_Length_Unit.FormattingEnabled = true;
            this.KOP_Length_Unit.Items.AddRange(new object[] {
            "Aggies",
            "Bohr Radius",
            "Feet",
            "Inches",
            "Kilometers",
            "Meters",
            "Yards",
            "Centimeters",
            "Nautical League"});
            this.KOP_Length_Unit.Location = new System.Drawing.Point(284, 155);
            this.KOP_Length_Unit.Name = "KOP_Length_Unit";
            this.KOP_Length_Unit.Size = new System.Drawing.Size(121, 21);
            this.KOP_Length_Unit.TabIndex = 15;
            this.KOP_Length_Unit.Text = "Feet";
            // 
            // Build_Rate_Unit
            // 
            this.Build_Rate_Unit.FormattingEnabled = true;
            this.Build_Rate_Unit.Items.AddRange(new object[] {
            "Degrees/100feet",
            "Degrees/100Meters",
            "Radians/100feet",
            "Radians/100Meters"});
            this.Build_Rate_Unit.Location = new System.Drawing.Point(284, 184);
            this.Build_Rate_Unit.Name = "Build_Rate_Unit";
            this.Build_Rate_Unit.Size = new System.Drawing.Size(121, 21);
            this.Build_Rate_Unit.TabIndex = 16;
            this.Build_Rate_Unit.Text = "Degrees/100feet";
            this.Build_Rate_Unit.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // Drop_Rate_Unit
            // 
            this.Drop_Rate_Unit.FormattingEnabled = true;
            this.Drop_Rate_Unit.Items.AddRange(new object[] {
            "Degrees/100feet",
            "Degrees/100Meters",
            "Radians/100feet",
            "Radians/100Meters"});
            this.Drop_Rate_Unit.Location = new System.Drawing.Point(284, 214);
            this.Drop_Rate_Unit.Name = "Drop_Rate_Unit";
            this.Drop_Rate_Unit.Size = new System.Drawing.Size(121, 21);
            this.Drop_Rate_Unit.TabIndex = 17;
            this.Drop_Rate_Unit.Text = "Degrees/100feet";
            // 
            // Tolerance_Unit
            // 
            this.Tolerance_Unit.FormattingEnabled = true;
            this.Tolerance_Unit.Items.AddRange(new object[] {
            "Aggies",
            "Bohr Radius",
            "Feet",
            "Inches",
            "Kilometers",
            "Meters",
            "Yards",
            "Centimeters",
            "Nautical League"});
            this.Tolerance_Unit.Location = new System.Drawing.Point(284, 245);
            this.Tolerance_Unit.Name = "Tolerance_Unit";
            this.Tolerance_Unit.Size = new System.Drawing.Size(121, 21);
            this.Tolerance_Unit.TabIndex = 18;
            this.Tolerance_Unit.Text = "Feet";
            // 
            // Turn_Rate_Unit
            // 
            this.Turn_Rate_Unit.FormattingEnabled = true;
            this.Turn_Rate_Unit.Items.AddRange(new object[] {
            "Degrees/100feet",
            "Degrees/100Meters",
            "Radians/100feet",
            "Radians/100Meters"});
            this.Turn_Rate_Unit.Location = new System.Drawing.Point(284, 272);
            this.Turn_Rate_Unit.Name = "Turn_Rate_Unit";
            this.Turn_Rate_Unit.Size = new System.Drawing.Size(121, 21);
            this.Turn_Rate_Unit.TabIndex = 19;
            this.Turn_Rate_Unit.Text = "Degrees/100feet";
            // 
            // Iteration_Increment_Unit
            // 
            this.Iteration_Increment_Unit.FormattingEnabled = true;
            this.Iteration_Increment_Unit.Items.AddRange(new object[] {
            "Aggies",
            "Bohr Radius",
            "Feet",
            "Inches",
            "Kilometers",
            "Meters",
            "Yards",
            "Centimeters",
            "Nautical League"});
            this.Iteration_Increment_Unit.Location = new System.Drawing.Point(284, 300);
            this.Iteration_Increment_Unit.Name = "Iteration_Increment_Unit";
            this.Iteration_Increment_Unit.Size = new System.Drawing.Size(121, 21);
            this.Iteration_Increment_Unit.TabIndex = 20;
            this.Iteration_Increment_Unit.Text = "Feet";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(15, 363);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(34, 13);
            this.label15.TabIndex = 21;
            this.label15.Text = "Angle";
            this.label15.Click += new System.EventHandler(this.label15_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(12, 396);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(40, 13);
            this.label16.TabIndex = 22;
            this.label16.Text = "Length";
            // 
            // Angle_Output_Unit
            // 
            this.Angle_Output_Unit.FormattingEnabled = true;
            this.Angle_Output_Unit.Items.AddRange(new object[] {
            "Degrees",
            "Radians",
            "Compass"});
            this.Angle_Output_Unit.Location = new System.Drawing.Point(284, 360);
            this.Angle_Output_Unit.Name = "Angle_Output_Unit";
            this.Angle_Output_Unit.Size = new System.Drawing.Size(121, 21);
            this.Angle_Output_Unit.TabIndex = 23;
            this.Angle_Output_Unit.Text = "Compass";
            this.Angle_Output_Unit.SelectedIndexChanged += new System.EventHandler(this.Angle_Output_Unit_SelectedIndexChanged);
            // 
            // Length_Output_Unit
            // 
            this.Length_Output_Unit.FormattingEnabled = true;
            this.Length_Output_Unit.Items.AddRange(new object[] {
            "Aggies",
            "Bohr Radius",
            "Feet",
            "Inches",
            "Kilometers",
            "Meters",
            "Yards",
            "Centimeters",
            "Nautical League"});
            this.Length_Output_Unit.Location = new System.Drawing.Point(284, 393);
            this.Length_Output_Unit.Name = "Length_Output_Unit";
            this.Length_Output_Unit.Size = new System.Drawing.Size(121, 21);
            this.Length_Output_Unit.TabIndex = 24;
            this.Length_Output_Unit.Text = "Feet";
            this.Length_Output_Unit.SelectedIndexChanged += new System.EventHandler(this.Length_Output_Unit_SelectedIndexChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(406, 39);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(31, 13);
            this.label17.TabIndex = 25;
            this.label17.Text = "Units";
            // 
            // Surface_Unit
            // 
            this.Surface_Unit.FormattingEnabled = true;
            this.Surface_Unit.Items.AddRange(new object[] {
            "Aggies",
            "Bohr Radius",
            "Feet",
            "Inches",
            "Kilometers",
            "Meters",
            "Yards",
            "Centimeters",
            "Nautical League"});
            this.Surface_Unit.Location = new System.Drawing.Point(408, 58);
            this.Surface_Unit.Name = "Surface_Unit";
            this.Surface_Unit.Size = new System.Drawing.Size(106, 21);
            this.Surface_Unit.TabIndex = 26;
            this.Surface_Unit.Text = "Feet";
            this.Surface_Unit.SelectedIndexChanged += new System.EventHandler(this.comboBox9_SelectedIndexChanged);
            // 
            // Target_Unit
            // 
            this.Target_Unit.FormattingEnabled = true;
            this.Target_Unit.Items.AddRange(new object[] {
            "Aggies",
            "Bohr Radius",
            "Feet",
            "Inches",
            "Kilometers",
            "Meters",
            "Yards",
            "Centimeters",
            "Nautical League"});
            this.Target_Unit.Location = new System.Drawing.Point(408, 93);
            this.Target_Unit.Name = "Target_Unit";
            this.Target_Unit.Size = new System.Drawing.Size(106, 21);
            this.Target_Unit.TabIndex = 27;
            this.Target_Unit.Text = "Feet";
            this.Target_Unit.SelectedIndexChanged += new System.EventHandler(this.comboBox10_SelectedIndexChanged);
            // 
            // X_Surface
            // 
            this.X_Surface.Location = new System.Drawing.Point(158, 58);
            this.X_Surface.Name = "X_Surface";
            this.X_Surface.Size = new System.Drawing.Size(58, 20);
            this.X_Surface.TabIndex = 28;
            this.X_Surface.Text = "0.0";
            this.X_Surface.TextChanged += new System.EventHandler(this.X_Surface_TextChanged);
            // 
            // X_Target
            // 
            this.X_Target.Location = new System.Drawing.Point(158, 96);
            this.X_Target.Name = "X_Target";
            this.X_Target.Size = new System.Drawing.Size(58, 20);
            this.X_Target.TabIndex = 29;
            this.X_Target.Text = "500.0";
            // 
            // Y_Surface
            // 
            this.Y_Surface.Location = new System.Drawing.Point(242, 58);
            this.Y_Surface.Name = "Y_Surface";
            this.Y_Surface.Size = new System.Drawing.Size(58, 20);
            this.Y_Surface.TabIndex = 30;
            this.Y_Surface.Text = "0.0";
            // 
            // Y_Target
            // 
            this.Y_Target.Location = new System.Drawing.Point(242, 96);
            this.Y_Target.Name = "Y_Target";
            this.Y_Target.Size = new System.Drawing.Size(58, 20);
            this.Y_Target.TabIndex = 31;
            this.Y_Target.Text = "500.0";
            this.Y_Target.TextChanged += new System.EventHandler(this.Y_Target_TextChanged);
            // 
            // Z_Surface
            // 
            this.Z_Surface.Location = new System.Drawing.Point(327, 58);
            this.Z_Surface.Name = "Z_Surface";
            this.Z_Surface.Size = new System.Drawing.Size(58, 20);
            this.Z_Surface.TabIndex = 32;
            this.Z_Surface.Text = "0.0";
            // 
            // Z_Target
            // 
            this.Z_Target.Location = new System.Drawing.Point(327, 96);
            this.Z_Target.Name = "Z_Target";
            this.Z_Target.Size = new System.Drawing.Size(58, 20);
            this.Z_Target.TabIndex = 33;
            this.Z_Target.Text = "8000.0";
            this.Z_Target.TextChanged += new System.EventHandler(this.Z_Target_TextChanged);
            // 
            // KOP_Length
            // 
            this.KOP_Length.Location = new System.Drawing.Point(158, 155);
            this.KOP_Length.Name = "KOP_Length";
            this.KOP_Length.Size = new System.Drawing.Size(100, 20);
            this.KOP_Length.TabIndex = 34;
            this.KOP_Length.Text = "1000.0";
            // 
            // Build_Rate
            // 
            this.Build_Rate.Location = new System.Drawing.Point(157, 184);
            this.Build_Rate.Name = "Build_Rate";
            this.Build_Rate.Size = new System.Drawing.Size(100, 20);
            this.Build_Rate.TabIndex = 35;
            this.Build_Rate.Text = "1.0";
            this.Build_Rate.TextChanged += new System.EventHandler(this.Build_Rate_TextChanged);
            // 
            // Drop_Rate
            // 
            this.Drop_Rate.Location = new System.Drawing.Point(157, 214);
            this.Drop_Rate.Name = "Drop_Rate";
            this.Drop_Rate.Size = new System.Drawing.Size(100, 20);
            this.Drop_Rate.TabIndex = 36;
            this.Drop_Rate.Text = "1.0";
            // 
            // Tolerance
            // 
            this.Tolerance.Location = new System.Drawing.Point(157, 245);
            this.Tolerance.Name = "Tolerance";
            this.Tolerance.Size = new System.Drawing.Size(100, 20);
            this.Tolerance.TabIndex = 37;
            this.Tolerance.Text = "100.0";
            // 
            // Turn_Rate
            // 
            this.Turn_Rate.Location = new System.Drawing.Point(157, 272);
            this.Turn_Rate.Name = "Turn_Rate";
            this.Turn_Rate.Size = new System.Drawing.Size(100, 20);
            this.Turn_Rate.TabIndex = 38;
            this.Turn_Rate.Text = "0.1";
            // 
            // Iteration_Increment
            // 
            this.Iteration_Increment.Location = new System.Drawing.Point(157, 300);
            this.Iteration_Increment.Name = "Iteration_Increment";
            this.Iteration_Increment.Size = new System.Drawing.Size(100, 20);
            this.Iteration_Increment.TabIndex = 39;
            this.Iteration_Increment.Text = "0.1";
            // 
            // Wellbore_Trajectory_Button
            // 
            this.Wellbore_Trajectory_Button.Location = new System.Drawing.Point(203, 468);
            this.Wellbore_Trajectory_Button.Name = "Wellbore_Trajectory_Button";
            this.Wellbore_Trajectory_Button.Size = new System.Drawing.Size(158, 23);
            this.Wellbore_Trajectory_Button.TabIndex = 40;
            this.Wellbore_Trajectory_Button.Text = "Wellbore Trajectory Simulation";
            this.Wellbore_Trajectory_Button.UseVisualStyleBackColor = true;
            this.Wellbore_Trajectory_Button.Click += new System.EventHandler(this.Done_Button_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(12, 15);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(138, 13);
            this.label18.TabIndex = 41;
            this.label18.Text = "Select Discritization Method";
            // 
            // Save_File_WT
            // 
            this.Save_File_WT.Location = new System.Drawing.Point(284, 431);
            this.Save_File_WT.Name = "Save_File_WT";
            this.Save_File_WT.ReadOnly = true;
            this.Save_File_WT.Size = new System.Drawing.Size(230, 20);
            this.Save_File_WT.TabIndex = 43;
            this.Save_File_WT.Text = "C:\\Users\\rbc658\\Desktop";
            this.Save_File_WT.TextChanged += new System.EventHandler(this.Save_File_Dialogue_TextChanged);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(13, 434);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(95, 13);
            this.label19.TabIndex = 44;
            this.label19.Text = "Save File Location";
            this.label19.Click += new System.EventHandler(this.label19_Click);
            // 
            // Survey_Button
            // 
            this.Survey_Button.Location = new System.Drawing.Point(203, 546);
            this.Survey_Button.Name = "Survey_Button";
            this.Survey_Button.Size = new System.Drawing.Size(156, 23);
            this.Survey_Button.TabIndex = 45;
            this.Survey_Button.Text = "Survey Simulation";
            this.Survey_Button.UseVisualStyleBackColor = true;
            this.Survey_Button.Click += new System.EventHandler(this.button1_Click);
            // 
            // Save_Button_WT
            // 
            this.Save_Button_WT.Location = new System.Drawing.Point(159, 429);
            this.Save_Button_WT.Name = "Save_Button_WT";
            this.Save_Button_WT.Size = new System.Drawing.Size(100, 23);
            this.Save_Button_WT.TabIndex = 46;
            this.Save_Button_WT.Text = "Save Path";
            this.Save_Button_WT.UseVisualStyleBackColor = true;
            this.Save_Button_WT.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // Survey_File_Path
            // 
            this.Survey_File_Path.Location = new System.Drawing.Point(159, 509);
            this.Survey_File_Path.Name = "Survey_File_Path";
            this.Survey_File_Path.Size = new System.Drawing.Size(99, 23);
            this.Survey_File_Path.TabIndex = 47;
            this.Survey_File_Path.Text = "Survey File Path";
            this.Survey_File_Path.UseVisualStyleBackColor = true;
            this.Survey_File_Path.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(13, 514);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(40, 13);
            this.label21.TabIndex = 49;
            this.label21.Text = "Survey";
            // 
            // Survey_File
            // 
            this.Survey_File.Location = new System.Drawing.Point(284, 511);
            this.Survey_File.Name = "Survey_File";
            this.Survey_File.ReadOnly = true;
            this.Survey_File.Size = new System.Drawing.Size(224, 20);
            this.Survey_File.TabIndex = 50;
            this.Survey_File.TextChanged += new System.EventHandler(this.SurveyFile_TextChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(15, 581);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(62, 13);
            this.label20.TabIndex = 51;
            this.label20.Text = "Comparison";
            this.label20.Click += new System.EventHandler(this.label20_Click);
            // 
            // C_Method_1
            // 
            this.C_Method_1.FormattingEnabled = true;
            this.C_Method_1.Items.AddRange(new object[] {
            "Angle Averaging",
            "Minimum Curvature",
            "Tangential",
            "Radius of Curvature",
            "Improved Angle Averaging Method",
            "Secant Method",
            "Helical Arc Method",
            "Circular Arc Method"});
            this.C_Method_1.Location = new System.Drawing.Point(284, 578);
            this.C_Method_1.Name = "C_Method_1";
            this.C_Method_1.Size = new System.Drawing.Size(180, 21);
            this.C_Method_1.TabIndex = 52;
            this.C_Method_1.Text = "Tangential";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(158, 578);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(52, 13);
            this.label22.TabIndex = 53;
            this.label22.Text = "Method 1";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(158, 608);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(52, 13);
            this.label23.TabIndex = 54;
            this.label23.Text = "Method 2";
            this.label23.Click += new System.EventHandler(this.label23_Click);
            // 
            // C_Method_2
            // 
            this.C_Method_2.FormattingEnabled = true;
            this.C_Method_2.Items.AddRange(new object[] {
            "Angle Averaging",
            "Minimum Curvature",
            "Tangential",
            "Radius of Curvature",
            "Improved Angle Averaging Method",
            "Secant Method",
            "Helical Arc Method",
            "Circular Arc Method"});
            this.C_Method_2.Location = new System.Drawing.Point(284, 605);
            this.C_Method_2.Name = "C_Method_2";
            this.C_Method_2.Size = new System.Drawing.Size(180, 21);
            this.C_Method_2.TabIndex = 55;
            this.C_Method_2.Text = "Angle Averaging";
            // 
            // Compare_Button
            // 
            this.Compare_Button.Location = new System.Drawing.Point(203, 632);
            this.Compare_Button.Name = "Compare_Button";
            this.Compare_Button.Size = new System.Drawing.Size(156, 23);
            this.Compare_Button.TabIndex = 56;
            this.Compare_Button.Text = "Compare Methods";
            this.Compare_Button.UseVisualStyleBackColor = true;
            this.Compare_Button.Click += new System.EventHandler(this.Compare_Button_Click);
            // 
            // WellboreTrajectoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 671);
            this.Controls.Add(this.Compare_Button);
            this.Controls.Add(this.C_Method_2);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.C_Method_1);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.Survey_File);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.Survey_File_Path);
            this.Controls.Add(this.Save_Button_WT);
            this.Controls.Add(this.Survey_Button);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.Save_File_WT);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.Wellbore_Trajectory_Button);
            this.Controls.Add(this.Iteration_Increment);
            this.Controls.Add(this.Turn_Rate);
            this.Controls.Add(this.Tolerance);
            this.Controls.Add(this.Drop_Rate);
            this.Controls.Add(this.Build_Rate);
            this.Controls.Add(this.KOP_Length);
            this.Controls.Add(this.Z_Target);
            this.Controls.Add(this.Z_Surface);
            this.Controls.Add(this.Y_Target);
            this.Controls.Add(this.Y_Surface);
            this.Controls.Add(this.X_Target);
            this.Controls.Add(this.X_Surface);
            this.Controls.Add(this.Target_Unit);
            this.Controls.Add(this.Surface_Unit);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.Length_Output_Unit);
            this.Controls.Add(this.Angle_Output_Unit);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.Iteration_Increment_Unit);
            this.Controls.Add(this.Turn_Rate_Unit);
            this.Controls.Add(this.Tolerance_Unit);
            this.Controls.Add(this.Drop_Rate_Unit);
            this.Controls.Add(this.Build_Rate_Unit);
            this.Controls.Add(this.KOP_Length_Unit);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.W_Method);
            this.Name = "WellboreTrajectoryForm";
            this.Text = "Wellbore Trajectory";
            this.Load += new System.EventHandler(this.WellboreTrajectoryForm_Load);
            this.Click += new System.EventHandler(this.WellboreTrajectoryForm_Click);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox W_Method;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox KOP_Length_Unit;
        private System.Windows.Forms.ComboBox Build_Rate_Unit;
        private System.Windows.Forms.ComboBox Drop_Rate_Unit;
        private System.Windows.Forms.ComboBox Tolerance_Unit;
        private System.Windows.Forms.ComboBox Turn_Rate_Unit;
        private System.Windows.Forms.ComboBox Iteration_Increment_Unit;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox Angle_Output_Unit;
        private System.Windows.Forms.ComboBox Length_Output_Unit;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox Surface_Unit;
        private System.Windows.Forms.ComboBox Target_Unit;
        private System.Windows.Forms.TextBox X_Surface;
        private System.Windows.Forms.TextBox X_Target;
        private System.Windows.Forms.TextBox Y_Surface;
        private System.Windows.Forms.TextBox Y_Target;
        private System.Windows.Forms.TextBox Z_Surface;
        private System.Windows.Forms.TextBox Z_Target;
        private System.Windows.Forms.TextBox KOP_Length;
        private System.Windows.Forms.TextBox Build_Rate;
        private System.Windows.Forms.TextBox Drop_Rate;
        private System.Windows.Forms.TextBox Tolerance;
        private System.Windows.Forms.TextBox Turn_Rate;
        private System.Windows.Forms.TextBox Iteration_Increment;
        private System.Windows.Forms.Button Wellbore_Trajectory_Button;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox Save_File_WT;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button Survey_Button;
        private System.Windows.Forms.Button Save_Button_WT;
        private System.Windows.Forms.Button Survey_File_Path;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox Survey_File;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ComboBox C_Method_1;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.ComboBox C_Method_2;
        private System.Windows.Forms.Button Compare_Button;

    }
}

