using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ENB.Restaurant.Event.Bookings.Entities;
using ENB.Restaurant.Event.Bookings.Entities.Repositories;
using ENB.Restaurant.Event.Bookings.Infrastructure;
using ENB.Restaurant.Event.Bookings.MVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;

namespace ENB.Restaurant.Event.Bookings.Controllers
{
    [Authorize]
    public class BookingEventController : Controller
    {
        // GET: Lawyer

        private readonly IMapper _mapper;
        private readonly ILogger<BookingEventController> _logger;
        private readonly IAsyncCustomerRepository _asyncCustomerRepository;
        private readonly IAsyncStaffRepository _asyncStaffRepository;
        private readonly IAsyncUnitOfWorkFactory _asyncUnitOfWorkFactory;
        private readonly INotyfService _notyf;
        public BookingEventController(IMapper mapper, ILogger<BookingEventController> logger,
                                   IAsyncCustomerRepository asyncCustomerRepository,
                                   IAsyncStaffRepository asyncStaffRepository,
                                   IAsyncUnitOfWorkFactory asyncUnitOfWorkFactory,
                                   INotyfService notyf)
        {
            _mapper = mapper;
            _logger = logger;
            _asyncCustomerRepository = asyncCustomerRepository;
            _asyncStaffRepository = asyncStaffRepository;
            _asyncUnitOfWorkFactory = asyncUnitOfWorkFactory;
            _notyf = notyf;
        }
        public IActionResult Index(DateTime? eventDate)
        {
            ViewBag.EventDate = eventDate ?? DateTime.Now;
            return View();
        }

        public async Task<IActionResult> List(int CustomerId)
        {
            ViewBag.Idcustm = CustomerId;
            var customer = await _asyncCustomerRepository.FindById(CustomerId);

            ViewBag.Message = customer.FullName;

            return View();
        }


        public JsonResult GetListEvents(int CustomerId)
        {

            var lwyrevents = (from bkev in _asyncCustomerRepository.FindAll().Where(x=>x.Id==CustomerId).SelectMany(cst => cst.ListBooking)
                              join cst in _asyncCustomerRepository.FindAll() on bkev.CustomerId equals cst.Id
                              select new DisplayBookingEvent
                              {
                                  Id = bkev.Id,
                                  CustomerId = bkev.CustomerId,
                                  BookingNumber = bkev.BookingNumber,
                                  Start = bkev.Start,
                                  EventStatus= bkev.EventStatus,
                                  Payment_Method=bkev.Payment_Method, 
                                  End = bkev.End,
                                  Color = bkev.Color,
                                  AllDay = bkev.AllDay
                              }).ToArray();

            return Json(new {data= lwyrevents });


        }


