namespace Aspire.FormCreate.ElementUi
{
    public class FormInputAttribute : FormItemAttribute
    {
        public FormInputAttribute(string title) : base(title)
        {
        }

        public FormInputAttribute(string title, string type) : this(title)
        {
            Type = type;
        }

        /// <summary>
        /// 类型
        /// <para>默认值: text</para>
        /// <para>可选值: text，textarea 和其他 原生 input 的 type 值 https://developer.mozilla.org/en-US/docs/Web/HTML/Element/input#Form_%3Cinput%3E_types</para>
        /// </summary>
        public string Type { get; set; }
    }
}
