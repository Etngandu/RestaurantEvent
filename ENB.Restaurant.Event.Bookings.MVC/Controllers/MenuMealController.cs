using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using ENB.Restaurant.Event.Bookings.Entities;
using ENB.Restaurant.Event.Bookings.Entities.Repositories;
using ENB.Restaurant.Event.Bookings.Infrastructure;
using ENB.Restaurant.Event.Bookings.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ENB.Restaurant.Event.Bookings.MVC.Controllers
{
    [Authorize]
    public class MenuMealController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly IAsyncMenuRepository _asyncMenuRepository;
        private readonly IAsyncMealRepository _asyncMealRepository;
        private readonly IAsyncUnitOfWorkFactory _asyncUnitOfWorkFactory;
        private readonly ILogger<MenuMealController> _logger;
        private readonly IMapper _mapper;
        private readonly INotyfService _notyf;


        public MenuMealController(IAsyncMenuRepository asyncMenuRepository,
                           IAsyncMealRepository asyncMealRepository,
                           IAsyncUnitOfWorkFactory asyncUnitOfWorkFactory,
                           ILogger<MenuMealController> logger,
                           IMapper mapper,
                           INotyfService notyf)
        {
            _asyncMenuRepository = asyncMenuRepository;
            _asyncMealRepository = asyncMealRepository;
            _asyncUnitOfWorkFactory = asyncUnitOfWorkFactory;
            _logger = logger;
            _mapper = mapper;
            _notyf = notyf;
        }

        public async Task<IActionResult> MealsperMenuList(int MenuId)
        {
            ViewBag.Idmenu = MenuId;
            var Menu = await _asyncMenuRepository.FindById(MenuId);

            ViewBag.Message = Menu.Menu_name;

            return View();
        }

        public async Task<IActionResult> MenusperMealList(int MealId)
        {
            ViewBag.Idmeal = MealId;
            var meal = await _asyncMealRepository.FindById(MealId);

            ViewBag.Message = meal.MealName;

            return View();
        }


        public  IActionResult GetMealsperMenuData(int MenuId)
        {

            var allMeal = (from mm in _asyncMenuRepository.FindAll().Where(x => x.Id == MenuId).SelectMany(bkg => bkg.Menu_Meals)
                           join mn in _asyncMenuRepository.FindAll() on mm.MenuId equals mn.Id
                           join ml in _asyncMealRepository.FindAll() on mm.MealId equals ml.Id
                           select new DisplayMenuMeal
                           {                               
                               
                               Id=(int)mm.MealId!,
                               MealName = ml.MealName,
                               MenuName=mn.Menu_name, 
                               Mealdate=ml.Dateofmeal,
                               Other_Menu_Meal_detail=ml.Other_MealDetail



                           }).ToList();

           

          // var Mpdata =_mapper.Map<List<DisplayMenuMeal>>(allMeal).ToList();

            //await Task.FromResult(allMeal);

            return Json(new { data = allMeal });


        }

        public IActionResult GetMenusperMealData(int MealId)
        {

            var allMenus = (from mm in _asyncMealRepository.FindAll().Where(x => x.Id == MealId).SelectMany(bkg => bkg.Menu_Meals)
                           join ml in _asyncMealRepository.FindAll() on mm.MealId equals ml.Id
                           join mn in _asyncMenuRepository.FindAll() on mm.MenuId equals mn.Id
                           select new DisplayMenuMeal
                           {

                               Id = (int)mm.MenuId!,
                               MealName = ml.MealName,
                               MenuName = mn.Menu_name,
                               Mealdate = ml.Dateofmeal,
                               Other_Menu_Meal_detail = ml.Other_MealDetail



                           }).ToList();



            // var Mpdata =_mapper.Map<List<DisplayMenuMeal>>(allMeal).ToList();

            //await Task.FromResult(allMeal);

            return Json(new { data = allMenus });


        }

        [HttpGet]
        public async Task<IActionResult> MealsperMenuCreate(int MenuId)
        {
            ViewBag.Idmenu = MenuId;

            var data = new CreateAndEditMenuMeal()
            {

                ListMeal = _asyncMealRepository.FindAll()
                      .Select(d => new SelectListItem
                      {
                          Text = d.MealName,
                          Value = d.Id.ToString(),
                          Selected = true

                      }).Distinct().ToList()

            };

            var Menu = await _asyncMenuRepository.FindById(MenuId);

            ViewBag.Message = Menu.Menu_name;

            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> MenusperMealCreate(int MealId)
        {
            ViewBag.Idmeal = MealId;

            var data = new CreateAndEditMenuMeal()
            {

                ListMenu = _asyncMenuRepository.FindAll()
                      .Select(d => new SelectListItem
                      {
                          Text = d.Menu_name,
                          Value = d.Id.ToString(),
                          Selected = true

                      }).Distinct().ToList()

            };

            var meal = await _asyncMealRepository.FindById(MealId);

            ViewBag.Message = meal.MealName;

            return View(data);
        }

        // POST: LawyerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MealsperMenuCreate(CreateAndEditMenuMeal createAndEditMenuMeal,
                                                  int MenuId)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                try
                {
                    await using (await _asyncUnitOfWorkFactory.Create())
                    {

                        var menu = await _asyncMenuRepository.FindById(MenuId);
                        

                        Menu_Meal MenuMeal = new();

                        _mapper.Map(createAndEditMenuMeal, MenuMeal);

                         menu.Menu_Meals.Add(MenuMeal);

                        _notyf.Success("MenuMeal  Added  Successfully! ");

                        return RedirectToAction(nameof(MealsperMenuList), new { MenuId });
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

        // POST: LawyerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MenusperMealCreate(CreateAndEditMenuMeal createAndEditMenuMeal,
                                                  int MealId)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                try
                {
                    await using (await _asyncUnitOfWorkFactory.Create())
                    {

                        var meal = await _asyncMealRepository.FindById(MealId);


                        Menu_Meal MenuMeal = new();

                        _mapper.Map(createAndEditMenuMeal, MenuMeal);

                        meal.Menu_Meals.Add(MenuMeal);

                        _notyf.Success("MenuMeal  Added  Successfully! ");

                        return RedirectToAction(nameof(MenusperMealList), new { MealId });
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

        public async Task<IActionResult> MealsperMenuEdit(int MenuId, int MealId)
        {

            var menu = await _asyncMenuRepository.FindById(MenuId , x=>x.Menu_Meals);
            var menumeal= await Task.FromResult(menu.Menu_Meals.Single(y=>y.MealId== MealId));

            ViewBag.Message = menu.Menu_name;
            ViewBag.Idmenu = MenuId;         

          

            if (menumeal == null)
            {
                return NotFound();
            }

            var data = new CreateAndEditMenuMeal()
            {

                ListMeal = _asyncMealRepository.FindAll()
                      .Select(d => new SelectListItem
                      {
                          Text = d.MealName,
                          Value = d.Id.ToString(),
                          Selected = true

                      }).Distinct().ToList()

            };



            _mapper.Map(menumeal, data);

            return View(data);
        }

        public async Task<IActionResult> MenusperMealEdit(int MenuId, int MealId)
        {

            var meal = await _asyncMealRepository.FindById(MealId, x => x.Menu_Meals);
            var menumeal = await Task.FromResult(meal.Menu_Meals.Single(y => y.MenuId == MenuId));

            ViewBag.Message = meal.MealName;
            ViewBag.Idmenu = MenuId;
            ViewBag.Idmeal = MealId;



            if (menumeal == null)
            {
                return NotFound();
            }

            var data = new CreateAndEditMenuMeal()
            {

                ListMenu = _asyncMenuRepository.FindAll()
                      .Select(d => new SelectListItem
                      {
                          Text = d.Menu_name,
                          Value = d.Id.ToString(),
                          Selected = true

                      }).Distinct().ToList()

            };



            _mapper.Map(menumeal, data);

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MealsperMenuEdit(CreateAndEditMenuMeal 
                                                         createAndEditMenuMeal,
                                                          int MenuId,
                                                          int MealId)
        {

            ViewBag.Idcustm = MenuId;
            if (ModelState.IsValid)
            {
                try
                {
                    await using (await _asyncUnitOfWorkFactory.Create())
                    {

                        var menu = await _asyncMenuRepository.FindById(MenuId, x => x.Menu_Meals);
                        var menu_meal = await Task.FromResult(menu.Menu_Meals.Single(x => x.MealId == createAndEditMenuMeal.Id));

                        _mapper.Map(createAndEditMenuMeal, menu_meal);

                        _notyf.Success("MenuMeal updated Successfully");

                        return RedirectToAction(nameof(MenusperMealList), new { MealId });
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MenusperMealEdit(CreateAndEditMenuMeal
                                                         createAndEditMenuMeal,
                                                          int MenuId,
                                                          int MealId)
        {

            ViewBag.Idcustm = MenuId;
            if (ModelState.IsValid)
            {
                try
                {
                    await using (await _asyncUnitOfWorkFactory.Create())
                    {

                        var meal = await _asyncMealRepository.FindById(MenuId, x => x.Menu_Meals);
                        var menu_meal = await Task.FromResult(meal.Menu_Meals.Single(x => x.MenuId == createAndEditMenuMeal.Id));

                        _mapper.Map(createAndEditMenuMeal, menu_meal);

                        _notyf.Success("MenuMeal updated Successfully");

                        return RedirectToAction(nameof(MenusperMealList), new { MealId });
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

        public async Task<IActionResult> MealsperMenuDetails(int MenuId, int MealId)
        {
            ViewBag.Idmenu = MenuId;
            ViewBag.Idmeal = MealId;

            var allMeal = (from mm in _asyncMenuRepository.FindAll().Where(x => x.Id == MenuId).SelectMany(bkg => bkg.Menu_Meals)
                           join mn in _asyncMenuRepository.FindAll() on mm.MenuId equals mn.Id
                           join ml in _asyncMealRepository.FindAll() on mm.MealId equals ml.Id
                           select new DisplayMenuMeal
                           {

                              // Id = (int)mm.MealId!,
                               MealId= mm.MealId!,
                               MenuId= mm.MenuId!,
                               MealName = ml.MealName,
                               MenuName = mn.Menu_name,
                               Mealdate = ml.Dateofmeal,
                               Other_Menu_Meal_detail = ml.Other_MealDetail



                           }).ToList();

           
            
           

            var mysglmenumeal = await Task.FromResult(allMeal.Single(y => y.MealId == MealId));

                 
            //  ViewBag.Message = menu.MenuName;
            ViewBag.Message = mysglmenumeal.MenuName;
            return View(mysglmenumeal);
        }

        public async Task<IActionResult> MenusperMealDetails(int MenuId, int MealId)
        {
            ViewBag.Idmenu = MenuId;
            ViewBag.Idmeal = MealId;

            var allMeal = (from mm in _asyncMealRepository.FindAll().Where(x => x.Id == MealId).SelectMany(bkg => bkg.Menu_Meals)
                           join ml in _asyncMealRepository.FindAll() on mm.MealId equals ml.Id
                           join mn in _asyncMenuRepository.FindAll() on mm.MenuId equals mn.Id
                           select new DisplayMenuMeal
                           {

                               // Id = (int)mm.MealId!,
                               MealId = mm.MealId!,
                               MenuId = mm.MenuId!,
                               MealName = ml.MealName,
                               MenuName = mn.Menu_name,
                               Mealdate = ml.Dateofmeal,
                               Other_Menu_Meal_detail = ml.Other_MealDetail



                           }).ToList();





            var mysglmenumeal = await Task.FromResult(allMeal.Single(y => y.MealId == MealId));


            //  ViewBag.Message = menu.MenuName;
            ViewBag.Message = mysglmenumeal.MenuName;
            return View(mysglmenumeal);
        }

        // GET: ApartmentController/Delete/5
        public async Task<IActionResult> MealsperMenuDelete(int MenuId, int MealId)
        {
            ViewBag.Idmenu = MenuId;
            ViewBag.Idmeal = MealId;

            var allMeal = (from mm in _asyncMenuRepository.FindAll().Where(x => x.Id == MenuId).SelectMany(bkg => bkg.Menu_Meals)
                           join mn in _asyncMenuRepository.FindAll() on mm.MenuId equals mn.Id
                           join ml in _asyncMealRepository.FindAll() on mm.MealId equals ml.Id
                           select new DisplayMenuMeal
                           {

                               // Id = (int)mm.MealId!,
                               MealId = mm.MealId!,
                               MenuId = mm.MenuId!,
                               MealName = ml.MealName,
                               MenuName = mn.Menu_name,
                               Mealdate = ml.Dateofmeal,
                               Other_Menu_Meal_detail = ml.Other_MealDetail



                           }).ToList();





            var mysglmenumeal = await Task.FromResult(allMeal.Single(y => y.MealId == MealId));


            
            ViewBag.Message = mysglmenumeal.MenuName;
            return View(mysglmenumeal);
        }

        public async Task<IActionResult> MenusperMealDelete(int MenuId, int MealId)
        {
            ViewBag.Idmenu = MenuId;
            ViewBag.Idmeal = MealId;

            var allMeal = (from mm in _asyncMealRepository.FindAll().Where(x => x.Id == MealId).SelectMany(bkg => bkg.Menu_Meals)
                           join ml in _asyncMealRepository.FindAll() on mm.MealId equals ml.Id
                           join mn in _asyncMenuRepository.FindAll() on mm.MenuId equals mn.Id
                           select new DisplayMenuMeal
                           {

                               // Id = (int)mm.MealId!,
                               MealId = mm.MealId!,
                               MenuId = mm.MenuId!,
                               MealName = ml.MealName,
                               MenuName = mn.Menu_name,
                               Mealdate = ml.Dateofmeal,
                               Other_Menu_Meal_detail = ml.Other_MealDetail



                           }).ToList();





            var mysglmenumeal = await Task.FromResult(allMeal.Single(y => y.MealId == MealId));


            //  ViewBag.Message = menu.MenuName;
            ViewBag.Message = mysglmenumeal.MenuName;
            return View(mysglmenumeal);
        }

        // POST: ApartmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MealsperMenuDelete(DisplayMenuMeal displayMenuMeal, int MenuId)
        {
            // ViewBag.Case_Id = caseid;
            await using (await _asyncUnitOfWorkFactory.Create())
            {
                var menu = await _asyncMenuRepository.FindById(MenuId, x => x.Menu_Meals);
                var menu_meal = await Task.FromResult(menu.Menu_Meals.Single(x => x.MealId == displayMenuMeal.Id));


                menu.Menu_Meals.Remove(menu_meal);

                _notyf.Error("MenuMeal  removed  Successfully");
            }
            return RedirectToAction(nameof(MealsperMenuList), new { MenuId });
        }

        // POST: ApartmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MenusperMealDelete(DisplayMenuMeal displayMenuMeal, int MealId)
        {
            // ViewBag.Case_Id = caseid;
            await using (await _asyncUnitOfWorkFactory.Create())
            {
                var meal = await _asyncMealRepository.FindById(MealId, x => x.Menu_Meals);
                var menu_meal = await Task.FromResult(meal.Menu_Meals.Single(x => x.MenuId == displayMenuMeal.Id));


                meal.Menu_Meals.Remove(menu_meal);

                _notyf.Error("MenuMeal  removed  Successfully");
            }
            return RedirectToAction(nameof(MenusperMealList), new { MealId });
        }
    }
}
