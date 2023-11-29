using Application.Candidates.Commands;
using Application.Dictionaries.Commands;
using Application.Foreigners.Commands;
using Application.Foreigners.Queries;
using Domain.Enums;
using FluentAssertions;
using FluentAssertions.Extensions;

namespace Application.IntegrationTests.Candidates.Commands;

public class ForeignerCommandTests : BaseDatabaseTest
{
    public ForeignerCommandTests(DatabaseFixture fixture) : base(fixture)
    {
    }
    
    [Fact]
    public async Task CreatesForeigner()
    {
        var userId = await _fixture.RunAsAdministrator();
        var createCitizenshipCommand = new CreateCitizenshipCommand { NameRu = "Name ru", NameKg = "Name kg" };
        var citizenshipDto = await _fixture.SendAsync(createCitizenshipCommand);
        
        var createForeignerCommand = new CreateForeignerCommand()
        {
            LastName = "LastName",
            FirstName = "FirstName",
            PatronymicName = "PatronymicName",
            Gender = Gender.Male,
            BirthDate = new DateOnly(2000, 6, 10),
            DeathDate = new DateOnly(2012, 12, 21),
            CitizenshipId = citizenshipDto.Id,
        };
        
        var foreignerDto = await _fixture.SendAsync(createForeignerCommand);

        foreignerDto.Should().NotBeNull();
        foreignerDto.Should().BeEquivalentTo(createForeignerCommand);

        foreignerDto.CitizenshipRu.Should().NotBeNullOrEmpty();
        foreignerDto.CitizenshipKg.Should().NotBeNullOrEmpty();
        
        foreignerDto.CreatedBy.Should().Be(userId);
        foreignerDto.ModifiedBy.Should().Be(userId);
        foreignerDto.ModifiedAt.Should().BeCloseTo(foreignerDto.CreatedAt, 1.Seconds());
        foreignerDto.CreatedByUser.Should().NotBeNullOrEmpty().And.Be(foreignerDto.ModifiedByUser);
    }
    
    [Fact]
    public async Task UpdatesForeigner_WithoutUpdatingImage()
    {
        var userId = await _fixture.RunAsAdministrator();
        var citizenshipDto = await _fixture.SendAsync(new CreateCitizenshipCommand { NameRu = "Name ru", NameKg = "Name kg" });
        var anotherCitizenshipDto = await _fixture.SendAsync(new CreateCitizenshipCommand { NameRu = "Russia", NameKg = "Russia" });
        var createForeignerCommand = new CreateForeignerCommand()
        {
            LastName = "LastName",
            FirstName = "FirstName",
            PatronymicName = "PatronymicName",
            Gender = Gender.Male,
            BirthDate = new DateOnly(2000, 6, 10),
            DeathDate = new DateOnly(2012, 12, 21),
            CitizenshipId = citizenshipDto.Id,
        };
        var createdDto = await _fixture.SendAsync(createForeignerCommand);
        
        var updateForeignerCommand = new UpdateForeignerCommand()
        {
            Id = createdDto.Id,
            LastName = "LastName updated",
            FirstName = "FirstName updated",
            PatronymicName = "PatronymicName updated",
            Gender = Gender.Female,
            BirthDate = new DateOnly(2002, 8, 12),
            DeathDate = new DateOnly(2015, 3, 1),
            CitizenshipId = anotherCitizenshipDto.Id,
            ImageName = "image-updated.jpg",
            Image = Convert.ToBase64String(new byte[] { 1, 2, 3 }),
        };
        
        await _fixture.SendAsync(updateForeignerCommand);
        var updatedForeigner = await _fixture.SendAsync(new GetForeignerQuery() { Id = updateForeignerCommand.Id });

        updatedForeigner.Should().NotBeNull();
        updatedForeigner.Should().BeEquivalentTo(updateForeignerCommand, options => options
            .Excluding(c => c.Image)
            .Excluding(c => c.ImageName));
        
        updatedForeigner.ModifiedBy.Should().Be(userId);
    }

    [Fact]
    public async Task UpdatesForeignerImage()
    {
        var userId = await _fixture.RunAsAdministrator();
        var createCitizenshipCommand = new CreateCitizenshipCommand { NameRu = "Name ru", NameKg = "Name kg" };
        var citizenshipDto = await _fixture.SendAsync(createCitizenshipCommand);
        
        var createForeignerCommand = new CreateForeignerCommand()
        {
            LastName = "LastName",
            FirstName = "FirstName",
            PatronymicName = "PatronymicName",
            Gender = Gender.Male,
            BirthDate = new DateOnly(2000, 6, 10),
            DeathDate = new DateOnly(2012, 12, 21),
            CitizenshipId = citizenshipDto.Id,
        };
        var created = await _fixture.SendAsync(createForeignerCommand);

        var updateForeignerImageCommand = new UpdateForeignerImageCommand()
        {
            Id = created.Id, ImageName = "image-updated.jpg", Image = Convert.ToBase64String(new byte[] { 3, 4 }),
        };
        await _fixture.SendAsync(updateForeignerImageCommand);

        var updated = await _fixture.SendAsync(new GetForeignerQuery() { Id = updateForeignerImageCommand.Id });

        updated.Should().NotBeNull().And.BeEquivalentTo(updateForeignerImageCommand);
        updated.CreatedBy.Should().Be(userId).And.Be(created.CreatedBy);
    }

    [Fact]
    public async Task DeletesForeigner()
    {
        await _fixture.RunAsAdministrator();
        var createCitizenshipCommand = new CreateCitizenshipCommand { NameRu = "Name ru", NameKg = "Name kg" };
        var citizenshipDto = await _fixture.SendAsync(createCitizenshipCommand);
        var createForeignerCommand = new CreateForeignerCommand()
        {
            LastName = "LastName",
            FirstName = "FirstName",
            PatronymicName = "PatronymicName",
            Gender = Gender.Male,
            BirthDate = new DateOnly(2000, 6, 10),
            DeathDate = new DateOnly(2012, 12, 21),
            CitizenshipId = citizenshipDto.Id,
        };
        var foreignerDto = await _fixture.SendAsync(createForeignerCommand);

        var act = async () => await _fixture.SendAsync(new DeleteCandidateCommand() { Id = foreignerDto.Id });

        await act.Should().NotThrowAsync();
    }
}