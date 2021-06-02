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
            0.0f, 0.1f, 0.2f, 0.3f, 0.4f, 0.5f, 0.6f, 0.7f,
            0.8f, 0.9f, 1.0f, 1.2f, 1.4f, 1.6f, 1.8f, 
            2.0f, 3.0f, 4.0f, 5.0f, 10.0f, 20.0f, 50.0f 
        };
        
        // Центры Вертикальных окружностей. 
        private static readonly float[] BaseArg2 = { 
            0.2f, 0.4f, 0.6f, 0.8f, 1.0f,
            1.2f, 1.4f, 1.6f, 1.8f, 2.0f,
            3.0f, 4.0f, 5.0f, 10.0f, 20.0f, 50.0f 
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
            var dataForm = new InitialDataForm() { Visible = true};
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

        private void button2_Click(object sender, EventArgs e)
        {
            //if (OpenFileButtonPressed == false)
            //    TableOfContoursFromFile = null;
            //SavePointsButton.Enabled = true;
            //PmaxOutput.Visible = true;
            //btnBack.Enabled = true;

            //задание входных параметров транзистора
            /* float Vds0 = (float)this.Vds0;
            float Ids0 = (float)(this.Ids0 * System.Math.Pow(10, -3));
            float Cgs = (float)(this.Cgs * System.Math.Pow(10, -12));
            float Cds = (float)(this.Cds * System.Math.Pow(10, -12));
            float Cgd = (float)(this.Cgd * System.Math.Pow(10, -12));
            float Ls = (float)(this.Ls * System.Math.Pow(10, -9));
            float Ld = (float)(this.Ld * System.Math.Pow(10, -9));
            float gm = (float)(this.Gm * System.Math.Pow(10, -3)); */

            if (_parameters == null)
            {
                return;
            }

            // Создаем объект класса LoadPull и передаем параметры для расчета.
            var z0 = Z.Value == 0 ? 50 : (float) Z.Value;
            var builder = new LoadPull(
                _parameters,
                z0);

            // Рассчитываем точки контуров. 
            var outputPoint = builder.CalculatePoint();
            Pmax.Text = Math.Round(builder.PMaxOutput, 2).ToString();
            
            // Рисуем контура.
            DrawManager.DrawContours(
                outputPoint,
                this.SmithChart.CreateGraphics(),
                _smithChart,
                builder,
                z0);

            /*if ((_parameters.Difference == 0) || (valueWaveR == 50))//проверяем значение волнового сопротивления
            {
                //создание объекта класса, вычисляющего точки контуров мощности
                MyDataClass = new LoadPullData(Vds0, Ids0, Cgs, Cds, Cgd, Ls, Ld, gm, default_Z0);
            }
            else //создание объекта класса, вычисляющего точки контуров мощности
                MyDataClass = new LoadPullData(Vds0, Ids0, Cgs, Cds, Cgd, Ls, Ld, gm, valueWaveR);*/


            //массив частот 
            /*var frequences = new List<float>();
            frequences = this.Frequences;
            int n = this.n; //требуемое число контуров
            float difference = this.difference;//падение мощности
            float P = this.P;//требуемая мощность контура

            //рассчитываем точки контуров
            if (OpenFileButtonPressed == false)//если кнопка "открыть файл" не нажималась 
            {
                var output = builder.CalculatePoint(n, difference, frequences, P);

                //рисуем контуры

                DrawContours(output);
            }*/
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
    }
}
