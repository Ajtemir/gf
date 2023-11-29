using Application.Common.Exceptions;
using Application.Dictionaries.Commands;
using Application.Dictionaries.Queries;
using FluentAssertions;

namespace Application.IntegrationTests.Dictionaries.Queries;

public class CitizenshipQueryTests : BaseDatabaseTest
{
    public CitizenshipQueryTests(DatabaseFixture fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task ReturnsSingleCitizenshipDto()
    {
        await _fixture.RunAsAdministrator();
        var citizenshipDto = await _fixture.SendAsync(BuildCreateCommand());

        citizenshipDto.Should().NotBeNull();
    }

    [Fact]
    public async Task WhenQueryingCitizenship_ThrowsNotFound()
    {
        await _fixture.RunAsAdministrator();
        var act = async () => await _fixture.SendAsync(new GetCitizenshipQuery() { Id = 1 });

        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task ReturnsMultipleCitizenshipDtos()
    {
        await _fixture.RunAsAdministrator();
        var commands = BuildCreateCommands(3);
        foreach (var command in commands)
        {
            await _fixture.SendAsync(command);
        }

        var citizenshipDtos = await _fixture.SendAsync(new GetCitizenshipListQuery());
        citizenshipDtos.Should().NotBeNull();
        citizenshipDtos.Should().HaveCount(3);
    }

    private static CreateCitizenshipCommand BuildCreateCommand(int id = 0) => new CreateCitizenshipCommand()
    {
        NameRu = $"Name ru {id}",
        NameKg = $"Name kg {id}",
    };
    
    private static IEnumerable<CreateCitizenshipCommand> BuildCreateCommands(int amount = 3) =>
        Enumerable.Range(0, amount).Select(BuildCreateCommand);
}