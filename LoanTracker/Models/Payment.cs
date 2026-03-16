namespace LoanTracker.Models
{
    public class Payment
    {
        public int Id { get; set; }

        public int LoanId { get; set; }

        public DateTime PaymentDate { get; set; }

        public decimal AmountPaid { get; set; }
    }
}
