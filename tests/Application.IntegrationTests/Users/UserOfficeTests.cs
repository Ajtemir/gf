using Application.Common.Dto;
using Application.Offices.Commands;
using Application.Users.Commands;
using Application.Users.Queries;
using FluentAssertions;

namespace Application.IntegrationTests.Users;

public class UserOfficeTests : BaseDatabaseTest
{
    public UserOfficeTests(DatabaseFixture fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task AddUserToOffice()
    {
        var userId = await _fixture.RunAsAdministrator();
        var createOfficeCommand = new CreateOfficeCommand() { Id = 1, NameRu = "Name ru", NameKg = "Name kg"};
        var office = await _fixture.SendAsync(createOfficeCommand);

        var addUserToOfficeCommand = new AddUserToOfficeCommand() { UserId = userId, OfficeId = office.Id };
        await _fixture.SendAsync(addUserToOfficeCommand);

        var user = await _fixture.SendAsync(new GetUserQuery() { Id = userId });
        user.Offices.Should().Contain(new UserOfficeDto()
        {
            OfficeId = office.Id,
            NameRu = createOfficeCommand.NameRu,
            NameKg = createOfficeCommand.NameKg 
        });
    }

    [Fact]
    public async Task DeletesUserFromOffice()
    {
        var userId = await _fixture.RunAsAdministrator();
        var createOfficeCommand = new CreateOfficeCommand() { Id = 1, NameRu = "Name ru", NameKg = "Name kg"};
        var office = await _fixture.SendAsync(createOfficeCommand);

        var addUserToOfficeCommand = new AddUserToOfficeCommand() { UserId = userId, OfficeId = office.Id };
        await _fixture.SendAsync(addUserToOfficeCommand);
        var deleteUserFromOfficeCommand = new DeleteUserFromOfficeCommand() { UserId = userId, OfficeId = office.Id };
        await _fixture.SendAsync(deleteUserFromOfficeCommand);
        
        var user = await _fixture.SendAsync(new GetUserQuery() { Id = userId });
        user.Offices.Should().NotContain(new UserOfficeDto()
        {
            OfficeId = office.Id,
            NameRu = createOfficeCommand.NameRu,
            NameKg = createOfficeCommand.NameKg,
        });
    }
}