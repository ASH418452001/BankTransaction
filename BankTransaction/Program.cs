using BankTransaction.Model;
using BankTransaction.Profiles;
using BankTransaction.shared;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(connectionString));
builder.Services.AddControllers();
//abod add
// builder.Services.AddErrorHandling();
//abod add
// builder.Services.AddSignalR(cfg=> cfg.EnableDetailedErrors = true);
builder.Services.AddJwt(builder.Configuration);
builder.Services.AddAuthentication();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingbankTransactionProfile));
builder.Services.AddAutoMapper(typeof(MappingUserProfile));
//abod add
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .WithOrigins("http://localhost:3000");
    });
});


var app = builder.Build();
app.UseCors("CorsPolicy");


app.UseRouting();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DataContext>();
    db.Database.Migrate();
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// app.UseEndpoints(endpoints =>
// {
//     endpoints.MapGet("/", async context =>
//     {
//         await context.Response.WriteAsync("BankTransaction");
//     });
//     endpoints.MapHub<MessageHub>("/messagehub");
// });

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
