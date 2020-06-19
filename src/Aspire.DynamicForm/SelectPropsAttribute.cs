using System;
using System.Reflection;

using Aspire.Application.AppServices;

namespace Aspire.DynamicForm
{
    public class SelectOptionAttribute : Attribute 
    {
    }

    public class SelectPropsAttribute : AspireFormAttribute
    {
        public SelectPropsAttribute(string title) : base(title)
        {
        }

        /// <summary>
        /// 是否多选
        /// <para> 默认值: false</para>
        /// </summary>
        public bool Multiple { get; set; }
        /// <summary>
        /// 是否禁用
        /// <para> 默认值: false</para>
        /// </summary>
        public bool Disabled { get; set; }
        /// <summary>
        /// 作为 value 唯一标识的键名，绑定值为对象类型时必填
        /// <para> 默认值: value</para>
        /// </summary>
        public string ValueKey { get; set; }
        /// <summary>
        /// 输入框尺寸
        /// <para> 默认值: </para>
        /// <para> 可选值: medium/small/mini</para>
        /// </summary>
        public string Size { get; set; }
        /// <summary>
        /// 是否可以清空选项
        /// <para> 默认值: false</para>
        /// </summary>
        public bool Clearable { get; set; }
        /// <summary>
        /// 多选时是否将选中值按文字的形式展示
        /// <para> 默认值: false</para>
        /// </summary>
        public bool CollapseTags { get; set; }
        /// <summary>
        /// 多选时用户最多可以选择的项目数，为 0 则不限制
        /// <para> 默认值: 0</para>
        /// </summary>
        public int MultipleLimit { get; set; }
        /// <summary>
        /// select input 的 name 属性
        /// <para> 默认值: null</para>
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// select input 的 autocomplete 属性
        /// <para> 默认值: off</para>
        /// </summary>
        public string Autocomplete { get; set; }
        /// <summary>
        /// 占位符
        /// <para> 默认值: 请选择</para>
        /// </summary>
        public string Placeholder { get; set; }
        /// <summary>
        /// 是否可搜索
        /// <para> 默认值: false</para>
        /// </summary>
        public bool Filterable { get; set; }
        /// <summary>
        /// 是否允许用户创建新条目，需配合filterable 使用
        /// <para> 默认值: false</para>
        /// </summary>
        public bool AllowCreate { get; set; }
        /// <summary>
        /// 自定义搜索方法 
        /// <para> 默认值: null</para>
        /// </summary>
        public string FilterMethod { get; set; }
        /// <summary>
        /// 是否为远程搜索
        /// <para> 默认值: false</para>
        /// </summary>
        public bool Remote { get; set; }
        /// <summary>
        /// 远程搜索方法
        /// <para> 默认值: null</para>
        /// </summary>
        public string RemoteMethod { get; set; }
        /// <summary>
        /// 是否正在从远程获取数据
        /// <para> 默认值: false</para>
        /// </summary>
        public bool Loading { get; set; }
        /// <summary>
        /// 远程加载时显示的文字
        /// <para> 默认值: 加载中</para>
        /// </summary>
        public string LoadingText { get; set; }
        /// <summary>
        /// 搜索条件无匹配时显示的文字
        /// <para> 默认值: 无匹配数据</para>
        /// </summary>
        public string NoMatchText { get; set; }
        /// <summary>
        /// 选项为空时显示的文字
        /// <para> 默认值: 无数据</para>
        /// </summary>
        public string NoDataText { get; set; }
        /// <summary>
        /// Select 下拉框的类名
        /// <para> 默认值: null</para>
        /// </summary>
        public string PopperClass { get; set; }
        /// <summary>
        /// 多选且可搜索时，是否在选中一个选项后保留当前的搜索关键词
        /// <para> 默认值: false</para>
        /// </summary>
        public bool ReserveKeyword { get; set; }
        /// <summary>
        /// 在输入框按下回车，选择第一个匹配项。需配合 filterable或 remote 使用
        /// <para> 默认值: false</para>
        /// </summary>
        public bool DefaultFirstOption { get; set; }
        /// <summary>
        /// 是否将弹出框插入至 body 元素。在弹出框的定位出现问题时，可将该属性设置为
        /// <para> 默认值: true</para>
        /// </summary>
        public bool PopperAppendToBody { get; set; }
        /// <summary>
        /// 对于不可搜索的 Select，是否在输入框获得焦点后自动弹出选项菜单
        /// <para> 默认值: false</para>
        /// </summary>
        public bool AutomaticDropdown { get; set; }

        public override object Format(PropertyInfo property, object dtoInstance = null)
        {
            throw new NotImplementedException();
        }
    }
}
