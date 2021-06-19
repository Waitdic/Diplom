using System;
using System.Drawing;
using System.Windows.Forms;
using COPOL.BLL;
using COPOL.BLL.Models;

namespace COPOL.Forms
{
    public partial class DiagrammForm : Form
    {
        private readonly SmithChart _smithChart = new SmithChart();
        private Parameters _parameters;

        // Центры горизонтальных окружностей. 
        private static readonly float[] BaseArg1 = {
            0.0f, 0.2f, 0.4f, 0.6f,
            0.8f, 1.0f, 1.4f, 1.8f, 
            2.2f, 3.0f, 4.0f, 5.0f, 10.0f
        };
        
        // Центры Вертикальных окружностей. 
        private static readonly float[] BaseArg2 = { 
            0.2f, 0.4f, 0.6f, 0.8f, 1.0f,
            1.2f, 1.4f, 1.6f, 1.9f, 2.3f,
            3.0f, 4.0f, 5.0f, 10.0f
        };

        private float[] _arg1 = BaseArg1;
        private float[] _arg2 = BaseArg2;

        /// <summary>
        /// Initialize Form1.
        /// </summary>
        public DiagrammForm()
        {
            InitializeComponent();
        }
        
        private void SetParametersButton_Click(object sender, EventArgs e)
        {
            var dataForm = new InitialDataForm {Visible = true};
            dataForm.SetParameters(_parameters);
        }

        /// <summary>
        /// Построение диаграммы Смита.
        /// </summary>
        private void SmithChart_Paint(object sender, PaintEventArgs e)
        {
            float xCross; 
            float yCross;
            var userPen = new Pen(Color.DarkGray);

            // TODO : Подумать над тем чтобы не добавлять их
            // Draw axises
            /*_smithChart.AxesCount = 3;
            _smithChart.DrawAxises(e.Graphics);*/

            // Рисуем основную окружность.
            for (var i = 0; i < _arg1.Length; i++)
            {
                _smithChart.DrawCircleType1(
                    e.Graphics,
                    i == 0 ? new Pen(Color.Black, 3) : userPen,
                    _arg1[i]);

                xCross = (_arg1[i] - 1) / (_arg1[i] + 1);
                yCross = 0;

                _smithChart.DrawText(
                    e.Graphics,
                    Color.Blue,
                    new Font(
                        "Arial",
                        7,
                        FontStyle.Bold),
                    Math.Round(_arg1[i], 2).ToString(),
                    xCross,
                    yCross);
            }

            // Рисуем сетку диграммы Смита.
            foreach (var arg in _arg2)
            {
                _smithChart.DrawCircleType2(e.Graphics, userPen, arg);
                _smithChart.DrawCircleType2(e.Graphics, userPen, -arg);
                
                xCross = ((arg * arg) - 1) / ((arg * arg) + 1);
                yCross = (2 * arg) / ((arg * arg) + 1);

                _smithChart.DrawText(
                    e.Graphics, Color.Blue,
                    new Font("Arial", 7, FontStyle.Bold),
                    Math.Round(arg, 2).ToString(),
                    xCross,
                    yCross);
                
                _smithChart.DrawText(
                    e.Graphics,
                    Color.Blue,
                    new Font("Arial", 7, FontStyle.Bold),
                    Math.Round(-arg, 2).ToString(),
                    xCross,
                    -yCross);
            }
            //btnBack.Enabled = false;
        }

        private void BuildButton_Click(object sender, EventArgs e)
        {
            if (_parameters == null)
            {
                MessageBox.Show("Параметры для транзистора не были заполнены.", "Ошибка!");
                return;
            }

            try
            {
                //задание входных параметров транзистора
                var parameters = new Parameters
                {
                    Vds0 = _parameters.Vds0,
                    Ids0 = (float) (_parameters.Ids0 * Math.Pow(10, -3)),
                    Cgs = (float) (_parameters.Cgs * Math.Pow(10, -12)),
                    Cds = (float) (_parameters.Cds * Math.Pow(10, -12)),
                    Cgd = (float) (_parameters.Cgd * Math.Pow(10, -12)),
                    Ls = (float) (_parameters.Ls * Math.Pow(10, -9)),
                    Ld = (float) (_parameters.Ld * Math.Pow(10, -9)),
                    Gm = (float) (_parameters.Gm * Math.Pow(10, -3)),
                    Frequences = _parameters.Frequences,
                    LoopP = _parameters.LoopP,
                    N = _parameters.N,
                    Step = _parameters.Step,
                };

                // Создаем объект класса LoadPull и передаем параметры для расчета.
                var z0 = Z.Value == 0 ? 50 : (float) Z.Value;
                var builder = new LoadPull(
                    parameters,
                    z0);

                // Рассчитываем точки контуров. 
                var outputPoint = builder.CalculatePoint();
                pMaxOutput.Value = (decimal)Math.Round(builder.PMaxOutput, 2);
            
                // Рисуем контура.
                DrawManager.DrawContours(
                    outputPoint,
                    this.SmithChart.CreateGraphics(),
                    _smithChart,
                    builder,
                    z0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка.");
            }
            
        }

        private void DrawUsersPoint_Click(object sender, EventArgs e)
        {
            //вот тут, скорее всего, неправильно создается графический объект
            var objGraphics = SmithChart.CreateGraphics();

            // перерисуем диаграмму для нового значения волнового сопротивления
            for (var i = 0; i < _arg1.Length; ++i)
            {
                _arg1[i] = (BaseArg1[i] * 50) / (float)Z.Value;
            }

            for (var i = 0; i < _arg2.Length; ++i)
            {
                _arg2[i] = (BaseArg2[i] * 50) / (float)Z.Value;
            }

            SmithChart.Refresh();

            //рисуем точку
            _smithChart.DrawUserCircles(objGraphics, (int)Z.Value, (int)ActiveR.Value, (int)ReactiveX.Value);

            /*//делаем доступной для нажатия кнопку отмена
            if (btnBack.Enabled == false)
            {
                btnBack.Enabled = true;
            }*/
        }

        private void CleanButton_Click(object sender, EventArgs e)
        {

        }

        private void OpenFileButton_Click(object sender, EventArgs e)
        {

        }
    }
}
