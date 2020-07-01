using System;

namespace Aspire.FormCreate.ElementUi
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class FormRadioPropsAttribute : Attribute
    {
        /// <summary>
        /// 为 button 时使用按钮样式
        /// <para>默认值: - </para>
        /// </summary>
        public RadioTypeEnum Type { get; set; }

        /// <summary>
        /// 尺寸
        /// <para>默认值: - </para>
        /// </summary>
        public SizeEnum Size { get; set; }

        /// <summary>
        /// 是否垂直排列，按钮样式下无效	
        /// <para>默认值: false </para>
        /// </summary>
        public bool Vertical { get; set; }
    }
}
