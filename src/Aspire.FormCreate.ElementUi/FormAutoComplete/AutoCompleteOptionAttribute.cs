namespace Aspire.FormCreate.ElementUi
{
    public class AutoCompleteOptionAttribute : FormItemOptionAttribute
    {
        public AutoCompleteOptionAttribute(string label) : base(label)
        {
        }

        public AutoCompleteOptionAttribute(string label, object value) : base(label, value)
        {
        }
    }
}
