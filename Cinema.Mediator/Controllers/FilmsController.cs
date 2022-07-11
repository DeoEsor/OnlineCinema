using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineCinema.DAL;
using OnlineCinema.Domain;

namespace Cinema.Mediator.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmsController : ControllerBase
{
    private readonly EfFilms _efFilms;
    private readonly ILogger<FilmsController> _logger;
    public FilmsController(EfFilms context, ILogger<FilmsController> logger)
    {
        _efFilms = context;
        _logger = logger;
    }
 
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Film>>> Get()
    {
        var users =  await _efFilms.GetListAsync();

        return new ObjectResult(users);
    }
 
    // GET api/users/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Film>> Get(int id)
    {
        Film Film = null!;
        try
        {
            Film = await _efFilms.Context.Films.FirstOrDefaultAsync(film =>  film.Id == id)
                   ?? throw new KeyNotFoundException();
            return Ok(Film);
        }
        catch (Exception e)
        {
            
            _logger.LogError($"{e.Message}");
            return NotFound();
        }
    }
 
    // POST api/users
    [HttpPost]
    public async Task<ActionResult<Film>> Post(Film Film)
    {
        if (Film == null)
        {
            return BadRequest();
        }

        if (_efFilms.Context.Films
            .Any(u => u.Id == Film.Id))
            return ValidationProblem();
        try
        {
            await _efFilms.AddAsync(Film);
            return Ok(Film);
        }
        catch (Exception e)
        {
            _logger.LogError($"{e.Message}");
            return Problem();
        }
    }
 
    // PUT api/users/
    [HttpPut]
    public async Task<ActionResult<Film>> Put(Film Film)
    {
        if (Film == null)
            return BadRequest();
        try
        {
            await _efFilms.ChangeAsync(Film);
            return Ok(Film);
        }
        catch (Exception e)
        {
            
            _logger.LogError($"{e.Message}");
            return NotFound(Film);
        }
    }
 
    // DELETE api/users/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Film>> Delete(int id)
    {
        try
        {
            await _efFilms.DeleteAsync(await _efFilms.Context.Films.FirstOrDefaultAsync(u => u.Id == id)
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