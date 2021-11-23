using System;
using Zoo_Management.Data;
using Zoo_Management.Models.Database;

namespace Zoo_Management.Models
{
    public class AnimalSearchRequest
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        private string? _search;
        private string? _name;
        private string? _species;
        
        public string? Species
        {
            get => _species?.ToLower();
            set => _species = value;
        }
        public Classification? Classification { get; set; }
        public int? Age { get; set; }
        public string? Name
        {
            get => _name?.ToLower();
            set => _name = value;
        }
        public DateTime? DateAcquired { get; set; }
        public OrderBy OrderBy { get; set; }
    }
}
