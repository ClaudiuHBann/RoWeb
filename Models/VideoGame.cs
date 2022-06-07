using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models {
    public class VideoGame {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public byte Rating { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string? Genre { get; set; }
        public decimal Price { get; set; }
    }
}
