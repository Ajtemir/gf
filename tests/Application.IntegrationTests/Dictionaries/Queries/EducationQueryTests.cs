using Application.Common.Exceptions;
using Application.Dictionaries.Commands;
using Application.Dictionaries.Queries;
using FluentAssertions;

namespace Application.IntegrationTests.Dictionaries.Queries;

public class EducationQueryTests : BaseDatabaseTest
{
    public EducationQueryTests(DatabaseFixture fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task ReturnsSingleEducationDto()
    {
        await _fixture.RunAsAdministrator();
        var educationDto = await _fixture.SendAsync(BuildCreateCommand());
        var education = await _fixture.SendAsync(new GetEducationQuery() { Id = educationDto.Id });
        
        education.Should().NotBeNull();
    }
    
    [Fact]
    public async Task WhenQueryingEducation_ThrowsNotFound()
    {
        await _fixture.RunAsAdministrator();

        var act = async () => await _fixture.SendAsync(new GetEducationQuery() { Id = 1 });
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task ReturnsMultipleEducationDtos()
    {
        await _fixture.RunAsAdministrator();
        var commands = BuildCreateCommands(3);
        foreach (var command in commands)
        {
            await _fixture.SendAsync(command);
        }

        var educationDtos = await _fixture.SendAsync(new GetEducationListQuery());
        
        educationDtos.Should().NotBeNull();
        educationDtos.Should().HaveCount(3);
    }

    private static CreateEducationCommand BuildCreateCommand(int id = 0) => new CreateEducationCommand()
    {
        NameRu = $"Name ru {id}",
        NameKg = $"Name kg {id}"
    };

    private static IEnumerable<CreateEducationCommand> BuildCreateCommands(int amount = 3) =>
        Enumerable.Range(0, amount).Select(BuildCreateCommand);
}