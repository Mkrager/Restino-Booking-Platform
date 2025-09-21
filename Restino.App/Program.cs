using Restino.App.Contracts;
using Restino.App.Infrastructure.HttpHandlers;
using Restino.App.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorPages();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IAccommodationDataService, AccommodationDataService>();
builder.Services.AddScoped<ICategoryDataService, CategoryDataService>();
builder.Services.AddScoped<IReservationDataService, ReservationDataService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IUserDataService, UserDataService>();
builder.Services.AddScoped<IPasswordDataService, PasswordDataService>();
builder.Services.AddScoped<ITwoFactorDataService, TwoFactorDataService>();
builder.Services.AddScoped<ITokenProvider, TokenProvider>();

builder.Services.AddSingleton<JwtHelper>();

builder.Services.AddTransient<AuthHeaderHandler>();

var baseUrl = builder.Configuration["App:BaseUrl"];
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri(baseUrl);
})
.AddHttpMessageHandler<AuthHeaderHandler>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

await app.RunAsync();
