namespace GameZone.ViewModels
{
    public class CreateGameFormViewModel: GameFormViewModel
    {
        [AllowedExtensions(FileConsts.AllowedExtensions),
        MaxFileSize(FileConsts.MaxImageSizeInByte)]
        public IFormFile Cover { get; set; } = default!;

    }
}
