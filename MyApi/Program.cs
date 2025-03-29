using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<EmployeeContext>(opts => 
    opts.UseSqlServer(builder.Configuration["ConnectionString:EmployeeDB"]));

//builder.Services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
builder.Services.AddScoped<IEmployeeProcess, EmployeeProcess>();  
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();      

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configure Swagger with additional options
builder.Services.AddSwaggerGen(c =>
{
    
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Employee API",
        Version = "v1",
        Description = "API for managing employee data",
        Contact = new OpenApiContact
        {
            Name = "Your Name",
            Email = "your.email@example.com"
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Employee API V1");
        c.RoutePrefix = string.Empty; // Makes Swagger UI available at root URL
        // Optional: Customize UI
        c.DisplayRequestDuration();
    });
}

app.MapControllers();
app.Run();