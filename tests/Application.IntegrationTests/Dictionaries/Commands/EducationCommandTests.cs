using Application.Common.Exceptions;
using Application.Dictionaries.Commands;
using Application.Dictionaries.Queries;
using FluentAssertions;

namespace Application.IntegrationTests.Dictionaries.Commands;

public class EducationCommandTests : BaseDatabaseTest
{
    public EducationCommandTests(DatabaseFixture fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task CreatesEducation()
    {
        await _fixture.RunAsAdministrator();
        var command = new CreateEducationCommand() { NameRu = "Name ru", NameKg = "Name kg" };
        var educationDto = await _fixture.SendAsync(command);

        educationDto.Should().NotBeNull();
        educationDto.Should().BeEquivalentTo(command);
    }

    [Fact]
    public async Task UpdatesEducation()
    {
        await _fixture.RunAsAdministrator();
        var command = new CreateEducationCommand() { NameRu = "Name ru", NameKg = "Name kg" };
        var created = await _fixture.SendAsync(command);

        var updateCommand = new UpdateEducationCommand() { Id = created.Id, NameRu = "Name ru updated", NameKg = "Name kg updated" };
        await _fixture.SendAsync(updateCommand);
        var updated = await _fixture.SendAsync(new GetEducationQuery() { Id = updateCommand.Id });

        updated.Should().NotBeNull();
        updated.Should().BeEquivalentTo(updateCommand);
    }

    [Fact]
    public async Task WhenUpdatingEducation_ThrowsNotFound()
    {
        await _fixture.RunAsAdministrator();
        var updateCommand = new UpdateEducationCommand() { Id = 1, NameRu = "Name ru updated", NameKg = "Name kg updated" };

        var act = async () => await _fixture.SendAsync(updateCommand);
        await act.Should().ThrowAsync<NotFoundException>();
    }
    
    
    [Fact]
    public async Task DeletesEducation()
    {
        await _fixture.RunAsAdministrator();
        var command = new CreateEducationCommand() { NameRu = "Name ru", NameKg = "Name kg" };
        var educationDto = await _fixture.SendAsync(command);
        var deleteCommand = new DeleteEducationCommand() { Id = educationDto.Id };
        await _fixture.SendAsync(deleteCommand);

        var act = async () => await _fixture.SendAsync(new GetEducationQuery() { Id = educationDto.Id });

        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task WhenDeletingEducation_ThrowsNotFound()
    {
        await _fixture.RunAsAdministrator();
        var act = async () => await _fixture.SendAsync(new DeleteEducationCommand() { Id = 1 });

        await act.Should().ThrowAsync<NotFoundException>();
    }
}