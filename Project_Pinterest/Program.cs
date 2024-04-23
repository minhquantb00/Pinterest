using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.VisualBasic;
using Project_Pinterest.Constants;
using Project_Pinterest.DataContexts;
using Project_Pinterest.Payloads.Converters;
using Project_Pinterest.Payloads.DataResponses.DataCollection;
using Project_Pinterest.Payloads.DataResponses.DataPost;
using Project_Pinterest.Payloads.DataResponses.DataPostCollection;
using Project_Pinterest.Payloads.DataResponses.DataReport;
using Project_Pinterest.Payloads.DataResponses.DataToken;
using Project_Pinterest.Payloads.DataResponses.DataUser;
using Project_Pinterest.Payloads.Responses;
using Project_Pinterest.Services.Implements;
using Project_Pinterest.Services.Interfaces;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
// Add services to the container.

builder.Services.AddControllersWithViews()
                .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseMySql(builder.Configuration.GetConnectionString(AppSettingsKeys.MYSQL_CONNECTION), MySqlServerVersion.AutoDetect(builder.Configuration.GetConnectionString(AppSettingsKeys.MYSQL_CONNECTION))), ServiceLifetime.Transient);
builder.Services.AddCors();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "NS.Core API", Version = "v1" });
    option.CustomSchemaIds(type => type.ToString());
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<ResponseObject<DataResponsePost>>();
builder.Services.AddScoped<ResponseObject<DataResponseUser>>();
builder.Services.AddScoped<ICollectionService, CollectionService>();
builder.Services.AddScoped<IPostCollectionService, PostCollectionService>();
builder.Services.AddScoped<ResponseObject<DataResponseToken>>();
builder.Services.AddScoped<ResponseObject<DataResponseComment>>();
builder.Services.AddScoped<IRelationshipService, RelationshipService>();
builder.Services.AddScoped<ResponseObject<DataResponseLike>>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<UserConverter>();
builder.Services.AddScoped<CollectionConverter>();
builder.Services.AddScoped<ResponseObject<Enums.Action>>();
builder.Services.AddScoped<ResponseObject<DataResponseCollection>>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<ResponseObject<DataResponseReport>>();
builder.Services.AddScoped<ReportConverter>();
builder.Services.AddScoped<ResponseObject<DataResponsePostCollection>>();
builder.Services.AddScoped<PostCollectionConverter>();
builder.Services.AddScoped<PostConverter>();
builder.Services.AddScoped<LikeConverter>();
builder.Services.AddScoped<CommentConverter>();
builder.Services.AddScoped<LikeCommentConverter>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        ValidateIssuer = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            builder.Configuration.GetSection(AppSettingsKeys.AUTH_SECRET).Value!))
    };
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials());
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
