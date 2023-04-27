namespace RatingService.Exceptions
{
    public sealed class SercivePlatformNotFoundException : NotFoundException
    {
        public SercivePlatformNotFoundException(int id) 
            : base($"The Service Platform with the identifier {id} was not found.")
        {

        }
    }
        
    
}
