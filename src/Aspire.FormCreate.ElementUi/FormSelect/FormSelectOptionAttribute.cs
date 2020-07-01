using System;

namespace Aspire.FormCreate.ElementUi
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class FormSelectOptionAttribute : FormItemOptionAttribute
    {
        public FormSelectOptionAttribute(string label) : base(label)
        {
        }

        public FormSelectOptionAttribute(string label, object value) : base(label, value)
        {
        }
    }
}
