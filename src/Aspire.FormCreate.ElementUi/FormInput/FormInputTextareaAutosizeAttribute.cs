using System;

namespace Aspire.FormCreate.ElementUi
{
    /// <summary>
    /// 自适应内容高度，仅在 textarea 类型下有效，如 { minRows: 2, maxRows: 6 }
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class FormInputTextareaAutosizeAttribute : Attribute
    {
        /// <summary>
        /// 自适应内容高度，仅在 textarea 类型下有效，如 { minRows: 2, maxRows: 6 }
        /// </summary>
        public FormInputTextareaAutosizeAttribute(int minRows, int maxRows)
        {
            MinRows = minRows;
            MaxRows = maxRows;
        }

        public int MinRows { get; }
        public int MaxRows { get; }
    }
}
