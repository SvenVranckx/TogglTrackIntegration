using FluentAssertions;
using Toggl.Track.SDK.Options;

namespace Toggl.Track.SDK.Test.Tests
{
    public class UserTests : ContextTests
    {
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
            var me = await Context.Me.Get(UserOptions.WithRelatedData);
            Assert.NotNull(me);
            me.FullName.Should().NotBeNull();
            me.Email.Should().NotBeNull();
            me.Workspaces.Should().NotBeEmpty();
            me.Projects.Should().NotBeEmpty();
        }
    }
}