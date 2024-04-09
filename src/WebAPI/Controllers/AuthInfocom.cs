using Application.Common.Exceptions;
using Application.Common.Extensions;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebAPI.Extensions;
using WebAPI.Services.Integrator;

namespace WebAPI.Controllers;

public partial class AccountController
{
    [HttpGet("[action]")]
    [AllowAnonymous]
    public async Task<ActionResult> AuthInfocom([FromQuery]ArgumentCode argument)
    {
        var getAuthInfocomHrm = new GetAuthInfocomHrm(argument);
        var response = await getAuthInfocomHrm.ExecuteAsync();
        if (response.Status != "OK")
            throw new BadRequestException(response.Error);
        var user = await _context.Users.Include(x=>x.UserRoles).FirstOrDefaultAsync(x=>x.Pin == response.Employee.Pin);
        if (user == null)
        {
            user = new ApplicationUser
            {
                UserName = response.Employee.Email != null
                    ? response.Employee.Email.Split('@')[0]
                    : $"{response.Employee.Name[0]}.{response.Employee.Sname}",
                Email = response.Employee.Email,
                LockoutEnabled = false,
                CreatedAt = DateTime.Now.SetKindUtc(),
                ModifiedAt = DateTime.Now.SetKindUtc(),
                FirstName = response.Employee.Name,
                LastName = response.Employee.Sname,
                NormalizedUserName = response.Employee.Name.ToUpper(),
                NormalizedEmail = response.Employee.Email.ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString(),
                ModifiedBy = 1,
                CreatedBy = 1,
            };
            var password = new PasswordHasher<ApplicationUser>();
            var hashed = password.HashPassword(user,"password");
            user.PasswordHash = hashed;
            await _context.SaveChangesAsync();
        }

        var roleId = response.Employee.Level_id.ToInt();
        if (await _context.UserRoles.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == user.Id && x.RoleId == roleId) == null)
        {
            await _context.UserRoles.AddAsync(new ApplicationUserRole
            {
                RoleId = roleId,
                UserId = user.Id,
            });
            await _context.SaveChangesAsync();
        }
        
        return Ok(user.Pin);
    }
}

