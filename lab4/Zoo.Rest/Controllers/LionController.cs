using Microsoft.AspNetCore.Mvc;
using Zoo.Common;
using Zoo.Infrastructure.Data;

namespace Zoo.Rest.Controllers;

[Route("/lions")]
public class LionController : Controller
{
    [HttpGet] 
    public async Task<IResult> GetAll(
        [FromServices] ICrudServiceAsync<LionModel> service)
    {
        return Results.Ok(await service.ReadAllAsync());
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IResult> Get(
        [FromRoute] Guid id,
        [FromServices] ICrudServiceAsync<LionModel> service)
    {
        var lion = await service.ReadAsync(id);
    
        if (lion == null)
        {
            return Results.NotFound($"Ноутбук з id {id} не знайдено");
        }
    
        return Results.Ok(lion);
    }
    
    [HttpPost]
    public async Task<IResult> Create(
        [FromBody] LionModel value,
        [FromServices] ICrudServiceAsync<LionModel> service)
    {
        var result = await service.CreateAsync(value);
    
        if (result)
            return Results.Created($"/lions/{value.Id}", value);
    
        return Results.BadRequest();
    }
    
    [HttpPatch("{id:guid}")]
    public async Task<IResult> Update(
        [FromRoute] Guid id,
        [FromBody] LionModel value,
        [FromServices] ICrudServiceAsync<LionModel> service)
    {
        value.Id = id;
            
        var result = await service.UpdateAsync(value);
    
        if (result)
            return Results.Ok(value);
    
        return Results.BadRequest();
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IResult> Delete(
        [FromRoute] Guid id,
        [FromServices] ICrudServiceAsync<LionModel> service)
    {
        var found = await service.ReadAsync(id);
    
        if (found == null)
            return Results.NotFound();
            
        var result = await service.RemoveAsync(found);
    
        if (result)
            return Results.Ok();
    
        return Results.BadRequest();
    }
}