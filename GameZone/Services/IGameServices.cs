namespace GameZone.Services
{
    public interface IGameServices
    {
        IEnumerable<SelectListItem> GetCategorySelectList();
        IEnumerable<SelectListItem> GetDeviceSelectList();

        Task Create (CreateGameFormViewModel Model);
        Task<Game?> Edit (EditGameFormViewModel Model);
        IEnumerable<Game> GetAllGames();
        Game? GetById (int id);
        bool Delete(int id);
    }
}
