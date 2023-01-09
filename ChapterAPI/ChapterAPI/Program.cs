using ChapterAPI.Contexts;
using ChapterAPI.Interfaces;
using ChapterAPI.Repositories;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ChapterContext, ChapterContext>();// config serviço contexto
builder.Services.AddTransient<ILivroRepository, LivroRepository>(); // config serviço repository
builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddControllers();
//add sevice de cors
builder.Services.AddCors(opitins =>
{
    opitins.AddPolicy("CorsPolicy", builder =>
    {
        builder.WithOrigins("http://localhost:7008")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});
//add serviço de jwt bearer
builder.Services.AddAuthentication(options =>
{
    options.DefaultChallengeScheme = "JwtBearer";
    options.DefaultAuthenticateScheme = "JwtBearer";
})
// define parametros de validação do token
.AddJwtBearer("JwtBearer", options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        //valida quem está solicitando
        ValidateIssuer = true,
        //validad quem está recebendo
        ValidateAudience = true,
        //define se haverá expiração por tempo
        ValidateLifetime = true,
        // forma de criptografica e valida a chave de autentificação
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("chave-autentificacao")),
        // tempo de expiração do token
        ClockSkew = TimeSpan.FromMinutes(30),
        // nome do Issure de onde esta vindo
        ValidIssuer = "Chapter",
        // nome do Issure de onde está indo
        ValidAudience = "Chapter"
    };

});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
