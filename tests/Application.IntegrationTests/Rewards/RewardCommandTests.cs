using Application.Common.Exceptions;
using Application.Rewards.Commands;
using Application.Rewards.Queries;
using FluentAssertions;
using FluentAssertions.Extensions;

namespace Application.IntegrationTests.Rewards;

public class RewardCommandTests : BaseDatabaseTest
{
    public RewardCommandTests(DatabaseFixture fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task CreatesReward()
    {
        var userId = await _fixture.RunAsAdministrator();
        
        var createCommand = new CreateRewardCommand()
        {
            NameRu = "Name ru",
            NameKg = "Name kg",
            ImageName = "reward.jpg",
            Image = Convert.ToBase64String(new byte[] { 1, 2, 3 }),
        };

        var rewardDto = await _fixture.SendAsync(createCommand);
        
        rewardDto.Should().NotBeNull();
        rewardDto.Should().BeEquivalentTo(createCommand);
        
        rewardDto.CreatedBy.Should().Be(userId).And.Be(rewardDto.ModifiedBy);
        rewardDto.CreatedAt.Should().BeCloseTo(rewardDto.ModifiedAt, 1.Seconds());
        rewardDto.CreatedByUser.Should().NotBeNullOrEmpty().And.Be(rewardDto.ModifiedByUser);
    }

    [Fact]
    public async Task ShouldDenyAnonymousUser()
    {
        var createCommand = new CreateRewardCommand()
        {
            NameRu = "Name ru",
            NameKg = "Name kg",
            ImageName = "reward.jpg",
            Image = Convert.ToBase64String(new byte[] { 1, 2, 3 }),
        };

        var act = async () => await _fixture.SendAsync(createCommand);

        await act.Should().ThrowAsync<ForbiddenAccessException>();
    }

    [Fact]
    public async Task UpdatesReward()
    {
        var userId = await _fixture.RunAsAdministrator();
        
        var command = new CreateRewardCommand()
        {
            NameRu = "Name ru",
            NameKg = "Name kg",
            ImageName = "reward.jpg",
            Image = Convert.ToBase64String(new byte[] { 1, 2, 3}),
        };

        var created = await _fixture.SendAsync(command);

        var updateCommand = new UpdateRewardDetailsCommand()
        {
            Id = created.Id,
            NameRu = "Updated Ru",
            NameKg = "Updated kg",
        };

        await _fixture.SendAsync(updateCommand);
        var updated = await _fixture.SendAsync(new GetRewardQuery() { Id = created.Id });

        updated.Should().BeEquivalentTo(updateCommand);
        
        updated.CreatedBy.Should().Be(created.CreatedBy);
        updated.CreatedAt.Should().BeCloseTo(created.CreatedAt, 1.Seconds());
        updated.ModifiedBy.Should().Be(userId);
        updated.ModifiedAt.Should().BeAfter(created.CreatedAt);
    }

    [Fact]
    public async Task WhenUpdatingReward_ThrowsNotFound()
    {
        await _fixture.RunAsAdministrator();
        
        var updateCommand = new UpdateRewardDetailsCommand()
        {
            Id = 1,
            NameRu = "Updated Ru",
            NameKg = "Updated kg",
        };

        var act = async () => await _fixture.SendAsync(updateCommand);
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task DeletesReward()
    {
        await _fixture.RunAsAdministrator();
        
        var command = new CreateRewardCommand()
        {
            NameRu = "Name ru",
            NameKg = "Name kg",
            ImageName = "reward.jpg",
            Image = Convert.ToBase64String(new byte[] { 1, 2, 3 }),
        };

        var rewardDto = await _fixture.SendAsync(command);
        var deleteCommand = new DeleteRewardCommand() { Id = rewardDto.Id };
        await _fixture.SendAsync(deleteCommand);

        var act = async () => await _fixture.SendAsync(new GetRewardQuery() { Id = rewardDto.Id });

        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task WhenDeletingReward_ThrowsNotFound()
    {
        await _fixture.RunAsAdministrator();
        
        var act = async () => await _fixture.SendAsync(new DeleteRewardCommand() { Id = 1 });
        await act.Should().ThrowAsync<NotFoundException>();
    }
}
