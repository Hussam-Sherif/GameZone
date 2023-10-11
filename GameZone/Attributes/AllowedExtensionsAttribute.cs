namespace GameZone.Attributes
{
    public class AllowedExtensionsAttribute:ValidationAttribute
    {
        private readonly string _AllowedExtensions;

        public AllowedExtensionsAttribute(string allowedExtensions)
        {
            _AllowedExtensions = allowedExtensions;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile; 
            if (file != null) 
            {
                var extension = Path.GetExtension(file.FileName);
                var IsAllowed = _AllowedExtensions.Split(',').Contains(extension, StringComparer.OrdinalIgnoreCase);
                if(!IsAllowed) 
                {
                    return new ValidationResult( $" Only {_AllowedExtensions} are allowed !");
                }
               
            }
            return ValidationResult.Success;
        }
    }
}
