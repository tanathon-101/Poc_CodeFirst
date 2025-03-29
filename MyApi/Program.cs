using Asp.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<EmployeeContext>(opts => 
    opts.UseSqlServer(builder.Configuration["ConnectionString:EmployeeDB"]));
builder.Services.AddScoped<IEmployeeProcess, EmployeeProcess>();  
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();      



builder.Services.AddControllers();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Employee API",
        Version = "1.0",
        Description = "API for managing employee data",
    });
   options.EnableAnnotations();
});


builder.Services.AddEndpointsApiExplorer();

// Configure Swagger with additional options


var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
     app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.MapControllers();
await app.RunAsync();