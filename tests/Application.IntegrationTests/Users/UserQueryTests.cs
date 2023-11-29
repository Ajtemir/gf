using Application.Users.Commands;
using Application.Users.Queries;
using FluentAssertions;

namespace Application.IntegrationTests.Users;

public class UserQueryTests : BaseDatabaseTest
{
    public UserQueryTests(DatabaseFixture fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task ReturnsSingleUserDto()
    {
        await _fixture.RunAsAdministrator();

        var createCommand = BuildCommand();
        var createdUserDto = await _fixture.SendAsync(createCommand);
        createdUserDto.Should().NotBeNull();
        createdUserDto.Should().BeEquivalentTo(createCommand, options => options
            .Excluding(x => x.Password));
    }

    [Fact]
    public async Task ReturnsMultipleUserDto()
    {
        await _fixture.RunAsAdministrator();
        var commands = BuildCommands(3);
        foreach (var command in commands)
        {
            await _fixture.SendAsync(command);
        }

        var userDtoPaginatedList = await _fixture.SendAsync(new GetUsersListQuery());

        userDtoPaginatedList.Should().NotBeNull();
        // Maybe admin or other users are created automatically
        userDtoPaginatedList.Items.Should().HaveCountGreaterOrEqualTo(3);
        userDtoPaginatedList.TotalCount.Should().BeGreaterOrEqualTo(3);
    }

    private static CreateUserCommand BuildCommand(int id = 0) =>
        new CreateUserCommand()
        {
            UserName = $"UserName-{id}",
            Password = $"Password {id}",
            LastName = $"LastName {id}",
            FirstName = $"FirstName {id}",
        };

    private static IEnumerable<CreateUserCommand> BuildCommands(int amount = 3) =>
        Enumerable.Range(0, amount).Select(BuildCommand);
}