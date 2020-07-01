namespace Aspire.FormCreate.ElementUi
{
    public class CheckboxOptionAttribute : FormItemOptionAttribute
    {
        public CheckboxOptionAttribute(string label) : base(label)
        {
        }

        public CheckboxOptionAttribute(string label, object value) : base(label, value)
        {
        }
    }
}
