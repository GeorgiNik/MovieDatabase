namespace MovieDatabase.Services.Contracts
{
    using MovieDatabase.Data.Models;    

    public interface IMovieService : ICrudService<Movie>
    {
    }
}