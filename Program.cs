var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddSession(); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession(); 

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

// Add in Program.cs 
app.Use(async (context, next) =>
{
    if (context.Request.Method == "POST" && context.Request.Query.ContainsKey("handler") && context.Request.Query["handler"] == "ChangeRole")
    {
        var role = context.Request.Form["selectedRole"];
        context.Session.SetString("DemoRole", role);
        context.Response.Redirect(context.Request.Path);
        return;
    }
    await next();
});

app.Run();
