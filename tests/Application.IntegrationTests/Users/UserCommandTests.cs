using Application.Common.Exceptions;
using Application.Users.Commands;
using Application.Users.Queries;
using FluentAssertions;
using FluentAssertions.Extensions;

namespace Application.IntegrationTests.Users;

[Collection(nameof(DatabaseCollection))]
public class UserCommandTests : BaseDatabaseTest
{
    public UserCommandTests(DatabaseFixture fixture) : base(fixture)
    {
    }
    
    [Fact]
    public async Task CreatesUser()
    {
        var userId = await _fixture.RunAsAdministrator();
        var createCommand = new CreateUserCommand()
        {
            UserName = "UserName",
            Password = "Password",
            LastName = "LastName",
            FirstName = "FirstName",
            PatronymicName = "PatronymicName",
            Email = "email@test.com",
            Pin = "20101197012345",
            Image = Convert.ToBase64String(new byte[] { 1, 2, 3}),
        };

        var createdUserDto = await _fixture.SendAsync(createCommand);

        createdUserDto.Should().NotBeNull();
        createdUserDto.Should().BeEquivalentTo(createCommand, options =>
            options.Excluding(command => command.Password));
        createdUserDto.CreatedBy.Should().Be(userId);
        createdUserDto.ModifiedBy.Should().Be(userId);
        createdUserDto.CreatedAt.Should().BeCloseTo(createdUserDto.ModifiedAt, 5.Seconds());
    }


