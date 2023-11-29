using Application.Common.Exceptions;
using Application.Dictionaries.Commands;
using Application.Dictionaries.Queries;
using FluentAssertions;

namespace Application.IntegrationTests.Dictionaries.Commands;

public class CitizenshipCommandTests : BaseDatabaseTest
{
    public CitizenshipCommandTests(DatabaseFixture fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task CreatesCitizenship()
    {
        await _fixture.RunAsAdministrator();
        var command = new CreateCitizenshipCommand() { NameRu = "Name ru", NameKg = "Name kg" };
        var citizenshipDto = await _fixture.SendAsync(command);

        citizenshipDto.Should().NotBeNull();
        citizenshipDto.Should().BeEquivalentTo(command);
    }

    [Fact]
    public async Task UpdatesCitizenship()
    {
        await _fixture.RunAsAdministrator();
        var command = new CreateCitizenshipCommand() { NameRu = "Name ru", NameKg = "Name kg" };
        var created = await _fixture.SendAsync(command);

        var updateCommand = new UpdateCitizenshipCommand() { Id = created.Id, NameRu = "Name ru updated", NameKg = "Name kg updated" };
        await _fixture.SendAsync(updateCommand);
        var updated = await _fixture.SendAsync(new GetCitizenshipQuery() { Id = updateCommand.Id });

        updated.Should().NotBeNull();
        updated.Should().BeEquivalentTo(updateCommand);
    }

    [Fact]
    public async Task WhenUpdatingCitizenship_ThrowsNotFound()
    {
        await _fixture.RunAsAdministrator();
        
        var updateCommand = new UpdateCitizenshipCommand() { Id = 1, NameRu = "Name ru updated", NameKg = "Name kg updated" };
        var act = async () => await _fixture.SendAsync(updateCommand);

        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task DeletesCitizenship()
    {
        await _fixture.RunAsAdministrator();
        var command = new CreateCitizenshipCommand() { NameRu = "Name ru", NameKg = "Name kg" };
        var citizenshipDto = await _fixture.SendAsync(command);

        var deleteCommand = new DeleteCitizenshipCommand() { Id = citizenshipDto.Id };
        await _fixture.SendAsync(deleteCommand);

        var act = async () => await _fixture.SendAsync(new GetCitizenshipQuery() { Id = citizenshipDto.Id });
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task WhenDeletingCitizenship_ThrowsNotFound()
    {
        await _fixture.RunAsAdministrator();
        var act = async () => await _fixture.SendAsync(new DeleteCitizenshipCommand() { Id = 1 });

        await act.Should().ThrowAsync<NotFoundException>();
    }
}