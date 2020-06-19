namespace Aspire.Utils
{
    public class VerifyHelper
    {
        public static bool IsDefaultValue(object value)
        {
            if (value == null) return true;
            switch (value)
            {
                case short _:
                case int _:
                case float _:
                case long _:
                case double _:
                case decimal _:
                    return value.Equals(0);
                case string _:
                    return value.Equals(null) || value.Equals(string.Empty);
                case bool _:
                    return value.Equals(false);
                default: return true;
            }
        }
    }
}
