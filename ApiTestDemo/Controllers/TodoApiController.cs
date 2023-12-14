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
        if (dto.x == null) return Json(new
        {
            status = "fail",
            result = "x not found"
        });
        if (dto.y==null) return Json(new
        {
            status = "fail",
            result = "y not found"
        });
        return Json(new
        {
            status = "ok",
            result = _todoService.Penambahan(dto.x.GetValueOrDefault(), dto.y.GetValueOrDefault())
        });
    }

    [HttpPost("Pengurangan")]
    public JsonResult Pengurangan([FromBody] PenambahanDto dto)
    {
        return Json(new
        {
            status = "ok",
            result = _todoService.Pengurangan(dto.x.GetValueOrDefault(), dto.y.GetValueOrDefault())
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