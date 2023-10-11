

using Microsoft.AspNetCore.Http.HttpResults;
using System.Reflection.Metadata.Ecma335;

namespace GameZone.Services
{
    public class GamesServices : IGameServices
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imagesPath;

        public GamesServices(ApplicationDBContext dbContext,
            IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
            _imagesPath = $"{_webHostEnvironment.WebRootPath}{FileConsts.FilePath}";
        }

        public async Task Create(CreateGameFormViewModel viewModel)
        {

            var CoverName = await SaveName(viewModel.Cover);
            Game game = new()
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
                CategoryID = viewModel.CategoryID,
                Cover = CoverName,
                Devices = viewModel.SelectedDevices.Select(d => new GameDevice { DeviceID = d }).ToList(),

            };
            _dbContext.Games.Add(game);
            _dbContext.SaveChanges();
        }

        public bool Delete(int id)
        {
            var IsDeleted = false;
            var record = _dbContext.Games.Find(id);
            if (record is null)
                return IsDeleted;
            _dbContext.Remove(record);
            var effectedrows = _dbContext.SaveChanges();
            if (effectedrows > 0)
            {
                IsDeleted = !IsDeleted;
                var OldCover = Path.Combine(_imagesPath, record.Cover);
                    File.Delete(OldCover);

            }
            return IsDeleted;
        }

        public async Task<Game?> Edit(EditGameFormViewModel Model)
        {
            var record = _dbContext.Games.Include(d=>d.Devices).FirstOrDefault(g=>g.ID==Model.ID);
            if (record is null)
                return null;

            var HasNewCover = Model.Cover is not null;
            var OldtCover = record.Cover;

            record.Name = Model.Name;
            record.Description = Model.Description;
            record.CategoryID = Model.CategoryID;
            record.Devices = Model.SelectedDevices.Select(d => new GameDevice { DeviceID = d }).ToList();
            if (HasNewCover)
            {
                record.Cover = await SaveName(Model.Cover!);
            }
            var effectedrows = _dbContext.SaveChanges();
            if (effectedrows > 0)
            {
                if (HasNewCover)
                {
                    var OldCover = Path.Combine(_imagesPath, OldtCover);
                    File.Delete(OldCover);
                    
                }
                return record;
            }
            else
            {
                var Cover = Path.Combine(_imagesPath, record.Cover);
                File.Delete(Cover);
                return null;
            }
          
        }

        public IEnumerable<Game> GetAllGames()
        {
            return _dbContext.Games
                .Include(c => c.Category)
                .Include(gd => gd.Devices)
                .ThenInclude(d => d.Device)
                .AsNoTracking()
                .ToList();

        }

        public Game? GetById(int id)
        {
            return _dbContext.Games
                .Include(c => c.Category)
                .Include(gd => gd.Devices)
                .ThenInclude(d => d.Device)
                .AsNoTracking()
                .SingleOrDefault(g => g.ID == id);


        }

        public IEnumerable<SelectListItem> GetCategorySelectList()
        {

            return _dbContext.Categories
                .OrderBy(c => c.Name)
                    .Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.Name });
        }

        public IEnumerable<SelectListItem> GetDeviceSelectList()
        {

            return _dbContext.Devices
                .OrderBy(c => c.Name)
                .Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.Name })
                .AsNoTracking()
                .ToList();
        }
        private async Task<string> SaveName(IFormFile Cover)
        {
            var CoverName = $"{Guid.NewGuid()}{Path.GetExtension(Cover.FileName)}";
            var path = Path.Combine(_imagesPath, CoverName);
            using var stream = File.Create(path);
            await Cover.CopyToAsync(stream);
            return CoverName;
        }
    }
}
