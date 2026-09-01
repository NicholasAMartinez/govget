using System.Globalization;

namespace GovGet.Core.Models;

/// <summary>
/// Represents supported query parameters for the USGS Earthquake Catalog API.
/// </summary>
public sealed class UsgsEarthquakeQuery
{
    private static readonly string[] Iso8601Formats =
    [
        "yyyy-MM-dd",
        "yyyy-MM-dd'T'HH:mm",
        "yyyy-MM-dd'T'HH:mm:ss",
        "yyyy-MM-dd'T'HH:mm:ss.FFFFFFF",
        "yyyy-MM-dd'T'HH:mm'Z'",
        "yyyy-MM-dd'T'HH:mm:ss'Z'",
        "yyyy-MM-dd'T'HH:mm:ss.FFFFFFF'Z'",
        "yyyy-MM-dd'T'HH:mmzzz",
        "yyyy-MM-dd'T'HH:mm:sszzz",
        "yyyy-MM-dd'T'HH:mm:ss.FFFFFFFzzz"
    ];

    /// <summary>
    /// Gets the earliest event time to include.
    /// </summary>
    public DateTimeOffset? StartTime { get; private set; }

    /// <summary>
    /// Gets the latest event time to include.
    /// </summary>
    public DateTimeOffset? EndTime { get; private set; }

    /// <summary>
    /// Gets the maximum number of events to return.
    /// </summary>
    public int? Limit { get; private set; }

    /// <summary>
    /// Gets the minimum event magnitude to include.
    /// </summary>
    public double? MinMagnitude { get; private set; }

    /// <summary>
    /// Gets whether any query parameters have been set.
    /// </summary>
    public bool HasParameters =>
        StartTime.HasValue
        || EndTime.HasValue
        || Limit.HasValue
        || MinMagnitude.HasValue;

    /// <summary>
    /// Parses USGS earthquake query options from command-line arguments.
    /// </summary>
    /// <param name="args">The arguments following the USGS operation.</param>
    /// <returns>A validated USGS earthquake query.</returns>
    /// <exception cref="ArgumentException">
    /// Thrown when an option is unknown, duplicated, missing a value, or invalid.
    /// </exception>
    public static UsgsEarthquakeQuery Parse(string[] args)
    {
        var query = new UsgsEarthquakeQuery();
        var seenOptions = new HashSet<string>(StringComparer.Ordinal);

        for (int index = 0; index < args.Length; index++)
        {
            string option = args[index];

            switch (option)
            {
                case "--starttime":
                    EnsureNotDuplicate(seenOptions, option);
                    query.StartTime = ParseDateTimeOffset(
                        ReadValue(args, ref index, option),
                        option
                    );
                    break;

                case "--endtime":
                    EnsureNotDuplicate(seenOptions, option);
                    query.EndTime = ParseDateTimeOffset(
                        ReadValue(args, ref index, option),
                        option
                    );
                    break;

                case "--limit":
                    EnsureNotDuplicate(seenOptions, option);
                    query.Limit = ParseLimit(
                        ReadValue(args, ref index, option),
                        option
                    );
                    break;

                case "--minmagnitude":
                    EnsureNotDuplicate(seenOptions, option);
                    query.MinMagnitude = ParseMinMagnitude(
                        ReadValue(args, ref index, option),
                        option
                    );
                    break;

                default:
                    throw new ArgumentException($"Unknown USGS option: {option}");
            }
        }

        return query;
    }

    /// <summary>
    /// Converts the populated values into URL-encoded USGS query parameters.
    /// </summary>
    /// <returns>Encoded query parameters without a leading question mark.</returns>
    public string ToQueryString()
    {
        var parameters = new List<string>();

        if (StartTime.HasValue)
        {
            AddParameter(
                parameters,
                "starttime",
                StartTime.Value.ToString("O", CultureInfo.InvariantCulture)
            );
        }

        if (EndTime.HasValue)
        {
            AddParameter(
                parameters,
                "endtime",
                EndTime.Value.ToString("O", CultureInfo.InvariantCulture)
            );
        }

        if (Limit.HasValue)
        {
            AddParameter(
                parameters,
                "limit",
                Limit.Value.ToString(CultureInfo.InvariantCulture)
            );
        }

        if (MinMagnitude.HasValue)
        {
            AddParameter(
                parameters,
                "minmagnitude",
                MinMagnitude.Value.ToString("R", CultureInfo.InvariantCulture)
            );
        }

        return string.Join("&", parameters);
    }

    private static void EnsureNotDuplicate(
        HashSet<string> seenOptions,
        string option)
    {
        if (!seenOptions.Add(option))
        {
            throw new ArgumentException($"USGS option specified more than once: {option}");
        }
    }

    private static string ReadValue(
        string[] args,
        ref int index,
        string option)
    {
        if (index + 1 >= args.Length
            || args[index + 1].StartsWith("--", StringComparison.Ordinal))
        {
            throw new ArgumentException($"Missing value for USGS option: {option}");
        }

        index++;
        return args[index];
    }

    private static DateTimeOffset ParseDateTimeOffset(
        string value,
        string option)
    {
        if (!DateTimeOffset.TryParseExact(
                value,
                Iso8601Formats,
                CultureInfo.InvariantCulture,
                DateTimeStyles.AssumeUniversal,
                out DateTimeOffset result))
        {
            throw new ArgumentException(
                $"Invalid value for {option}: '{value}'. Expected an ISO 8601 date."
            );
        }

        return result;
    }

    private static int ParseLimit(string value, string option)
    {
        if (!int.TryParse(
                value,
                NumberStyles.Integer,
                CultureInfo.InvariantCulture,
                out int result)
            || result <= 0)
        {
            throw new ArgumentException(
                $"Invalid value for {option}: '{value}'. Expected a positive integer."
            );
        }

        return result;
    }

    private static double ParseMinMagnitude(string value, string option)
    {
        if (!double.TryParse(
                value,
                NumberStyles.Float,
                CultureInfo.InvariantCulture,
                out double result)
            || !double.IsFinite(result))
        {
            throw new ArgumentException(
                $"Invalid value for {option}: '{value}'. Expected a finite number."
            );
        }

        return result;
    }

    private static void AddParameter(
        List<string> parameters,
        string name,
        string value)
    {
        parameters.Add(
            $"{Uri.EscapeDataString(name)}={Uri.EscapeDataString(value)}"
        );
    }
}
