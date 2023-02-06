using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using ENB.Restaurant.Event.Bookings.Entities;
using ENB.Restaurant.Event.Bookings.Entities.Collections;
using ENB.Restaurant.Event.Bookings.Entities.Repositories;
using ENB.Restaurant.Event.Bookings.Infrastructure;
using ENB.Restaurant.Event.Bookings.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ENB.Restaurant.Event.Bookings.MVC.Controllers
{
    [Authorize]
    public class MenuBookedController : Controller
    {       

        private readonly IAsyncCustomerRepository _asyncCustomerRepository;
        private readonly IAsyncMenuRepository _asyncMenuRepository;        
        private readonly IAsyncUnitOfWorkFactory _asyncUnitOfWorkFactory;
        private readonly ILogger<MenuBookedController> _logger;
        private readonly INotyfService _notyfService;
        private readonly IMapper _imapper;
        
        public MenuBookedController(IAsyncCustomerRepository asyncCustomerRepository,
                                    IAsyncMenuRepository asyncMenuRepository,
                                            IAsyncUnitOfWorkFactory asyncUnitOfWorkFactory,
                                            ILogger<MenuBookedController> logger,
                                            INotyfService notyfService,
                                            IMapper imapper)
        {
            _asyncCustomerRepository = asyncCustomerRepository;
            _asyncMenuRepository = asyncMenuRepository;
            _asyncUnitOfWorkFactory = asyncUnitOfWorkFactory;
            _logger = logger;
            _notyfService = notyfService;
            _imapper = imapper;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> List(int CustomerId, 
                                                int BookingId, 
                                                 int MenuId)
        {
            ViewBag.Idcustm = CustomerId;
            ViewBag.presId = BookingId;
            ViewBag.MenuId = MenuId;

            var customer = await _asyncCustomerRepository.FindById(CustomerId, mnbk => mnbk.Menus_Booked);
            var menubooked = customer.Menus_Booked.Where(x => x.BookingId == BookingId);
                              

            ViewBag.Message = customer.FullName;
            //ViewBag.presNumber = booking.BookingNumber;

            return View();
        }


        public async Task<IActionResult> GetMenusBooked(int customerId,
                                                         int BookingId,
                                                         int MenuId)
        {


            var bookingnotes = (from mnbkd in _asyncCustomerRepository.FindAll().Where(cs => cs.Id == customerId).SelectMany(x => x.Menus_Booked)
                                   .Where(x => x.BookingId == BookingId)
                                join bkgs in _asyncCustomerRepository.FindAll().Where(cs => cs.Id == customerId).SelectMany(bkg => bkg.ListBooking)
                                    on mnbkd.BookingId equals bkgs.Id
                                join cst in _asyncCustomerRepository.FindAll() on bkgs.CustomerId equals cst.Id
                                join mnu in _asyncMenuRepository.FindAll() on mnbkd.BookingId equals mnu.Id
                                join mnbk in _asyncMenuRepository.FindAll().Where(mn=>mn.Id==MenuId).SelectMany(y=>y.Menus_Booked) 
                                   on  mnu.Id equals mnbk.MenuId

                                select new DisplayMenuBooked
                                {
                                    Id = mnbkd.Id,




                                }).ToList();




            var Mpdata = new List<DisplayMenuBooked>();

            var lst = await Task.FromResult(bookingnotes);

            _imapper.Map(lst, Mpdata);

            return Json(new { data = Mpdata });


        }

        [HttpGet]
        public async Task<IActionResult> Create(int CustomerId, 
                                                int BookingId  )
        {
            ViewBag.Idcustm = CustomerId;
            ViewBag.presId = BookingId;
            

            var data = new CreateAndEditMenuBooked()
            {

                ListMenu = _asyncMenuRepository.FindAll()                         
                      .Select(d => new SelectListItem
                      {
                          Text = d.Menu_name,
                          Value = d.Id.ToString(),
                          Selected = true

                      }).Distinct().ToList()

            };

            var customer = await _asyncCustomerRepository.FindById(CustomerId);


            ViewBag.Message = customer.FullName;

            return View(data);
        }

        // POST: LawyerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAndEditMenuBooked createAndEditMenuBooked,
                                                    int CustomerId,
                                                    int BookingId)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                try
                {
                    await using (await _asyncUnitOfWorkFactory.Create())
                    {
                        var customer = await _asyncCustomerRepository.FindById(CustomerId, x => x.ListBooking);

                        var booking = customer.ListBooking.Single(x => x.Id == BookingId);

                        Menu_Booked menu_Booked = new();

                        _imapper.Map(createAndEditMenuBooked, menu_Booked);

                        booking.Menus_Booked.Add(menu_Booked);
                       

                        _notyfService.Success("menu_booked Added  Successfully! ");

                        return RedirectToAction(nameof(List), new { CustomerId, BookingId });
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



        public async Task<IActionResult> Edit(int CustomerId,
                                                    int BookingId,
                                                   int MenuId,
                                                    int id)
        {

            var customer = await _asyncCustomerRepository.FindById(CustomerId, x => x.Menus_Booked);
            var menubooked = customer.Menus_Booked.Where(x => x.BookingId == BookingId);
              var menuid= menubooked.Where(y=> y.MenuId == MenuId)
                            .Single(z=>z.Id==id);     
            
            ViewBag.Message = customer.FullName;
            ViewBag.Idcustm = CustomerId;
            ViewBag.presId = BookingId;
            ViewBag.Id = id;



            if (menuid == null)
            {
                return NotFound();
            }

            

            var data = new CreateAndEditMenuBooked()
            {

                ListMenu = _asyncMenuRepository.FindAll()
                     .Select(d => new SelectListItem
                     {
                         Text = d.Menu_name,
                         Value = d.Id.ToString(),
                         Selected = true

                     }).Distinct().ToList()

            };

            _imapper.Map(menubooked, data);

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateAndEditMenuBooked createAndEditMenuBooked
                                                   , int CustomerId,
                                                    int BookingId,
                                                    int MenuId)
        {

            ViewBag.Idcustm = CustomerId;
            if (ModelState.IsValid)
            {
                try
                {
                    await using (await _asyncUnitOfWorkFactory.Create())
                    {

                        var menubookeddb = (from mnbk in _asyncCustomerRepository.FindAll().Where(cs => cs.Id == CustomerId).SelectMany(x => x.Menus_Booked)
                                         .Where(mnbk => mnbk.BookingId == BookingId)
                                       join mngr in _asyncMenuRepository.FindAll().Where(mnu => mnu.Id == MenuId).SelectMany(bkg => bkg.Menus_Booked)
                                        on mnbk.MenuId equals MenuId
                                        join mn in _asyncMenuRepository.FindAll() on mngr.MenuId equals mn.Id
                                        join bkg in _asyncCustomerRepository.FindAll().Where(cs=>cs.Id==CustomerId).SelectMany(z=>z.ListBooking)
                                        on mnbk.BookingId equals bkg.Id

                                        select new CreateAndEditMenuBooked
                                       {
                                           Id = mnbk.Id,
                                           MenuId= mn.Id,
                                           BookingId=bkg.Id,

                                       }).ToList();

                        var menubooked = menubookeddb.Single(x => x.Id == createAndEditMenuBooked.Id);
                                   

                        _imapper.Map(createAndEditMenuBooked, menubooked);

                        _notyfService.Success("Bookingnote updated Successfully");

                        return RedirectToAction(nameof(List), new { CustomerId, BookingId });
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

        public async Task<IActionResult> Details(int CustomerId,
                                                    int BookingId,
                                                    int MenuId,
                                                    int id)
        {

            ViewBag.Idcustm = CustomerId;
            ViewBag.presId = BookingId;
            ViewBag.MenuId = MenuId;
            ViewBag.Id = id;


            var menubookeddb = (from mnbk in _asyncCustomerRepository.FindAll().Where(cs => cs.Id == CustomerId).SelectMany(x => x.Menus_Booked)
                                          .Where(mnbk => mnbk.BookingId == BookingId)
                                join mngr in _asyncMenuRepository.FindAll().Where(mnu => mnu.Id == MenuId).SelectMany(bkg => bkg.Menus_Booked)
                                 on mnbk.MenuId equals MenuId
                                join mn in _asyncMenuRepository.FindAll() on mngr.MenuId equals mn.Id
                                join bkg in _asyncCustomerRepository.FindAll().Where(cs => cs.Id == CustomerId).SelectMany(z => z.ListBooking)
                                on mnbk.BookingId equals bkg.Id

                                select new DisplayMenuBooked
                                {
                                    Id = mnbk.Id,
                                    MenuId = mn.Id,
                                    BookingId = bkg.Id,

                                }).ToList();

            var menubooked = menubookeddb.Single(x => x.Id == id);

            if (menubooked == null)
            {
                return NotFound();
            }
           

            var sglmnbk = await Task.FromResult(menubooked);
            // ViewBag.Message = sglprsbkg.;
            // sglevent.Color = Enum.GetName(typeof(EventStatus), Int32.Parse(sglevent.Color));
            return View(sglmnbk);
        }


        // GET: ApartmentController/Delete/5
       // public async Task<IActionResult> Delete(int CustomerId,
        //                                            int BookingId, int id)
        //{
        //    ViewBag.Idcustm = CustomerId;
        //    ViewBag.presId = BookingId;
        //    ViewBag.Id = id;


        //    var bookingnotes = (from bknt in _asyncCustomerRepository.FindAll().Where(cs => cs.Id == CustomerId).SelectMany(bknt => bknt.BookingNotes)
        //                        .Where(x => x.BookingId == BookingId)
        //                        join bkgs in _asyncCustomerRepository.FindAll().Where(cs => cs.Id == CustomerId).SelectMany(bkg => bkg.ListBooking)
        //                            on bknt.BookingId equals bkgs.Id
        //                        join cst in _asyncCustomerRepository.FindAll() on bkgs.CustomerId equals cst.Id


        //                        select new DisplayBookingNote
        //                        {
        //                            Id = bknt.Id,




        //                        }).ToList();


        //    if (bookingnotes == null)
        //    {
        //        return NotFound();
        //    }
        //    var mybkgnote = new DisplayBookingNote();

        //    var sglprsbkg = await Task.FromResult(_imapper.Map(bookingnotes.Single(x => x.Id == id), mybkgnote));
        //    // ViewBag.Message = sglprsbkg.;
        //    // sglevent.Color = Enum.GetName(typeof(EventStatus), Int32.Parse(sglevent.Color));
        //    return View(sglprsbkg);
        //}

        // POST: ApartmentController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Delete(DisplayBookingNote displayBookingNote,
        //       int CustomerId, int BookingId)
        //{

        //    await using (await _asyncUnitOfWorkFactory.Create())
        //    {
        //        var customer = await _asyncCustomerRepository.FindById(CustomerId, x => x.BookingNotes);
        //        var bookingnote = customer.BookingNotes.Where(x => x.BookingId == BookingId)
        //            .Single(y => y.Id == displayBookingNote.Id);

        //        customer.BookingNotes.Remove(bookingnote);

        //        _notyfService.Error("BookingNotes removed  Successfully");
        //    }
        //    return RedirectToAction(nameof(List), new { CustomerId, BookingId });
        //}

    }
}
