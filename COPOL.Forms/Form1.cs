using System;
using System.Drawing;
using System.Windows.Forms;
using COPOL.BLL;

namespace COPOL.Forms
{
    public partial class DiagrammForm : System.Windows.Forms.Form
    {
        private readonly SmithChart _smithChart = new SmithChart();
            
        // Центры горизонтальных окружностей. 
        private static readonly float[] _baseArg1 = new float[] 
        {
            0.0f, 0.1f, 0.2f, 0.3f, 0.4f, 0.5f, 0.6f, 0.7f,
            0.8f, 0.9f, 1.0f, 1.2f, 1.4f, 1.6f, 1.8f, 
            2.0f, 3.0f, 4.0f, 5.0f, 10.0f, 20.0f, 50.0f 
        };
        
        // Центры Вертикальных окружностей. 
        private static readonly float[] _baseArg2 = new float[] 
        { 
            0.2f, 0.4f, 0.6f, 0.8f, 1.0f,
            1.2f, 1.4f, 1.6f, 1.8f, 2.0f,
            3.0f, 4.0f, 5.0f, 10.0f, 20.0f, 50.0f 
        };

        private float[] _arg1 = _baseArg1;
        private float[] _arg2 = _baseArg2;

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

        private void SmithChart_Paint(object sender, PaintEventArgs e)
        {
            float xCross; 
            float yCross;
            
            var color = Color.DarkGray; 
            var userPen = new Pen(color);

            // Draw axises
            _smithChart.AxesCount = 3;
            _smithChart.DrawAxises(e.Graphics);

            // Draw circles of type1
            for (var i = 0; i < _arg1.Length; i++)
            {
                if (i == 0)
                {
                    var userPen1 = new Pen(Color.Black, 3);
                    _smithChart.DrawCircleType1(e.Graphics, userPen1, _arg1[i]);
                }
                else
                {
                    _smithChart.DrawCircleType1(e.Graphics, userPen, _arg1[i]);
                }
                
                xCross = (_arg1[i] - 1) / (_arg1[i] + 1);
                yCross = 0;
                
                _smithChart.DrawText(
                    e.Graphics,
                    Color.Blue,
                    new Font(
                        "Arial",
                        7,
                        FontStyle.Bold),
                    Math.Round((float)_arg1[i], 2).ToString(),
                    xCross,
                    yCross);
            }

            foreach (var arg in _arg2)
            {
                _smithChart.DrawCircleType2(e.Graphics, userPen, arg);
                _smithChart.DrawCircleType2(e.Graphics, userPen, -arg);

                xCross = ((arg * arg) - 1) / ((arg * arg) + 1);
                yCross = (2 * arg) / ((arg * arg) + 1);
                
                _smithChart.DrawText(
                    e.Graphics, Color.Blue,
                    new Font("Arial", 7, FontStyle.Bold),
                    Math.Round((float)arg, 2).ToString(),
                    xCross,
                    yCross);
                
                _smithChart.DrawText(
                    e.Graphics,
                    Color.Blue,
                    new Font("Arial", 7, FontStyle.Bold),
                    Math.Round((float)-arg, 2).ToString(),
                    xCross,
                    -yCross);
            }
            
            //btnBack.Enabled = false;
        }
    }
}
