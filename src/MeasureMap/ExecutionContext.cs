using System.Collections.Generic;

namespace MeasureMap
{
    public interface IExecutionContext
    {
        IDictionary<string, object> SessionData { get; }
    }

    public class ExecutionContext : IExecutionContext
    {
        public IDictionary<string, object> SessionData { get; } = new Dictionary<string, object>();
    }

    public static class ExecutionContextExtensions
    {
        public static object Get(this IExecutionContext context, string key)
        {
            if (context.SessionData.ContainsKey(key))
            {
                var tmp = context.SessionData[key];
                return tmp;
            }

            return null;
        }

        public static T Get<T>(this IExecutionContext context, string key)
        {
            if (context.SessionData.ContainsKey(key))
            {
                var tmp = context.SessionData[key];
                return (T)tmp;
            }

            return default(T);
        }

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
    }

    public static class ContextKeys
    {
        public const string Iteration = "iteration";
    }
}
