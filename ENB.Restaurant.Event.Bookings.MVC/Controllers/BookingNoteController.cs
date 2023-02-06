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
    public class BookingNoteController : Controller
    {
        private readonly IAsyncCustomerRepository _asyncCustomerRepository;       
        private readonly IAsyncUnitOfWorkFactory _asyncUnitOfWorkFactory;
        private readonly ILogger<BookingNoteController> _logger;
        private readonly INotyfService _notyfService;
        private readonly IMapper _imapper;
        public BookingNoteController(IAsyncCustomerRepository asyncCustomerRepository,                                           
                                            IAsyncUnitOfWorkFactory asyncUnitOfWorkFactory,
                                            ILogger<BookingNoteController> logger,
                                            INotyfService notyfService,
                                            IMapper imapper)
        {
            _asyncCustomerRepository = asyncCustomerRepository;            
            _asyncUnitOfWorkFactory = asyncUnitOfWorkFactory;
            _logger = logger;
            _notyfService = notyfService;
            _imapper = imapper;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> List(int CustomerId, int BookingId)
        {
            ViewBag.Idcustm = CustomerId;
            ViewBag.Idbook = BookingId;

            var customer = await _asyncCustomerRepository.FindById(CustomerId, bkg => bkg.ListBooking);
            var booking = customer.ListBooking.Single(x => x.Id == BookingId);

            ViewBag.Message = customer.FullName;
            ViewBag.presNumber = booking.BookingNumber;

            return View();
        }


        public async Task<IActionResult> GetBookingNotes(int CustomerId, int BookingId)
        {


            var bookingnotes = (from bknt in _asyncCustomerRepository.FindAll().Where(cs => cs.Id == CustomerId).SelectMany(bknt => bknt.BookingNotes)
                                   .Where(x => x.BookingId == BookingId)
                                join bkgs in _asyncCustomerRepository.FindAll().Where(cs => cs.Id == CustomerId).SelectMany(bkg => bkg.ListBooking)
                                 on bknt.BookingId equals bkgs.Id
                                join cst in _asyncCustomerRepository.FindAll() on bkgs.CustomerId equals cst.Id
                             

                             select new DisplayBookingNote
                             {
                                 Id = bknt.Id,
                                 Details_of_notes=bknt.Details_of_notes,
                                 NameCustomer=cst.FullName,
                                 BookingNumber=bkgs.BookingNumber,
                                 DateCreated=bknt.DateCreated,
                                 DateModified=bknt.DateModified,                                 
                                 

                             }).ToList();




            var Mpdata = new List<DisplayBookingNote>();

            var lst = await Task.FromResult(bookingnotes);

            _imapper.Map(lst, Mpdata);

            return Json(new { data = Mpdata });


        }

        [HttpGet]
        public async Task<IActionResult> Create(int CustomerId, int BookingId)
        {
            ViewBag.Idcustm = CustomerId;
            ViewBag.Idbook = BookingId;

            
            var customer = await _asyncCustomerRepository.FindById(CustomerId);


            ViewBag.Message = customer.FullName;

            return View();
        }

        // POST: LawyerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAndEditBookingNote createAndEditBookingNote,
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

                        Booking_Note booking_Note  = new();

                        _imapper.Map(createAndEditBookingNote, booking_Note);
                       

                        booking.BookingNotes.Add(booking_Note);

                        _notyfService.Success("booking_Note Added  Successfully! ");

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
                                                    int BookingId, int id)
        {

            var customer = await _asyncCustomerRepository.FindById(CustomerId, x => x.BookingNotes);
            var bookingnote = customer.BookingNotes.Where(x => x.BookingId == BookingId)
                       .Single(y => y.Id == id);
            ViewBag.Message = customer.FullName;
            ViewBag.Idcustm = CustomerId;
            ViewBag.Idbook = BookingId;
            ViewBag.Id = id;



            if (customer == null)
            {
                return NotFound();
            }

            var data = new CreateAndEditBookingNote();

            //var data = new CreateAndEditPrescriptionItem()
            //{

            //    ListDrugs = _asyncDrugCompanyRepository.FindAll()
            //             .SelectMany(x => x.Drug_Medications)
            //         .Select(d => new SelectListItem
            //         {
            //             Text = d.Drug_name,
            //             Value = d.Id.ToString(),
            //             Selected = true

            //         }).Distinct().ToList()

            //};

            _imapper.Map(bookingnote, data);

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateAndEditBookingNote createAndEditBookingNote
                                              , int CustomerId,
                                                    int BookingId)
        {

            ViewBag.Idcustm = CustomerId;
            if (ModelState.IsValid)
            {
                try
                {
                    await using (await _asyncUnitOfWorkFactory.Create())
                    {

                        var customer = await _asyncCustomerRepository.FindById(CustomerId, x => x.BookingNotes);
                        var bookingnote = customer.BookingNotes.Where(x => x.BookingId == BookingId)
                                   .Single(y => y.Id == createAndEditBookingNote.Id);

                        _imapper.Map(createAndEditBookingNote, bookingnote);

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
                                                    int BookingId, int id)
        {

            ViewBag.Idcustm = CustomerId;
            ViewBag.presId = BookingId;
            ViewBag.Id = id;


            var bookingnotes = (from bknt in _asyncCustomerRepository.FindAll().Where(cs => cs.Id == CustomerId).SelectMany(bknt => bknt.BookingNotes)
                                .Where(x => x.BookingId == BookingId)
                                join bkgs in _asyncCustomerRepository.FindAll().Where(cs => cs.Id == CustomerId).SelectMany(bkg => bkg.ListBooking)
                                    on bknt.BookingId equals bkgs.Id
                                join cst in _asyncCustomerRepository.FindAll() on bkgs.CustomerId equals cst.Id


                                select new DisplayBookingNote
                                {
                                    Id = bknt.Id,




                                }).ToList();


            if (bookingnotes == null)
            {
                return NotFound();
            }
            var mybkgnote = new DisplayBookingNote();

            var sglprsbkg = await Task.FromResult(_imapper.Map(bookingnotes.Single(x => x.Id == id), mybkgnote));
           // ViewBag.Message = sglprsbkg.;
            // sglevent.Color = Enum.GetName(typeof(EventStatus), Int32.Parse(sglevent.Color));
            return View(sglprsbkg);
        }


        // GET: ApartmentController/Delete/5
        public async Task<IActionResult> Delete(int CustomerId,
                                                    int BookingId, int id)
        {
            ViewBag.Idcustm = CustomerId;
            ViewBag.presId = BookingId;
            ViewBag.Id = id;


            var bookingnotes = (from bknt in _asyncCustomerRepository.FindAll().Where(cs => cs.Id == CustomerId).SelectMany(bknt => bknt.BookingNotes)
                                .Where(x => x.BookingId == BookingId)
                                join bkgs in _asyncCustomerRepository.FindAll().Where(cs => cs.Id == CustomerId).SelectMany(bkg => bkg.ListBooking)
                                    on bknt.BookingId equals bkgs.Id
                                join cst in _asyncCustomerRepository.FindAll() on bkgs.CustomerId equals cst.Id


                                select new DisplayBookingNote
                                {
                                    Id = bknt.Id,




                                }).ToList();


            if (bookingnotes == null)
            {
                return NotFound();
            }
            var mybkgnote = new DisplayBookingNote();

            var sglprsbkg = await Task.FromResult(_imapper.Map(bookingnotes.Single(x => x.Id == id), mybkgnote));
            // ViewBag.Message = sglprsbkg.;
            // sglevent.Color = Enum.GetName(typeof(EventStatus), Int32.Parse(sglevent.Color));
            return View(sglprsbkg);
        }

        // POST: ApartmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DisplayBookingNote displayBookingNote,
               int CustomerId, int BookingId)
        {

            await using (await _asyncUnitOfWorkFactory.Create())
            {
                var customer = await _asyncCustomerRepository.FindById(CustomerId, x => x.BookingNotes);
                var bookingnote = customer.BookingNotes.Where(x => x.BookingId == BookingId)
                    .Single(y => y.Id == displayBookingNote.Id);

                customer.BookingNotes.Remove(bookingnote);

                _notyfService.Error("BookingNotes removed  Successfully");
            }
            return RedirectToAction(nameof(List), new { CustomerId, BookingId });
        }
    }

}

