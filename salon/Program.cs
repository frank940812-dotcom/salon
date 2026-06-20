var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// ?? 關鍵修改 1：註冊控制器的服務，微軟後端才會去讀取 BookingController.cs
builder.Services.AddControllers();

builder.Services.AddCors(options => options.AddPolicy("AllowAll",
    p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapRazorPages();

// ?? 關鍵修改 2：啟用控制器路由對應，把 /api/booking 這條路真正開通
app.MapControllers();

app.Run();