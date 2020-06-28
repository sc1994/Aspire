using System;

namespace Aspire.FormCreate.ElementUi
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class InputTextareaAutosizeAttribute : Attribute
    {
        public InputTextareaAutosizeAttribute(int minRows, int maxRows)
        {
            MinRows = minRows;
            MaxRows = maxRows;
        }

        public int MinRows { get; }
        public int MaxRows { get; }
    }
}
