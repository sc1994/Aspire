using System;
using System.Reflection;

using Aspire.Application.AppServices;

namespace Aspire.FormCreate.ElementUi
{
    /// <summary>
    /// 表单样式
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class FormStyleAttribute : AspireFormAttribute
    {
        public FormStyleAttribute(string title) : base(title)
        {
        }

        /// <summary> 
        /// 行内表单模式 
        /// <para>默认值: false</para>
        /// </summary>
        public bool? Inline { get; set; }
        /// <summary> 
        /// 表单域标签的位置，如果值为 left 或者 right 时，则需要设置 LabelWidth  
        /// <para>默认值: right</para>
        /// </summary>
        public string LabelPosition { get; set; }
        /// <summary> 
        /// 表单域标签的后缀  
        /// <para>默认值: null</para>
        /// </summary>
        public string LabelSuffix { get; set; }
        /// <summary> 
        /// 是否显示必填字段的标签旁边的红色星号 
        /// <para>默认值: false</para>
        /// </summary>
        public bool? HideRequiredAsterisk { get; set; }
        /// <summary> 
        /// 表单域标签的宽度，例如 '50px'。作为 Form 直接子元素的 form-item 会继承该值。支持 。
        /// <para>默认值: 125px</para>
        /// <para>可选值: ??px / auto</para>
        /// </summary>
        public string LabelWidth { get; set; }
        /// <summary> 
        /// 是否显示校验错误信息
        /// <para>默认值: true</para>
        /// </summary>
        public bool? ShowMessage { get; set; }
        /// <summary> 
        /// 是否以行内形式展示校验信息
        /// <para>默认值: false</para>
        /// </summary>
        public bool? InlineMessage { get; set; }
        /// <summary> 
        /// 是否在输入框中显示校验结果反馈图标
        /// <para>默认值: false</para>
        /// </summary>
        public bool? StatusIcon { get; set; }
        /// <summary> 
        /// 是否在 rules 属性改变后立即触发一次验证
        /// <para>默认值: true</para>
        /// </summary>
        public bool? ValidateOnRuleChange { get; set; }
        /// <summary> 
        /// 是否禁用该表单内的所有组件。若设置为 true，则表单内组件上的 disabled 属性不再生效
        /// <para>默认值: false</para>
        /// </summary>
        public bool? Disabled { get; set; }
        /// <summary> 
        /// 用于控制该表单内组件的尺寸 
        /// <para>默认值: null</para>
        /// <para>可选值: medium / small / mini</para>
        /// </summary>
        public string Size { get; set; }

        public override object Format(PropertyInfo property, object dtoInstance = null)
        {
            throw new NotImplementedException();
        }
    }
}
