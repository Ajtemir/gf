using Application.Entities.Commands;
using Application.Entities.Queries;
using FluentAssertions;
using FluentAssertions.Extensions;

namespace Application.IntegrationTests.Candidates.Commands;

public class EntityCommandTests : BaseDatabaseTest
{
    public EntityCommandTests(DatabaseFixture fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task CreatesEntity()
    {
        var userId = await _fixture.RunAsAdministrator();

        var createEntityCommand = new CreateEntityCommand()
        {
            NameRu = "Name ru",
            NameKg = "Name kg",
            ImageName = "image.jpg",
            Image = Convert.ToBase64String(new byte[] { 1, 2 }),
        };

        var entityDto = await _fixture.SendAsync(createEntityCommand);

        entityDto.Should().NotBeNull();
        entityDto.Should().BeEquivalentTo(createEntityCommand, options => options
            .Excluding(x => x.Image)
            .Excluding(x => x.ImageName));
        entityDto.NameRu.Should().Be(createEntityCommand.NameRu);

        entityDto.CreatedBy.Should().Be(userId);
        entityDto.ModifiedBy.Should().Be(userId);
        entityDto.ModifiedAt.Should().BeCloseTo(entityDto.CreatedAt, 1.Seconds());
        entityDto.CreatedByUser.Should().NotBeNullOrEmpty().And.Be(entityDto.ModifiedByUser);
    }

    [Fact]
    public async Task UpdateEntity_WithoutUpdatingImage()
    {
        var userId = await _fixture.RunAsAdministrator();
        var createEntityCommand = new CreateEntityCommand()
        {
            NameRu = "Name ru",
            NameKg = "Name kg",
            ImageName = "image.jpg",
            Image = Convert.ToBase64String(new byte[] { 1, 2 }),
        };
        var created = await _fixture.SendAsync(createEntityCommand);

        var updateEntityCommand = new UpdateEntityCommand() { Id = created.Id, NameRu = "Name ru updated", NameKg = "Name kg updated" };
        await _fixture.SendAsync(updateEntityCommand);

        var updated = await _fixture.SendAsync(new GetEntityQuery() { Id = updateEntityCommand.Id });

        updated.Should().NotBeNull();
        updated.Should().BeEquivalentTo(updateEntityCommand);

        updated.CreatedBy.Should().Be(userId);
        updated.ImageName.Should().Be(created.ImageName);
        updated.Image.Should().Be(created.Image);
    }

    [Fact]
    public async Task UpdateEntityImage()
    {
        var userId = await _fixture.RunAsAdministrator();
        var createEntityCommand = new CreateEntityCommand()
        {
            NameRu = "Name ru",
            NameKg = "Name kg",
            ImageName = "image.jpg",
            Image = Convert.ToBase64String(new byte[] { 1, 2 }),
        };
        var created = await _fixture.SendAsync(createEntityCommand);

        var updateEntityImageCommand = new UpdateEntityImageCommand()
        {
            Id = created.Id, ImageName = "image-updated.jpg", Image = Convert.ToBase64String(new byte[] { 3, 4 }),
        };
        await _fixture.SendAsync(updateEntityImageCommand);
        var updated = await _fixture.SendAsync(new GetEntityQuery() { Id = updateEntityImageCommand.Id });

        updated.Should().NotBeNull().And.BeEquivalentTo(updateEntityImageCommand);
        updated.CreatedBy.Should().Be(userId).And.Be(created.CreatedBy);
    }
}