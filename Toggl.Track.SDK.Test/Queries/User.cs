using FluentAssertions;
using Toggl.Track.SDK.Queries;

namespace Toggl.Track.SDK.Test.Queries
{
    public class UserTests : IClassFixture<ContextFixture>
    {
        public UserTests(ContextFixture fixture) { Context = fixture.Context; }

        protected ApiContext Context { get; }

        [Fact]
        public async Task GetMe()
        {
            var me = await Context.Me.Get();
            Assert.NotNull(me);
            me.FullName.Should().NotBeNull();
            me.Email.Should().NotBeNull();
        }

        [Fact]
        public async Task GetMeWithRelatedData()
        {
            var me = await Context.Me.Get(UserQuery.WithRelatedData);
            Assert.NotNull(me);
            me.FullName.Should().NotBeNull();
            me.Email.Should().NotBeNull();
            me.Workspaces.Should().NotBeEmpty();
            me.Projects.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetPreferences()
        {
            var preferences = await Context.Preferences.Get();
            Assert.NotNull(preferences);
            preferences.AlphaFeatures.Should().NotBeEmpty();
        }
    }
}