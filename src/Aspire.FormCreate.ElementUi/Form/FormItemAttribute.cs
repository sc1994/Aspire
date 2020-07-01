using System;

namespace Aspire.FormCreate.ElementUi
{
    /// <summary>
    /// 表单样式
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class FormItemAttribute : Attribute
    {
        /// <summary>
        /// Input 输入框
        /// </summary>
        public FormItemAttribute()
        {
        }

        /// <summary>
        /// Input 输入框
        /// </summary>
        /// <param name="title">表单项标题</param>
        public FormItemAttribute(string title)
        {
            Title = title;
        }

        public string Title { get; }
    }
}
