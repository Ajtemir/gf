using Application.Common.Exceptions;
using Application.Dictionaries.Commands;
using Application.Dictionaries.Queries;
using FluentAssertions;

namespace Application.IntegrationTests.Dictionaries.Commands;

public class PositionCommandTests : BaseDatabaseTest
{
    public PositionCommandTests(DatabaseFixture fixture) : base(fixture)
    {
    }
    
    [Fact]
    public async Task CreatesPosition()
    {
        await _fixture.RunAsAdministrator();
        
        var createCommand = new CreatePositionCommand()
        {
            NameRu = "Name ru",
            NameKg = "Name kg",
        };

        var positionDto = await _fixture.SendAsync(createCommand);
        var position = await _fixture.SendAsync(new GetPositionQuery { Id = positionDto.Id });
        
        position.Should().NotBeNull();
        position.Should().BeEquivalentTo(createCommand);
    }
    
    [Fact]
    public async Task UpdatesPosition()
    {
        await _fixture.RunAsAdministrator();
        
        var command = new CreatePositionCommand()
        {
            NameRu = "Name ru",
            NameKg = "Name kg",
        };

        var positionDto = await _fixture.SendAsync(command);
        
        var updateCommand = new UpdatePositionCommand()
        {
            Id = positionDto.Id, NameRu = "Name ru updated", NameKg = "Name kg updated",
        };
        await _fixture.SendAsync(updateCommand);
        
        var updated = await _fixture.SendAsync(new GetPositionQuery { Id = positionDto.Id });

        updated.Should().BeEquivalentTo(updateCommand);
    }

    [Fact]
    public async Task WhenUpdatingPosition_ThrowsNotFound()
    {
        await _fixture.RunAsAdministrator();
        
        var updateCommand = new UpdatePositionCommand()
        {
            Id = 1, NameRu = "Name ru updated", NameKg = "Name kg updated",
        };
        
        var act = async () => await _fixture.SendAsync(updateCommand);
        await act.Should().ThrowAsync<NotFoundException>();
    }
    
    [Fact]
    public async Task DeletesPosition()
    {
        await _fixture.RunAsAdministrator();
        
        var command = new CreatePositionCommand()
        {
            NameRu = "Name ru",
            NameKg = "Name kg",
        };

        var positionDto = await _fixture.SendAsync(command);
        var deleteCommand = new DeletePositionCommand() { Id = positionDto.Id };
        await _fixture.SendAsync(deleteCommand);

        var act = async () => await _fixture.SendAsync(new GetPositionQuery() { Id = positionDto.Id });
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task WhenDeletingPosition_ThrowsNotFound()
    {
        await _fixture.RunAsAdministrator();

        var act = async () => await _fixture.SendAsync(new DeletePositionCommand() { Id = 1 });
        await act.Should().ThrowAsync<NotFoundException>();
    }
}