using Toggl.Track.SDK.Models;

namespace Toggl.Track.SDK.Queries
{
    public class ProjectQuery : Query<Project>
    {
        private readonly long? _workspaceId;
        private readonly IEnumerable<long>? _clientIds;

        public ProjectQuery() : this(null)
        { }

        public ProjectQuery(long? workspaceId = null, IEnumerable<long>? clientIds = null) : 
            base("me/projects")
        {
            _workspaceId = workspaceId;
            _clientIds = clientIds;
        }

        public static ProjectQuery ByWorkspace(Workspace workspace) => new(workspaceId: workspace.Id);
        public static ProjectQuery ByClient(Client client) => new(workspaceId: client.WorkspaceId, clientIds: [client.Id]);
        public static ProjectQuery ByClients(Workspace workspace, params Client[] clients) => new(workspaceId: workspace.Id, clientIds: clients.Select(c => c.Id));

        protected override OptionsBuilder Options(OptionsBuilder builder)
        {
            if (_workspaceId is not null && _clientIds is not null)
            {
                var ids = string.Join(",", _clientIds);
                if (!string.IsNullOrEmpty(ids))
                    builder.Add("client_ids", ids);
            }
            return builder;
        }

        protected override string Build()
        {
            if (_workspaceId is not null)
                return ApplyOptions($"workspaces/{_workspaceId}/projects");
            return ApplyOptions(Path);
        }
    }
}
