
using System.Windows.Forms;

namespace COPOL.Forms
{
    partial class DiagrammForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ReactiveX = new System.Windows.Forms.NumericUpDown();
            this.ActiveR = new System.Windows.Forms.NumericUpDown();
            this.Z = new System.Windows.Forms.NumericUpDown();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.DrawUsersPoint = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pMaxOutput = new System.Windows.Forms.NumericUpDown();
            this.Pmax_label = new System.Windows.Forms.Label();
            this.mdB = new System.Windows.Forms.Label();
            this.Pmax = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.BuildButton = new System.Windows.Forms.Button();
            this.SetParametersButton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.CleanButton = new System.Windows.Forms.Button();
            this.OpenFileButton = new System.Windows.Forms.Button();
            this.SmithChart = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ReactiveX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActiveR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Z)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pMaxOutput)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SmithChart)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ReactiveX);
            this.groupBox1.Controls.Add(this.ActiveR);
            this.groupBox1.Controls.Add(this.Z);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.DrawUsersPoint);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(558, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(190, 207);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Точка";
            // 
            // ReactiveX
            // 
            this.ReactiveX.Location = new System.Drawing.Point(44, 147);
            this.ReactiveX.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.ReactiveX.Name = "ReactiveX";
            this.ReactiveX.Size = new System.Drawing.Size(100, 21);
            this.ReactiveX.TabIndex = 31;
            // 
            // ActiveR
            // 
            this.ActiveR.Location = new System.Drawing.Point(43, 94);
            this.ActiveR.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.ActiveR.Name = "ActiveR";
            this.ActiveR.Size = new System.Drawing.Size(101, 21);
            this.ActiveR.TabIndex = 30;
            // 
            // Z
            // 
            this.Z.Location = new System.Drawing.Point(44, 41);
            this.Z.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.Z.Name = "Z";
            this.Z.Size = new System.Drawing.Size(100, 21);
            this.Z.TabIndex = 29;
            this.Z.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(9, 150);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(25, 15);
            this.label19.TabIndex = 28;
            this.label19.Text = "X =";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(10, 97);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(26, 15);
            this.label18.TabIndex = 27;
            this.label18.Text = "R =";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(150, 149);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(25, 15);
            this.label17.TabIndex = 26;
            this.label17.Text = "Ом";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(150, 97);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(25, 15);
            this.label16.TabIndex = 25;
            this.label16.Text = "Ом";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(150, 41);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(25, 15);
            this.label15.TabIndex = 24;
            this.label15.Text = "Ом";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(9, 44);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(31, 15);
            this.label12.TabIndex = 23;
            this.label12.Text = "Zв =";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label13.Location = new System.Drawing.Point(9, 24);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(142, 14);
            this.label13.TabIndex = 20;
            this.label13.Text = "Волновое сопротивление:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label14.Location = new System.Drawing.Point(8, 130);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(152, 14);
            this.label14.TabIndex = 22;
            this.label14.Text = "Реактивное сопротивление:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label20.Location = new System.Drawing.Point(9, 77);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(142, 14);
            this.label20.TabIndex = 21;
            this.label20.Text = "Активное сопротивление:";
            // 
            // DrawUsersPoint
            // 
            this.DrawUsersPoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DrawUsersPoint.Location = new System.Drawing.Point(10, 178);
            this.DrawUsersPoint.Name = "DrawUsersPoint";
            this.DrawUsersPoint.Size = new System.Drawing.Size(174, 23);
            this.DrawUsersPoint.TabIndex = 19;
            this.DrawUsersPoint.Text = "нарисовать точку";
            this.DrawUsersPoint.UseVisualStyleBackColor = true;
            this.DrawUsersPoint.Click += new System.EventHandler(this.DrawUsersPoint_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(126, 150);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(0, 15);
            this.label11.TabIndex = 11;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(127, 132);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(0, 15);
            this.label10.TabIndex = 10;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(128, 65);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(0, 15);
            this.label9.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 150);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 15);
            this.label6.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 132);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 15);
            this.label5.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 15);
            this.label4.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 131);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 15);
            this.label3.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 15);
            this.label2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 15);
            this.label1.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pMaxOutput);
            this.groupBox2.Controls.Add(this.Pmax_label);
            this.groupBox2.Controls.Add(this.mdB);
            this.groupBox2.Controls.Add(this.Pmax);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.BuildButton);
            this.groupBox2.Controls.Add(this.SetParametersButton);
            this.groupBox2.Location = new System.Drawing.Point(558, 220);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(190, 217);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Load-pull";
            // 
            // pMaxOutput
            // 
            this.pMaxOutput.Location = new System.Drawing.Point(63, 184);
            this.pMaxOutput.Name = "pMaxOutput";
            this.pMaxOutput.Size = new System.Drawing.Size(81, 20);
            this.pMaxOutput.TabIndex = 32;
            // 
            // Pmax_label
            // 
            this.Pmax_label.AutoSize = true;
            this.Pmax_label.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Pmax_label.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Pmax_label.Location = new System.Drawing.Point(10, 164);
            this.Pmax_label.Name = "Pmax_label";
            this.Pmax_label.Size = new System.Drawing.Size(116, 14);
            this.Pmax_label.TabIndex = 25;
            this.Pmax_label.Text = "Выходная мощность:";
            // 
            // mdB
            // 
            this.mdB.AutoSize = true;
            this.mdB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.mdB.Location = new System.Drawing.Point(150, 184);
            this.mdB.Name = "mdB";
            this.mdB.Size = new System.Drawing.Size(31, 15);
            this.mdB.TabIndex = 28;
            this.mdB.Text = "дБм";
            // 
            // Pmax
            // 
            this.Pmax.AutoSize = true;
            this.Pmax.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Pmax.Location = new System.Drawing.Point(10, 184);
            this.Pmax.Name = "Pmax";
            this.Pmax.Size = new System.Drawing.Size(52, 15);
            this.Pmax.TabIndex = 27;
            this.Pmax.Text = "Pmax = ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(130, 181);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(0, 13);
            this.label8.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 127);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 13);
            this.label7.TabIndex = 12;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(10, 118);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(174, 31);
            this.button3.TabIndex = 11;
            this.button3.Text = "Сохранить точки";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // BuildButton
            // 
            this.BuildButton.Location = new System.Drawing.Point(11, 69);
            this.BuildButton.Name = "BuildButton";
            this.BuildButton.Size = new System.Drawing.Size(174, 31);
            this.BuildButton.TabIndex = 10;
            this.BuildButton.Text = "Построить";
            this.BuildButton.UseVisualStyleBackColor = true;
            this.BuildButton.Click += new System.EventHandler(this.BuildButton_Click);
            // 
            // SetParametersButton
            // 
            this.SetParametersButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SetParametersButton.Location = new System.Drawing.Point(11, 19);
            this.SetParametersButton.Name = "SetParametersButton";
            this.SetParametersButton.Size = new System.Drawing.Size(174, 31);
            this.SetParametersButton.TabIndex = 9;
            this.SetParametersButton.Text = "Задать параметры";
            this.SetParametersButton.UseVisualStyleBackColor = true;
            this.SetParametersButton.Click += new System.EventHandler(this.SetParametersButton_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.CleanButton);
            this.groupBox3.Controls.Add(this.OpenFileButton);
            this.groupBox3.Location = new System.Drawing.Point(558, 443);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(190, 104);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Действия";
            // 
            // CleanButton
            // 
            this.CleanButton.Location = new System.Drawing.Point(10, 56);
            this.CleanButton.Name = "CleanButton";
            this.CleanButton.Size = new System.Drawing.Size(174, 31);
            this.CleanButton.TabIndex = 13;
            this.CleanButton.Text = "Очистить";
            this.CleanButton.UseVisualStyleBackColor = true;
            this.CleanButton.Click += new System.EventHandler(this.CleanButton_Click);
            // 
            // OpenFileButton
            // 
            this.OpenFileButton.Location = new System.Drawing.Point(10, 19);
            this.OpenFileButton.Name = "OpenFileButton";
            this.OpenFileButton.Size = new System.Drawing.Size(175, 31);
            this.OpenFileButton.TabIndex = 12;
            this.OpenFileButton.Text = "Открыть файл";
            this.OpenFileButton.UseVisualStyleBackColor = true;
            this.OpenFileButton.Click += new System.EventHandler(this.OpenFileButton_Click);
            // 
            // SmithChart
            // 
            this.SmithChart.BackColor = System.Drawing.Color.White;
            this.SmithChart.Location = new System.Drawing.Point(12, 7);
            this.SmithChart.Name = "SmithChart";
            this.SmithChart.Size = new System.Drawing.Size(540, 540);
            this.SmithChart.TabIndex = 11;
            this.SmithChart.TabStop = false;
            this.SmithChart.Paint += new System.Windows.Forms.PaintEventHandler(this.SmithChart_Paint);
            // 
            // DiagrammForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(758, 559);
            this.Controls.Add(this.SmithChart);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Location = new System.Drawing.Point(15, 15);
            this.Name = "DiagrammForm";
            this.Text = "Диаграмма Смита";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ReactiveX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActiveR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Z)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pMaxOutput)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SmithChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button BuildButton;
        private System.Windows.Forms.Button SetParametersButton;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button CleanButton;
        private System.Windows.Forms.Button OpenFileButton;
        private System.Windows.Forms.PictureBox SmithChart;
        private NumericUpDown ReactiveX;
        private NumericUpDown ActiveR;
        private NumericUpDown Z;
        private Label label19;
        private Label label18;
        private Label label17;
        private Label label16;
        private Label label15;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label20;
        private Button DrawUsersPoint;
        private NumericUpDown pMaxOutput;
        private Label Pmax_label;
        private Label mdB;
        private Label Pmax;
    }
}