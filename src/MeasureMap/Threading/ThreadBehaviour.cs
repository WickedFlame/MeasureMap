
namespace MeasureMap
{
    /// <summary>
    /// Define the behaviour of threading to use. This can influence the way the benchmarks run with dependencies outside the benchmark.
    /// </summary>
    public enum ThreadBehaviour
    {
        /// <summary>
        /// Use System.Threading.Thread to run benchmarks
        /// </summary>
        Thread,

        /// <summary>
        /// Use System.Threading.Tasks.Task to run benchmarks
        /// </summary>
        Task,

        /// <summary>
        /// All tasks will be run on the main thread. This option is ignored when using multiple threads.
        /// </summary>
        RunOnMainThread
    }
}
