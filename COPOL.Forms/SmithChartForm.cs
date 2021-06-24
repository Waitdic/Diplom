using System;
using System.Windows.Forms;
using COPOL.BLL;
using COPOL.BLL.Models;

namespace COPOL.Forms
{
    public partial class DiagrammForm : Form
    {
        private readonly SmithChart _smithChart = new SmithChart();
        private Parameters _parameters;
        private PaintEventArgs _graphicsBase;

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
            if (_parameters == null)
            {
                _parameters = new Parameters();
            }

            dataForm.SetParameters(_parameters);
        }

        /// <summary>
        /// Построение диаграммы Смита.
        /// </summary>
        private void SmithChart_Paint(object sender, PaintEventArgs e)
        {
            _smithChart.DrawGraphics(e, _arg1, _arg2);
            _graphicsBase = e;
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
                    SmithChart.CreateGraphics(),
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
        }

        private void CleanButton_Click(object sender, EventArgs e)
        {
            Z.Value = 50;
            ReactiveX.Value = 0;
            ActiveR.Value = 0;
            
            _arg1 = BaseArg1;
            _arg2 = BaseArg2;

            SmithChart.Paint += SmithChart_Paint;
            SmithChart.Refresh();
        }

        private void OpenFileButton_Click(object sender, EventArgs e)
        {

        }
    }
}
