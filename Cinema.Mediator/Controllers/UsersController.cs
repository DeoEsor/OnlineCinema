using Microsoft.AspNetCore.Mvc;
using OnlineCinema.DAL.Users;
using OnlineCinema.Domain.User;

namespace Cinema.Mediator.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly EfUsers _efUsers;
    public UsersController(EfUsers context)
    {
        _efUsers = context;
    }
 
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<User>>> Get()
    {
        var users =  await _efUsers.GetListAsync();

        return new ObjectResult(users);
    }
 
    // GET api/users/5
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> Get(int id)
    {
        User user = null!;
        try
        {
            user = await _efUsers.FindByGuidAsync(id);
            return Ok(user);
        }
        catch (Exception e)
        {
            //logger
            return NotFound();
        }
    }
 
    // POST api/users
    [HttpPost]
    public async Task<ActionResult<User>> Post(User user)
    {
        if (user == null)
        {
            return BadRequest();
        }

        if (_efUsers.Context.Users
            .Any(u => u.Id == user.Id))
            return ValidationProblem();
        try
        {
            await _efUsers.AddAsync(user);
            return Ok(user);
        }
        catch (Exception e)
        {
            
            //logger
            return Problem();
        }
    }
 
    // PUT api/users/
    [HttpPut]
    public async Task<ActionResult<User>> Put(User user)
    {
        if (user == null)
            return BadRequest();
        try
        {
            await _efUsers.ChangeAsync(user);
            return Ok(user);
        }
        catch (Exception e)
        {
            return NotFound(user);
        }
    }
 
    // DELETE api/users/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<User>> Delete(int id)
    {
        try
        {
            await _efUsers.DeleteAsync(await _efUsers.FindByGuidAsync(id));            
            return Ok(id);
        }
        catch (Exception e)
        {
            return NotFound(id);
        }
    }
}