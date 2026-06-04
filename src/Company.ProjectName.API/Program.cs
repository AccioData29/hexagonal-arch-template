using Company.ProjectName.API.Middlewares;
using Company.ProjectName.Host;
using Company.ProjectName.Host.Extensions;

var builder = WebApplication.CreateBuilder(args);

HostExtensions.ConfigurationService(builder.Services, builder.Configuration);

builder.Services.AddExceptionHandler<GlobalExceptionsHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.UseCustomSwagger();
app.UseExceptionHandler();
app.UseMiddleware<DomainExceptionMiddleware>();

app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();
