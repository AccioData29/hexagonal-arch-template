namespace Company.ProjectName.Host.Extensions;

public static class IApplicationBuilderExtensions
{
    public static WebApplication UseCustomSwagger(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Company.ProjectName API v1"));
        }
        return app;
    }
}
