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


}
