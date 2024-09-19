
using System.Security.Claims;
using System.Text;
using BookStore.Helper;
using BookStore.Models;
using BookStore.Repositories;
using BookStore.Repositories.Interfaces;
using BookStore.Services;
using BookStore.Services.Interfaces;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;

var builder = WebApplication.CreateBuilder(args);




// Add services to the container.
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddControllers();

// registration firebase

FirebaseApp.Create(new AppOptions(){
    Credential = GoogleCredential.FromFile("bookstore-59884-firebase-adminsdk-p59pi-12966435ab.json")
    
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "My API", Version = "v1" });

    // Define the OAuth2.0 scheme that's in use (JWT Bearer)
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = Microsoft.OpenApi.Models.ParameterLocation.Header,

            },
            new List<string>()
        }
    });
});

// database connection string sql server
builder.Services.AddDbContext<BookStoreContext>( option => 
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")),
    ServiceLifetime.Scoped);
builder.Services.AddSingleton<FirebaseStorageService>();
builder.Services.AddSingleton<JwtService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBookRepository,BookRepository>();
builder.Services.AddScoped<IBookService,BookService>();


// sign up service Authentication and jwt 
builder.Services.AddAuthentication(option => {
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme  =  JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(option => {
    var jwtToken = builder.Configuration.GetSection("JwtSettings");
    option.TokenValidationParameters = new TokenValidationParameters{
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateAudience  = true,
        ValidateIssuerSigningKey  = true,
        ValidIssuer = jwtToken["Issuer"],
        ValidAudience = jwtToken["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtToken["Key"])),
        RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"

    };
    option.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine("Authentication failed: " + context.Exception.Message);
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            Console.WriteLine("Token validated");
            return Task.CompletedTask;
        }
    };
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

