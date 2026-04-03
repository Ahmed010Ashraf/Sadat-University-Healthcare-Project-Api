namespace DAL.Exceptions
{
    public class PharmacyNotFoundException(Guid id) : NotFoundException($"Pharmacy with id {id} was not found.") { }

}
