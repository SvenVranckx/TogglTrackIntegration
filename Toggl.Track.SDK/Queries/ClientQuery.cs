using Toggl.Track.SDK.Models;

namespace Toggl.Track.SDK.Queries
{
    public class ClientQuery : Query<Client>
    {
        private readonly long? _workspaceId;
        private readonly string? _status;
        private readonly string? _name;

        public ClientQuery() : this(null, null)
        { }

        public ClientQuery(long? workspaceId = null, string? status = null, string? name = null) : base("me/clients")
        {
            _workspaceId = workspaceId;
            _status = status;
            _name = name;
        }

        public static readonly ClientQuery Active = new(status: "active");
        public static readonly ClientQuery Archived = new(status: "archived");

        public static ClientQuery ByWorkspace(Workspace workspace) => new(workspaceId: workspace.Id);
        public static ClientQuery Name(string name) => new(name: name);

        public static ClientQuery operator |(ClientQuery left, ClientQuery right) =>
            new(left._workspaceId ?? right._workspaceId,
                JoinStatus(left._status, right._status),
                left._name ?? right._name);

        private static string? JoinStatus(string? left, string? right)
        {
            if (left is null)
                return right;
            if (right is null)
                return left;
            if (string.Equals(left, right))
                return left;
            else
                return "both";
        }

        protected override OptionsBuilder Options(OptionsBuilder builder)
        {
            if (_workspaceId is not null)
            {
                if (!string.IsNullOrEmpty(_status))
                    builder.Add("status", _status);
                if (!string.IsNullOrEmpty(_name))
                    builder.Add("name", _name);
            }
            return builder;
        }

        protected override string Build()
        {
            if (_workspaceId is not null)
                return ApplyOptions($"workspaces/{_workspaceId}/clients");
            return ApplyOptions(Path);
        }
    }
}
