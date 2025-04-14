using BugTrackerPro.BL;
using BugTrackerPro.DAL;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDataAccessServices(builder.Configuration);
builder.Services.AddBusinessLogicServices();

builder.Services.AddIdentity<User, Role>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredUniqueChars = 2;
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<BugContext>()
.AddDefaultTokenProviders();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:IssuerIP"],

        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:AudienceIP"],

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecritKey"])),

        ValidateLifetime = true
    };
});

builder.Services.AddAuthorization(options =>
{
   
    options.AddPolicy("Admin", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
    options.AddPolicy("User", policy => policy.RequireClaim(ClaimTypes.Role, "User"));
    options.AddPolicy("Manager", policy => policy.RequireClaim(ClaimTypes.Role, "Manager"));
    options.AddPolicy("AdminOrManager", policy => policy.RequireClaim(ClaimTypes.Role, "Admin", "Manager"));
    options.AddPolicy("AdminOrUser", policy => policy.RequireClaim(ClaimTypes.Role, "Admin", "User"));
    options.AddPolicy("UserOrManager", policy => policy.RequireClaim(ClaimTypes.Role, "User", "Manager"));
    options.AddPolicy("AdminOrUserOrManager", policy => policy.RequireClaim(ClaimTypes.Role, "Admin", "User", "Manager"));
    options.AddPolicy("UserOrManagerOrAdmin", policy => policy.RequireClaim(ClaimTypes.Role, "User", "Manager", "Admin"));
    options.AddPolicy("UserOrManagerOrAdminOrGuest", policy => policy.RequireClaim(ClaimTypes.Role, "User", "Manager", "Admin", "Guest"));
    options.AddPolicy("UserOrManagerOrAdminOrGuestOrSupport", policy => policy.RequireClaim(ClaimTypes.Role, "User", "Manager", "Admin", "Guest", "Support"));
    options.AddPolicy("UserOrManagerOrAdminOrGuestOrSupportOrDeveloper", policy => policy.RequireClaim(ClaimTypes.Role, "User", "Manager", "Admin", "Guest", "Support", "Developer"));
    options.AddPolicy("UserOrManagerOrAdminOrGuestOrSupportOrDeveloperOrTester", policy => policy.RequireClaim(ClaimTypes.Role, "User", "Manager", "Admin", "Guest", "Support", "Developer", "Tester"));
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
