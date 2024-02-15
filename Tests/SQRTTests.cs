using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SQRT.Services;
using System.Text.RegularExpressions;


namespace Tests
{
    public class SQRTTests
    {
        [Theory]
        [InlineData("25", "5")]          // Арифметический корень
        [InlineData("0", "0")]           // Корень из нуля
        [InlineData("4,84", "2,2")]         // Арифметический корень с десятичными
        [InlineData("100000000000000000000000000000000000000000000000000", "1E+25")] // Длинное число
        [InlineData("3,5E+2", "18,71")] // Научная нотация
        public void SqrtFunction_ValidInput(string input, string expectedResult)
        {
            var service = new SqrtWorker();

       
            SqrtWorker sqrt = new SqrtWorker();
            string result = sqrt.CalculateSquareRoot(input, 2);
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("-9", "3i")] // Аналитический корень (результат комплексный)
        [InlineData("0", "0")]  // Аналитический корень из нуля
        [InlineData("16", "4")] // Аналитический корень с вещественной и мнимой частью
        [InlineData("-16", "4i")]
        [InlineData("3 + 4i", "2+1i")]
        public void SqrtFunction_AnalyticRoot(string input, string expectedResult)
        {

            SqrtWorker sqrt = new SqrtWorker();
            // Act
            string result = sqrt.CalculateSquareRoot(input, 2);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("abc", "Введите число в корректном формате")]
        [InlineData("", "Введите число в корректном формате")]
        [InlineData(null, "Введите число в корректном формате")] 
        [InlineData("1.2.3", "Введите число в корректном формате")] 
        public void SqrtFunction_InvalidInput(string input, string expectedResult)
        {
            SqrtWorker sqrt = new SqrtWorker();
            // Act
            string result = sqrt.CalculateSquareRoot(input, 2);

            // Assert
            Assert.Equal(expectedResult, result);
        }
        

       

    }
    public class SqrtWorker 
    {
        public string CalculateSquareRoot(string input, int precision)
        {
            if (precision > 10)
            {
                precision = 10;
            }
            bool flag;
            if (String.IsNullOrEmpty(input))
            {
                return "Введите число в корректном формате";
            }
            else if (flag = Double.TryParse(input, out double result))
            {
                input = input.Replace(" ", "");
                input = input.Replace(".", ",");
                Complex complexNumber = new Complex(result, 0);
                Complex resultComplex = Complex.Sqrt(complexNumber);
                string resultString = ConvertComplexToString(resultComplex, precision);
                return resultString;
            }
            else if (IsComplexNumber(input))
            {
                input = input.Replace(" ", "");
                input = input.Replace(".", ",");
                input = input.Replace("i", "");
                if (input.Contains("+"))
                {
                    string[] parts = input.Split('+');
                    Complex complexNumber = new Complex(Double.Parse(parts[0]), Double.Parse(parts[1]));
                    Complex resultComplex = Complex.Sqrt(complexNumber);
                    string resultString = ConvertComplexToString(resultComplex, precision);
                    return resultString;
                }
                else
                {
                    string[] parts = input.Split('-');
                    Complex complexNumber = new Complex(Double.Parse(parts[0]), Double.Parse(parts[1]) * -1);
                    Complex resultComplex = Complex.Sqrt(complexNumber);
                    string resultString = ConvertComplexToString(resultComplex, precision);
                    return resultString;
                }
            }
            else
            {
                return "Введите число в корректном формате";
            }
        }
        public bool IsComplexNumber(string input)
        {
            // Регулярное выражение для проверки строки комплексного числа в формате "a+bi" или "a-bi"
            string pattern = @"^\s*([-+]?\d*\.?\d+)\s*([-+])\s*(\d*\.?\d*)i\s*$";

            // Проверка совпадения строки с регулярным выражением
            Match match = Regex.Match(input, pattern);

            return match.Success;
        }

        public string ConvertComplexToString(Complex complex, int precision)
        {
            if (complex.Real > 0 || complex.Real < 0)
            {
                if (complex.Imaginary == 0)
                {
                    string result = Math.Round(complex.Real, precision).ToString();
                    return result;
                }
                else if (complex.Imaginary > 0)
                {
                    string result = Math.Round(complex.Real, precision).ToString() + "+" + Math.Round(complex.Imaginary, precision).ToString() + "i";
                    return result;
                }
                else
                {
                    string result = Math.Round(complex.Real, precision).ToString() + Math.Round(complex.Imaginary, precision).ToString() + "i";
                    return result;
                }
            }
            else
            {
                if (complex.Imaginary == 0)
                {
                    string result = "0";
                    return result;
                }
                else
                {
                    string result = Math.Round(complex.Imaginary, precision).ToString() + "i";
                    return result;
                }
            }
        }
    }

}
