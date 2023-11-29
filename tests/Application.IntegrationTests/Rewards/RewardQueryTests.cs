using Application.Common.Exceptions;
using Application.Rewards.Commands;
using Application.Rewards.Queries;
using FluentAssertions;
using FluentAssertions.Extensions;

namespace Application.IntegrationTests.Rewards;

public class RewardQueryTests  : BaseDatabaseTest
{
    public RewardQueryTests(DatabaseFixture fixture) : base(fixture)
    {
    }
    
    [Fact]
    public async Task ReturnsSingleRewardDto()
    {
        var userId = await _fixture.RunAsAdministrator();
        var createCommand = new CreateRewardCommand()
        {
            NameRu = "Name ru",
            NameKg = "Name kg",
            ImageName = "image.jpg",
            Image = Convert.ToBase64String(new byte[] { 1, 2, 3 }),
        };

        var rewardDto = await _fixture.SendAsync(createCommand);

        rewardDto.Should().NotBeNull();
        rewardDto.Should().BeEquivalentTo(createCommand);
        rewardDto.CreatedBy.Should().Be(userId).And.Be(rewardDto.ModifiedBy);
        rewardDto.ModifiedAt.Should().BeCloseTo(rewardDto.CreatedAt, 1.Seconds());
        rewardDto.CreatedByUser.Should().NotBeEmpty().And.Be(rewardDto.ModifiedByUser);
    }
    
    [Fact]
    public async Task WhenQueryingReward_ShouldThrowNotFound()
    {
        await _fixture.RunAsAdministrator();
        var act = async () => await _fixture.SendAsync(new GetRewardQuery() { Id = 1 });

        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task ReturnsMultipleRewards()
    {
        await _fixture.RunAsAdministrator();
        var commands = BuildCreateCommands(3).Select(c => _fixture.SendAsync(c));
        await Task.WhenAll(commands);

        var rewards = (await _fixture.SendAsync(new GetRewardListQuery())).ToList();
        rewards.Should().HaveCount(3);
    }

    private static CreateRewardCommand BuildCreateCommand(int id) =>
        new CreateRewardCommand()
        {
            NameRu = $"Name ru {id}",
            NameKg = $"Name kg {id}",
            ImageName = $"image{id}.jpg",
            Image = Convert.ToBase64String(new byte[] { 1, 2, (byte)id})
        };

    private static IEnumerable<CreateRewardCommand> BuildCreateCommands(int amount = 3) =>
        Enumerable.Range(0, amount).Select(BuildCreateCommand);
}