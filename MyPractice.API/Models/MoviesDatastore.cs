namespace MyPractice.API.Models
{
    public class MoviesDatastore
    {
        public List<MoviesDto> MoviesList { get; set; } = new List<MoviesDto>();

        private static readonly Lazy<MoviesDatastore> _instance = new Lazy<MoviesDatastore>(() => new MoviesDatastore());
        public static MoviesDatastore Current => _instance.Value;

        private MoviesDatastore()
        {
            MoviesList = new List<MoviesDto>
            {
                new MoviesDto{Id = 1, Name = "breakfast club", Year = 1985,
                    Actors = new List<ActorDto>{
                        new() {Id=1, Name="christian" },
                        new() {Id=2, Name="sarah" },
                    }},
                new MoviesDto{Id = 2, Name = "Terminator", Year = 1984,
                    Actors = new List<ActorDto>{
                        new() {Id=3, Name="stanley" },
                        new() {Id=4, Name="sai" },
                    }},
                new MoviesDto{Id = 3, Name = "little miss sunshine", Year = 2006}
            };
        }
    }
}
