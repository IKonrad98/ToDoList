using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ToDoApi.Data;
using ToDoApi.Data.Entities;
using ToDoApi.Models;

namespace ToDoApi.Controllers;

[Route("api/todo")]
[ApiController]
public class ToDoItemController : ControllerBase
{
    private readonly ToDoApiDbContext _context;
    private readonly IMapper _mapper;

    public ToDoItemController(ToDoApiDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    public IActionResult Get(Guid id)
    {
        var entity = _context.ToDoItems.Find(id);
        if (entity == null) return NotFound();

        var model = _mapper.Map<ToDoItemModel>(entity);
        return Ok(model);
    }

    [HttpPost]
    public IActionResult Create(CreateToDoItemModel model)
    {
        var entity = _mapper.Map<ToDoItemEntity>(model);
        entity.Id = Guid.NewGuid();

        _context.ToDoItems.Add(entity);
        _context.SaveChanges();

        var result = _mapper.Map<ToDoItemModel>(entity);
        return CreatedAtAction(nameof(Get), new { id = entity.Id }, result);
    }

    [HttpPut("{id}")]
    public IActionResult Update(Guid id, UpdateToDoItemModel model)
    {
        var entity = _context.ToDoItems.Find(id);
        if (entity == null) return NotFound();

        _mapper.Map(model, entity);
        _context.SaveChanges();

        return NoContent();
    }
}