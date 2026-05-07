using API_pro3;
using API_pro3.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;


builder.Services.AddService(config);//extention yaratdib ordan cagiririq

var app = builder.Build();
if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Configure the HTTP request pipeline.
app.UseStaticFiles();
app.UseAuthorization();

app.MapControllers();

app.Run();
