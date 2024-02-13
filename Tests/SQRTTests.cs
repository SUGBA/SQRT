using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQRT.Services;

namespace Tests
{
    public class SQRTTests
    {
        /// <summary>
        /// Пример теста
        /// Создаем сервис
        /// Assert - метод проверяющий условие
        /// Почитать: На метаните есть статья посвященная xUnit
        /// </summary>
        [Fact]
        public void ExampleTest()
        {
            var service = new SqrtWorker();

            Assert.True(true);
        }
    }
}
