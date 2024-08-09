var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.Use(async (context, next) =>
{
    Endpoint? endpoint = context.GetEndpoint();
    if (endpoint != null)
    {
        await context.Response.WriteAsync($"EndPoint:{endpoint.DisplayName}\n");
    }
    await next(context);
});

//enable routing
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.Map("files/{filename}.{extension}", async
        context =>
    {
        string? filename = Convert.ToString(context.Request.RouteValues["filename"]);
        string? extension = Convert.ToString(context.Request.RouteValues["extension"]);

        await context.Response.WriteAsync($"In files {filename}-{extension}");
    });
    endpoints.Map("employee/profile/{Employeename}", async
            context =>
    {
            string? employeeName = Convert.ToString(context.Request.RouteValues["employeename"]);
            await context.Response.WriteAsync($"In Employee profile {employeeName}");

        
        
    });
    //Eg : product/details/1
    endpoints.Map("products/details/{id?}", async
        context =>
    {
        if (context.Request.RouteValues.ContainsKey("id"))
        {
            int Id = Convert.ToInt32(context.Request.RouteValues["id"]);
            await context.Response.WriteAsync($"The product eith the Id is --->{Id}");

        }
        else
        {
            await context.Response.WriteAsync($"Product id is not supplied");

        }
    });
});
app.Run(async context =>
{
    await context.Response.WriteAsync($"Request received at {context.Request.Path}");
});

app.Run();
