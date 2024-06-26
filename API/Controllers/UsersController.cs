﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace API.Controllers;

public class UsersController : BaseApiController
{
    private readonly DataContext _dataContext;

    public UsersController(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetAllUsers()
    {
        var users = await _dataContext.AppUsers.ToListAsync();
        if (users is null)
        {
            return NotFound();
        }
        return Ok(users);
    }
    [HttpGet]
    [Route("{id}")]
    [Authorize]
    public async Task<ActionResult<AppUser>> GetUserbyId(int id)
    {
        var user = await _dataContext.AppUsers.FirstOrDefaultAsync(x => x.Id == id);
        if (user is null)
        {
            return NotFound();
        }
        return Ok(user);
    }
}
