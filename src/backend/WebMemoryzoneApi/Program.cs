using Application;
using Infrastructure;
using Infrastructure.Data.Seed;
using Infrastructure.Middleware;
using Infrastructure.ModelBinding;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration;
builder.Services.AddControllers(options =>
{
    options.ModelBinderProviders.Insert(0, new CreateProductCommandModelBinderProvider());
});
builder.Services.AddApplicationServices();
// Add Swagger services
builder.Services.AddInfrastructureServices(configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
    // Register the operation filter
});
builder.Services.AddScoped<ISeed, SeedData>();


builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var seed = scope.ServiceProvider.GetService<ISeed>();
    await seed.InitData();
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.UseAuthorization();
app.MapControllers();
app.Run();
