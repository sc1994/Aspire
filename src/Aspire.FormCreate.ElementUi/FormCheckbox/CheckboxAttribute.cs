using System;

namespace Aspire.FormCreate.ElementUi
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CheckboxAttribute : FormItemAttribute
    {
        public CheckboxAttribute(string title) : base(title)
        {
        }
    }
}
