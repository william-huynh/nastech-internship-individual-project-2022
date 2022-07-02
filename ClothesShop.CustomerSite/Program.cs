using ClothesShop.API.Authorization;
using ClothesShop.API.Data;
using ClothesShop.API.Helpers;
using ClothesShop.API.Services;
using ClothesShop.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var env = builder.Environment;
var configuration = builder.Configuration;

// Add services to the container.
{
    // Add Razor pages runtime compilation
    services.AddRazorPages().AddRazorRuntimeCompilation();

    // Add services to the container.
    services.AddControllersWithViews();

    services.AddDbContext<ClothesDbContext>(options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    services.AddControllers().AddJsonOptions(x =>
    {
        // Serialize enums as strings in api responses (e.g. Role)
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

    // Configure strongly typed settings object
    services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

    services.AddMvc().AddSessionStateTempDataProvider();
    services.AddSession();

    // Configure DI for application services
    services.AddScoped<IJwtUtils, JwtUtils>();
    services.AddScoped<IUserService, UserService>();

    /* services.AddTransient<IUserRepository, UserRepository>();
    services.AddTransient<ITokenService, TokenService>();
    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["Jwt:Issuer"],
            ValidAudience = configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
        };
    }); */
}

var app = builder.Build();

// Configure HTTP request pipeline
{

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseSession();

    app.Use(async (context, next) =>
    {
        var token = context.Session.GetString("Token");
        if (!string.IsNullOrEmpty(token))
        {
            context.Request.Headers.Add("Authorization", "Bearer " + token);
        }
        await next();
    });

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    // Global error handler
    app.UseMiddleware<ErrorHandlerMiddleware>();

    // Custom jwt auth middleware
    app.UseMiddleware<JwtMiddleware>();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}");
}

app.Run();
