namespace LoanTracker.Models
{
    public class Borrower
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public DateTime DOB { get; set; }

        public string MobileNumber { get; set; }

        public string Email { get; set; }

        public string LocalAddress { get; set; }

        public string PermanentAddress { get; set; }

        public string MaritalStatus { get; set; }

        public string GuardianName { get; set; }

        public string AadharNumber { get; set; }

        public string PANNumber { get; set; }

    }
}
