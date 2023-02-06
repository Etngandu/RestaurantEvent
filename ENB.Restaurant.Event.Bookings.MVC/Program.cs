using AspNetCoreHero.ToastNotification;
using AutoMapper;
using ENB.Restaurant.Event.Bookings.EF;
using ENB.Restaurant.Event.Bookings.EF.Repositories;
using ENB.Restaurant.Event.Bookings.Entities;
using ENB.Restaurant.Event.Bookings.Entities.Repositories;
using ENB.Restaurant.Event.Bookings.Infrastructure;
using ENB.Restaurant.Event.Bookings.MVC.Factory;
using ENB.Restaurant.Event.Bookings.MVC.Help;
using ENB.Restaurant.Event.Bookings.MVC.Models;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<RestaurantEventBookingContext>(opt => opt.UseSqlServer(
    builder.Configuration.GetConnectionString("RestaurantCstrg")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(
               opt =>
               {
                   opt.Password.RequiredLength = 7;
                   opt.Password.RequireDigit = false;
                   opt.Password.RequireUppercase = false;
               })
                .AddEntityFrameworkStores<RestaurantEventBookingContext>();

builder.Services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, CustomClaimsFactory>();
builder.Services.AddAutoMapper(typeof(RestaurantBookingProfile));
builder.Services.AddScoped<IMapper, Mapper>();
builder.Services.AddScoped<IAsyncCustomerRepository, AsyncCustomerRepository>();
builder.Services.AddScoped<IAsyncStaffRepository, AsyncStaffRepository>();
builder.Services.AddScoped<IAsyncMenuRepository, AsyncMenuRepository>();
builder.Services.AddScoped<IAsyncMealRepository, AsyncMealRepository>();
builder.Services.AddScoped<IAsyncUnitOfWorkFactory, AsyncEFUnitOfWorkFactory>();
builder.Services.AddNotyf(config => { config.DurationInSeconds = 10; config.IsDismissable = true; config.Position = NotyfPosition.TopRight; });
builder.Services.AddScoped<IValidator<CreateAndEditCustomer>, CreateAndEditCustomerValidator>();



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
