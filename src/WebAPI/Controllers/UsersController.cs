using Application.Common.Dto;
using Application.Users.Commands;
using Application.Users.Queries;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Authorize(Roles = nameof(DomainRole.Administrator))]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public class UsersController : ApiControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<PaginatedList<UserSummaryDto>>> GetUsers([FromQuery] GetUsersListQuery request,
        CancellationToken cancellationToken)
    {
        return await Mediator.Send(request, cancellationToken);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDto>> GetUser(int id, CancellationToken cancellationToken)
    {
        return await Mediator.Send(new GetUserQuery { Id = id }, cancellationToken);
    }

    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<UserDto>> Create(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var userDto = await Mediator.Send(request, cancellationToken);
        return CreatedAtAction("GetUser", new { id = userDto.Id }, userDto);
    }

    [HttpPut("{id:int}/update-details")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateUserDetails(int id, UpdateUserDetailsCommand request,
        CancellationToken cancellationToken)
    {
        request.Id = id;
        await Mediator.Send(request, cancellationToken);
        return NoContent();
    }

    [HttpPut("{id:int}/update-image")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateUserImage(int id, UpdateUserImageCommand request,
        CancellationToken cancellationToken)
    {
        request.Id = id;
        await Mediator.Send(request, cancellationToken);
        return NoContent();
    }

    [HttpPut("update/{id:int}/password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateUserPassword(int id, UpdateUserPasswordCommand request,
        CancellationToken cancellationToken)
    {
        request.Id = id;
        await Mediator.Send(request, cancellationToken);
        return NoContent();
    }

    [HttpDelete("delete/{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await Mediator.Send(new DeleteUserCommand { Id = id }, cancellationToken);
        return NoContent();
    }

    [HttpPost("add-to-office")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddUserToOffice(AddUserToOfficeCommand request, CancellationToken cancellationToken)
    {
        await Mediator.Send(request, cancellationToken);
        return NoContent();
    }
    
    [HttpPost("delete-from-office")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUserFromOffice(DeleteUserFromOfficeCommand request, CancellationToken cancellationToken)
    {
        await Mediator.Send(request, cancellationToken);
        return NoContent();
    }
}