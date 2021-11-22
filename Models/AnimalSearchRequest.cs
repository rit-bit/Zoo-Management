namespace Zoo_Management.Models
{
    public class AnimalSearchRequest
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        private string _search;
        public string Search
        {
            get => _search?.ToLower();
            set => _search = value;
        }
    }
}