using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Aspire.Application.AppServices;
using Aspire.Utils;

namespace Aspire.FormCreate.ElementUi
{
    internal class Util
    {
        public static Dictionary<string, object> DefaultFormat(PropertyInfo property, object dtoInstance, AspireFormAttribute formInstance)
        {
            // 首字符小写
            var field = ConvertHelper.ToLowercaseFirstCharacter(property.Name);

            var props = formInstance.GetType().GetProperties()
                .Where(x => !VerifyHelper.IsDefaultValue(x.GetValue(formInstance)) && x.Name != "TypeId")
                .ToDictionary(x => ConvertHelper.ToLowercaseFirstCharacter(x.Name), x => x.GetValue(formInstance))
                .ToDictionary(x => x.Key, x => x.Value);

            var result = new Dictionary<string, object>
            {
                { "field", field }, // 表单字段
                { "title", props["title"]}, // 标题属性移植到这
            };

            // 移除多余的标题
            props.Remove("title");
            // 表单类型
            switch (formInstance)
            {
                case InputPropsAttribute _:
                    result.Add("type", "input"); break;
                case SelectPropsAttribute _:
                    result.Add("type", "select"); break;
                default:
                    throw new NotImplementedException();
            }
            // 默认值
            var value = property.GetValue(dtoInstance);
            if (!VerifyHelper.IsDefaultValue(value))
            {
                result.Add("value", value);
            }
            // 验证规则
            var validatas = property.GetCustomAttributes<ValidateAttribute>();
            if (validatas?.Any() == true)
            {

                var validate = validatas.Select(
                    item => item.GetType().GetProperties()
                        .Where(x => x.Name != "TypeId" && !VerifyHelper.IsDefaultValue(x.GetValue(item)))
                        .ToDictionary(x => ConvertHelper.ToLowercaseFirstCharacter(x.Name), x => x.GetValue(item)));
                result.Add("validate", validate);
            }

            //  输入框多行文本 的 大小设置
            if (props.TryGetValue("type", out var input) && input.Equals("textarea") && formInstance is InputPropsAttribute)
            {
                var autosize = property.GetCustomAttribute<InputTextareaAutosizeAttribute>();
                if (autosize != null)
                    props.Add("autosize",
                        autosize.GetType()
                                .GetProperties()
                                .Where(x => x.Name != "TypeId" && !VerifyHelper.IsDefaultValue(x.GetValue(autosize)))
                                .ToDictionary(x => ConvertHelper.ToLowercaseFirstCharacter(x.Name), x => x.GetValue(autosize)));
            }
            // 下拉选项处理
            else if (formInstance is SelectPropsAttribute)
            {
                var selectOptions = new List<SelectOption>();

                var tmp = property.GetCustomAttributes<SelectOptionAttribute>()
                    ?.SelectMany(x =>
                    {
                        if (x.Enum == null)
                            return new List<SelectOption> { new SelectOption(x.Label, VerifyHelper.IsDefaultValue(x.Value) ? x.Label : x.Value) };
                        return GetSelectOptionsByEnum(x.Enum);
                    });
                if (tmp != null) selectOptions.AddRange(tmp);

                if (selectOptions.Any())
                    result.Add("options", selectOptions);
            }

            if (props.Count > 0) // 表单属性
                result.Add("props", props);

            return result;
        }

        private static IEnumerable<SelectOption> GetSelectOptionsByEnum(Type @enum)
        {
            if (@enum == null) throw new NullReferenceException(nameof(@enum));

            var enumerator = @enum.GetEnumValues().GetEnumerator();
            while (enumerator.MoveNext())
                yield return new SelectOption(enumerator.Current.ToString(), enumerator.Current.GetHashCode());
        }
    }

    class SelectOption
    {
        public SelectOption(string label, object value)
        {
            Label = label;
            Value = value;
        }

        public string Label { get; }
        public object Value { get; }
    }
}
