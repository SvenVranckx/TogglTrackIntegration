using System.Text.Json.Serialization;

namespace Toggl.Track.SDK.Models
{
    public class Period
    {
        [JsonPropertyName("start_date")]
        public DateTimeOffset? StartDate { get; set; }

        [JsonPropertyName("end_date")]
        public DateTimeOffset? EndDate { get; set; }
    }
}
