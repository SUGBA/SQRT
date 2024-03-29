﻿using System.Numerics;
using System.Text.RegularExpressions;


namespace SQRT.Services
{
    public class SqrtWorker : ISqrtWorker
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
            else
            {
                input = input.Replace(" ", "");
                input = input.Replace(".", ",");
                if (flag = Double.TryParse(input, out double result))
                {
                    Complex complexNumber = new Complex(result, 0);
                    Complex resultComplex = Complex.Sqrt(complexNumber);
                    string resultString = ConvertComplexToString(resultComplex, precision);
                    return resultString;
                }
                else if (IsComplexNumber(input))
                {
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
