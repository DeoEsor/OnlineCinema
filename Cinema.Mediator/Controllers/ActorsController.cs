using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineCinema.DAL;
using OnlineCinema.Domain;

namespace Cinema.Mediator.Controllers;

[ApiController]
[Route("[controller]")]
public class ActorsController : ControllerBase
{
    private readonly EfActors _efActors;
    private readonly ILogger<ActorsController> _logger;

    public ActorsController(EfActors context, ILogger<ActorsController> logger)
    {
        _efActors = context;
        _logger = logger;
    }
 
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Actor>>> Get()
    {
        var users =  await _efActors.GetListAsync();

        return new ObjectResult(users);
    }
 
    // GET api/users/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Actor>> Get(int id)
    {
        Actor Actor = null!;
        try
        {
            Actor =  await _efActors.Context.Actors.FirstOrDefaultAsync(actor =>  actor.Id == id)
                ?? throw new KeyNotFoundException();
            return Ok(Actor);
        }
        catch (Exception e)
        {
            _logger.LogError($"{e.Message}");
            return NotFound();
        }
    }
 
    // POST api/users
    [HttpPost]
    public async Task<ActionResult<Actor>> Post(Actor Actor)
    {
        if (Actor == null)
        {
            return BadRequest();
        }

        if (_efActors.Context.Actors
            .Any(u => u.Id == Actor.Id))
            return ValidationProblem();
        try
        {
            await _efActors.AddAsync(Actor);
            return Ok(Actor);
        }
        catch (Exception e)
        {
            _logger.LogError($"{e.Message}");
            return Problem();
        }
    }
 
    // PUT api/users/
    [HttpPut]
    public async Task<ActionResult<Actor>> Put(Actor Actor)
    {
        if (Actor == null)
            return BadRequest();
        try
        {
            await _efActors.ChangeAsync(Actor);
            return Ok(Actor);
        }
        catch (Exception e)
        {
            _logger.LogError($"{e.Message}");
            return NotFound(Actor);
        }
    }
 
    // DELETE api/users/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Actor>> Delete(int id)
    {
        try
        {
            await _efActors.DeleteAsync(await _efActors.Context.Actors.FirstOrDefaultAsync(u => u.Id == id)
            ?? throw new KeyNotFoundException());            
            return Ok(id);
        }
        catch (Exception e)
        {
            _logger.LogError($"{e.Message}");
            return NotFound(id);
        }
    }
}