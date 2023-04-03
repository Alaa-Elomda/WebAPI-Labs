using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Registeration.DAL;

namespace Registeration.APIs.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DataController : ControllerBase
{
    private readonly UserManager<Employee> _userManager;

    public DataController(UserManager<Employee> userManager) 
    {
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<ActionResult> GetUserInfo()
    {
        Employee? user = await _userManager.GetUserAsync(User);
        return Ok(new string[] { user!.Email!, user.UserName! , user!.PerformanceRate.ToString() });
    }

    [HttpGet]
    [Route("admin-only")]
    [Authorize(Policy = "AllowAdminsOnly")]
    public async Task<ActionResult> GetInfoForManager()
    {
        Employee? user = await _userManager.GetUserAsync(User);
        return Ok(new string[] { user!.PerformanceRate.ToString() });
    }

    [HttpGet]
    [Route("adminAndUser-only")]
    [Authorize(Policy = "AllowAdminsAndUsersOnly")]
    public async Task<ActionResult> GetInfoForUser()
    {
        Employee? user = await _userManager.GetUserAsync(User);
        return Ok(new string[] {user!.UserName! , user!.PerformanceRate.ToString() });
    }
}
