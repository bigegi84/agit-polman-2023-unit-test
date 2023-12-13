using ApiTestDemo.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ApiTestDemo.Services;

public interface ITodoService
{
    Task<TodoDto> AddAsync(TodoForCreationDto todoForCreationDto);
    
    Task<TodoDto?> GetByIdAsync(long id);
    //JsonResult HelloWorld();
    int Penambahan(int x, int y);
}