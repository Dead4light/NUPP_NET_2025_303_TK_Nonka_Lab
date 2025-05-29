using Microsoft.EntityFrameworkCore;
using Zoo.Common;
using Zoo.Infrastructure;
using Zoo.Infrastructure.Data;
using Zoo.Infrastructure.Repositories;
using Zoo.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddOpenApiDocument(config =>
{
    config.AddSecurity("Bearer", new NSwag.OpenApiSecurityScheme
    {
        Type = NSwag.OpenApiSecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = NSwag.OpenApiSecurityApiKeyLocation.Header,
        Name = "Authorization",
        Description = "Type 'Bearer' followed by a space and your token"
    });
    
    config.OperationProcessors.Add(
        new NSwag.Generation.Processors.Security.AspNetCoreOperationSecurityScopeProcessor("Bearer"));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<ZooContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
    //options.UseMongoDB(config.GetConnectionString("Mongo")!, config.GetConnectionString("MongoDb")!));

builder.Services.AddScoped<IRepository<LionModel>, Repository<LionModel>>();

builder.Services.AddScoped<ICrudServiceAsync<LionModel>, DataCrudService<LionModel>>();

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowAll");
app.UseHttpsRedirection();

app.UseOpenApi();
app.UseSwaggerUi();
app.UseReDoc(config =>
{
    config.Path = "/redoc";
    config.DocumentPath = "/swagger/v1/swagger.json";
});


app.MapControllers();

app.Run();
