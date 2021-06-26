using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace COPOL.BLL.Models
{
    public class LoadPull
    {
        private readonly Parameters _parameters;
        private readonly Hashtable _points = new Hashtable();

        private const float VSat = 1.5f;
        private const float Rd = 2f;

        private float Z0;
        private Hashtable _zOpt = new Hashtable();

        public float PMaxOutput { get; private set; }
        public Hashtable ZOpt { get => _zOpt; private set => _zOpt = value; }

        public LoadPull(Parameters parameters, float z0)
        {
            _parameters = parameters;
            Z0 = z0;
        }

        /// <summary>
        /// Расчет точек контуров выходной мощности.
        /// </summary>
        /// <returns>Точки в HashTable.</returns>
        /// <exception cref="ArgumentException">ArgumentException.</exception>
        public Hashtable CalculatePoint()
        {
            var vMax = 2 * (_parameters.Vds0 - VSat);
            var iMax = 2 * _parameters.Ids0;

            var rOpt = (vMax / iMax);
            foreach (var frequence in _parameters.Frequences)
            {
                // Расчет оптимальной нагрузки для каждой частоты.
                var rOptComplex = new Complex(rOpt, 0);
                var zOpt = CalculateOptimalLoad(rOptComplex, frequence);
                ZOpt.Add("F = " + (frequence * Math.Pow(10,-9)), zOpt);
                
                var pMax = (float) (0.25 * (iMax * iMax) * zOpt.real);
                var pMaxDBm = (float)(10 * Math.Log10((1000 * pMax)));
                PMaxOutput = pMaxDBm;

                var stepPToPmax = _parameters.Step;
                if (stepPToPmax == 0)
                {
                    stepPToPmax = pMaxDBm - _parameters.LoopP;
                }

                // Падение в dBm на каждом контуре.
                var step = stepPToPmax;
                
                // Цикл для каждого контура.
                for (var i = 0; i < _parameters.N; i++)
                {
                    // Расчет мощности для каждого контура.
                    var pOutDBm = pMaxDBm - step;
                    var p = (float) (pMax / Math.Pow(10, ((pMaxDBm - pOutDBm) / 10)));
                    
                    // Точки сдвинутых контуров для каждой частоты.
                    var pointsOfShiftContours = new List<Complex>();
                    if (pOutDBm > 0)
                    {
                        var circlesParameters = CalculatingCirclesParameters(p, pMax, rOpt);
                        var alpha = (double)((float)circlesParameters["alpha"]);
                        var beta = (double)((float)circlesParameters["beta"]);
                        var pointsOfOldContours = CalculatePointsOfOldContours(
                            alpha,
                            beta,
                            (float) circlesParameters["Or1"],
                            (float) circlesParameters["Pr1"],
                            (float) circlesParameters["Og2"],
                            (float) circlesParameters["Pg2"]);

                        pointsOfShiftContours
                            .AddRange(pointsOfOldContours
                                .Select(point => CalculatePointsOfNewContours(point, frequence)));

                        step += stepPToPmax;
                    }
                    else
                    {
                        throw new ArgumentException(
                            "Не удается произвести расчет контуров для указанных данных." +
                            "\nПопробуйте изменить значение количества контуров или падения мощности.");
                    }
                    
                    var str2 = Math.Round(pOutDBm, 2).ToString("F1");
                    var key = "F = " + frequence + " " + "P = " + str2;
                    
                    try
                    {
                        _points.Add(key, pointsOfShiftContours);
                    }
                    catch
                    {
                        throw new ArgumentException("Ошибка при заполнении массива данных", "Ошибка");
                    }
                }
            }

            return _points;
        }
        
        private Complex CalculateOptimalLoad(Complex rOpt,float frequence)
        {
            var rdComplex = new Complex(Rd, 0);
            Complex numerator;
            Complex denominator;

            if (rOpt.imaginary == 0)
            {  
                //вычисляем значение нагрузки для каждой частоты
                var b = (_parameters.Cgd / _parameters.Gm) + rOpt.real * (_parameters.Cgd + _parameters.Cds);
                var a = (rOpt.real * _parameters.Cds) - (_parameters.Cgs / _parameters.Gm);
                var c = (_parameters.Ls * a) - (b * _parameters.Ld);
                var d = _parameters.Ls - _parameters.Ld;
                
                //циклическая частота W в квадрате
                var w = 2 * Math.PI * frequence;
                
                //вычисляем значение нагрузки для каждой частоты 
                numerator = new Complex(
                    rOpt.real - (w * w * c),
                    -w * d);
                
                denominator = new Complex(
                    1,
                    -w * b);
            }
            else
            {
                //вычисляем значение нагрузки для каждой частоты
                var a = _parameters.Cgs / _parameters.Gm;
                var b = _parameters.Cgd / _parameters.Gm;
                var c = _parameters.Cgd + _parameters.Cds;
                var w = 2 * Math.PI * frequence;
                var d = (rOpt.real * _parameters.Cds) - a;
                var e = b + (c * rOpt.real);
                var f = (_parameters.Ls * _parameters.Cds) - (_parameters.Ld * c);
                var g = _parameters.Ls - _parameters.Ld;
                
                //вычисляем значение нагрузки для каждой частоты 
                numerator = new Complex(
                    rOpt.real - w * w * ((_parameters.Ls * d) - (_parameters.Ld * e)),
                    rOpt.imaginary - (w * w * rOpt.imaginary * f) - (w * g));
                
                denominator = new Complex(
                    1 + (w * c * rOpt.imaginary), 
                    -w * (b + (c * rOpt.real)));
            }
            
            //оптимальная нагрузка
            return (numerator/ denominator) - rdComplex;
        }
          
          private Hashtable CalculatingCirclesParameters(float p,float pMaxDBm,float rOpt)
          {
              var outputCirclesParameters = new Hashtable();
              
              //формулы для построения контуров
              var R1 = rOpt * (p / pMaxDBm);
              var R2 = rOpt * (pMaxDBm / p);
              var r1 = R1 / Z0;
              var r2 = 1/(R2 / Z0);
              
              // Координаты центра и радиус 1 окружности.
              var Or1 = r1 / (1 + r1);
              var Pr1 = 1 / (1 + r1);
              
              outputCirclesParameters.Add("Or1", Or1);
              outputCirclesParameters.Add("Pr1", Pr1);
              
              // Координаты центра и радиус 2 окружности.
              var Og2 = -r2 / (1 + r2);
              var Pg2 = 1 / (1 + r2);
              
              outputCirclesParameters.Add("Og2", Og2);
              outputCirclesParameters.Add("Pg2", Pg2);

              // Координаты точек пересечения.
              var x = ((Pr1 * Pr1) - (Pg2 * Pg2) + (Og2 * Og2) - (Or1 * Or1)) / (2 * (Og2 - Or1));
              var y1 = (float)(Math.Sqrt((Pr1 * Pr1) - ((x - Or1) * (x - Or1))));
              var y2 = (float)(-Math.Sqrt((Pr1 * Pr1) - ((x - Or1) * (x - Or1))));
              
              outputCirclesParameters.Add("x",x);
              outputCirclesParameters.Add("y1", y1);
              outputCirclesParameters.Add("y2",y2);
              
              // Углы поворота точек пересечения окружностей.
              var alpha = (float)(Math.Acos((Or1 - x) / Pr1));
              var beta = (float)(Math.Acos((x - Og2) / Pg2));
              
              outputCirclesParameters.Add("alpha", alpha);
              outputCirclesParameters.Add("beta",beta);

              return outputCirclesParameters;
          }
          
          private Complex CalculatePointsOfNewContours(PointF oldPointValue, float frequence)
          {
              var z00 = new Complex(Z0, 0);
              var oldPoint = new Complex
              {
                  real = oldPointValue.X,
                  imaginary = oldPointValue.Y
              };
              
              //перевели все точки из гамма в сопротивление
              var newPoint = z00 * (new Complex(1, 0) + oldPoint) / (new Complex(1, 0) - oldPoint);
              
              //пересчитываем точки с помощью формулы для оптимальной нагрузки
              var zOpt = CalculateOptimalLoad(newPoint,frequence);
              var zOptGamma = ConvertPointsToGammaCoefficient(zOpt, z00);
              
              return zOptGamma;
          }
        
        private static IEnumerable<PointF> CalculatePointsOfOldContours(
            double alpha,
            double beta,
            float or1,
            float pr1,
            float og2,
            float pg2)
        {
            var pointsOfOldContours = new List<PointF>();

            var firstHalfOfFirstArch = new List<PointF>();
            var secondHalfOfFirstArch = new List<PointF>();
            var firstHalfOfSecondArch = new List<PointF>();
            var secondHalfOfSecondArch = new List<PointF>();

            float x;
            float y1;

            for (var i = alpha; i >= 0; i = i - 0.002)
            {
                x = (float) (or1 - (pr1 * Math.Cos(i)));
                y1 = (float) (Math.Sqrt((pr1 * pr1) - ((x - or1) * (x - or1))));

                // Записываем все точки с положительными координатами в первый массив,
                // а с отрицательными - во второй
                firstHalfOfFirstArch.Add(new PointF(x,y1));
                secondHalfOfFirstArch.Add(new PointF(x, -y1));
                
            }

            for (var j = beta; j >= 0; j -= 0.02)
            {
                x = (float)(pg2 * Math.Cos(j)) + og2;
                y1 = (float)Math.Sqrt((pg2 * pg2) - ((x - og2) * (x - og2)));

                // Записываем все точки с отрицательными координатами в первый массив,
                // а с положительными - во второй.
                firstHalfOfSecondArch.Add(new PointF(x, y1));
                secondHalfOfSecondArch.Add(new PointF(x, -y1));
            }

            // Переворачиваем массивы наоборот, чтобы можно было обойти контур по часовой стрелке.
            secondHalfOfFirstArch.Reverse();
            secondHalfOfSecondArch.Reverse();

            // Соединяем все массивы в массив контура,обойденного по часовой стрелке.
            pointsOfOldContours.AddRange(firstHalfOfFirstArch);
            pointsOfOldContours.AddRange(secondHalfOfFirstArch);
            pointsOfOldContours.AddRange(firstHalfOfSecondArch);
            pointsOfOldContours.AddRange(secondHalfOfSecondArch);

            return pointsOfOldContours;
        }
        
        /// <summary>
        /// Перевод точки в координаты коэффициента отражения.
        /// </summary>
        /// <param name="point">Точки.</param>
        /// <param name="z00"></param>
        /// <returns>Значение коэфициента отражения.</returns>
        private static Complex ConvertPointsToGammaCoefficient(Complex point,Complex z00)
        {
            return (point - z00) / (point + z00);;
        }
    }
}