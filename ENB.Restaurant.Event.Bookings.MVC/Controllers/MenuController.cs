using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using ENB.Restaurant.Event.Bookings.Entities;
using ENB.Restaurant.Event.Bookings.Entities.Repositories;
using ENB.Restaurant.Event.Bookings.Infrastructure;
using ENB.Restaurant.Event.Bookings.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENB.PharmaciesAndPrescriptions.MVC.Controllers
{
    [Authorize]
    public class MenuController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger<MenuController> _logger;
        private readonly IAsyncMenuRepository _asyncMenuRepository;
        private readonly IAsyncUnitOfWorkFactory _asyncUnitOfWorkFactory;
        private readonly INotyfService _notyf;
        public MenuController(IMapper mapper, ILogger<MenuController> logger,
                                   IAsyncMenuRepository asyncMenuRepository,
                                   IAsyncUnitOfWorkFactory asyncUnitOfWorkFactory,
                                   INotyfService notyf)
        {
            _mapper = mapper;
            _logger = logger;
            _asyncMenuRepository = asyncMenuRepository;
            _asyncUnitOfWorkFactory = asyncUnitOfWorkFactory;
            _notyf = notyf;
        }

        // GET: CustomerController
        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetMenuData()
        {
            IQueryable<Menu> allMenu = _asyncMenuRepository.FindAll();

            var Mpdata = _mapper.Map<List<DisplayMenu>>(allMenu).ToList();
            await Task.FromResult(Mpdata);
            return Json(new { data = Mpdata });
        }

        // GET: CustomerController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            ViewBag.Id = id;

            _logger.LogError($"Id :{id} of Menu not found");

            Menu dbMenu = await _asyncMenuRepository.FindById(id);

            ViewBag.Message = dbMenu.Menu_name;

            _logger.LogInformation($"Details of Menu: {ViewBag.Message}");

            if (dbMenu == null)
            {
                return NotFound();
            }

            var data = _mapper.Map<DisplayMenu>(dbMenu);

            return View(data);
        }

        // GET: CustomerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAndEditMenu createAndEditMenu)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                try
                {
                    await using (await _asyncUnitOfWorkFactory.Create())
                    {

                        Menu dbMenu = new();

                        _mapper.Map(createAndEditMenu, dbMenu);
                        await _asyncMenuRepository.Add(dbMenu);

                        _notyf.Success("Menu Created  Successfully! ");

                        return RedirectToAction("Index");
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

            ViewBag.Idmenu = id;

            Menu dbMenu = await _asyncMenuRepository.FindById(id);

            if (dbMenu == null)
            {
               _logger.LogError($"Menu {id} not found");
                return NotFound();
            }
            var data = await Task.FromResult(_mapper.Map<CreateAndEditMenu>(dbMenu));

            return View(data);
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateAndEditMenu createAndEditMenu)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await using (await _asyncUnitOfWorkFactory.Create())
                    {

                        Menu dbMenuToUpdate = await _asyncMenuRepository.FindById(createAndEditMenu.Id);

                        _mapper.Map(createAndEditMenu, dbMenuToUpdate, typeof(CreateAndEditMenu), typeof(Menu));

                        _notyf.Success("Menu Updated  Successfully! ");

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
            Menu dbMenu = await _asyncMenuRepository.FindById(id);
            ViewBag.Message = dbMenu.Menu_name;

            if (dbMenu == null)
            {
                return NotFound();
            }
            var data = _mapper.Map<DisplayMenu>(dbMenu);
            return View(data);
        }

        // POST: CustomerController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Menu dbMenu = await _asyncMenuRepository.FindById(id);
            await using (await _asyncUnitOfWorkFactory.Create())
            {
               await   _asyncMenuRepository.Remove(id);

                _notyf.Error("Menu Removed  Successfully! ");
            }

            return RedirectToAction(nameof(Index)); ;
        }
    }
}
