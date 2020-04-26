using Microsoft.AspNetCore.Builder;

namespace AppWeather.Api.Framework.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseApiRequestPipeline(this IApplicationBuilder app)
        {
            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            app.UseSwagger();

            //set swagger at app root
            //REF: https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-3.1&tabs=visual-studio
            app.UseSwaggerUI(cfg =>
            {
                cfg.SwaggerEndpoint("swagger/v1/swagger.json", "AppWeather API V1");
                cfg.RoutePrefix = string.Empty;
            });

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public static void UseEngineContext(this IApplicationBuilder app)
        {
            EngineContext.Current.SetServiceProvider(app.ApplicationServices);
        }
    }
}