        [HttpGet]
        public IActionResult CreateCal(string eventDate)
        {
            ViewBag.EventDate = eventDate;
            var data = new CreateAndEditBookingEvent()
            {
                ListCustomers = _asyncCustomerRepository.FindAll()
                       .Select(d => new SelectListItem
                       {
                           Text = d.FullName,
                           Value = d.Id.ToString(),
                           Selected = true

                       }).Distinct().ToList(),

                ListStaff = _asyncStaffRepository.FindAll()
                       .Select(d => new SelectListItem
                       {
                           Text = d.FullName,
                           Value = d.Id.ToString(),
                           Selected = true

                       }).Distinct().ToList()


            };

           

            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> CreateEvent(int CustomerId)
        {
            ViewBag.Idcustm = CustomerId;

            var customer = await _asyncCustomerRepository.FindById(CustomerId);
            var data = new CreateAndEditBookingEvent()
            {
                ListStaff = _asyncStaffRepository.FindAll()
                      .Select(d => new SelectListItem
                      {
                          Text = d.FullName,
                          Value = d.Id.ToString(),
                          Selected = true

                      }).Distinct().ToList()


            };

            ViewBag.Message = customer.FullName;

            return View(data);
        }

        // POST: LawyerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAndEditBookingEvent createAndEditBookingEvent)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                try
                {
                await    using (await _asyncUnitOfWorkFactory.Create())
                    {
                        var customer = await _asyncCustomerRepository.FindById(createAndEditBookingEvent.CustomerId);
                        Booking  custbooking = new();

                        _mapper.Map(createAndEditBookingEvent, custbooking);
                       
                        customer.ListBooking.Add(custbooking);

                        _notyf.Success("Booking event Added  Successfully! ");

                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (ModelValidationException mvex)
                {
                    foreach (var error in mvex.ValidationErrors)
                    {
                        ModelState.AddModelError(error.MemberNames.FirstOrDefault() ?? "", error.ErrorMessage!);
                    }
                }
            }
            return View();
        }

        public JsonResult GetEvents()
        {

            var lwyrevents = (from bkev in _asyncCustomerRepository.FindAll().SelectMany(cst => cst.ListBooking)
                              //join cst in _asyncCustomerRepository.FindAll() on bkev.CustomerId equals cst.Id
                              select new DisplayBookingEvent
                              {
                                  Id = bkev.Id,                                 
                                  CustomerId=bkev.CustomerId,
                                  BookingNumber=bkev.BookingNumber,
                                  Title="Booking  " + bkev.Customer!.FullName,
                                  Description=bkev.BookingNumber,
                                  Start = bkev.Start,
                                  End = bkev.End,                                 
                                  Color =bkev.Color,
                                  AllDay = bkev.AllDay
                              }).ToArray();

            return Json(lwyrevents);


        }

        public async Task<IActionResult> EditEvent(int customerId, int id)
        {

            var customer = await _asyncCustomerRepository.FindById(customerId);
            ViewBag.Message = customer.FullName;
            ViewBag.Idcustm = customerId;
            ViewBag.Id = id;

            var customerevt = await _asyncCustomerRepository.FindById(customerId, ev => ev.ListBooking);

            if (customerevt == null)
            {
                return NotFound();
            }

            var data = new CreateAndEditBookingEvent()
            {
                ListStaff = _asyncStaffRepository.FindAll()
                       .Select(d => new SelectListItem
                       {
                           Text = d.FullName,
                           Value = d.Id.ToString(),
                           Selected = true

                       }).Distinct().ToList()


            };

            _mapper.Map(customerevt.ListBooking.Single(x => x.Id == id), data);

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateAndEditBookingEvent createAndEditBookingEvent, int customerId)
        {

            ViewBag.CustomerId = customerId;
            if (ModelState.IsValid)
            {
                try
                {
                 await   using (await _asyncUnitOfWorkFactory.Create())
                    {

                        var customerevet = await _asyncCustomerRepository.FindById(customerId, x => x.ListBooking);
                        var bookingevent = customerevet.ListBooking.Single(x => x.Id == createAndEditBookingEvent.Id);

                        _mapper.Map(createAndEditBookingEvent, bookingevent);

                        _notyf.Success($"Event related to Customer{customerevet.FullName} updated Successfully");

                        return RedirectToAction(nameof(List),new {customerId});
                    }
                }
                catch (ModelValidationException mvex)
                {
                    foreach (var error in mvex.ValidationErrors)
                    {
                        ModelState.AddModelError(error.MemberNames.FirstOrDefault() ?? "", error.ErrorMessage!);
                    }
                }
            }
            return View();
        }

        public async Task<IActionResult> Details(int customerId, int id)
        {

            var customer = await _asyncCustomerRepository.FindById(customerId);
            ViewBag.Message = customer.FullName;
            ViewBag.Idcustm = customerId;
            ViewBag.Id = id;

            var customerevent = await _asyncCustomerRepository.FindById(customerId, ev => ev.ListBooking);

            if (customerevent == null)
            {
                return NotFound();
            }
            var myevent = new DisplayBookingEvent();

            var sglevent = _mapper.Map(customerevent.ListBooking.Single(x => x.Id == id),myevent);
            
           // sglevent.Color = Enum.GetName(typeof(EventStatus), Int32.Parse(sglevent.Color!));
            return View(sglevent);
        }

        public async Task<IActionResult> Delete(int customerId, int id)
        {

            var customer = await _asyncCustomerRepository.FindById(customerId);
            ViewBag.Message = customer.FullName;
            ViewBag.Idcustm = customerId;
            ViewBag.Id = id;

            var customerevent = await _asyncCustomerRepository.FindById(customerId, ev => ev.ListBooking);

            if (customerevent == null)
            {
                return NotFound();
            }
            var myevent = new DisplayBookingEvent();

            var sglevent = _mapper.Map(customerevent.ListBooking.Single(x => x.Id == id), myevent);

            // sglevent.Color = Enum.GetName(typeof(EventStatus), Int32.Parse(sglevent.Color!));
            return View(sglevent);
        }

        // POST: BookingEventController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DisplayBookingEvent displayBookingEvent, int customerId)
        {
          
          await  using (await _asyncUnitOfWorkFactory.Create())
            {
                var customer = await _asyncCustomerRepository.FindById(customerId, x => x.ListBooking);
                var customerevent = customer.ListBooking.Single(x => x.Id == displayBookingEvent.Id);

                    customer.ListBooking.Remove(customerevent);

                _notyf.Error($"Event related to Customer {customer.FullName} removed  Successfully");
            }
            return RedirectToAction(nameof(Index));
        }


    }
}
