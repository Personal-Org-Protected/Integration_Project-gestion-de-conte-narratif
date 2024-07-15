using Application.DependencyInjections;
using FluentValidation.AspNetCore;
using Infrastructure.DependencyInjections;
using LogginLibrary;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Polly;
using SearchImage.Filters;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
//add configuration to get environnment variable
builder.Host.ConfigureHostConfiguration(confhost =>
{
    confhost.AddEnvironmentVariables(prefix: "StoryTell_");
});
ConfigurationManager configuration = builder.Configuration;
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//versionning
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1,0);
});


builder.Services.AddEndpointsApiExplorer();
//swagger configuration
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1",new OpenApiInfo { 
        
        Title ="Api StoryTelling", 
        Version = "v1",
        Description="The first version of this api",
        TermsOfService=new Uri("https://nwb.one/blog/swagger-api-versioning-dotnet-core"),
        Contact=new OpenApiContact { 
        Name="Mouhsine Ayi",
        Email= "Psr11140@students.ephec.be",
        Url=new Uri("https://twitter.com/elonmusk?ref_src=twsrc%5Egoogle%7Ctwcamp%5Eserp%7Ctwgr%5Eauthor")
        },
        License=new OpenApiLicense { 
        
        Name="Use under App Tech",
        Url=new Uri("https://waytolearnx.com/")
        
        }
    });
    opt.SwaggerDoc("v2", new OpenApiInfo
    {

        Title = "The Feather",
        Version = "v2",
        Description = "The second version of this api",
        TermsOfService = new Uri("https://nwb.one/blog/swagger-api-versioning-dotnet-core"),
        Contact = new OpenApiContact
        {
            Name = "Mouhsine Ayi",
            Email = "Psr11140@students.ephec.be",
            Url = new Uri("https://twitter.com/elonmusk?ref_src=twsrc%5Egoogle%7Ctwcamp%5Eserp%7Ctwgr%5Eauthor")
        },
        License = new OpenApiLicense
        {

            Name = "Use under App Tech",
            Url = new Uri("https://waytolearnx.com/")

        }
    });
    //for documentation
    var xmlFile=$"{Assembly.GetExecutingAssembly().GetName().Name}.xml";   
    var xmlPath=Path.Combine(AppContext.BaseDirectory,xmlFile);
    opt.IncludeXmlComments(xmlPath);
});

//cors policy
builder.Services.AddCors(option =>
{
    option.AddPolicy(name: configuration["CorsPolicy:Angular:name"],
                      policy =>
                      {
                          policy
                          .AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      
                      });
});



//add dependencies injections from other projects
builder.Services.AddApplication(configuration);
builder.Services.AddInfrastructure(configuration);
builder.Services.AddLogginService();

builder.Services.AddMvc(options =>
{
    //all filters to catch exceptions
    options.Filters.Add(new ApiExceptionFilterAttribute());
    options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status406NotAcceptable));
    options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));
    options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status401Unauthorized));
    options.ReturnHttpNotAcceptable = true;
}).AddFluentValidation();

//authentification configuration
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{//using jwt bearer configuration
    options.Authority = configuration["Auth0:Authority"];
    //options.Audience = configuration["Auth0:Audience:MyApi"];
    options.TokenValidationParameters = new TokenValidationParameters//modfied
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudiences = new List<string>
        {
            configuration["Auth0:Audience:MyApi"],
            configuration["Auth0:Audience:NativeApi"]
        }
    };
});


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opt=>opt.SwaggerEndpoint("/swagger/v2/swagger.json", "Api_StoryTelling v2"));
}

app.UseHttpsRedirection();

app.UseCors(configuration["CorsPolicy:Angular:name"]);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
