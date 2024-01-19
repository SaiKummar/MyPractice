namespace MyPractice.API.Models
{
    public class MoviesDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Year { get; set; }
        public ICollection<ActorDto> Actors { get; set; } = new List<ActorDto>();
    }
}
