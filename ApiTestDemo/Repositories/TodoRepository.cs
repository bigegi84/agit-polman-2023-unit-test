using ApiTestDemo.Data;
using ApiTestDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Reflection.Metadata.Ecma335;

namespace ApiTestDemo.Repositories;

public class TodoRepository : ITodoRepository
{
    private readonly TodoDbContext _todoDbContext;
    public DatabaseFacade _Db;

    public TodoRepository(TodoDbContext todoDbContext)
    {
        _todoDbContext = todoDbContext;
        _Db = todoDbContext.Database;
    }

    public async Task<Todo> CreateAsync(string title, string description)
    {
        var todo = new Todo { Title = title, Description = description, IsCompleted = false };
        _todoDbContext.Todos.Add(todo);
        await _todoDbContext.SaveChangesAsync();

        return todo;
    }

    public Task<Todo?> GetByIdAsync(long id)
    {
        return _todoDbContext.Todos.FirstOrDefaultAsync(t => t.Id == id);
    }
    //public JsonResult HelloWorld()
    //{
    //    return new
    //    {

    //    };
    //}
}