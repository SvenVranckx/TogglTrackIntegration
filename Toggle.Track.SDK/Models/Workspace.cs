using System.Text.Json.Serialization;

namespace Toggle.Track.SDK.Models
{
    public class Workspace : ConfigurationEntity
    {
        [JsonPropertyName("organization_id")]
        public long OrganizationId { get; set; }

        [JsonPropertyName("profile")]
        public long Profile { get; set; }

        [JsonPropertyName("premium")]
        public bool Premium { get; set; }

        [JsonPropertyName("business_ws")]
        public bool BusinessWorkspace { get; set; }

        [JsonPropertyName("admin")]
        public bool Administrator { get; set; }

        [JsonPropertyName("role")]
        public string? Role { get; set; }

        [JsonPropertyName("default_hourly_rate")]
        public decimal? DefaultHourlyRate { get; set; }

        [JsonPropertyName("rate_last_updated")]
        public DateTimeOffset? RateLastUpdated { get; set; }

        [JsonPropertyName("default_currency")]
        public string? DefaultCurrency { get; set; }

        [JsonPropertyName("only_admins_may_create_projects")]
        public bool OnlyAdminsMayCreateProjects { get; set; }
        
        [JsonPropertyName("only_admins_may_create_tags")]
        public bool OnlyAdminsMayCreateTags { get; set; }
        
        [JsonPropertyName("only_admins_see_billable_rates")]
        public bool OnlyAdminsSeeBillableRates { get; set; }
        
        [JsonPropertyName("only_admins_see_team_dashboard")]
        public bool OnlyAdminsSeeTeamDashboard { get; set; }
        
        [JsonPropertyName("projects_billable_by_default")]
        public bool ProjectsBillableByDefault { get; set; }
        
        [JsonPropertyName("projects_private_by_default")]
        public bool ProjectsPrivateByDefault { get; set; }

        [JsonPropertyName("last_modified")]
        public DateTimeOffset? LastModified { get; set; }

        [JsonPropertyName("reports_collapse")]
        public bool ReportsCollapse { get; set; }

        [JsonPropertyName("rounding")]
        public int Rounding { get; set; }

        [JsonPropertyName("rounding_minutes")]
        public int RoundingMinutes { get; set; }

        [JsonPropertyName("api_token")]
        public string? ApiToken { get; set; }

        [JsonPropertyName("logo_url")]
        public string? LogoUrl { get; set; }

        [JsonPropertyName("ical_url")]
        public string? ICalUrl { get; set; }

        [JsonPropertyName("ical_enabled")]
        public bool ICalEnabled { get; set; }

        [JsonPropertyName("csv_upload")]
        public CsvUpload? CsvUpload { get; set; }

        [JsonPropertyName("subscription")]
        public Subscription? Subscription { get; set; }

        [JsonPropertyName("hide_start_end_times")]
        public bool HideStartEndTimes { get; set; }

        [JsonPropertyName("working_hours_in_minutes")]
        public int? WorkingHoursInMinutes { get; set; }
    }

    public class CsvUpload
    {
        [JsonPropertyName("at")]
        public DateTimeOffset? At { get; set; }

        [JsonPropertyName("log_id")]
        public long LogId { get; set; }
    }

    public class Subscription
    {
        [JsonPropertyName("auto_renew")]
        public bool AutoRenew { get; set; }


        /*
         * auto_renew	boolean	-
card_details	models.CardDetails	-
company_id	integer	-
contact_detail	models.ContactDetail	-
created_at	string	-
currency	string	-
customer_id	integer	-
deleted_at	string	-
last_pricing_plan_id	integer	-
organization_id	integer	-
payment_details	models.PaymentDetail	-
pricing_plan_id	integer	-
renewal_at	string	-
subscription_id	integer	-
subscription_period	models.Period	-
workspace_id	integer	-
         */
    }

    public class CardDetails { }
    public class ContactDetail { }
    public class PaymentDetail { }
    public class Period { }
}
