using MeasureMap.Tracers;
using MeasureMap.Tracers.Metrics;
using MeasureMap.Verification;
using System.IO;
using System.Runtime.CompilerServices;

namespace MeasureMap
{
    /// <summary>
    /// Extension methods for the IBenchmarkResult
    /// </summary>
    public static class BenchmarkResultExtensions
    {
        /// <summary>
        /// Gets the Markdown representation of a Benchmark Test
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static string ToMarkdown(this IBenchmarkResult result)
        {
            var writer = new StringResultWriter();
            result.Trace(writer);
            return writer.Value;
        }

        /// <summary>
        /// Saves the Benchmarkresult as a Markdown file. The default file name is the name of the source file with the extension .benchmark.md
        /// </summary>
        /// <param name="result"></param>
        /// <param name="fileName"></param>
        /// <param name="sourceFilePath"></param>
        /// <param name="overwrite"></param>
        /// <returns></returns>
        public static IBenchmarkResult SaveMarkdown(this IBenchmarkResult result, string fileName = "", [CallerFilePath] string sourceFilePath = "", bool overwrite = false)
        {
            var md = result.ToMarkdown();
            var filePath = fileName.GetSavePath(sourceFilePath);
            if (!filePath.EndsWith(".benchmark.md"))
            {
                filePath += ".benchmark.md";
            }

            if (File.Exists(filePath))
            {
                if (!overwrite)
                {
                    return result;
                }

                File.Delete(filePath);
            }

            File.WriteAllText(filePath, md);

            return result;
        }

        /// <summary>
        /// Save the Benchmarkresult as a YAML file. The default file name is the name of the source file with the extension .benchmark.yaml
        /// </summary>
        /// <param name="result"></param>
        /// <param name="fileName"></param>
        /// <param name="sourceFilePath"></param>
        /// <returns></returns>
        public static IBenchmarkResult Save(this IBenchmarkResult result, string fileName = "", [CallerFilePath] string sourceFilePath = "")
        {
            var filePath = fileName.GetSavePath(sourceFilePath);
            if (!filePath.EndsWith(".yml"))
            {
                filePath += ".benchmark.yml";
            }

            if (File.Exists(filePath))
            {
                return result;
            }

            var serializer = new Serialization.YamlSerializer();
            serializer.Serialize(result, filePath);

            return result;
        }

        /// <summary>
        /// Verify the Benchmarkresult against the saved baseline result.
        /// </summary>
        /// <param name="result"></param>
        /// <param name="fileName"></param>
        /// <param name="thresholdPercentage"></param>
        /// <param name="noiseFloorMilliseconds"></param>
        /// <param name="sourceFilePath"></param>
        /// <returns></returns>
        [AssertionMethod]
        public static IBenchmarkResult Verify(this IBenchmarkResult result, string fileName = "", double thresholdPercentage = 10, double noiseFloorMilliseconds = 1, [CallerFilePath] string sourceFilePath = "")
        {
            var filePath = fileName.GetSavePath(sourceFilePath);
            if (!filePath.EndsWith(".benchmark.yml"))
            {
                filePath += ".benchmark.yml";
            }

            result.Save(filePath);


            //var last = YamlMap.Serializer.DeserializeFromFile<BenchmarkModel>(filePath);
            var serializer = new MeasureMap.Serialization.YamlSerializer();
            var last = serializer.Deserialize(filePath);


            var verifier = new BemchmarkVerifier();
            verifier.Verify(result, last, filePath, thresholdPercentage, noiseFloorMilliseconds);

            return result;
        }

        /// <summary>
        /// Traces the output of a Benchmark Test
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static void Trace(this IBenchmarkResult result)
        {
            result.Trace(TraceOptions.Default.Tracer, TraceOptions.Default.ResultWriter, TraceOptions.Default.Clone());
        }

        /// <summary>
        /// Traces the output of a Benchmark Test
        /// </summary>
        /// <param name="result"></param>
        /// <param name="metrics"></param>
        /// <returns></returns>
        public static void Trace(this IBenchmarkResult result, TraceMetrics metrics)
        {
            var options = TraceOptions.Default.Clone();
            options.Metrics = metrics;
            result.Trace(options);
        }

        /// <summary>
        /// Trace the output of a Benchmark Test
        /// </summary>
        /// <param name="result"></param>
        /// <param name="options"></param>
        public static void Trace(this IBenchmarkResult result, TraceOptions options)
        {
            result.Trace(options.Tracer, options.ResultWriter, options);
        }

        /// <summary>
        /// Trace the output of a Benchmark Test
        /// </summary>
        /// <param name="result"></param>
        /// <param name="tracer"></param>
        public static void Trace(this IBenchmarkResult result, ITracer tracer)
        {
            result.Trace(tracer, TraceOptions.Default.ResultWriter, TraceOptions.Default.Clone());
        }

        /// <summary>
        /// Trace the output of a Benchmark Test
        /// </summary>
        /// <param name="result"></param>
        /// <param name="writer"></param>
        public static void Trace(this IBenchmarkResult result, IResultWriter writer)
        {
            result.Trace(TraceOptions.Default.Tracer, writer, TraceOptions.Default.Clone());
        }

        /// <summary>
        /// Trace the output of a Benchmark Test
        /// </summary>
        /// <param name="result"></param>
        /// <param name="tracer"></param>
        /// <param name="writer"></param>
        public static void Trace(this IBenchmarkResult result, ITracer tracer, IResultWriter writer)
        {
            result.Trace(tracer, writer, TraceOptions.Default.Clone());
        }

        /// <summary>
        /// Trace the output of a Benchmark Test
        /// </summary>
        /// <param name="result"></param>
        /// <param name="tracer"></param>
        /// <param name="writer"></param>
        /// <param name="options"></param>
        public static void Trace(this IBenchmarkResult result, ITracer tracer, IResultWriter writer, TraceOptions options)
        {
            options.Metrics ??= BenchmarkTraceMetrics.GetDefaultTraceMetrics();

            tracer.Trace(result, writer, options);
        }

        private static string GetSavePath(this string fileName, [CallerFilePath] string sourceFilePath = "")
        {
            var directory = Path.GetDirectoryName(sourceFilePath) ?? string.Empty;
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = Path.GetFileNameWithoutExtension(sourceFilePath);
            }

            return Path.Combine(directory, fileName);
        }
    }
}
