using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibraries.Core.Attributes
{
    /// <summary>
    /// Добавление этого атрибута регистрирует элемент в контейнере как Singleton.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class SingletonRegistrationAttribute : Attribute
    {
    }
}
