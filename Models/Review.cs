using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models {
    public class Review {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public Movie? Movie { get; set; }
        public string? Username { get; set; }
        [DataType(DataType.Date)]
        public DateTime PostDate { get; set; }
        public string? Content { get; set; }
    }
}