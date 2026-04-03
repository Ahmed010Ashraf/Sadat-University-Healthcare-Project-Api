namespace DAL.Exceptions
{

    public class PrescriptionDispenseItemNotFoundException(Guid id)
        : NotFoundException($"Prescription dispense item with id {id} was not found.")
    { }
}
