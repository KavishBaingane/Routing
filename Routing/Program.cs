var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.Use(async (context, next) =>
{
    Endpoint? endpoint = context.GetEndpoint();
    if(endpoint != null)
    {
        await context.Response.WriteAsync($"EndPoint:{endpoint.DisplayName}\n");
    }
    await next(context);
});

app.UseRouting();

app.Use(async (context, next) =>
{
    Endpoint? endpoint = context.GetEndpoint();
    if (endpoint != null)
    {
        await context.Response.WriteAsync($"EndPoint:{endpoint.DisplayName}\n");
    }
    await next(context);
});

app.UseEndpoints(endpoints =>
{

    //whenever a middleware is executed basically a routing , it is call as endpoint
    endpoints.MapGet("map1", async (context) =>
    {
        await context.Response.WriteAsync("In Map 1");
    });

    endpoints.MapPost("map2", async (context) =>
    {
        await context.Response.WriteAsync("In Map 2");
    });
});
app.Run(async context =>
{
    await context.Response.WriteAsync($"Request received at {context.Request.Path}");
});

app.Run();
