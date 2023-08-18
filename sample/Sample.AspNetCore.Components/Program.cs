using Sample.AspNetCore.Components;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBizer(options=>options.AssembyNames.Add("Sample.*"))
    .AddComponents()
    .AddMenuManager<DemoMenuManager>()
    ;

var app = builder.Build();


app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
