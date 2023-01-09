using ChapterAPI.Contexts;
using ChapterAPI.Interfaces;
using ChapterAPI.Repositories;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ChapterContext, ChapterContext>();// config servi�o contexto
builder.Services.AddTransient<ILivroRepository, LivroRepository>(); // config servi�o repository
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
//add servi�o de jwt bearer
builder.Services.AddAuthentication(options =>
{
    options.DefaultChallengeScheme = "JwtBearer";
    options.DefaultAuthenticateScheme = "JwtBearer";
})
// define parametros de valida��o do token
.AddJwtBearer("JwtBearer", options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        //valida quem est� solicitando
        ValidateIssuer = true,
        //validad quem est� recebendo
        ValidateAudience = true,
        //define se haver� expira��o por tempo
        ValidateLifetime = true,
        // forma de criptografica e valida a chave de autentifica��o
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("chave-autentificacao")),
        // tempo de expira��o do token
        ClockSkew = TimeSpan.FromMinutes(30),
        // nome do Issure de onde esta vindo
        ValidIssuer = "Chapter",
        // nome do Issure de onde est� indo
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
