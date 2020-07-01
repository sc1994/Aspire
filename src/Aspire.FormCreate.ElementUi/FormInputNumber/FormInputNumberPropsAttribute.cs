using System;

namespace Aspire.FormCreate.ElementUi
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class FormInputNumberPropsAttribute : Attribute
    {
        /// <summary>
        /// 最大值
        /// <para>默认值: - </para>
        /// </summary>
        public int Max { get; set; }

        /// <summary>
        /// 最小值
        /// <para>默认值: - </para>
        /// </summary>
        public int Min { get; set; }

        /// <summary>
        /// 每次改变的步伐，可以是小数     
        /// <para>默认值: 1 </para>        
        /// </summary>
        public double Step { get; set; }

        /// <summary>
        /// 输入框尺寸
        /// <para>默认值: - </para>
        /// </summary>
        public SizeEnum Size { get; set; }

        /// <summary>
        /// 设置禁用状态
        /// <para>默认值: false </para>
        /// </summary>
        public bool Disabled { get; set; }

        /// <summary>
        /// 占位文本
        /// <para>默认值: - </para>
        /// </summary>
        public string Placeholder { get; set; }

        /// <summary>
        /// 指定输入框展示值的格式
        /// <para>默认值: - </para>
        /// <para>备注: 填写remote api地址, 请求参数为当前表单, 相应参数根据要求自定义 </para>
        /// </summary>
        public string Formatter { get; set; }

        /// <summary>
        /// 指定从formatter里转换回数字的方式，和formatter搭配使用
        /// <para>默认值: - </para>
        /// <para>备注: 填写remote api地址, 请求参数为当前表单, 相应参数根据要求自定义 </para>
        /// </summary>
        public string Parser { get; set; }

        /// <summary>
        /// 是否设置为只读
        /// <para>默认值: false </para>
        /// </summary>
        public bool Readonly { get; set; }

        /// <summary>
        /// 是否可编辑
        /// <para>默认值: true </para>
        /// </summary>
        public bool Editable { get; set; }

        /// <summary>
        /// 数值精度
        /// <para>默认值: - </para>
        /// </summary>
        public int Precision { get; set; }

        /// <summary>
        /// 给表单元素设置id，详见Form用法。
        /// <para>默认值: - </para>
        /// </summary>
        public string ElementId { get; set; }

        /// <summary>
        /// 是否实时响应数据，设置为false时，只会在失焦时更改数据
        /// <para>默认值: true </para>
        /// </summary>
        public bool ActiveChange { get; set; }
    }
}
