using System.Collections.Generic;
using MeasureMap.Diagnostics;
using MeasureMap.Threading;

namespace MeasureMap
{
    /// <summary>
    /// The context containing info to the execution run
    /// </summary>
    public class ExecutionContext : IExecutionContext
    {
        /// <summary>
        /// Default constructor for ExecutionContext
        /// </summary>
        public ExecutionContext() 
            : this(new ProfilerSettings())
        {
        }

        /// <summary>
        /// Default constructor for ExecutionContext
        /// </summary>
        public ExecutionContext(IProfilerSettings settings)
        {
            Settings = settings;
        }

        /// <summary>
        /// The data store for the context
        /// </summary>
        public IDictionary<string, object> SessionData { get; } = new Dictionary<string, object>();
        
        /// <summary>
        /// Gets the settings associated with the session
        /// </summary>
        public IProfilerSettings Settings { get; }

        /// <summary>
        /// Gets the logger associated with the session
        /// </summary>
        public ILogger Logger => Settings.Logger;
    }

    /// <summary>
    /// The extensions and logic for the execution context
    /// </summary>
    public static class ExecutionContextExtensions
    {
        /// <summary>
        /// Gets a value stored in the SessionData of the context
        /// </summary>
        /// <param name="context">The execution context</param>
        /// <param name="key">The key of the stored value</param>
        /// <returns></returns>
        public static object Get(this IExecutionContext context, string key)
        {
            key = key.ToLower();
            if (context.SessionData.ContainsKey(key))
            {
                var tmp = context.SessionData[key];
                return tmp;
            }

            return null;
        }

        /// <summary>
        /// Gets a value stored in the SessionData of the context
        /// </summary>
        /// <param name="context">The execution context</param>
        /// <param name="key">The key of the stored value</param>
        public static T Get<T>(this IExecutionContext context, string key)
        {
            key = key.ToLower();
            if (context.SessionData.ContainsKey(key))
            {
                var tmp = context.SessionData[key];
                return (T)tmp;
            }

            return default(T);
        }

        /// <summary>
        /// Sets a value to the SessionData of the context
        /// </summary>
        /// <param name="context">The execution context</param>
        /// <param name="key">The key of the stored value</param>
        /// <param name="value">The value to store</param>
        public static IExecutionContext Set(this IExecutionContext context, string key, object value)
        {
            key = key.ToLower();
            if (context.SessionData.ContainsKey(key))
            {
                context.SessionData[key] = value;
            }
            else
            {
                context.SessionData.Add(key, value);
            }

            return context;
        }

        /// <summary>
        /// Removes a value from the SessionData of the context
        /// </summary>
        /// <param name="context">The execution context</param>
        /// <param name="key">The key of the stored value</param>
        public static IExecutionContext Remove(this IExecutionContext context, string key)
        {
            key = key.ToLower();
            if (context.SessionData.ContainsKey(key))
            {
                context.SessionData.Remove(key);
            }

            return context;
        }

        /// <summary>
        /// Cleares all values from the SessionData of the context
        /// </summary>
        /// <param name="context">The execution context</param>
        public static IExecutionContext Clear(this IExecutionContext context)
        {
            context.SessionData.Clear();

            return context;
        }

        /// <summary>
        /// Clones the <see cref="IExecutionContext"/>
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static IExecutionContext Clone(this IExecutionContext context)
        {
            var child = new ExecutionContext(context.Settings);

            foreach(var item in context.SessionData)
            {
                child.Set(item.Key, item.Value);
            }

            return child;
        }
    }

    /// <summary>
    /// General Context keys
    /// </summary>
    public static class ContextKeys
    {
        /// <summary>
        /// Iteration
        /// </summary>
        public const string Iteration = "iteration";

        /// <summary>
        /// ThreadId
        /// </summary>
        public const string ThreadId = "threadid";

        /// <summary>
        /// The number of the thread that is created by MeasureMap. This is not the same as the ThreadId
        /// </summary>
        public const string ThreadNumber = "threadnumber";

        /// <summary>
        /// ProcessId
        /// </summary>
        public const string ProcessId = "processid";
    }
}
