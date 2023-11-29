using Application.Candidates.Queries;
using Application.Common.Exceptions;
using Application.Dictionaries.Commands;
using Application.Entities.Commands;
using Application.Foreigners.Commands;
using Application.Mothers.Commands;
using Domain.Entities;
using Domain.Enums;
using FluentAssertions;

namespace Application.IntegrationTests.Candidates.Queries;

public class CandidateQueryTests : BaseDatabaseTest
{
    public CandidateQueryTests(DatabaseFixture fixture) : base(fixture)
    {
    }
    

    [Fact]
    public async Task WhenQueryingCandidates_ThrowsNotFound()
    {
        await _fixture.RunAsAdministrator();
        var act = async () => await _fixture.SendAsync(new GetCandidateQuery() { Id = 1 });

        await act.Should().ThrowAsync<NotFoundException>();
    }
    
    [Fact]
    public async Task ReturnsSingleCandidate_Mother()
    {
        var userId = await _fixture.RunAsAdministrator();
        var createEntityCommand = new CreateMotherCommand()
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
        var created = await _fixture.SendAsync(createEntityCommand);

        var candidateDto = await _fixture.SendAsync(new GetCandidateQuery() { Id = created.Id });
        
        candidateDto.CandidateType.Should().Be(nameof(Mother));
        candidateDto.CreatedBy.Should().Be(userId).And.Be(created.CreatedBy);
        candidateDto.CreatedByUser.Should().NotBeEmpty().And
            .Be(candidateDto.ModifiedByUser);
    }

    [Fact]
    public async Task ReturnsSingleCandidate_Entity()
    {
        var userId = await _fixture.RunAsAdministrator();
        var createEntityCommand = new CreateEntityCommand()
        {
            NameRu = "Name ru",
            NameKg = "Name kg"
        };
        var created = await _fixture.SendAsync(createEntityCommand);

        var candidateDto = await _fixture.SendAsync(new GetCandidateQuery() { Id = created.Id });
        
        candidateDto.CandidateType.Should().Be(nameof(Entity));
        candidateDto.CreatedBy.Should().Be(userId).And.Be(created.CreatedBy);
        candidateDto.CreatedByUser.Should().NotBeEmpty().And
            .Be(candidateDto.ModifiedByUser);
    }

    [Fact]
    public async Task ReturnsMultipleCandidateDtos()
    {
        await _fixture.RunAsAdministrator();
        await CreateTwoCandidates();

        var candidateDtos = await _fixture.SendAsync(new GetCandidateListQuery());

        candidateDtos.Should().HaveCount(2);
    }

    [Fact]
    public async Task ReturnsEmptyCandidateList()
    {
        await _fixture.RunAsAdministrator();
        var candidateDtos = await _fixture.SendAsync(new GetCandidateListQuery());

        candidateDtos.Should().HaveCount(0);
    }

    private async Task CreateTwoCandidates()
    {
        var createEntityCommand = new CreateEntityCommand()
        {
            NameRu = "Name ru",
            NameKg = "Name kg",
            ImageName = "image.jpg",
            Image = Convert.ToBase64String(new byte[] {1, 2}),
        };
        await _fixture.SendAsync(createEntityCommand);
        
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
        await _fixture.SendAsync(createForeignerCommand);
    }
}