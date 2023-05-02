using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodewarsSprintBackend.Models;
using CodewarsSprintBackend.Models.DTO;
using CodewarsSprintBackend.Services;


namespace CodewarsSprintBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
   private readonly UserService _data;
    public UserController(UserService dataFromService)
    {
        _data = dataFromService;

    }

    [HttpPost("Login")]
    public IActionResult Login([FromBody] LoginDTO User)
    {
        return _data.Login(User);
    }

    [HttpPost("AddUser")]
    public bool AddUser(CreateAccountDTO UserToAdd)
    {
        return _data.AddUser(UserToAdd);

    }

}