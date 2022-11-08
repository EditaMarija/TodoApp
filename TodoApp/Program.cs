using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TodoApp;
using TodoApp.Model;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();

//var todos = new List<Todo>()
//{
//    new Todo("Male huset"),
//    new Todo("Skifte til vinterdekk"),
//    new Todo("Snekre terrasse")
//};

var json = File.ReadAllText("todos.json");
var todos = JsonSerializer.Deserialize<List<Todo>>(json);

app.MapGet("/todo/{id}", (Guid id) =>
{
    return todos.SingleOrDefault(t => t.Id == id);
});

app.MapGet("/todo", () =>
{
    return todos;
});

app.MapPut("/todo", async (Todo todo) =>
{
    var todoInList = todos.SingleOrDefault(t => t.Id == todo.Id);   
    todoInList.DoneDate = DateTime.Today;
    var json = JsonSerializer.Serialize(todos);
    await File.WriteAllTextAsync("todos.json", json);
});

app.MapPost("/todo", async (Todo todo) =>
{
    todos.Add(todo);
    var json = JsonSerializer.Serialize(todos);
    await File.WriteAllTextAsync("todos.json", json);
});

app.MapDelete("/todo/{id}", async (Guid id) =>
{
    //var guidId = Guid.Parse(id);
    //var todoInList = todos.FirstOrDefault(t => t.Id == guidId);
    //if (todoInList == null) throw new NullReferenceException();
    //todos.Remove(todoInList);
    //var json = JsonSerializer.Serialize(todos);
    //await File.WriteAllTextAsync("todos.json", json);

    todos.RemoveAll(it => it.Id == id);
});



app.Run();
