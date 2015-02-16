using System;
using System.Collections.Generic;

namespace Hal.WebApi.Formatter
{
    public class HalNodes<T> : List<T> where T : HalNode
    {
        /// <summary>
        /// Accesses the node by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T this[string key]
        {
            get { return base.Find(i => i.Key == key); }
        }

        /// <summary>
        /// Adds a node to the collection
        /// </summary>
        /// <param name="node"></param>
        public new void Add(T node)
        {
            if (node != null && node.Key != null)
            {
                if (base.Exists(i => i.Key == node.Key))
                    throw new InvalidOperationException("Another node with the same key already exists");
            }

            base.Add(node);
        }
    }

    public class HalNodes : HalNodes<HalNode>
    {
        /// <summary>
        /// Adds to the list
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public virtual void Add(string key, object value)
        {
            this.Add(new HalNode(key, value));
        }
    }
}
