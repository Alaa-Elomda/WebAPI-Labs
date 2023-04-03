using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Registeration.BL.DTOs;
using Registeration.DAL;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace Registeration.BL;

public class EmployeesManager : IEmployeesManager
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<Employee> _userManager;

    public EmployeesManager(IConfiguration configuration,
        UserManager<Employee> userManager)
    {
        _configuration = configuration;
        _userManager = userManager;
    }

    #region Register
    public async Task<OperationResult<Employee>> RegisterEmployee(RegisterDto registerDto, string role)
    {
        var employee = new Employee
        {
            UserName = registerDto.UserName,
            Email = registerDto.Email,
            PerformanceRate = 50
        };

        var result = await _userManager.CreateAsync(employee, registerDto.Password);
        if (result.Succeeded)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, employee.Id),
                new Claim(ClaimTypes.Role, role),
        };

            await _userManager.AddClaimsAsync(employee, claims);

            return new OperationResult<Employee>(data: employee);
        }
        else
        {
            return new OperationResult<Employee>(errorMessage: result.Errors.First().Description);
        }
    }

    #endregion

    #region Login
    public async Task<OperationResult<TokenDto>> Login(LoginDto credentials)
    {
        Employee? employee = await _userManager.FindByNameAsync(credentials.UserName);
        if (employee is null)
        {
            return new OperationResult<TokenDto>("User not found");
        }

        var isPasswordCorrect = await _userManager.CheckPasswordAsync(employee, credentials.Password);
        if (!isPasswordCorrect)
        {
            return new OperationResult<TokenDto>("Invalid password");
        }

        var claims = await _userManager.GetClaimsAsync(employee);
        DateTime exp = DateTime.Now.AddMinutes(20);

        var tokenString = GenerateToken(claims, exp);
        return new OperationResult<TokenDto>(new TokenDto(tokenString));
    }

    #endregion

    #region Generate Token
    public string GenerateToken(IList<Claim> claimsList, DateTime exp)
    {
        var secretKeyString = _configuration.GetValue<string>("SecretKey") ?? "";
        var secretKeyInBytes = Encoding.ASCII.GetBytes(secretKeyString);
        var securityKey = new SymmetricSecurityKey(secretKeyInBytes);

        var signingCredentials = new SigningCredentials(securityKey,
            SecurityAlgorithms.HmacSha256Signature);


        var jwt = new JwtSecurityToken(
            claims: claimsList,
            expires: exp,
            signingCredentials: signingCredentials);

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenString = tokenHandler.WriteToken(jwt);

        return tokenString;
    }
    #endregion

}
