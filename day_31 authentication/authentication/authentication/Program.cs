using System.Reflection;
using System.Text;
using authentication.Data;
using authentication.Helpers;
using authentication.Middlewares;
using authentication.Models;
using authentication.Services.Implementations;
using authentication.Services.Interfaces;
using authentication.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Map JWT
builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));

// Validate and configure MailSetting
var mailSetting = builder.Configuration.GetSection("MailSetting").Get<MailSetting>();
if (mailSetting == null || string.IsNullOrEmpty(mailSetting.SmtpServer) || string.IsNullOrEmpty(mailSetting.UserName) || string.IsNullOrEmpty(mailSetting.Password))
{
    throw new InvalidOperationException("MailSetting configuration is missing or invalid. Please check the 'MailSetting' section in appsettings.json.");
}
builder.Services.Configure<MailSetting>(builder.Configuration.GetSection("MailSetting"));

// Validate and configure ServerSetting
var serverSetting = builder.Configuration.GetSection("ServerSetting").Get<ServerSetting>();
if (serverSetting == null || string.IsNullOrEmpty(serverSetting.FrontendBaseUrlForConfirmEmail) || string.IsNullOrEmpty(serverSetting.FrontendBaseUrlForResetPassword))
{
    throw new InvalidOperationException("ServerSetting configuration is missing or invalid. Please check the 'ServerSetting' section in appsettings.json.");
}
builder.Services.Configure<ServerSetting>(builder.Configuration.GetSection("ServerSetting"));

// Configure DataProtectionTokenProviderSetting
var tokenSetting = builder.Configuration.GetSection("DataProtectionTokenProviderSetting").Get<DataProtectionTokenProviderSetting>();
if (tokenSetting == null || tokenSetting.ExpiresIn <= 0)
{
    throw new InvalidOperationException("DataProtectionTokenProviderSetting configuration is missing or invalid. Please check the 'DataProtectionTokenProviderSetting' section in appsettings.json.");
}
builder.Services.Configure<DataProtectionTokenProviderSetting>(builder.Configuration.GetSection("DataProtectionTokenProviderSetting"));

// Add Identity and Token Providers
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // Configure Identity options
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders(); // Register DataProtectorTokenProvider as 'Default'

// Configure DataProtectionTokenProvider options
builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    options.Name = "Default";
    options.TokenLifespan = TimeSpan.FromMinutes(builder.Configuration.GetValue<int>("DataProtectionTokenProviderSetting:ExpiresIn"));
});

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = false;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
    };
});

// Add other services
builder.Services.AddAutoMapper(_ => { }, Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IFileUpload, FileUpload>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddLogging(logging =>
{
    logging.AddConsole();
    logging.AddDebug();
});

// Add Controllers and Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CRUD_Operations", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseStaticFiles();
app.UseMiddleware<ExceptionHandler>();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var loggerFactory = services.GetRequiredService<ILoggerFactory>();
var logger = loggerFactory.CreateLogger("app");

try
{
    var context = services.GetRequiredService<ApplicationDbContext>();
    await context.Database.MigrateAsync();
    logger.LogInformation("Seeding data");
    logger.LogInformation("Application starts");
}
catch (Exception ex)
{
    logger.LogError(ex, "An error occurred during migration");
}

app.Run();