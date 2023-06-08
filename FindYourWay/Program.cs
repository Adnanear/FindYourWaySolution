global using FindYourWay.Models;

using Microsoft.EntityFrameworkCore;
using FindYourWay.Data;
using FindYourWay.Services.User;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using FindYourWay.Middlewares;

// Let's create a web app builders
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Add Swagger Auto Docs generator from OpenApi
builder.Services.AddSwaggerGen(options =>
{
    // We are tell the auto generator to infer schemes from classes
    options.InferSecuritySchemes();

    // And then enable authorization with bearer token
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            new string[] {}
        }
    });
});
builder.Services.Configure<SwaggerGeneratorOptions>(options =>
{
    options.InferSecuritySchemes = true;
});


// We have disabled the JwtBearer authentication and the typical authorization
// But we have created our own middleware `UseAuthenticationMiddleware` to add authentication with custom tokens

//builder.Services.AddAuthorization();
//builder.Services.AddAuthentication("Bearer").AddJwtBearer();

// Adding UserService
builder.Services.AddScoped<IUserService, UserService>();

// Adding Database context to services
builder.Services.AddDbContext<AppDbContext>(options =>
{
    // We use the connection string from builder configuration json file
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Enabled cors
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    // Bypassing cors just because it is just a studying purpose api
    // otherwise we would have only allowed the origins we trust
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));

// Now, let's create an instance based on the builder we configured above
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Enable swagger only on development environments
  app.UseSwagger();
  app.UseSwaggerUI();
}

// Allow https redirections
// It should redirect calls comming from `http` scheme to `https` one
app.UseHttpsRedirection();

// Use the cors middlware
app.UseCors("corsapp");

//app.UseAuthorization();
app.UseAuthenticationMiddleware();

// Auto mappers for routes/controllers
app.MapControllers();

// Run the application
app.Run();
