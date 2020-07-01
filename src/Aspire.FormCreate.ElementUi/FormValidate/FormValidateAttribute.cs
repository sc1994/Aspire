using System;

namespace Aspire.FormCreate.ElementUi
{
    /// <summary>
    /// 更多高级用法可研究 https://github.com/yiminghe/async-validator
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class FormValidateAttribute : Attribute
    {
        /// <summary>
        /// 枚举类型 指定选中值必须在指定枚举中
        /// </summary>
        public Type Enum { get; set; }
        /// <summary>
        /// 字段长度
        /// <para> 默认值: </para>
        /// <para> 可选值: </para>
        /// </summary>
        public int Len { get; set; }
        /// <summary>
        /// 最大长度
        /// <para> 默认值: </para>
        /// <para> 可选值: </para>
        /// </summary>
        public int Max { get; set; }
        /// <summary>
        /// 校验文案
        /// <para> 默认值: </para>
        /// <para> 可选值: </para>
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 最小长度
        /// <para> 默认值: </para>
        /// <para> 可选值: </para>
        /// </summary>
        public int Min { get; set; }
        /// <summary>
        /// 正则表达式校验
        /// <para> 默认值: </para>
        /// <para> 可选值: </para>
        /// </summary>
        public string Pattern { get; set; }
        /// <summary>
        /// 是否必选
        /// <para> 默认值: </para>
        /// <para> 可选值: </para>
        /// </summary>
        public bool Required { get; set; }
        /// <summary>
        /// 校验前转换字段值
        /// <para> 默认值: string</para>
        /// <para> 可选值: </para>
        /// <para> string: Must be of type string. This is the default type. </para>
        /// <para> number: Must be of type number. </para>
        /// <para> boolean: Must be of type boolean. </para>
        /// <para> method: Must be of type function. </para>
        /// <para> regexp: Must be an instance of RegExp or a string that does not generate an exception when creating a new RegExp. </para>
        /// <para> integer: Must be of type number and an integer. </para>
        /// <para> float: Must be of type number and a floating point number. </para>
        /// <para> array: Must be an array as determined by Array.isArray. </para>
        /// <para> object: Must be of type object and not Array.isArray. </para>
        /// <para> enum: Value must exist in the enum. </para>
        /// <para> date: Value must be valid as determined by Date </para>
        /// <para> url: Must be of type url. </para>
        /// <para> hex: Must be of type hex. </para>
        /// <para> email: Must be of type email. </para>
        /// <para> any: Can be any type. </para>
        /// </summary>
        public string Transform { get; set; }
        /// <summary>
        /// 内建校验类型，
        /// <para> 默认值: </para>
        /// <para> 可选值: </para>
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 自定义校验 校验的远程API地址, API的响应值必须为 boolean
        /// <para> 默认值: </para>
        /// <para> 可选值: </para>
        /// </summary>
        public string Validator { get; set; }
        /// <summary>
        /// 必选时，空格是否会被视为错误
        /// <para> 默认值: false</para>
        /// <para> 可选值: </para>
        /// </summary>
        public bool Whitespace { get; set; }
    }
}
