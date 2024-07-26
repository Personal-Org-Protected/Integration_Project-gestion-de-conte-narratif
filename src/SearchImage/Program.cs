using Application.DependencyInjections;
using FluentValidation.AspNetCore;
using Infrastructure.DependencyInjections;
using LogginLibrary;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Polly;
using SearchImage.Filters;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
//add configuration to get environnment variable
builder.Host.ConfigureHostConfiguration(confhost =>
{
    confhost.AddEnvironmentVariables(prefix: "StoryTell_");
});
ConfigurationManager configuration = builder.Configuration;

builder.Services.AddDistributedMemoryCache();

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
        }
    
    );

    // add JWT Authentication
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "JWT Authentication",
        Description = "Enter JWT Bearer token **_only_**",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer", // must be lower case
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    opt.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {securityScheme, Array.Empty<string>()}
        });
    //for documentation
    var xmlFile=$"{Assembly.GetExecutingAssembly().GetName().Name}.xml";   
    var xmlPath=Path.Combine(AppContext.BaseDirectory,xmlFile);
    opt.IncludeXmlComments(xmlPath);
});

//builder.Services.AddSingleton<IAuthorizationHandler, IsAdminHandlerUsingIdp>();

//builder.Services.AddAuthorizationBuilder()
//    .AddPolicy("IsAdminRequirementPolicy", policyIsAdminRequirement =>
//    {
//        policyIsAdminRequirement.Requirements.Add(new IsAdminRequirement());
//    });

//cors policy
builder.Services.AddCors(option =>
{
    option.AddPolicy(name: configuration["CorsPolicy:Default:name"],
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

builder.Services.AddControllers(options =>
{
    //all filters to catch exceptions
    options.Filters.Add(new ApiExceptionFilterAttribute());
    options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status406NotAcceptable));
    options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));
    options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status401Unauthorized));
    options.ReturnHttpNotAcceptable = true;

    var policy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            // .RequireClaim("email") // disabled this to test with users that have no email (no license added)
            .Build();
    options.Filters.Add(new AuthorizeFilter(policy));
});

builder
    .Services.AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters();


builder.Services.AddMicrosoftIdentityWebApiAuthentication(
        configuration, "AzureB2CUserApi");
builder.Services.AddMicrosoftIdentityWebApiAuthentication(
    configuration, "AzureB2CAdminApi", "BearerAdmin");



//authentification configuration
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//  .AddMicrosoftIdentityWebApi(options =>
//{
//    builder.Configuration.Bind("AzureAdB2C", options);
//    options.Authority = configuration["Azure:Issuer"];
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidIssuer = configuration["Azure:Issuer"],
//        ValidAudiences = new List<string>
//        {
//            configuration["Azure:Audience"],
//            configuration["AzureAdB2C:ClientId"]
//        },
//    };
//}, options => { builder.Configuration.Bind("AzureAdB2C", options); });

//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("default", pol => pol.RequireAuthenticatedUser());
//    options.DefaultPolicy = options.GetPolicy("default");
//    options.AddPolicy("ReadContent", pol =>
//                                            {
//                                                pol.RequireScope(new[]
//                                                {
//                                                    "read:item"
//                                                });
//                                            });
//});

//}).AddJwtBearer(options =>
//{//using jwt bearer configuration
//    options.Authority = configuration["Auth0:Authority"];
//    //options.Audience = configuration["Auth0:Audience:MyApi"];
//    options.TokenValidationParameters = new TokenValidationParameters//modfied
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidAudiences = new List<string>
//        {
//            configuration["Auth0:Audience:MyApi"],
//            configuration["Auth0:Audience:NativeApi"]
//        }
//    };
//});


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opt=>opt.SwaggerEndpoint("/swagger/v2/swagger.json", "Api_StoryTelling v2"));
}

app.UseHttpsRedirection();

app.UseCors(configuration["CorsPolicy:Default:name"]);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
