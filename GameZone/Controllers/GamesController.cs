

using GameZone.Services;

namespace GameZone.Controllers
{
    public class GamesController : Controller
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly IGameServices _gameServices;

        public GamesController(ApplicationDBContext dbContext,
            IGameServices gameServices)
        {
            _dbContext = dbContext;
            _gameServices = gameServices;
        }

        public IActionResult Index()
        {
            var record = _gameServices.GetAllGames();
            return View(record);
        }
        public async Task<IActionResult> Create()
        {
            CreateGameFormViewModel viewModel = new()
            {
                Categories = _gameServices.GetCategorySelectList(),
                Devices = _gameServices.GetDeviceSelectList()
            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {

                viewModel.Categories = _gameServices.GetCategorySelectList();
                viewModel.Devices = _gameServices.GetDeviceSelectList();
                return View(viewModel);
            }
            await _gameServices.Create(viewModel);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var record =  _gameServices.GetById(id);
            if (record == null)
                return NotFound();

            return View(record);

        }
        [HttpGet]
        public async Task<IActionResult>Edit(int id)
        {
            if (id == 0)
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();
            var record =_gameServices.GetById(id);
            if (record is null)
                return NotFound();
            EditGameFormViewModel ViewModel = new()
            {
                ID= record.ID,
                Name= record.Name,
                Description= record.Description,
                CategoryID= record.CategoryID,
                CoverName=record.Cover,
                SelectedDevices= record.Devices.Select(b=>b.DeviceID).ToList(),
                Categories= _gameServices.GetCategorySelectList(),
                Devices= _gameServices.GetDeviceSelectList(),

            };
            return View(ViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditGameFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categories = _gameServices.GetCategorySelectList();
                viewModel.Devices = _gameServices.GetDeviceSelectList();
                return View(viewModel);
            }
            var game= await _gameServices.Edit(viewModel);
            if (game is null)
                return BadRequest();
            return RedirectToAction(nameof(Index));
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var isDeleted = _gameServices.Delete(id);

            return isDeleted ? Ok() : BadRequest();
        }
    }
}
