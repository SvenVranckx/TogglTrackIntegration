using System.Text.Json.Serialization;

namespace Toggl.Track.SDK.Models
{
    public class Preferences
    {
        [JsonPropertyName("send_product_emails")]
        public bool SendProductEmails { get; set; }

        [JsonPropertyName("send_timer_notifications")]
        public bool SendTimerNotifications { get; set; }

        [JsonPropertyName("date_format")]
        public string? DateFormat { get; set; }

        [JsonPropertyName("timeofday_format")]
        public string? TimeOfDayFormat { get; set; }

        [JsonPropertyName("duration_format")]
        public string? DurationFormat { get; set; }

        [JsonPropertyName("record_timeline")]
        public bool RecordTimeline { get; set; }

        [JsonPropertyName("send_weekly_report")]
        public bool SendWeeklyReport { get; set; }

        [JsonPropertyName("pg_time_zone_name")]
        public string? PgTimeZoneName { get; set; }

        [JsonPropertyName("alpha_features")]
        public AlphaFeature[]? AlphaFeatures { get; set; }
    }

    public class AlphaFeature
    {
        [JsonPropertyName("code")]
        public string? Code { get; set; }

        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; }

        public override string? ToString() => $"{Code}: {Enabled}";
    }
}
