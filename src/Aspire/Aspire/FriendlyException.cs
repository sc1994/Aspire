using System;
using System.Linq;

namespace Aspire
{
    /// <summary>
    ///     友好的异常.
    /// </summary>
    public class FriendlyException : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="FriendlyException" /> class.
        /// </summary>
        /// <param name="messages">消息集合.</param>
        public FriendlyException(params string[] messages)
            : base(messages?.FirstOrDefault())
        {
            Messages = messages ?? throw new ArgumentNullException(nameof(messages));
        }

        /// <summary>
        ///     Gets 消息集合.
        /// </summary>
        public string[] Messages { get; }
    }
}