using Application.Common.Exceptions;
using Application.Dictionaries.Commands;
using Application.Dictionaries.Queries;
using FluentAssertions;

namespace Application.IntegrationTests.Dictionaries.Queries;

public class PositionQueryTests : BaseDatabaseTest
{
    public PositionQueryTests(DatabaseFixture fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task ReturnsSinglePosition()
    {
        await _fixture.RunAsAdministrator();
        var createCommand = BuildCommand();
        var positionDto = await _fixture.SendAsync(createCommand);

        positionDto.Should().NotBeNull();
        positionDto.Should().BeEquivalentTo(createCommand);
    }

    [Fact]
    public async Task WhenQueryingPosition_ThrowsNotFound()
    {
        await _fixture.RunAsAdministrator();
        var act = async () => await _fixture.SendAsync(new GetPositionQuery() { Id = 1 });
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task ReturnsMultiplePositionDtos()
    {
        await _fixture.RunAsAdministrator();
        var commands = BuildCommands(3);
        foreach (var command in commands)
        {
            await _fixture.SendAsync(command);
        }

        var positionDtos = await _fixture.SendAsync(new GetPositionListQuery());
        positionDtos.Should().NotBeNull();
        positionDtos.Should().HaveCount(3);
    }

    private static CreatePositionCommand BuildCommand(int id = 0) =>
        new CreatePositionCommand() { NameRu = $"Name ru {id}", NameKg = $"Name kg {id}" };

    private static IEnumerable<CreatePositionCommand> BuildCommands(int amount = 3) =>
        Enumerable.Range(0, amount).Select(BuildCommand);
}