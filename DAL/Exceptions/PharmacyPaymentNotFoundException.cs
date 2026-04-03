namespace DAL.Exceptions
{
    public class PharmacyPaymentNotFoundException(Guid id) : NotFoundException($"PharmacyPayment with this id {id} is not found") { }
}
