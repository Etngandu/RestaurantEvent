using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ENB.Restaurant.Event.Bookings.Entities;
using ENB.Restaurant.Event.Bookings.MVC.Models;


namespace ENB.Restaurant.Event.Bookings.MVC.Help
{
    public class RestaurantBookingProfile : Profile
    {
        public RestaurantBookingProfile()
        {
            

            #region Customer 
            CreateMap<Customer, DisplayCustomer>();

            CreateMap<CreateAndEditCustomer, Customer>()
              .ForMember(d => d.DateCreated, t => t.Ignore())
              .ForMember(d => d.DateModified, t => t.Ignore())
              .ForMember(d=>d.AddressCustomer,t=>t.Ignore());
            CreateMap<Customer, CreateAndEditCustomer>();
            #endregion


            #region Staff
            CreateMap<Staff, DisplayStaff>();

            CreateMap<CreateAndEditStaff, Staff>()
              .ForMember(d => d.DateCreated, t => t.Ignore())
              .ForMember(d => d.DateModified, t => t.Ignore());
            CreateMap<Staff, CreateAndEditStaff>();

            #endregion

            #region Identity
            CreateMap<UserRegistrationModel, ApplicationUser>()
            .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
            #endregion


            #region Menu 
            CreateMap<Menu, DisplayMenu>();

            CreateMap<CreateAndEditMenu, Menu>()
              .ForMember(d => d.DateCreated, t => t.Ignore())
              .ForMember(d => d.DateModified, t => t.Ignore());
            CreateMap<Menu, CreateAndEditMenu>();
            #endregion

            #region Meal 
            CreateMap<Meal, DisplayMeal>();

            CreateMap<CreateAndEditMeal, Meal>()
              .ForMember(d => d.DateCreated, t => t.Ignore())
              .ForMember(d => d.DateModified, t => t.Ignore());
            CreateMap<Meal, CreateAndEditMeal>();
            #endregion

            #region MealMenu
            CreateMap<Menu_Meal, DisplayMenuMeal>()
             .ForMember(d => d.MenuId, t => t.MapFrom(y => y.MenuId))
             .ForMember(d => d.MealId, t => t.MapFrom(y => y.MealId));

            CreateMap<CreateAndEditMenuMeal, Menu_Meal>()
              .ForMember(d => d.MenuId, t => t.MapFrom(y => y.MenuId))
              .ForMember(d => d.MealId, t => t.MapFrom(y => y.MealId))
              .ForMember(d => d.Menu, t => t.Ignore())
              .ForMember(d => d.Meal, t => t.Ignore())
              .ReverseMap();

            #endregion

            #region Booking
            CreateMap<Booking, DisplayBookingEvent>()
             .ForMember(d => d.CustomerId, t => t.MapFrom(y => y.Id))
             .ForMember(d => d.Staff, t => t.Ignore())
             .ForMember(d => d.Customer, t => t.Ignore());

            CreateMap<CreateAndEditBookingEvent, Booking>()
              .ForMember(d => d.CustomerId, t => t.MapFrom(y => y.CustomerId))
              .ForMember(d => d.StaffId, t => t.MapFrom(y => y.StaffId))
              .ForMember(d => d.Color, t => t.MapFrom(y => y.EventStatus.ToString()))
              .ForMember(d => d.Customer, t => t.Ignore())
              .ForMember(d => d.Staff, t => t.Ignore())
              .ForMember(d => d.DateCreated, t => t.Ignore())
              .ForMember(d => d.DateModified, t => t.Ignore())
              .ReverseMap();
            #endregion

            #region BookingNotes
            CreateMap<Booking_Note, DisplayBookingNote>()
             .ForMember(d => d.Customer, t => t.Ignore())
             .ForMember(d => d.Booking, t => t.Ignore())            
             .ForMember(d => d.CustomerId, t => t.MapFrom(y => y.CustomerId))
             .ForMember(d => d.BookingId, t => t.MapFrom(y => y.BookingId));

            CreateMap<CreateAndEditBookingNote, Booking_Note>()
              .ForMember(d => d.BookingId, t => t.MapFrom(y => y.BookingId))            
              .ForMember(d => d.CustomerId, t => t.MapFrom(y => y.CustomerId))
              .ForMember(d => d.Customer, t => t.Ignore())
              .ForMember(d => d.Booking, t => t.Ignore())              
              .ForMember(d => d.DateCreated, t => t.Ignore())
              .ForMember(d => d.DateModified, t => t.Ignore())
              .ReverseMap();

            #endregion

            #region Address

            CreateMap<Address, EditAddress>()
                  .ForMember(d => d.CustomerId, t => t.Ignore())
                  .ForMember(d => d.StaffId, t => t.Ignore());                  
            CreateMap<EditAddress, Address>().ConstructUsing(s => new Address(s.Number_street!, s.City!, s.Zipcode!, s.State_province_county!, s.Country!));
            #endregion
        }
    }
}
