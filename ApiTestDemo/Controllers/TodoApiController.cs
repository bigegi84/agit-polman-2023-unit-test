using ApiTestDemo.Dto;
using ApiTestDemo.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace ApiTestDemo.Controllers;

[ApiController]
[Route("api/todos")]
public class TodoApiController : Controller
{
    private readonly ITodoService _todoService;

    public TodoApiController(ITodoService todoService)
    {
        _todoService = todoService;
    }

    [HttpGet("HelloWorld")]
    public JsonResult HelloWorld()
    {
        return Json(new
        {
            status = "ok",
            result = _todoService.HelloWorld()
        });
    }

    [HttpPost("Penambahan")]
    public JsonResult Penambahan([FromBody] PenambahanDto dto)
    {
        return Json(new
        {
            status = "ok",
            result = _todoService.Penambahan(dto.x, dto.y)
        });
    }

    [HttpGet("{id:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<TodoDto>> GetById(long id)
    {
        var todo = await _todoService.GetByIdAsync(id);
        if (todo == null)
        {
            return NotFound();
        }

        return Ok(todo);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<TodoDto>> Create([FromBody] TodoForCreationDto dto)
    {
        var todo = await _todoService.AddAsync(dto);
        
        return CreatedAtAction(nameof(GetById), new { id = todo.Id }, todo);
    }
}