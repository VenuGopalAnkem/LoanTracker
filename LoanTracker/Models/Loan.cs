namespace LoanTracker.Models
{
    public class Loan
    {
        public int Id { get; set; }

        public int BorrowerId { get; set; }

        public decimal LoanAmount { get; set; }

        public decimal CommissionPercent { get; set; }

        public decimal CommissionAmount { get; set; }

        public decimal FinalLoanGiven { get; set; }

        public int LoanPeriod { get; set; }

        public decimal DailyPay { get; set; }

        public string LoanPurpose { get; set; }

        public string Status { get; set; }
    }
}
