using System;

namespace COPOL.BLL.Models
{
    public struct Complex
    {
        public double real;
        public double imaginary;
     
        public Complex(double real, double imaginary)  //конструктор
        {
            this.real = real;// реальная часть
            this.imaginary = imaginary;// мнимая часть
        }
     
        // Объявление  оператора  (+)
        public static Complex operator +(Complex c1, Complex c2)
        {
            return new Complex(c1.real + c2.real, c1.imaginary + c2.imaginary);
        }
        
        // Объявление  оператора  (-)  
        public static Complex operator -(Complex c1, Complex c2)
        {
            return new Complex(c1.real - c2.real, c1.imaginary - c2.imaginary);
        }
        
        // Объявление  оператора  (*)
        public static Complex operator *(Complex c1, Complex c2)
        {
            return new Complex(c1.real * c2.real - c1.imaginary * c2.imaginary,
           c1.real * c2.imaginary + c1.imaginary * c2.real);
        }
        
        // Объявление  оператора  (/)
        public static Complex operator /(Complex c1, Complex c2)
        {
            var denominator = c2.real * c2.real + c2.imaginary * c2.imaginary;
            return new Complex((c1.real * c2.real + c1.imaginary * c2.imaginary) / denominator,
                (c2.real * c1.imaginary - c2.imaginary * c1.real) / denominator);
        }
        
        public static bool operator ==(Complex c1, Complex c2)
        {
            return c1.real == c2.real && c1.imaginary == c2.imaginary;
        }
        
        public static bool operator !=(Complex c1, Complex c2)
        {
            return !(c1 == c2);
        }
        
        public double Abs
        {
            get { return Math.Sqrt(imaginary * imaginary + real * real); }
        }
        
        public static Complex Sqrt(Complex c)
        {
            double abs = Math.Sqrt(c.Abs);
            return new Complex(abs * Math.Cos(c.Arg / 2), abs * Math.Sin(c.Arg / 2));
        }
        
        public static bool operator >(Complex c1, Complex c2)
        {
            return (c1 > c2);
        }
        
        public static bool operator <(Complex c1, Complex c2)
        {
            return (c1 < c2);
        }
     
        public double Arg
        {
            get
            {
                double res = Math.Atan2(imaginary, real);
                return res;
            }
        }
    }
}