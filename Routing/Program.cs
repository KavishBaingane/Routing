using Routing.CustomConstraints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRouting(options =>
{
    options.ConstraintMap.Add("months", typeof(MonthsCustomConstraint));
});
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
    endpoints.Map("employee/profile/{Employeename:length(4,7):=kavish}", async
            context =>
    {
        string? employeeName = Convert.ToString(context.Request.RouteValues["employeename"]);
        await context.Response.WriteAsync($"In Employee profile {employeeName}");



    });
    //Eg : product/details/1
    endpoints.Map("products/details/{id:int?}", async
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
    //Eg daily-sigest/{reportDate}
    endpoints.Map("daily-digest-report/{reportdate:datetime}", async
        context =>
    {

        DateTime reportDate = Convert.ToDateTime(context.Request.RouteValues["reportdate"]);
        await context.Response.WriteAsync($"In daily-digest-report --->{reportDate.ToShortDateString()}");

    });

    //Eg :cities/cityId 
    endpoints.Map("cities/{cityid:guid}", async context =>
    {
        Guid cityId = Guid.Parse(Convert.ToString(context.Request.RouteValues["cityid"])!);
        await context.Response.WriteAsync($"City Information - {cityId}");
    });
    //Sales report / 2024/ apr
    endpoints.Map("sales-report/{year:int:min(1900)}/{month:months}", async context =>
    {
        int year = Convert.ToInt32(context.Request.RouteValues["year"]);
        string? month = Convert.ToString(context.Request.RouteValues["month"]);
        if (month == "apr" || month == "jul" || month == "oct" || month == "jan")
        {
            await context.Response.WriteAsync($"sales report - {year}-{month}");
        }
        else if (month == "dec")
        {
            await context.Response.WriteAsync($"Good Man u did this in {month}");

        }

        else if (month == "feb")
        {
            await context.Response.WriteAsync($"It's ok but try other than this {month}");

        }
        else
        {
            await context.Response.WriteAsync($"{month} is not allowed");
        }

    });
});
app.Run(async context =>
{
    await context.Response.WriteAsync($"No route matched at {context.Request.Path}");
});

app.Run();
