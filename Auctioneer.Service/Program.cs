using Auctioneer.Logic.Database;
using Auctioneer.Logic.Hubs;
using Auctioneer.Logic.Proxies;
using Auctioneer.Logic.Services;
using Auctioneer.Service.Middleware;
using Auctioneer.Service.Routes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Configuration.AddEnvironmentVariables();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options => options.AddPolicy("allowAny", o => o.AllowAnyOrigin()));
builder.Services.AddSignalR();
builder.Services.AddDbContext<AuctionContext>(o =>
{
	var useInMemory = builder.Configuration.GetValue<bool>("UseInMemoryDatabase");
	if (useInMemory) { o.UseInMemoryDatabase("Auctioneer"); }
	else { o.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")); }
}, ServiceLifetime.Singleton);
builder.Services.AddSingleton<AuctionService>();
builder.Services.AddSingleton<RedisService>();
builder.Services.AddHostedService<ObservationProxy>();

var app = builder.Build();
if (app.Environment.IsDevelopment()) { app.UseDeveloperExceptionPage(); }
else { app.UseHttpsRedirection(); }

app.UseSwagger();
app.UseSwaggerUI();
app.UseRouting();
app.UseCors(x => x
	.AllowAnyMethod()
	.AllowAnyHeader()
	.SetIsOriginAllowed(origin => true)
	.AllowCredentials());
app.UseMiddleware<HandledResultMiddleware>();
app.useHealthRoutes();
app.useAuctionRoutes();
app.useLookupRoutes();
app.useBiddingRoutes();
app.MapHub<ObservationHub>("/observation-hub");
app.MapHub<ParticipationHub>("/participation-hub");

app.Run();