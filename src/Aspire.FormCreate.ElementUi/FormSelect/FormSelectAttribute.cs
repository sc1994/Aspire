using System;

namespace Aspire.FormCreate.ElementUi
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class FormSelectAttribute : FormItemAttribute
    {
        public FormSelectAttribute()
        {
        }

        public FormSelectAttribute(string title) : base(title)
        {
        }
    }
}
