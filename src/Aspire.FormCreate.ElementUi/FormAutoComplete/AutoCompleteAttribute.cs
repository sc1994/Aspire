using System;

namespace Aspire.FormCreate.ElementUi
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class AutoCompleteAttribute : FormItemAttribute
    {
        public AutoCompleteAttribute(string title) : base(title)
        {
        }
    }
}
