using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using ENB.Restaurant.Event.Bookings.EF.Repositories;
using ENB.Restaurant.Event.Bookings.Entities;
using ENB.Restaurant.Event.Bookings.Entities.Collections;
using ENB.Restaurant.Event.Bookings.Entities.Repositories;
using ENB.Restaurant.Event.Bookings.Infrastructure;
using ENB.Restaurant.Event.Bookings.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ENB.PharmaciesAndPrescriptions.MVC.Controllers
{
    [Authorize]
    public class MealController : Controller
    {
        
        private readonly IMapper _mapper;
        private readonly ILogger<MealController> _logger;
        private readonly IAsyncMenuRepository _asyncMenuRepository;
        private readonly IAsyncMealRepository _asyncMealRepository;
        private readonly IAsyncUnitOfWorkFactory _asyncUnitOfWorkFactory;
        private readonly INotyfService _notyf;
        public MealController(IMapper mapper, ILogger<MealController> logger,
                                   IAsyncMenuRepository asyncMenuRepository,                                  
                                   IAsyncMealRepository asyncMealRepository,
                                   IAsyncUnitOfWorkFactory asyncUnitOfWorkFactory,
                                   INotyfService notyf)
        {
            _mapper = mapper;
            _logger = logger;
            _asyncMenuRepository = asyncMenuRepository; 
            _asyncMealRepository = asyncMealRepository;
            _asyncUnitOfWorkFactory = asyncUnitOfWorkFactory;
            _notyf = notyf;
        }

        // GET: CustomerController
        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> List(int MenuId)
        {
            ViewBag.Idmenu = MenuId;
           
            var menu = await _asyncMenuRepository.FindById(MenuId);

            ViewBag.Message = menu.Menu_name;
            

            return View();
        }
        public async Task<IActionResult> GetMealData()
        {
            var allMeal = _asyncMealRepository.FindAll();

            var Mpdata = _mapper.Map<List<DisplayMeal>>(allMeal).ToList();
            await Task.FromResult(Mpdata);
            return Json(new { data = Mpdata });
        }

        // GET: CustomerController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            ViewBag.Id = id;

            _logger.LogError($"Id :{id} of Meal not found");

            Meal dbMeal = await _asyncMealRepository.FindById(id);

            ViewBag.Message = dbMeal.MealName;

            _logger.LogInformation($"Details of Meal: {ViewBag.Message}");

            if (dbMeal == null)
            {
                return NotFound();
            }

            var data = _mapper.Map<DisplayMeal>(dbMeal);

            return View(data);
        }

        // GET: CustomerController/Create
        public IActionResult Create()
        {
           
            return View();
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAndEditMeal createAndEditMeal)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                try
                {
                    await using (await _asyncUnitOfWorkFactory.Create())
                    {
                    

                        Meal meal = new();
                     

                        _mapper.Map(createAndEditMeal, meal);                       
                        

                        await _asyncMealRepository.Add(meal);
                       

                        _notyf.Success("Meal Created  Successfully! ");

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

        // GET: CustomerController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

           

            Meal dbMeal = await _asyncMealRepository.FindById(id);

            if (dbMeal == null)
            {
               _logger.LogError($"Meal {id} not found");
                return NotFound();
            }
            var data = await Task.FromResult(_mapper.Map<CreateAndEditMeal>(dbMeal));

            return View(data);
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateAndEditMeal createAndEditMeal)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await using (await _asyncUnitOfWorkFactory.Create())
                    {

                        Meal dbMealToUpdate = await _asyncMealRepository.FindById(createAndEditMeal.Id);

                        _mapper.Map(createAndEditMeal, dbMealToUpdate, typeof(CreateAndEditMeal), typeof(Meal));

                        _notyf.Success("Meal Updated  Successfully! ");

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

        // GET: CustomerController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            Meal dbMeal = await _asyncMealRepository.FindById(id);
            ViewBag.Message = dbMeal.MealName;

            if (dbMeal == null)
            {
                return NotFound();
            }
            var data = _mapper.Map<DisplayMeal>(dbMeal);
            return View(data);
        }

        // POST: CustomerController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Meal dbMeal = await _asyncMealRepository.FindById(id);
            await using (await _asyncUnitOfWorkFactory.Create())
            {
               await   _asyncMealRepository.Remove(id);

                _notyf.Error("Meal Removed  Successfully! ");
            }

            return RedirectToAction(nameof(Index)); ;
        }
    }
}
