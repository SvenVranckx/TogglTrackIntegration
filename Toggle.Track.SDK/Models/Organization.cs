using System.Text.Json.Serialization;

namespace Toggle.Track.SDK.Models
{
    public class Organization
    {
        /*
         *         "id": 8213090,
        "name": "S3 Consulting",
        "pricing_plan_id": 0,
        "created_at": "2024-03-27T13:16:08.909564Z",
        "at": "2024-03-27T13:27:31.798581Z",
        "server_deleted_at": null,
        "is_multi_workspace_enabled": false,
        "suspended_at": null,
        "user_count": 1,
        "trial_info": {
            "trial": false,
            "trial_available": true,
            "trial_end_date": null,
            "next_payment_date": null,
            "last_pricing_plan_id": null,
            "can_have_trial": true
        },
        "is_unified": false,
        "max_workspaces": 20,
        "admin": true,
        "owner": true
         */

        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }
}
