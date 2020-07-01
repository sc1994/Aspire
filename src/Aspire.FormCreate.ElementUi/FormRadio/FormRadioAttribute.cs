using System;

namespace Aspire.FormCreate.ElementUi
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class FormRadioAttribute : FormItemAttribute
    {
        public FormRadioAttribute()
        {
        }

        public FormRadioAttribute(string title) : base(title)
        {
        }
    }
}
