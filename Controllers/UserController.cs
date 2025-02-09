using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ToDoApi.Data;
using ToDoApi.Data.Entities;
using ToDoApi.Models;

namespace ToDoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly ToDoApiDbContext _context;
    private readonly IMapper _mapper;

    public UserController(ToDoApiDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetUsers()
    {
        var users = _context.Users.ToList();
        return Ok(_mapper.Map<List<UserModel>>(users));
    }

    [HttpGet("{id}")]
    public IActionResult GetUserById(Guid id)
    {
        var user = _context.Users.Find(id);
        if (user == null) return NotFound();
        `
        return Ok(_mapper.Map<UserModel>(user));
    }

    [HttpPost]
    public IActionResult CreateUser([FromBody] RegisterUserModel model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var user = _mapper.Map<UserEntity>(model);
        user.Id = Guid.NewGuid();
        user.CreateUser = DateTime.UtcNow;

        _context.Users.Add(user);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, _mapper.Map<UserModel>(user));
    }

    [HttpPut("{id}")]
    public IActionResult UpdateUser(Guid id, [FromBody] UserModel model)
    {
        var user = _context.Users.Find(id);
        if (user == null) return NotFound();

        _mapper.Map(model, user);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(Guid id)
    {
        var user = _context.Users.Find(id);
        if (user == null) return NotFound();

        _context.Users.Remove(user);
        _context.SaveChanges();

        return NoContent();
    }
}