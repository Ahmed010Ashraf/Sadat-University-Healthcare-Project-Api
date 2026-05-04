namespace DAL.Exceptions
{
    public class PrescriptionNotFoundException(Guid id) : NotFoundException($"Prescription with id {id} was not found.")
    {
    }
}
