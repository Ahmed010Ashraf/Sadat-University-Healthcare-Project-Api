namespace DAL.Exceptions
{
    public class PrescriptionRequestNotFoundException(Guid id) : NotFoundException($"Prescription request with id {id} was not found.")
    {
    }
}
