using Gym_Management;
using Gym_Management.DB_Context;
using Gym_Management.IRespository;
using Gym_Management.Mapper;
using Gym_Management.Models;
using Gym_Management.Respository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Globalization;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
    .WriteTo.File("D:/Projects/Asp.net/Gym Management/Gym Management/Logs.txt")
    .MinimumLevel.Information()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
builder.Services.AddControllersWithViews();

var cultureInfo = new CultureInfo("en-IN");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

builder.Services.AddDbContext<GymContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection")));
builder.Services.AddDbContext<GymAuthContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("AuthorizationConnection")));
builder.Services.AddScoped<IGymRespository<Attendance>, AttendanceRespository>();
builder.Services.AddScoped<IGymRespository<Customer>,CustomerRespository>();
builder.Services.AddScoped<IGymRespository<Payment>, PaymentRespository>();
builder.Services.AddScoped<IGymRespository<Receptionist>, ReceptionistRespository>();
builder.Services.AddScoped<ITokenRespository, TokenRespository>();
builder.Services.AddScoped<IGymRespository<Trainer>, TrainerRespository>();

builder.Services.AddAutoMapper(typeof(GymAutoMapperProfile));

builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("Gym Management")
    .AddEntityFrameworkStores<GymAuthContext>()
    .AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))

    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("~/NotFound.cshtml");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Pictures")),
    RequestPath = "/Pictures"
});

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Trainers}/{action=Home}/{id?}");

app.Run();
