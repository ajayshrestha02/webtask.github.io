
using EVJ.Infrastructure.Interface;
using EVJ.Infrastructure;

namespace EVJ
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


			var emailConfig =builder.Configuration.GetSection("EmailConfig").Get<EVJ.Configs.EmailConfig>();

			builder.Services.AddSingleton(emailConfig);
			builder.Services.AddScoped<IEmailSender, EmailSender>();

			// Add services to the container.
			builder.Services.AddControllersWithViews();

			var app = builder.Build();
			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}