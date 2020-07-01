using System;

namespace Aspire.FormCreate.ElementUi
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class AutoCompletePropsAttribute : Attribute
    {
        /// <summary>
        /// 是否可以清空选项
        /// <para>默认值: false </para>
        /// </summary>
        public bool Clearable { get; set; }

        /// <summary>
        /// 是否禁用
        /// <para>默认值: false </para>
        /// </summary>
        public bool Disabled { get; set; }

        /// <summary>
        /// 占位文本
        /// <para>默认值: - </para>
        /// </summary>
        public string Placeholder { get; set; }

        /// <summary>
        /// 输入框尺寸
        /// <para>默认值: - </para>
        /// </summary>
        public SizeEnum Size { get; set; }

        /// <summary>
        /// 输入框尾部图标
        /// <para>默认值: - </para>
        /// <para>可选值:  </para>
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 是否根据输入项进行筛选。当其为一个函数时，会接收<code>value</code>和<code>option</code>两个参数，当option符合筛选条件时，应返回true，反之 则返回false
        /// <para>默认值: false </para>
        /// <para>可选值:  </para>
        /// </summary>
        public bool FilterMethod { get; set; }

        /// <summary>
        /// 弹窗的展开方向，2.12.0版本开始支持自动识别
        /// <para>默认值: bottom </para>
        /// <para>可选值: bottom / top</para>
        /// </summary>
        public string Placement { get; set; }

        /// <summary>
        /// 是否将弹层放置于body内，在Tabs、带有fixed的Table列内使用时，建议添加此属性，它将不受父级样式影响，从而达到更好的效果
        /// <para>默认值: false </para>
        /// </summary>
        public bool Transfer { get; set; }

        /// <summary>
        /// 给表单元素设置id，详见Form用法。
        /// <para>默认值: - </para>
        /// </summary>
        public string ElementId { get; set; }
    }
}
