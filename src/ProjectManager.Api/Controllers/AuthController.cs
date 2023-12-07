using Microsoft.AspNetCore.Authentication;
using ProjectManager.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using ProjectManager.Api.Controllers.Models.Auth;
using ProjectManager.Data.Entities;

namespace ProjectManager.Api.Controllers;
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IClock _clock;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AuthController(
        IClock clock,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager
        )
    {
        _clock = clock; 
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpPost("api/v1/Account/Register")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Register(
       [FromBody] RegisterModel model
       )
    {
        var now = _clock.GetCurrentInstant();

        var newUser = new ApplicationUser
        {
            Id = Guid.NewGuid(),
            FullName = model.Name,
            Email = model.Email,
            UserName = model.Email,
            EmailConfirmed = true,
        }.SetCreateBySystem(now);

        await _userManager.CreateAsync(newUser);
        await _userManager.AddPasswordAsync(newUser, model.Password);

        return Ok();
    }

    [HttpPost("api/v1/Account/Login")]
    public async Task<ActionResult> Login([FromBody] LoginModel model)
    {
        var user = await _userManager.FindByNameAsync(model.Email);

        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "LOGIN_FAILED");
            return ValidationProblem(ModelState);
        }

        var signInResult = await _signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: true);
        if (!signInResult.Succeeded)
        {
            ModelState.AddModelError(string.Empty, "LOGIN_FAILED");
            return ValidationProblem(ModelState);
        }

        var userPrincipal = await _signInManager.CreateUserPrincipalAsync(user);
        await HttpContext.SignInAsync(userPrincipal);

        return Ok();
    }

    [HttpPost("api/v1/Account/Logout")]
    public async Task<ActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return NoContent();
    }
}
