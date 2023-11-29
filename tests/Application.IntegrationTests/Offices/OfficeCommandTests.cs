using Application.Offices.Commands;
using Application.Offices.Queries;
using FluentAssertions;
using FluentAssertions.Extensions;

namespace Application.IntegrationTests.Offices;

public class OfficeCommandTests : BaseDatabaseTest
{
    public OfficeCommandTests(DatabaseFixture fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task CreatesOffice()
    {
        var userId = await _fixture.RunAsAdministrator();

        var createCommand = new CreateOfficeCommand() { Id = 1, NameRu = "Name ru", NameKg = "Name kg" };
        var officeDto = await _fixture.SendAsync(createCommand);

        officeDto.Should().NotBeNull();
        officeDto.Should().BeEquivalentTo(createCommand);

        officeDto.CreatedBy.Should().Be(userId);
        officeDto.ModifiedBy.Should().Be(userId);
        officeDto.CreatedAt.Should().BeCloseTo(officeDto.ModifiedAt, 1.Seconds());
        officeDto.CreatedByUser.Should().NotBeNullOrEmpty().And.Be(officeDto.ModifiedByUser);
    }

    [Fact]
    public async Task UpdatesOffice()
    {
        await _fixture.RunAsAdministrator();
        var createCommand = new CreateOfficeCommand() { Id = 1, NameRu = "Name ru", NameKg = "Name kg" };
        var officeDto = await _fixture.SendAsync(createCommand);

        var createParentCommand =
            new CreateOfficeCommand() { Id = 2, NameRu = "Name parent ru", NameKg = "Name parent kg" };
        var created = await _fixture.SendAsync(createParentCommand);

        var updateCommand = new UpdateOfficeCommand()
        {
            Id = officeDto.Id, NewId = 3, NameRu = "Name ru updated", NameKg = "Name kg updated"
        };
        await _fixture.SendAsync(updateCommand);
        var updated = await _fixture.SendAsync(new GetOfficeQuery() { Id = updateCommand.NewId });

        updated.Id.Should().Be(updateCommand.NewId);
        updated.NameRu.Should().Be(updateCommand.NameRu);
        updated.NameKg.Should().Be(updateCommand.NameKg);
        updated.CreatedBy.Should().Be(created.CreatedBy);
        updated.CreatedAt.Should().BeCloseTo(created.CreatedAt, 1.Seconds());
    }

    [Fact]
    public async Task DeletesOffice()
    {
        await _fixture.RunAsAdministrator();
        var createCommand = new CreateOfficeCommand() { Id = 1, NameRu = "Name ru", NameKg = "Name kg" };
        var officeDto = await _fixture.SendAsync(createCommand);

        var act = async () => await _fixture.SendAsync(new DeleteOfficeCommand() { Id = officeDto.Id });

        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task AddsChildOfficesToOffice()
    {
        await _fixture.RunAsAdministrator();
        var createCommand = new CreateOfficeCommand() { Id = 1, NameRu = "Name ru", NameKg = "Name kg" };
        var created = await _fixture.SendAsync(createCommand);

        var childOffice1 =
            await _fixture.SendAsync(new CreateOfficeCommand()
            {
                Id = 2, NameRu = "First child", NameKg = "First child"
            });
        var childOffice2 =
            await _fixture.SendAsync(new CreateOfficeCommand()
            {
                Id = 3, NameRu = "Second child", NameKg = "Second child"
            });

        var updateChildOfficeFkCommand = new UpdateChildOfficeFkCommand()
        {
            OfficeId = created.Id, ChildOfficeIds = new[] { childOffice1.Id, childOffice2.Id, }
        };
        await _fixture.SendAsync(updateChildOfficeFkCommand);

        var officeDto = await _fixture.SendAsync(new GetOfficeQuery() { Id = created.Id, IncludeChildOffices = true });
        officeDto.ChildOffices.Should()
            .NotBeNull().And
            .HaveCount(updateChildOfficeFkCommand.ChildOfficeIds.Count());
    }

    [Fact]
    public async Task AddsParentOfficesToOffice()
    {
        await _fixture.RunAsAdministrator();
        var createCommand = new CreateOfficeCommand() { Id = 1, NameRu = "Name ru", NameKg = "Name kg" };
        var created = await _fixture.SendAsync(createCommand);

        var parentOffice1 =
            await _fixture.SendAsync(new CreateOfficeCommand()
            {
                Id = 2, NameRu = "First parent", NameKg = "First parent"
            });
        var parentOffice2 =
            await _fixture.SendAsync(new CreateOfficeCommand()
            {
                Id = 3, NameRu = "Second parent", NameKg = "Second parent"
            });
        var updateParentOfficeFkCommand = new UpdateParentOfficeFkCommand()
        {
            OfficeId = created.Id, ParentOfficeIds = new[] { parentOffice1.Id, parentOffice2.Id }
        };
        await _fixture.SendAsync(updateParentOfficeFkCommand);

        var officeDto = await _fixture.SendAsync(new GetOfficeQuery()
        {
            Id = created.Id, IncludeChildOffices = true, IncludeParentOffices = true
        });
        officeDto.ParentOffices.Should()
            .NotBeNull().And
            .HaveCount(updateParentOfficeFkCommand.ParentOfficeIds.Count());
    }

    [Fact]
    public async Task AddOfficeToParentOffices()
    {
        await _fixture.RunAsAdministrator();
        var createCommand =
            new CreateOfficeCommand() { Id = 1, NameRu = "Child office ru", NameKg = "Child office kg" };
        var created = await _fixture.SendAsync(createCommand);
        var createParentCommand =
            new CreateOfficeCommand() { Id = 2, NameRu = "Parent office ru", NameKg = "Parent office kg" };
        var parent = await _fixture.SendAsync(createParentCommand);

        await _fixture.SendAsync(new AddParentOfficeCommand() { OfficeId = created.Id, ParentOfficeId = parent.Id });

        var officeDto = await _fixture.SendAsync(new GetOfficeQuery() { Id = created.Id, IncludeParentOffices = true });
        officeDto.ParentOffices.Should().NotBeNull().And
            .Contain(parentOffice => parentOffice.Id == parent.Id);
    }

    [Fact]
    public async Task AddOfficeToChildOffices()
    {
        await _fixture.RunAsAdministrator();
        var createCommand = new CreateOfficeCommand() { Id = 1, NameRu = "Office ru", NameKg = "Office kg" };
        var created = await _fixture.SendAsync(createCommand);
        var createChildOffice =
            new CreateOfficeCommand() { Id = 2, NameRu = "Child office ru", NameKg = "Child office kg" };
        var child = await _fixture.SendAsync(createChildOffice);

        await _fixture.SendAsync(new AddChildOfficeCommand() { OfficeId = created.Id, ChildOfficeId = child.Id });
        var officeDto = await _fixture.SendAsync(new GetOfficeQuery() { Id = created.Id, IncludeChildOffices = true });

        officeDto.ChildOffices.Should().NotBeNull().And
            .Contain(childOffice => childOffice.Id == child.Id);
    }

    [Fact]
    public async Task RemovesParentOffice()
    {
        await _fixture.RunAsAdministrator();
        var createCommand =
            new CreateOfficeCommand() { Id = 1, NameRu = "Child office ru", NameKg = "Child office kg" };
        var created = await _fixture.SendAsync(createCommand);
        var createParentCommand =
            new CreateOfficeCommand() { Id = 2, NameRu = "Parent office ru", NameKg = "Parent office kg" };
        var parent = await _fixture.SendAsync(createParentCommand);

        await _fixture.SendAsync(new AddParentOfficeCommand() { OfficeId = created.Id, ParentOfficeId = parent.Id });
        await _fixture.SendAsync(new DeleteParentOfficeCommand() { OfficeId = created.Id, ParentOfficeId = parent.Id });

        var officeDto = await _fixture.SendAsync(new GetOfficeQuery() { Id = created.Id, IncludeParentOffices = true });

        officeDto.ParentOffices.Should().NotContain(parentOffice => parentOffice.Id == parent.Id);
    }

    [Fact]
    public async Task RemovesChildOffice()
    {
        await _fixture.RunAsAdministrator();
        var createCommand = new CreateOfficeCommand() { Id = 1, NameRu = "Office ru", NameKg = "Office kg" };
        var created = await _fixture.SendAsync(createCommand);
        var createChildOffice = new CreateOfficeCommand() { Id = 2, NameRu = "Child office ru", NameKg = "Child office kg" };
        var child = await _fixture.SendAsync(createChildOffice);

        await _fixture.SendAsync(new AddChildOfficeCommand() { OfficeId = created.Id, ChildOfficeId = child.Id });
        await _fixture.SendAsync(new DeleteChildOfficeCommand() { OfficeId = created.Id, ChildOfficeId = child.Id });

        var officeDto = await _fixture.SendAsync(new GetOfficeQuery() { Id = created.Id, IncludeChildOffices = true });

        officeDto.ChildOffices.Should().NotContain(childOffice => childOffice.Id == child.Id);
    }
}