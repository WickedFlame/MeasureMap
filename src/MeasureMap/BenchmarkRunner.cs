using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MeasureMap
{
    /// <summary>
    /// Runs multiple sessions to create benchmarks
    /// </summary>
    public class BenchmarkRunner
    {
        private readonly ProfilerSettings _settings;
        private readonly Dictionary<string, ProfilerSession> _sessions;

        /// <summary>
        /// Creates an instance of the BenchmaekRunner
        /// </summary>
        public BenchmarkRunner()
        {
            _settings = new ProfilerSettings();
            _sessions = new Dictionary<string, ProfilerSession>();
        }

        /// <summary>
        /// Gets the settings for the benchmarks
        /// </summary>
        public ProfilerSettings Settings => _settings;

        /// <summary>
        /// Add a new session to run benchmarks against
        /// </summary>
        /// <param name="name"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public BenchmarkRunner AddSession(string name, ProfilerSession session)
        {
            _sessions.Add(name, session);

            return this;
        }

        /// <summary>
        /// Run all sessions and benchmarks
        /// </summary>
        /// <returns></returns>
        public IBenchmarkResult RunSessions()
        {
            var results = new BenchmarkResult(_settings);

            foreach (var key in _sessions.Keys)
            {
                _settings.Logger.Write($"Starting Benchmarks for {key}", Diagnostics.LogLevel.Info, nameof(BenchmarkRunner));

                var session = _sessions[key];
                session.SetSettings(_settings);
                var result = session.RunSession();
                results.Add(key, result);
            }

            return results;
        }

        public IBenchmarkResult RunSession<T>()
        {

            var instance = Activator.CreateInstance<T>();

            var tmp = typeof(T).GetMethods().FirstOrDefault(m => m.GetCustomAttribute<OnStartPipelineAttribute>() != null);
            Action onStart = tmp != null ? () => tmp.Invoke(instance, null) : null;
            
            var tmp2 = typeof(T).GetMethods().FirstOrDefault(m => m.GetCustomAttribute<OnEndPipelineAttribute>() != null);
            Action onEnd = tmp2 != null ? () => tmp2.Invoke(instance, null) : null;
            
            var durationAttr = typeof(T).GetCustomAttribute<DurationAttribute>();
            if (durationAttr != null)
            {
                this.SetDuration(TimeSpan.FromSeconds(durationAttr.Duration));
            }

            var iterationAttr = typeof(T).GetCustomAttribute<IterationsAttribute>();
            if (iterationAttr != null)
            {
                this.SetIterations(iterationAttr.Iterations);
            }
            
            var threadsAttr = typeof(T).GetCustomAttribute<ThreadsAttribute>();
            
            var methods = typeof(T).GetMethods();
            foreach(var method in methods)
            {
                if (method.GetCustomAttribute<BenchmarkAttribute>() != null)
                {

                    var session = ProfilerSession.StartSession()
                        .AppendSettings(Settings);

                    if (onStart != null)
                    {
                        session.OnStartPipeline(s =>
                        {
                            onStart.Invoke();
                            return new ExecutionContext();
                        });
                    }

                    if (onEnd != null)
                    {
                        session.OnEndPipeline(e =>
                        {
                            onEnd.Invoke();
                        });
                    }

                    if (threadsAttr != null)
                    {
                        session.SetThreads(threadsAttr.Threads);
                    }
                    

                    if (method.GetParameters().Any(p => p.ParameterType == typeof(IExecutionContext)))
                    {
                        session.Task(ctx => method.Invoke(instance, [ctx]));
                    }
                    else
                    {

                        session.Task(() => method.Invoke(instance, null));
                    }
                    
                    AddSession(method.Name, session);
                }
            }

            return RunSessions();
        }
    }
}
