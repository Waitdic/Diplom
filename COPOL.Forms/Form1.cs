using System;
using System.Drawing;
using COPOL.BLL;

namespace COPOL.Forms
{
    public partial class DiagrammForm : System.Windows.Forms.Form
    {
        private SmithChart _smithChart;

        public DiagrammForm()
        {
            InitializeComponent();
        }

        private void DiagrammForm_Load(object sender, EventArgs e)
        {

        }

        private void SetParametersButton_Click(object sender, EventArgs e)
        {

        }

        private void SmithChart_Paint(object sender, EventArgs e)
        {
            float X_cross, Y_cross;
            Color color = Color.DarkGray;  // black color
            Pen userPen = new Pen(color);

            // Draw axises
            smithChart.AxesCount = 3;
            smithChart.DrawAxises(e.Graphics);

            // Draw circles of type1
            for (int i = 0; i < arg1.Length; i++)
            {
                if (i == 0)
                {
                    Pen userPen1 = new Pen(Color.Black, 3);
                    smithChart.DrawCircleType1(e.Graphics, userPen1, arg1[i]);
                }
                else
                {
                    smithChart.DrawCircleType1(e.Graphics, userPen, arg1[i]);
                }
                X_cross = (arg1[i] - 1) / (arg1[i] + 1);
                Y_cross = 0;
                smithChart.DrawText(e.Graphics, Color.Blue, new Font("Arial", 7, FontStyle.Bold), Convert.ToString((float)Math.Round(arg1[i], 2)), X_cross, Y_cross);
            }

            for (int i = 0; i <= arg2.Length - 1; i++)
            {
                smithChart.DrawCircleType2(e.Graphics, userPen, arg2[i]);
                smithChart.DrawCircleType2(e.Graphics, userPen, -arg2[i]);

                X_cross = ((arg2[i] * arg2[i]) - 1) / ((arg2[i] * arg2[i]) + 1);
                Y_cross = (2 * arg2[i]) / ((arg2[i] * arg2[i]) + 1);
                smithChart.DrawText(e.Graphics, Color.Blue, new Font("Arial", 7, FontStyle.Bold), Convert.ToString((float)Math.Round(arg2[i], 2)), X_cross, Y_cross);
                smithChart.DrawText(e.Graphics, Color.Blue, new Font("Arial", 7, FontStyle.Bold), Convert.ToString((float)Math.Round(-arg2[i], 2)), X_cross, -Y_cross);
            }
            btnBack.Enabled = false;
        }
    }
}
