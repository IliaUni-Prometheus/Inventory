using Hellang.Middleware.ProblemDetails;
using Shared.Extensions;

namespace Inventory.Extensions
{
    public static class CustomErrorHandlingExtensions
    {
        public static void UseCustomExceptionHandler(this IApplicationBuilder app)
        {
            app.UseProblemDetails();
        }

        public static void AddCustomErrorHandling(this IServiceCollection services)
        {
            services.AddProblemDetails(opt =>
            {
                opt.ShouldLogUnhandledException = (ctx, ex, pb) => true;
                opt.IncludeExceptionDetails = (ctx, err) =>
                {
                    var env = ctx.RequestServices.GetRequiredService<IHostEnvironment>();
                    return env.IsDevelopment();
                };
                opt.Map<BusinessException>((ctx, ex) =>
                {
                    var factory = ctx.RequestServices.GetRequiredService<ProblemDetailsFactory>();
                     
                    return factory.CreateProblemDetails(ctx, StatusCodes.Status400BadRequest, detail: ex.Message, type: "Business");
                });
            });
        }
    }
}
