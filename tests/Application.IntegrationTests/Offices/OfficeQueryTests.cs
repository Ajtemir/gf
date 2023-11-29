using Application.Common.Exceptions;
using Application.Offices.Commands;
using Application.Offices.Queries;
using FluentAssertions;

namespace Application.IntegrationTests.Offices;

public class OfficeQueryTests : BaseDatabaseTest
{
    public OfficeQueryTests(DatabaseFixture fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task ReturnsSingleOfficeDto()
    {
        await _fixture.RunAsAdministrator();
        var createCommand = new CreateOfficeCommand() { Id = 1, NameRu = "Name ru", NameKg = "Name kg" };
        var officeDto = await _fixture.SendAsync(createCommand);

        officeDto.Should().NotBeNull();
        officeDto.Should().BeEquivalentTo(createCommand);
    }

    [Fact]
    public async Task WhenQueryingOffice_ThrowsNotFound()
    {
        await _fixture.RunAsAdministrator();
        
        var act = async () => await _fixture.SendAsync(new GetOfficeQuery() { Id = 1 });
        await act.Should().ThrowAsync<NotFoundException>();
    }
}