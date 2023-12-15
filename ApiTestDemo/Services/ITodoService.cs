using ApiTestDemo.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ApiTestDemo.Services;

public interface ITodoService
{
    string HelloWorld();
    int Penambahan(int x, int y);
    int Pengurangan(int x, int y);
    int Pembagian(int x, int y);

    Task<TodoDto> AddAsync(TodoForCreationDto todoForCreationDto);
    
    Task<TodoDto?> GetByIdAsync(long id);
}