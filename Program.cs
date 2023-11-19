using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Serilog;
using StudenBloggApi.Extensions;
using StudentBloggAPI.Data;
using StudentBloggAPI.Mappers;
using StudentBloggAPI.Mappers.Interfaces;
using StudentBloggAPI.Middleware;
using StudentBloggAPI.Models.DTOs;
using StudentBloggAPI.Models.Entities;
using StudentBloggAPI.Repository;
using StudentBloggAPI.Repository.Interfaces;
using StudentBloggAPI.Services;
using StudentBloggAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

#region Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.RegisterMappers();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IMapper<User, UserDTO>, UserMapper>();
builder.Services.AddScoped<IMapper<User, UserRegistrationDTO>, UserRegMapper>();
builder.Services.AddScoped<IMapper<Post, PostDTO>, PostMapper>();
builder.Services.AddScoped<IMapper<Comment, CommentDTO>, CommentMapper>();
builder.Services.AddScoped<StudentBloggBasicAuthentication>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddFluentValidationAutoValidation(config => config.DisableDataAnnotationsValidation = false); // Sett til true for å disable annotation i DTO.
builder.Services.AddScoped<IUserRepo, UserRepositoryDbContext>();
builder.Services.AddScoped<IPostRepo, PostRepositoryDbContext>();
builder.Services.AddScoped<ICommentRepo, CommentRepositoryDbContext>();
builder.Services.AddTransient<GlobalExceptionMiddleware>();
builder.Services.AddDbContext<StudentBloggDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), 
        new MySqlServerVersion(new Version(8, 0, 21))));
#endregion

builder.AddSwaggerWithBasicAuthentication();
builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseMiddleware<StudentBloggBasicAuthentication>();
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
