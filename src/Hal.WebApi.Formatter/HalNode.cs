using System;

namespace Hal.WebApi.Formatter
{
    /// <summary>
    /// Represents a single Key Value pair node
    /// </summary>
    public class HalNode
    {
        private readonly string key;
        private readonly object value;

        /// <summary>
        /// Represents a single Key Value pair node
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public HalNode(string key, object value)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Input cannot be empty", "key");

            this.key = key;
            this.value = value;
        }

        public string Key
        {
            get { return this.key; }
        }

        public object Value
        {
            get { return this.value; }
        }
    }

    public class HalNode<T> : HalNode where T : class
    {
        /// <summary>
        /// Represents a single Key Value pair node
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public HalNode(string key, T value)
            : base(key, value)
        {

        }

        public new T Value
        {
            get { return base.Value as T; }
        }
    }
}
