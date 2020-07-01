using System;

namespace Aspire.FormCreate.ElementUi
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CheckboxPropsAttribute : Attribute
    {
        /// <summary>
        /// 多选框组的尺寸
        /// <para>默认值: - </para>
        /// </summary>
        public SizeEnum Size { get; set; }
    }
}
