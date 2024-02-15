namespace SQRT.Models
{
    /// <summary>
    /// Модель передаваемая в представлени
    /// </summary>
    public class MainDto
    {
        /// <summary>
        /// Реузльтат
        /// </summary>
        public string Result { get; set; } = string.Empty;

        /// <summary>
        /// Колличество символов
        /// </summary>
        public int DigitNumber { get; set; }

        /// <summary>
        /// Переводимое значение
        /// </summary>
        public string Value { get; set; } = string.Empty;
    }
}
