
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using StorytellingWebApp.Factory.ConsumeApi;
using StorytellingWebApp.Filters;
using StorytellingWebApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(opt =>
{
    opt.Filters.Add<WebExceptionFilters>();
});

builder.Services.AddHttpClient<IConsume<Chapitre>, Consume<Chapitre>>(conf =>
{
    conf.BaseAddress = new Uri(builder.Configuration["BaseAddress:Chapitre"]);
});
builder.Services.AddHttpClient<IConsume<Image>, Consume<Image>>(conf =>
{
    conf.BaseAddress = new Uri(builder.Configuration["BaseAddress:Image"]);
});
builder.Services.AddHttpClient<IConsume<Story>, Consume<Story>>(conf =>
{
    conf.BaseAddress = new Uri(builder.Configuration["BaseAddress:Story"]);
});
builder.Services.AddHttpClient<IConsume<StoryTell>, Consume<StoryTell>>(conf =>
{
    conf.BaseAddress = new Uri(builder.Configuration["BaseAddress:StoryTelling"]);
});
builder.Services.AddHttpClient<IConsume<TagVM>, Consume<TagVM>>(conf =>
{
    conf.BaseAddress = new Uri(builder.Configuration["BaseAddress:Tag"]);
});
builder.Services.AddHttpClient<IConsume<Tag>, Consume<Tag>>(conf =>
{
    conf.BaseAddress = new Uri(builder.Configuration["BaseAddress:Tag"]);
});
builder.Services.AddHttpClient<IConsume<User>, Consume<User>>(conf =>
{
    conf.BaseAddress = new Uri(builder.Configuration["BaseAddress:User"]);
});
builder.Services.AddHttpClient<IConsume<ImageClientVM>, Consume<ImageClientVM>>(conf =>
{
    conf.BaseAddress = new Uri(builder.Configuration["BaseAddress:Image"]);
});
builder.Services.AddHttpClient<IConsume<UserUpdate>, Consume<UserUpdate>>(conf =>
{
    conf.BaseAddress = new Uri(builder.Configuration["BaseAddress:Image"]);
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddAuthentication(conf =>
{
    conf.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    conf.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    conf.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie()
.AddOpenIdConnect("Auth0", options =>
{
    options.Authority = $"https://{builder.Configuration["Auth0:Domain"]}";
    options.ClientId = builder.Configuration.GetSection("StoryTell_ClientId").Value;
    options.ClientSecret = builder.Configuration.GetSection("StoryTell_ClientSecret").Value;
    options.ResponseType = OpenIdConnectResponseType.Code;

    options.Scope.Clear();
    options.Scope.Add("openid");
    options.Scope.Add("profile");
    options.Scope.Add("read:item");
    options.Scope.Add("write:item");
    options.Scope.Add("update:item");
    options.Scope.Add("delete:item");
    options.Scope.Add("read:users");
    options.Scope.Add("read:user_idp_tokens");
    options.Scope.Add("create:users");
    options.Scope.Add("delete:users");
    options.Scope.Add("update:users");

    options.CallbackPath = new PathString("/Callback");
    options.ClaimsIssuer = "Auth0";
    options.SaveTokens=true;

    options.Events = new OpenIdConnectEvents
    {
        OnRedirectToIdentityProviderForSignOut = (context) =>
        {
            var logoutUri = $"https://{builder.Configuration["Auth0:Domain"]}/v2/logout?client_id={builder.Configuration.GetSection("StoryTell_ClientId").Value}";
            var postLogoutUri = context.Properties.RedirectUri;
            if (!string.IsNullOrEmpty(postLogoutUri))
            {
                if (postLogoutUri.StartsWith("/"))
                {
                    var request = context.Request;
                    postLogoutUri = request.Scheme + "://" + request.Host + request.PathBase + postLogoutUri;

                }
                logoutUri += $"&returnTo={Uri.EscapeDataString(postLogoutUri)}";
            }
            context.Response.Redirect(logoutUri);
            context.HandleResponse();
            return Task.CompletedTask;
        },

        OnRedirectToIdentityProvider = context =>
        {
            context.ProtocolMessage.SetParameter("audience", builder.Configuration["Auth0:Audience"]);
            return Task.FromResult(0);
        },
        OnMessageReceived = context =>
        {
            if (context.ProtocolMessage.Error == "access denied")
            {
                context.HandleResponse();
                context.Response.Redirect("/Account/AccessDenied");
            }
            return Task.FromResult(0);
        }
        
    };

});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseCookiePolicy();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ImageClient}/{action=GetClientImage}/{pgNumber=1}"); 

app.Run();
