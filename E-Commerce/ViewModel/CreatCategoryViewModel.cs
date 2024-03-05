namespace E_Commerce.ViewModel
{
    public class CreatCategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public IFormFile ? photo { get; set; } = default!;

    }
}
