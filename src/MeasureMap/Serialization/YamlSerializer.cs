using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace MeasureMap.Serialization
{
    /// <summary>
    /// Serializes a <see cref="BenchmarkModel"/> to a YAML string.
    /// </summary>
    public class YamlSerializer
    {
        /// <summary>
        /// Reads a YAML file and deserializes it to a <see cref="BenchmarkModel"/>.
        /// </summary>
        /// <param name="path">The path to the YAML file</param>
        /// <returns>The deserialized model</returns>
        public BenchmarkModel Deserialize(string path)
        {
            var yaml = File.ReadAllText(path);
            return DeserializeContent(yaml);
        }

        /// <summary>
        /// Deserializes a YAML string produced by <see cref="Serialize(BenchmarkModel)"/> to a <see cref="BenchmarkModel"/>.
        /// </summary>
        /// <param name="yaml">The YAML content</param>
        /// <returns>The deserialized model</returns>
        public BenchmarkModel DeserializeContent(string yaml)
        {
            var model = new BenchmarkModel();

            if (string.IsNullOrEmpty(yaml))
            {
                return model;
            }

            var lines = yaml.Replace("\r\n", "\n").Replace("\r", "\n").Split('\n');

            Benchmark current = null;

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                var trimmed = line.TrimStart();
                var isListItem = trimmed.StartsWith("- ");
                if (isListItem)
                {
                    trimmed = trimmed.Substring(2).TrimStart();
                }

                var separator = trimmed.IndexOf(':');
                if (separator < 0)
                {
                    continue;
                }

                var key = trimmed.Substring(0, separator).Trim();
                var value = trimmed.Substring(separator + 1).Trim();

                if (key == "benchmarks")
                {
                    continue;
                }

                if (isListItem && key == "name")
                {
                    current = new Benchmark { Name = Unquote(value) };
                    model.Benchmarks.Add(current);
                    continue;
                }

                if (current == null)
                {
                    switch (key)
                    {
                        case "iterations":
                            model.Iterations = ParseInt(value);
                            break;
                        case "duration":
                            model.Duration = ParseTimeSpan(value);
                            break;
                    }

                    continue;
                }

                switch (key)
                {
                    case "name":
                        current.Name = Unquote(value);
                        break;
                    case "threadId":
                        current.ThreadId = ParseInt(value);
                        break;
                    case "threadNumber":
                        current.ThreadNumber = ParseInt(value);
                        break;
                    case "averageTicks":
                        current.AverageTicks = ParseLong(value);
                        break;
                    case "averageMilliseconds":
                        current.AverageMilliseconds = ParseDouble(value);
                        break;
                    case "averageTime":
                        current.AverageTime = ParseTimeSpan(value);
                        break;
                    case "totalTime":
                        current.TotalTime = ParseTimeSpan(value);
                        break;
                    case "fastest":
                        current.Fastest = ParseTimeSpan(value);
                        break;
                    case "slowest":
                        current.Slowest = ParseTimeSpan(value);
                        break;
                    case "increase":
                        current.Increase = ParseLong(value);
                        break;
                    case "initialSize":
                        current.InitialSize = ParseLong(value);
                        break;
                    case "endSize":
                        current.EndSize = ParseLong(value);
                        break;
                }
            }

            return model;
        }

        /// <summary>
        /// Serializes a <see cref="BenchmarkModel"/> to a YAML string.
        /// </summary>
        /// <param name="result"></param>
        /// <param name="fileName"></param>
        public void Serialize(IBenchmarkResult result, string fileName)
        {
            var items = new BenchmarkModel
            {
                Iterations = result.Iterations,
                Duration = result.Duration
            };

            foreach (var key in result.Keys)
            {
                var res = result[key];
                items.Benchmarks.Add(new Benchmark
                {
                    Name = key,
                    ThreadId = res.ThreadId,
                    ThreadNumber = res.ThreadNumber,
                    AverageTicks = res.AverageTicks,
                    AverageMilliseconds = res.AverageTicks.ToMilliseconds(),
                    AverageTime = res.AverageTime,
                    TotalTime = res.TotalTime,
                    Fastest = res.Fastest.Duration,
                    Slowest = res.Slowest.Duration,
                    Increase = res.Increase,
                    InitialSize = res.InitialSize,
                    EndSize = res.EndSize
                });
            }

            var yaml = Serialize(items);

            File.WriteAllText(fileName, yaml);
        }

        /// <summary>
        /// Serializes the <see cref="BenchmarkModel"/> to a simple YAML representation.
        /// </summary>
        /// <param name="model">The model to serialize</param>
        /// <returns>The YAML string</returns>
        public string Serialize(BenchmarkModel model)
        {
            var sb = new StringBuilder();

            AppendLine(sb, 0, "iterations", model.Iterations);
            AppendLine(sb, 0, "duration", model.Duration);

            sb.AppendLine("benchmarks:");

            foreach (var benchmark in model.Benchmarks)
            {
                AppendListItem(sb, 1, "name", benchmark.Name);
                AppendLine(sb, 2, "threadId", benchmark.ThreadId);
                AppendLine(sb, 2, "threadNumber", benchmark.ThreadNumber);
                AppendLine(sb, 2, "averageTicks", benchmark.AverageTicks);
                AppendLine(sb, 2, "averageMilliseconds", benchmark.AverageMilliseconds);
                AppendLine(sb, 2, "averageTime", benchmark.AverageTime);
                AppendLine(sb, 2, "totalTime", benchmark.TotalTime);
                AppendLine(sb, 2, "fastest", benchmark.Fastest);
                AppendLine(sb, 2, "slowest", benchmark.Slowest);
                AppendLine(sb, 2, "increase", benchmark.Increase);
                AppendLine(sb, 2, "initialSize", benchmark.InitialSize);
                AppendLine(sb, 2, "endSize", benchmark.EndSize);
            }

            return sb.ToString();
        }

        private static void AppendLine(StringBuilder sb, int indent, string key, object value)
        {
            sb.Append(new string(' ', indent * 2));
            sb.Append(key);
            sb.Append(": ");
            sb.AppendLine(FormatValue(value));
        }

        private static void AppendListItem(StringBuilder sb, int indent, string key, object value)
        {
            sb.Append(new string(' ', indent * 2));
            sb.Append("- ");
            sb.Append(key);
            sb.Append(": ");
            sb.AppendLine(FormatValue(value));
        }

        private static string FormatValue(object value)
        {
            switch (value)
            {
                case null:
                    return "null";
                case string s:
                    return QuoteIfNeeded(s);
                case bool b:
                    return b ? "true" : "false";
                case TimeSpan ts:
                    return QuoteIfNeeded(ts.ToString());
                case IFormattable formattable:
                    return formattable.ToString(null, CultureInfo.InvariantCulture);
                default:
                    return QuoteIfNeeded(value.ToString());
            }
        }

        private static string QuoteIfNeeded(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return "\"\"";
            }

            var needsQuotes = value.IndexOfAny(new[] { ':', '#', '-', '{', '}', '[', ']', ',', '\'', '"', '\n' }) >= 0
                || value[0] == ' '
                || value[value.Length - 1] == ' ';

            if (!needsQuotes)
            {
                return value;
            }

            return "\"" + value.Replace("\\", "\\\\").Replace("\"", "\\\"") + "\"";
        }

        private static string Unquote(string value)
        {
            if (value == null)
            {
                return null;
            }

            if (value == "null")
            {
                return null;
            }

            if (value.Length >= 2 && value[0] == '"' && value[value.Length - 1] == '"')
            {
                var inner = value.Substring(1, value.Length - 2);
                return inner.Replace("\\\"", "\"").Replace("\\\\", "\\");
            }

            return value;
        }

        private static int ParseInt(string value)
        {
            return int.Parse(Unquote(value), CultureInfo.InvariantCulture);
        }

        private static long ParseLong(string value)
        {
            return long.Parse(Unquote(value), CultureInfo.InvariantCulture);
        }

        private static double ParseDouble(string value)
        {
            return double.Parse(Unquote(value), CultureInfo.InvariantCulture);
        }

        private static TimeSpan ParseTimeSpan(string value)
        {
            return TimeSpan.Parse(Unquote(value), CultureInfo.InvariantCulture);
        }
    }
}
