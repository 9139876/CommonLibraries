using CommonLibraries.Core.Extensions;

namespace CommonLibraries.Graal.Enums
{
    /// <summary>
    /// Таймфрейм
    /// </summary>
    public enum TimeFrameEnum
    {
        /// <summary>
        /// 1-минутный
        /// </summary>
        [Description("1-минутный")]
        min1 = 10,
        /// <summary>
        /// 4-минутный
        /// </summary>
        [Description("4-минутный")]
        min4 = 20,
        /// <summary>
        /// Часовой
        /// </summary>
        [Description("Часовой")]
        H1 = 30,
        /// <summary>
        /// Дневной
        /// </summary>
        [Description("Дневной")]
        D1 = 40,
        /// <summary>
        /// Недельный
        /// </summary>
        [Description("Недельный")]
        W1 = 50,
        /// <summary>
        /// Месячный
        /// </summary>
        [Description("Месячный")]
        M1 = 60,
        /// <summary>
        /// Сезонный
        /// </summary>
        [Description("Сезонный")]
        Seasonly = 70,
        /// <summary>
        /// Годовой
        /// </summary>
        [Description("Годовой")]
        Y1 = 80

    }
}
