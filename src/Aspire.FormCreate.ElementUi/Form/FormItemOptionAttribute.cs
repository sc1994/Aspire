using System;

namespace Aspire.FormCreate.ElementUi
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class FormItemOptionAttribute : Attribute
    {
        public FormItemOptionAttribute()
        {
        }

        public FormItemOptionAttribute(string label) : this()
        {
            Label = label;
        }

        public FormItemOptionAttribute(string label, object value) : this(label)
        {
            Value = value;
        }

        public FormItemOptionAttribute(string label, object value, bool disabled) : this(label, value)
        {
            Disabled = disabled;
        }

        /// <summary>
        /// 选项文案
        /// </summary>
        public string Label { get; }

        /// <summary>
        /// 选项值
        /// </summary>
        public object Value { get; }

        /// <summary>
        /// 是否禁用当前项
        /// <para>默认值: false </para>
        /// </summary>
        public bool Disabled { get; set; }
    }
}
