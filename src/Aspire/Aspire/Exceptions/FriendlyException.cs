using System;

namespace Aspire.Exceptions
{
    /// <summary>
    ///     友好的异常.
    /// </summary>
    public class FriendlyException : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="FriendlyException" /> class.
        /// </summary>
        /// <param name="title">异常标题.</param>
        public FriendlyException(string title)
            : this(title, new string[0])
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="FriendlyException" /> class.
        /// </summary>
        /// <param name="title">异常标题.</param>
        /// <param name="innerException">内部异常.</param>
        public FriendlyException(string title, Exception innerException)
            : base(title, innerException)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="FriendlyException" /> class.
        /// </summary>
        /// <param name="title">异常标题.</param>
        /// <param name="messages">异常消息集合.</param>
        public FriendlyException(string title, params string[] messages)
            : this(FriendlyExceptionCode.BusinessException, title, messages)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="FriendlyException" /> class.
        /// </summary>
        /// <param name="code">异常 code.</param>
        /// <param name="title">异常标题.</param>
        /// <param name="messages">异常消息集合.</param>
        public FriendlyException(int code, string title, params string[] messages)
            : base(title)
        {
            Code = code;
            Title = title;
            Messages = messages;
        }

        /// <summary>
        ///     Gets code.
        /// </summary>
        public int Code { get; }

        /// <summary>
        ///     Gets Title.
        /// </summary>
        public string Title { get; }

        /// <summary>
        ///     Gets Messages.
        /// </summary>
        public string[] Messages { get; }
    }
}