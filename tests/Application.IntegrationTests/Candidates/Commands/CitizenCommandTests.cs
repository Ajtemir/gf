using Application.Citizens.Commands;
using Application.Citizens.Queries;
using Application.Dictionaries.Commands;
using Domain.Enums;
using FluentAssertions;
using FluentAssertions.Extensions;

namespace Application.IntegrationTests.Candidates.Commands;

public class CitizenCommandTests : BaseDatabaseTest
{
    public CitizenCommandTests(DatabaseFixture fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task CreatesCitizen()
    {
        var userId = await _fixture.RunAsAdministrator();

        var createEducationCommand = new CreateEducationCommand() { NameRu = "Name ru", NameKg = "Name kg" };
        var educationDto = await _fixture.SendAsync(createEducationCommand);
        var createCitizenCommand = BuildCreateCitizenCommand(educationDto.Id);
        var created = await _fixture.SendAsync(createCitizenCommand);

        created.Should().NotBeNull().And
            .BeEquivalentTo(createCitizenCommand, options => options
            .Excluding(x => x.Image)
            .Excluding(x => x.ImageName));

        created.CreatedBy.Should().Be(userId);
        created.ModifiedBy.Should().Be(userId);
        created.ModifiedAt.Should().BeCloseTo(created.CreatedAt, 1.Seconds());
        created.CreatedByUser.Should().NotBeNullOrEmpty().And.Be(created.ModifiedByUser);
    }

    [Fact]
    public async Task UpdateCitizen_WithoutUpdatingImage()
    {
        var userId = await _fixture.RunAsAdministrator();
        var createEducationCommand = new CreateEducationCommand() { NameRu = "Name ru", NameKg = "Name kg" };
        var educationDto = await _fixture.SendAsync(createEducationCommand);
        var createCitizenCommand = BuildCreateCitizenCommand(educationDto.Id);
        var created = await _fixture.SendAsync(createCitizenCommand);

        var updateCitizenCommand = new UpdateCitizenCommand()
        {
            Id = created.Id,
            LastName = "LastName",
            FirstName = "FirstName",
            PatronymicName = "PatronymicName",
            Pin = "11006199814512",
            PassportNumber = "ID1234567",
            Gender = Gender.Male,
            BirthDate = new DateOnly(1999, 1, 1),
            DeathDate = new DateOnly(2014, 4, 4),
            RegisteredAddress = "RegisteredAddress",
            ActualAddress = "ActualAddress",
            EducationId = educationDto.Id,
            YearsOfWorkTotal = 3,
            YearsOfWorkInIndustry = 3,
            YearsOfWorkInCollective = 3,
        };
        await _fixture.SendAsync(updateCitizenCommand);
        
        var updated = await _fixture.SendAsync(new GetCitizenQuery() { Id = updateCitizenCommand.Id });
        updated.Should().NotBeNull().And
            .BeEquivalentTo(updateCitizenCommand);

        updated.CreatedBy.Should().Be(userId).And.Be(created.CreatedBy);
    }

    [Fact]
    public async Task UpdateCitizenImage()
    {
        var userId = await _fixture.RunAsAdministrator();
        var createEducationCommand = new CreateEducationCommand() { NameRu = "Name ru", NameKg = "Name kg" };
        var educationDto = await _fixture.SendAsync(createEducationCommand);
        var createCitizenCommand = BuildCreateCitizenCommand(educationDto.Id);
        var created = await _fixture.SendAsync(createCitizenCommand);

        var updateCitizenImageCommand = new UpdateCitizenImageCommand()
        {
            Id = created.Id, ImageName = "image-updated.jpg", Image = Convert.ToBase64String(new byte[] { 3, 4 }),
        };
        await _fixture.SendAsync(updateCitizenImageCommand);

        var updated = await _fixture.SendAsync(new GetCitizenQuery() { Id = updateCitizenImageCommand.Id });

        updated.Should().NotBeNull().And
            .BeEquivalentTo(updateCitizenImageCommand);

        updated.CreatedBy.Should().Be(userId).And.Be(created.CreatedBy);
    }

    private static CreateCitizenCommand BuildCreateCitizenCommand(int educationId) =>
        new CreateCitizenCommand()
        {
            LastName = "LastName",
            FirstName = "FirstName",
            PatronymicName = "PatronymicName",
            Pin = "11006199814512",
            PassportNumber = "ID1234567",
            Gender = Gender.Male,
            BirthDate = new DateOnly(1999, 1, 1),
            DeathDate = new DateOnly(2014, 4, 4),
            RegisteredAddress = "RegisteredAddress",
            ActualAddress = "ActualAddress",
            EducationId = educationId,
            YearsOfWorkTotal = 3,
            YearsOfWorkInIndustry = 3,
            YearsOfWorkInCollective = 3,
            ImageName = "image.jpg",
            Image = Convert.ToBase64String(new byte[] { 1, 2 }),
        };
}