

namespace MVCApplication
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.
            BuildServices(builder.Services);

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

        private static void BuildServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddSingleton<ICustomerContainer, CustomerContainer>();
            services.AddSingleton<IOrderContainer, OrderContainer>();



            services.AddHttpClient<ICustomerClient, CustomerClient>(client =>
            {
                string baseAddress = "https://azurecandidatetestapi.azurewebsites.net/api/v1/";
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Add("accept", "text/plain");
                client.DefaultRequestHeaders.Add("ApiKey", "test1234");
            });
        }
    }
}

