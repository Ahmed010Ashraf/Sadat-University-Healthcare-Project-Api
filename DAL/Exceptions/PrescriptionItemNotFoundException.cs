namespace DAL.Exceptions
{
    public class PrescriptionItemNotFoundException(Guid id) : NotFoundException($"Prescription item with id {id} was not found.")
    {
    }
}
