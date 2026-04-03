namespace DAL.Exceptions
{

    public class PrescriptionDispenseNotFoundException(Guid id) : NotFoundException($"Prescription dispense with id {id} was not found.") { }

}
