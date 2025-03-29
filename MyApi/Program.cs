using Asp.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<EmployeeContext>(opts => 
    opts.UseSqlServer(builder.Configuration["ConnectionString:EmployeeDB"]));
builder.Services.AddScoped<IEmployeeProcess, EmployeeProcess>();  
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();      
builder.Services.AddLogging();

// builder.Services.AddApiVersioning(options =>
// {
//     options.DefaultApiVersion = new ApiVersion(1, 0);
//     options.AssumeDefaultVersionWhenUnspecified = true;
//     options.ReportApiVersions = true;
//     options.ApiVersionReader = new UrlSegmentApiVersionReader(); // e.g., /api/v1/employees
// }).AddMvc() 
// .AddApiExplorer(options =>
// {
//     options.GroupNameFormat = "'v'VVV";
//     options.SubstituteApiVersionInUrl = true;
// });

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API",
        Version = "v1", // This is the API version, not OpenAPI version
        Description = "A sample API with versioning"
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
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1"));
    app.UseDeveloperExceptionPage();
}
app.UseRouting();
app.MapControllers();
await app.RunAsync();