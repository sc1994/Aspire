using System;

namespace Aspire
{
    /// <summary>
    /// 注入到.
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false)]
    public class InjectToAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InjectToAttribute"/> class.
        /// </summary>
        public InjectToAttribute()
            : this(Lifecycle.Scoped)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InjectToAttribute"/> class.
        /// </summary>
        /// <param name="lifecycle">指定生命周期.</param>
        public InjectToAttribute(Lifecycle lifecycle)
        {
            Lifecycle = lifecycle;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InjectToAttribute"/> class.
        /// </summary>
        /// <param name="lifecycle">指定生命周期.</param>
        /// <param name="realizeType">指定实现类型(比如将此特性放置在接口上, 则需要声明此接口如何实现).</param>
        public InjectToAttribute(Lifecycle lifecycle, Type realizeType)
             : this(lifecycle)
        {
            ImplementationInstance = realizeType;
        }

        /// <summary>
        /// Gets 实现类型.
        /// </summary>
        public Type? ImplementationInstance { get; }

        /// <summary>
        /// Gets 生命周期.
        /// </summary>
        public Lifecycle Lifecycle { get; }
    }

    /// <summary>
    /// 生命周期.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1201:Elements should appear in the correct order", Justification = "减少无意义的枚举文件")]
    public enum Lifecycle
    {
        /// <summary>
        /// 单例.
        /// </summary>
        Singleton,

        /// <summary>
        /// 范围.
        /// </summary>
        Scoped,

        /// <summary>
        /// 瞬态.
        /// </summary>
        Transient
    }
}
