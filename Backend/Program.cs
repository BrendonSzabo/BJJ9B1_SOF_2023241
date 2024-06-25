using Backend;
using Backend.Controllers;
using Backend.Data;
using Backend.Logic;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("Azure2Connection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options
    .UseSqlServer(connectionString)
    .UseLazyLoadingProxies()
    );
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<User>(options => {

    options.SignIn.RequireConfirmedAccount = false; // Ezt kell true-ra állítani ha akarsz email megerõsítést (direkt false egyenlõre)
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 3;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
}
)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAuthentication()
    .AddFacebook(opt =>
    {
        opt.AppId = "1021949482474546";
        opt.AppSecret = "ab9c8840dd297d0af2183a8c3c508734";
    })
    .AddMicrosoftAccount(opt =>
    {
        opt.ClientId = "26596dcd-3671-41e7-a08f-5059c5c27b2a";
        opt.ClientSecret = "iR18Q~E_ugIoFyQTUa-PRYwM6xosQjIeeI1BjcXk";
        opt.SaveTokens = true;
    }
    );

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.Services.AddTransient<IEmailSender, EmailSender>();

builder.Services.AddScoped<HomeController>();
builder.Services.AddScoped<HomeLogic>();
builder.Services.AddScoped<ModelController<Match>>();
builder.Services.AddScoped<ModelLogic<Match>>();
builder.Services.AddScoped<ModelController<Player>>();
builder.Services.AddScoped<ModelLogic<Player>>();
builder.Services.AddScoped<ModelController<Team>>();
builder.Services.AddScoped<ModelLogic<Team>>();
builder.Services.AddScoped<ModelController<User>>();
builder.Services.AddScoped<ModelLogic<User>>();
builder.Services.AddScoped<ApiController>();

builder.Services.AddControllersWithViews();

builder.Services.AddSignalR();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API Title", Version = "v1" });

    // Optionally, configure Swagger to use XML comments from your project
    // var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    // var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    // c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<SignalRHub>("/hub");
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
    // Optionally, serve Swagger UI at the root URL (http://localhost:<port>/) with the endpoint
    // c.RoutePrefix = string.Empty;
});

app.Run();
