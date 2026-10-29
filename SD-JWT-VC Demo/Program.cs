using SD_JWT_VC_Demo.Components;
using SD_JWT_VC_Demo.Models;
using SD_JWT_VC_Demo.Services;

namespace SD_JWT_VC_Demo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.Configure<DemoOptions>(builder.Configuration.GetSection(nameof(DemoOptions)));

            builder.Services.AddTransient<UtilService>();
            builder.Services.AddTransient<JwtService>();
            builder.Services.AddTransient<IssuerService>();
            builder.Services.AddTransient<HolderService>();
            builder.Services.AddTransient<VerifierService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
            app.UseAntiforgery();

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
