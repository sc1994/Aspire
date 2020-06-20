using System;

namespace Aspire.DynamicForm
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class SelectOptionAttribute : Attribute
    {
        public SelectOptionAttribute(string label)
        {
            Label = label;
        }

        public SelectOptionAttribute(string label, object value)
        {
            Label = label;
            Value = value;
        }

        public SelectOptionAttribute(Type @enum)
        {
            Enum = @enum;
        }

        public Type Enum { get; }

        public string Label { get; }
        public object Value { get; }
    }
}
