using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Aspire.Application.AppServices;
using Aspire.Utils;

namespace Aspire.DynamicForm
{
    class Util
    {
        public static Dictionary<string, object> DefaultFormat<T>(PropertyInfo property, object dtoInstance, AspireFormAttribute formInstance)
            where T : AspireFormAttribute
        {
            // 首字符小写
            var field = ConvertHelper.ToLowercaseFirstCharacter(property.Name);

            var props = formInstance.GetType().GetProperties()
                .Where(x => !VerifyHelper.IsDefaultValue(x.GetValue(formInstance)) && x.Name != "TypeId")
                .ToDictionary(x => ConvertHelper.ToLowercaseFirstCharacter(x.Name), x => x.GetValue(formInstance))
                .ToDictionary(x => x.Key, x => x.Value);

            // 特殊处理 输入框多行文本 的 大小设置
            if (props.TryGetValue("type", out var type) && type.Equals("textarea") && formInstance is InputPropsAttribute)
            {
                var autosize = property.GetCustomAttribute<InputTextareaAutosizeAttribute>();
                if (autosize != null)
                    props.Add("autosize",
                        autosize.GetType()
                                .GetProperties()
                                .Where(x => x.Name != "TypeId")
                                .Where(x => x.Name != "Title")
                                .ToDictionary(x => ConvertHelper.ToLowercaseFirstCharacter(x.Name), x => x.GetValue(autosize)));
            }

            string formItemType;
            switch (formInstance)
            {
                case InputPropsAttribute _:
                    formItemType = "input"; break;
                case SelectPropsAttribute _:
                    formItemType = "select"; break;
                default:
                    throw new NotImplementedException();
            }

            var result = new Dictionary<string, object>
            {
                { "type", formItemType }, // 表单类型
                { "field", field }, // 表单字段
                { "title", props["title"]}, // 标题属性移植到这
                { "props", props }, // 表单属性
            };

            var value = property.GetValue(dtoInstance);
            if (!VerifyHelper.IsDefaultValue(value))
            {
                result.Add("value", value);
            }

            return result;
        }
    }
}