    [Fact]
    public async Task DeletesUser()
    {
        await _fixture.RunAsAdministrator();
        var createCommand = new CreateUserCommand()
        {
            UserName = "UserName",
            Password = "Password",
            LastName = "LastName",
            FirstName = "FirstName",
            PatronymicName = "PatronymicName",
            Email = "email@test.com",
            Pin = "20101197012345",
            Image = Convert.ToBase64String(new byte[] { 1, 2, 3}),
        };

        var createdUserDto = await _fixture.SendAsync(createCommand);
        var deleteCommand = new DeleteUserCommand() { Id = createdUserDto.Id };
        await _fixture.SendAsync(deleteCommand);

        var act = async () => await _fixture.SendAsync(new GetUserQuery() { Id = createdUserDto.Id });

        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task WhenDeletingUser_ThrowsNotFound()
    {
        await _fixture.RunAsAdministrator();

        var act = async () => await _fixture.SendAsync(new DeleteUserCommand() { Id = 1 });
        await act.Should().ThrowAsync<NotFoundException>();
    }
    
    [Fact]
    public async Task UpdatesUserDetails()
    {
        var userId = await _fixture.RunAsAdministrator();
        var createCommand = new CreateUserCommand()
        {
            UserName = "UserName",
            Password = "Password",
            LastName = "LastName",
            FirstName = "FirstName",
            PatronymicName = "PatronymicName",
            Email = "email@test.com",
            Pin = "20101197012345",
            Image = Convert.ToBase64String(new byte[] { 1, 2, 3}),
        };

        var created = await _fixture.SendAsync(createCommand);

        var updateCommand = new UpdateUserDetailsCommand()
        {
            Id = created.Id,
            UserName = "UserNameUpdated",
            LastName = "LastName Updated",
            FirstName = "FirstName Updated",
            PatronymicName = "PatronymicName Updated",
            Email = "updated@test.com",
            Pin = "20101197054321",
        };
        await _fixture.SendAsync(updateCommand);
        
        var updated = await _fixture.SendAsync(new GetUserQuery() { Id = created.Id });

        updated.Should().NotBeNull();
        updated.Should().BeEquivalentTo(updateCommand);
        
        updated.CreatedBy.Should().Be(created.CreatedBy);
        updated.CreatedAt.Should().BeCloseTo(created.CreatedAt, 1.Seconds());
        updated.ModifiedBy.Should().Be(userId);
        updated.ModifiedAt.Should().BeAfter(created.ModifiedAt);
    }
    
    [Fact]
    public async Task WhenUpdatingUser_ThrowsNotFound()
    {
        await _fixture.RunAsAdministrator();
        
        var updateCommand = new UpdateUserDetailsCommand()
        {
            Id = 100,
            UserName = "UserNameUpdated",
            LastName = "LastName Updated",
            FirstName = "FirstName Updated",
            PatronymicName = "PatronymicName Updated",
            Email = "updated@test.com",
            Pin = "20101197054321",
        };
        var act = async () => await _fixture.SendAsync(updateCommand);

        await act.Should().ThrowAsync<NotFoundException>();
    }
    
    [Fact]
    public async Task UpdateUserImage()
    {
        var userId = await _fixture.RunAsAdministrator();
        var createCommand = new CreateUserCommand()
        {
            UserName = "UserName",
            Password = "Password",
            LastName = "LastName",
            FirstName = "FirstName",
            PatronymicName = "PatronymicName",
            Email = "email@test.com",
            Pin = "20101197012345",
            Image = Convert.ToBase64String(new byte[] { 1, 2, 3 }),
        };

        var created = await _fixture.SendAsync(createCommand);

        var updateCommand = new UpdateUserImageCommand()
        {
            Id = created.Id,
            Image = Convert.ToBase64String(new byte[] { 1, 2, 3, 4, 5 })
        };
        await _fixture.SendAsync(updateCommand);
        
        var updated = await _fixture.SendAsync(new GetUserQuery() { Id = created.Id });
        
        updated.Should().NotBeNull();
        updated.Should().BeEquivalentTo(updateCommand);
        
        updated.CreatedBy.Should().Be(created.CreatedBy);
        updated.CreatedAt.Should().BeCloseTo(created.CreatedAt, 1.Seconds());
        updated.ModifiedBy.Should().Be(userId);
        updated.ModifiedAt.Should().BeAfter(created.ModifiedAt);
    }

    [Fact]
    public async Task UpdatesUserPassword()
    {
        var userId = await _fixture.RunAsAdministrator();
        var createCommand = new CreateUserCommand()
        {
            UserName = "UserName",
            Password = "Password",
            LastName = "LastName",
            FirstName = "FirstName",
            PatronymicName = "PatronymicName",
            Email = "email@test.com",
            Pin = "20101197012345",
            Image = Convert.ToBase64String(new byte[] { 1, 2, 3 }),
        };

        var created = await _fixture.SendAsync(createCommand);

        var updateCommand = new UpdateUserPasswordCommand() { Id = created.Id, NewPassword = "NewPassword" };
        await _fixture.SendAsync(updateCommand);
        
        var updated = await _fixture.SendAsync(new GetUserQuery() { Id = created.Id });

        updated.CreatedBy.Should().Be(created.CreatedBy);
        updated.CreatedAt.Should().BeCloseTo(created.CreatedAt, 1.Seconds());
        updated.ModifiedBy.Should().Be(userId);
        updated.ModifiedAt.Should().BeAfter(created.ModifiedAt);
    }
    
    [Fact]
    public async Task WhenCreatingUser_ShouldDenyAnonymousUser()
    {
        var createCommand = new CreateUserCommand()
        {
            UserName = "UserName",
            Password = "Password",
            LastName = "LastName",
            FirstName = "FirstName",
            PatronymicName = "PatronymicName",
            Email = "email@test.com",
            Pin = "20101197012345",
            Image = Convert.ToBase64String(new byte[] { 1, 2, 3}),
        };

        var act = async () => await _fixture.SendAsync(createCommand);
        await act.Should().ThrowAsync<ForbiddenAccessException>();
    }

    [Fact]
    public async Task WhenDeletingUser_ShouldDenyAnonymousUser()
    {
        await _fixture.RunAsAdministrator();
        var createCommand = new CreateUserCommand()
        {
            UserName = "UserName",
            Password = "Password",
            LastName = "LastName",
            FirstName = "FirstName",
            PatronymicName = "PatronymicName",
            Email = "email@test.com",
            Pin = "20101197012345",
            Image = Convert.ToBase64String(new byte[] { 1, 2, 3}),
        };

        var createdUserDto = await _fixture.SendAsync(createCommand);
        var deleteCommand = new DeleteUserCommand() { Id = createdUserDto.Id };
        
        var act = async () => await _fixture.SendAsync(deleteCommand);

        _fixture.ResetCurrentUserId();
        await act.Should().ThrowAsync<ForbiddenAccessException>();
    }

    [Fact]
    public async Task WhenUpdatingUserDetails_ShouldDenyAnonymousUser()
    {
        await _fixture.RunAsAdministrator();
        var createCommand = new CreateUserCommand()
        {
            UserName = "UserName",
            Password = "Password",
            LastName = "LastName",
            FirstName = "FirstName",
            PatronymicName = "PatronymicName",
            Email = "email@test.com",
            Pin = "20101197012345",
            Image = Convert.ToBase64String(new byte[] { 1, 2, 3}),
        };

        var createdUserDto = await _fixture.SendAsync(createCommand);
        var updateCommand = new UpdateUserDetailsCommand()
        {
            Id = createdUserDto.Id,
            UserName = "UserNameUpdated",
            LastName = "LastName Updated",
            FirstName = "FirstName Updated",
            PatronymicName = "PatronymicName Updated",
            Email = "updated@test.com",
            Pin = "20101197054321",
        };
        
        var act = async () => await _fixture.SendAsync(updateCommand);
        _fixture.ResetCurrentUserId();
        await act.Should().ThrowAsync<ForbiddenAccessException>();
    }
    
    
    [Fact]
    public async Task WhenUpdatingImage_ShouldDenyAnonymousUser()
    {
        await _fixture.RunAsAdministrator();
        var createCommand = new CreateUserCommand()
        {
            UserName = "UserName",
            Password = "Password",
            LastName = "LastName",
            FirstName = "FirstName",
            PatronymicName = "PatronymicName",
            Email = "email@test.com",
            Pin = "20101197012345",
            Image = Convert.ToBase64String(new byte[] { 1, 2, 3 }),
        };

        var createdUserDto = await _fixture.SendAsync(createCommand);
        var updateCommand = new UpdateUserImageCommand()
        {
            Id = createdUserDto.Id, Image = Convert.ToBase64String(new byte[] { 1, 2, 3, 4, 5 })
        };
        
        var act = async () => await _fixture.SendAsync(updateCommand);
        
        _fixture.ResetCurrentUserId();
        await act.Should().ThrowAsync<ForbiddenAccessException>();
    }
    
    
    [Fact]
    public async Task WhenUpdatingPassword_ShouldDenyAnonymousUser()
    {
        await _fixture.RunAsAdministrator();
        var createCommand = new CreateUserCommand()
        {
            UserName = "UserName",
            Password = "Password",
            LastName = "LastName",
            FirstName = "FirstName",
            PatronymicName = "PatronymicName",
            Email = "email@test.com",
            Pin = "20101197012345",
            Image = Convert.ToBase64String(new byte[] { 1, 2, 3 }),
        };

        var createdUserDto = await _fixture.SendAsync(createCommand);
        var updateCommand = new UpdateUserPasswordCommand() { Id = createdUserDto.Id, NewPassword = "NewPassword" };
        
        var act = async () => await _fixture.SendAsync(updateCommand);

        _fixture.ResetCurrentUserId();
        await act.Should().ThrowAsync<ForbiddenAccessException>();
    }
}
