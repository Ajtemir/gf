using Application.Mothers.Commands;
using Application.Mothers.Queries;
using FluentAssertions;
using FluentAssertions.Extensions;

namespace Application.IntegrationTests.Candidates.Commands;

public class MotherCommandTests : BaseDatabaseTest
{
    public MotherCommandTests(DatabaseFixture fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task CreatesMother()
    {
        var userId = await _fixture.RunAsAdministrator();
        var createMotherCommand = BuildCreateMotherCommand();
        var motherDto = await _fixture.SendAsync(createMotherCommand);

        motherDto.Should().NotBeNull();
        motherDto.Should().BeEquivalentTo(createMotherCommand, options => options
            .Excluding(x => x.Image)
            .Excluding(x => x.ImageName));

        motherDto.CreatedBy.Should().Be(userId);
        motherDto.ModifiedBy.Should().Be(userId);
        motherDto.ModifiedAt.Should().BeCloseTo(motherDto.CreatedAt, 1.Seconds());
        motherDto.CreatedByUser.Should().NotBeNullOrEmpty().And.Be(motherDto.ModifiedByUser);
    }

    [Fact]
    public async Task UpdatesMother_WithoutUpdatingImage()
    {
        var userId = await _fixture.RunAsAdministrator();
        var createMotherCommand = BuildCreateMotherCommand();
        var created = await _fixture.SendAsync(createMotherCommand);

        var updateMotherCommand = new UpdateMotherCommand()
        {
            Id = created.Id,
            LastName = "LastName",
            FirstName = "FirstName",
            PatronymicName = "PatronymicName",
            Pin = "11006199814512",
            PassportNumber = "ID1234567",
            BirthDate = new DateOnly(1999, 1, 1),
            DeathDate = new DateOnly(2014, 4, 4),
            RegisteredAddress = "RegisteredAddress",
            ActualAddress = "ActualAddress",
        };
        await _fixture.SendAsync(updateMotherCommand);
        var updated = await _fixture.SendAsync(new GetMotherQuery() { Id = updateMotherCommand.Id });
        
        updated.Should().NotBeNull().And.BeEquivalentTo(updateMotherCommand);
        updated.CreatedBy.Should().Be(userId).And.Be(created.CreatedBy);
    }

    [Fact]
    public async Task UpdatesMotherImage()
    {
        var userId = await _fixture.RunAsAdministrator();
        var createMotherCommand = BuildCreateMotherCommand();
        var created = await _fixture.SendAsync(createMotherCommand);

        var updateMotherImageCommand = new UpdateMotherImageCommand()
        {
            Id = created.Id, ImageName = "image-updated.jpg", Image = Convert.ToBase64String(new byte[] { 1, 2 }),
        };
        await _fixture.SendAsync(updateMotherImageCommand);

        var updated = await _fixture.SendAsync(new GetMotherQuery() { Id = updateMotherImageCommand.Id });
        updated.Should().BeEquivalentTo(updateMotherImageCommand);
        updated.CreatedBy.Should().Be(userId).And.Be(created.CreatedBy);
    }

    private static CreateMotherCommand BuildCreateMotherCommand() =>
        new CreateMotherCommand()
        {
            LastName = "LastName",
            FirstName = "FirstName",
            PatronymicName = "PatronymicName",
            Pin = "11006199814512",
            PassportNumber = "ID1234567",
            BirthDate = new DateOnly(1999, 1, 1),
            DeathDate = new DateOnly(2014, 4, 4),
            RegisteredAddress = "RegisteredAddress",
            ActualAddress = "ActualAddress",
            ImageName = "image.jpg",
            Image = Convert.ToBase64String(new byte[] { 1, 2 }),
        };
}