// using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using RDMS;
using RDMS.AplicationContext;
using RDMS.AppAuteticte;
using RDMS.Aputh;
using RDMS.Implementation.Repository;
using RDMS.Implimentation.Repositry;
using RDMS.Implimentation.Service;
using RDMS.Interface.Repositry;
using RDMS.Interface.Service;
using RDMS.Payment;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;//
using Microsoft.IdentityModel.Tokens;//
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;



        // {
        //     Configuration = configuration;
        // }

//  public  Program(IConfiguration configuration)
//         {
//             Configuration = configuration;
//         }

//         public IConfiguration Configuration { get; }


var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ResturantDbContext>(options=>options.UseMySql(
builder.Configuration.GetConnectionString("DefaultConnection"),ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
));
builder.Services.AddScoped<IUserRepositry, UserRepository>();
builder.Services.AddScoped<IUserServices, UserService>();

builder.Services.AddScoped<IRoleRepositry ,RoleRepositry>();
builder.Services.AddScoped<IRoleServices ,RoleService>();

builder.Services.AddScoped<IUserRepositry, UserRepository>();
builder.Services.AddScoped<IStaffRepositry, StaffRepositry>();

builder.Services.AddScoped<ICustomerRepositry, CustomerRepositry>();
builder.Services.AddScoped< ICustomerService ,CustomerServices>();

builder.Services.AddScoped<IEateryRepositry, EateryRepositry>();
builder.Services.AddScoped< IEateryServices ,EateryServices>();

builder.Services.AddScoped<IEateryAdminRepositry, EateryAdminRepositry>();
builder.Services.AddScoped<IAddressRepositry ,AddressRepositry>();

builder.Services.AddScoped<IAddressServices ,AddressServices>();
builder.Services.AddScoped<IFileUploders ,FileUploders>();

builder.Services.AddScoped<IBranchRepositry ,BranchRepositry>();
builder.Services.AddScoped<IBranchService ,BranchSerices>();

builder.Services.AddScoped<IBranchMangerService ,BranchManagerServices>();
builder.Services.AddScoped<IEateryManagerServices ,EateryManagerService >();


builder.Services.AddScoped<IBranchManagerRepositry ,BranchManagerRepositry>();

builder.Services.AddScoped<ICategoryservices , CategoriesService>();
builder.Services.AddScoped<ICategoryRepositry ,CategoryRepositry>();

builder.Services.AddScoped<IPositionService , PostitionServices>();
builder.Services.AddScoped<IPositionReposity , PositionRepositry>();

builder.Services.AddScoped<IPositionService , PostitionServices>();
builder.Services.AddScoped<IFoodServices , FoodServices>();

builder.Services.AddScoped<IFoodRepositry , FoodRepositry>();
builder.Services.AddScoped<IOrderService , OrderServices>();

builder.Services.AddScoped<IOrderRepositry , OrderRepositry>();
builder.Services.AddScoped<IFoodRepositry , FoodRepositry>();

builder.Services.AddScoped<IStaffService , StaffService>();
builder.Services.AddScoped<IStaffRepositry , StaffRepositry>();

builder.Services.AddScoped<ICartItemServices , CartItemServices>();
builder.Services.AddScoped<ICartItemRepositry , CartItemRepositry>();

builder.Services.AddScoped<ICartItemServices , CartItemServices>();
builder.Services.AddScoped<ICartItemRepositry , CartItemRepositry>();

builder.Services.AddScoped<ICommetServies , CommentService>();
builder.Services.AddScoped<ICommentRepositry , CommetRepositry>();

builder.Services.AddScoped<IAppAuthentication , AppAuthentiction>();

builder.Services.AddScoped<IAppAuthentication , AppAuthentiction>();
builder.Services.AddScoped<IMailServices , MailServices>();

builder.Services.AddScoped<IPaystackServices , PaymentServices>();
builder.Services.AddScoped<IPaymentRepositry , PaymentRepositry>();


// // builder.Services.AddScoped<IConfiguration>();

builder.Services.AddHttpContextAccessor();

//     builder.Services.AddSingleton<IJWTAuthenticationManager>(new JWTAuthenticationManager());

// //  <--! --> ///
// builder.Services.AddAuthentication(auth =>
//             {
//                 auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//                 auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//             })
//             .AddJwtBearer(options =>
//             {
//                 options.TokenValidationParameters = new TokenValidationParameters
//                 {
//                     ValidateIssuer = true,
//                     ValidateAudience = true,
//                     ValidateLifetime = true,
//                     ValidateIssuerSigningKey = true,
//                     ValidIssuer = builder.Configuration["Jwt:Issuer"],
//                     ValidAudience = builder.Configuration["Jwt:Issuer"],
//                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
//                 };
//                 options.SaveToken =true;
//             });

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(config =>
        {
            config.LoginPath = "/User/Login";
            config.Cookie.Name = "Restaurantapp";
            config.LogoutPath = "/User/Logout";
            config.ExpireTimeSpan = TimeSpan.FromMinutes(160);
            config.AccessDeniedPath = "/User/Login";
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
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


//seed database 
 //ApDbinSeed.Seed(app);
