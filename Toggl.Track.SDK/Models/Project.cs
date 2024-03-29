using System.Text.Json.Serialization;

namespace Toggl.Track.SDK.Models
{
    public class Project : ConfigurationEntity
    {
        [JsonPropertyName("active")]
        public bool Active { get; set; }

        [JsonPropertyName("actual_hours")]
        public int? ActualHours { get; set; }

        [JsonPropertyName("actual_seconds")]
        public int? ActualSeconds { get; set; }

        [JsonPropertyName("auto_estimates")]
        public bool? AutoEstimates { get; set; }

        [JsonPropertyName("billable")]
        public bool? Billable { get; set; }

        [JsonPropertyName("client_id")]
        public long ClientId { get; set; }

        [JsonPropertyName("color")]
        public string? Color { get; set; }

        [JsonPropertyName("created_at")]
        public DateTimeOffset? CreatedAt { get; set; }

        [JsonPropertyName("currency")]
        public string? Currency { get; set; }

        [JsonPropertyName("current_period")]
        public Period? CurrentPeriod { get; set; }

        [JsonPropertyName("end_date")]
        public DateTimeOffset? EndDate { get; set; }

        [JsonPropertyName("estimated_hours")]
        public int? EstimatedHours { get; set; }

        [JsonPropertyName("estimated_seconds")]
        public int? EstimatedSeconds { get; set; }

        [JsonPropertyName("fixed_fee")]
        public decimal? FixedFee { get; set; }

        [JsonPropertyName("is_private")]
        public bool IsPrivate { get; set; }

        [JsonPropertyName("rate")]
        public decimal? Rate { get; set; }

        [JsonPropertyName("rate_last_updated")]
        public DateTimeOffset? RateLastUpdated { get; set; }

        [JsonPropertyName("recurring")]
        public bool Recurring { get; set; }

        [JsonPropertyName("recurring_parameters")]
        public Recurrence? RecurringParameters { get; set; }

        [JsonPropertyName("start_date")]
        public DateTimeOffset? StartDate { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("template")]
        public bool? Template { get; set; }

        [JsonPropertyName("template_id")]
        public long? TemplateId { get; set; }

        [JsonPropertyName("workspace_id")]
        public long WorkspaceId { get; set; }
    }

    public class Recurrence
    {
        [JsonPropertyName("custom_period")]
        public long? CustomPeriod { get; set; }

        [JsonPropertyName("estimated_seconds")]
        public long EstimatedSeconds { get; set; }

        [JsonPropertyName("parameter_end_date")]
        public DateTimeOffset? ParameterEndDate { get; set; }

        [JsonPropertyName("parameter_start_date")]
        public DateTimeOffset? ParameterStartDate { get; set; }

        [JsonPropertyName("period")]
        public string? Period { get; set; }

        [JsonPropertyName("project_start_date")]
        public DateTimeOffset? ProjectStartDate { get; set; }
    }
}
