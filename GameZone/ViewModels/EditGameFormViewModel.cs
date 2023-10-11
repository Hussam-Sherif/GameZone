namespace GameZone.ViewModels
{
    public class EditGameFormViewModel: GameFormViewModel
    {
        public int ID { get; set; }
        public string? CoverName { get; set; }
        [AllowedExtensions(FileConsts.AllowedExtensions),
    MaxFileSize(FileConsts.MaxImageSizeInByte)]
        public IFormFile? Cover { get; set; } = default!;

    }
}
