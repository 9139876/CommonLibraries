using System;

namespace CommonLibraries.Core.Attributes
{
    /// <summary>
    /// Добавление этого атрибута принудительно исключает регистрацию элемента в контейнере.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class IgnoreRegistrationAttribute : Attribute
    {
    }
}
