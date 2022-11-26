using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PaulsAutoParts.Common;
using PaulsAutoParts.DataLayer;
using PaulsAutoParts.EntityLayer;
using PaulsAutoParts.AppClasses;

namespace PaulsAutoParts
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddControllersWithViews();

      // The next three lines are for session state
      services.AddDistributedMemoryCache();
      services.AddSession();
      services.AddHttpContextAccessor();

      // Add a UserSession object
      services.AddScoped<AppSession>();

      // Setup the Paul's Auto Parts DB Context
      // Read in the connection string from the appSettings.json file
      services.AddDbContext<PaulsAutoPartsDbContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

      // Set global action filters
      services.AddMvc(options =>
      {
        options.Filters.Add<SetViewDataItemsFilter>();
      });

      // Add Repositories to Services Collection
      AddRepositories(services);
    }

    protected void AddRepositories(IServiceCollection services)
    {
      services.AddScoped<IRepository<Customer, CustomerSearch>, CustomerRepository>();
      services.AddScoped<IRepository<CustomerPayment, CustomerPaymentSearch>, CustomerPaymentRepository>();
      services.AddScoped<IRepository<OrderHeader, OrderHeaderSearch>, OrderHeaderRepository>();
      services.AddScoped<IRepository<OrderDetail, OrderDetailSearch>, OrderDetailRepository>();

      services.AddScoped<IRepository<Product, ProductSearch>, ProductRepository>();
      services.AddScoped<IRepository<PromoCode, PromoCodeSearch>, PromoCodeRepository>();
      services.AddScoped<IRepository<VehicleType, VehicleTypeSearch>, VehicleTypeRepository>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment()) {
        // app.UseDeveloperExceptionPage();
        app.UseExceptionHandler("/Error/ErrorDevelopment");
      }
      else {
        app.UseExceptionHandler("/Error/Error");
      }
      app.UseStaticFiles();
      app.UseSession();

      // Add code to display status code pages
      app.UseStatusCodePages();
      app.UseStatusCodePagesWithReExecute("/Error/StatusCodeReExecute", "?code={0}");

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{controller=Home}/{action=Index}/{id?}");
      });
    }
  }
}
