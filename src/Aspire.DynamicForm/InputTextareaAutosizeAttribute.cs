using System;

namespace Aspire.DynamicForm
{
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
