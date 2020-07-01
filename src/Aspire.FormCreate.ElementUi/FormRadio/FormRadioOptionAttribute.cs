namespace Aspire.FormCreate.ElementUi
{
    public class FormRadioOptionAttribute : FormItemOptionAttribute
    {
        public FormRadioOptionAttribute(string label) : base(label)
        {
        }

        public FormRadioOptionAttribute(string label, object value) : base(label, value)
        {
        }

        /// <summary>
        /// 单选框的尺寸，
        /// <para>默认值: - </para>
        /// </summary>
        public SizeEnum Size { get; set; }
    }
}
