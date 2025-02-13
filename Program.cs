using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UTEvents.Context;
using UTEvents.IRepository;
using UTEvents.IService;
using UTEvents.Mapping;
using UTEvents.Repositories;
using UTEvents.Services;
using UTEvents.Filters; 
using UTEvents.Middlewares;
using UTEvents.Providers;
using UTEvents.Extensions;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ErrorFilter>(); // Registering global error handling filter
});

builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>()); //Registering Model Validation Filter
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();

// Configure SQL Server with the connection string
builder.Services.AddDbContext<UTEventsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

// Register Repositories
builder.Services.AddTransient<IEventRepository, EventRepository>();
builder.Services.AddTransient<IEventCategoryRepository, EventCategoryRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<ILocationRepository, LocationRepository>();
builder.Services.AddTransient<IRoleRepository, RoleRepository>();
builder.Services.AddTransient<IGroupRepository, GroupRepository>();
builder.Services.AddTransient<IAllowedEventRoleRepository, AllowedEventRoleRepository>();
builder.Services.AddTransient<IUserEventRepository, UserEventRepository>();

// Register Services
builder.Services.AddTransient<IEventService, EventService>();
builder.Services.AddTransient<IEventCategoryService, EventCategoryService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ILocationService, LocationService>();
builder.Services.AddTransient<IRoleService, RoleService>();
builder.Services.AddTransient<IGroupService, GroupService>();
builder.Services.AddTransient<IAllowedEventRoleService, AllowedEventRoleService>();
builder.Services.AddTransient<IUserEventService, UserEventService>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// Register ClaimsProvider for accessing user claims
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IClaimsProvider, ClaimsProvider>();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<UTEventsContext>();
    await RoleSeeder.SeedRolesAsync(context);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Add Middleware
app.UseMiddleware<LoggerMiddleware>(); // Log every request
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseCors();
app.UseHttpsRedirection();

app.UseAuthentication(); // Ensure authentication is enabled
app.UseAuthorization(); // Ensure authorization is enabled

app.MapControllers();

app.Run();
