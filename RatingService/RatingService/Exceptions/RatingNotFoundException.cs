namespace RatingService.Exceptions
{
    public sealed class RatingNotFoundException : NotFoundException
    {
        public RatingNotFoundException(int id) : base($"The rating with the identifier {id} was not found.")
        {
        }
    }
}
