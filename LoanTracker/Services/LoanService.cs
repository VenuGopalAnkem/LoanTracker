using LoanTracker.Data;
using LoanTracker.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace LoanTracker.Services
{
    public class LoanService
    {

        private readonly DBHelper db;

        public LoanService(DBHelper helper)
        {
            db = helper;
        }

        // GET BORROWERS
        public DataTable GetBorrowers()
        {
            return db.GetData("sp_GetBorrowers");
        }

        // ADD BORROWER
        public void AddBorrower(Borrower model)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@FullName",model.FullName),
                new SqlParameter("@DOB",model.DOB),
                new SqlParameter("@MobileNumber",model.MobileNumber),
                new SqlParameter("@Email",model.Email),
                new SqlParameter("@LocalAddress",model.LocalAddress),
                new SqlParameter("@PermanentAddress",model.PermanentAddress),
                new SqlParameter("@MaritalStatus",model.MaritalStatus),
                new SqlParameter("@GuardianName",model.GuardianName),
                new SqlParameter("@Aadhar",model.AadharNumber),
                new SqlParameter("@PAN",model.PANNumber)
            };

            db.Execute("sp_AddBorrower", p);
        }

        // GET LOANS
        public DataTable GetLoans()
        {
            return db.GetData("sp_GetLoans");
        }

        // ADD LOAN
        public void AddLoan(int borrowerId, decimal loanAmount, decimal commissionPercent, int loanPeriod, string loanPurpose)
        {

            SqlParameter[] p =
            {
                new SqlParameter("@BorrowerId",borrowerId),
                new SqlParameter("@LoanAmount",loanAmount),
                new SqlParameter("@CommissionPercent",commissionPercent),
                new SqlParameter("@LoanPeriod",loanPeriod),
                new SqlParameter("@LoanPurpose",loanPurpose)
            };

            db.Execute("sp_AddLoan", p);

        }

        // DAILY COLLECTION
        public DataTable GetDailyCollection()
        {
            return db.GetData("sp_DailyCollection");
        }

        // ADD PAYMENT
        public void AddPayment(int loanId, decimal amount)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@LoanId",loanId),
                new SqlParameter("@Amount",amount)
            };

            db.Execute("sp_AddPayment", p);
        }
        public DataTable GetReceipt(int LoanId)
        {
            SqlParameter[] param =
            {
        new SqlParameter("@LoanId", LoanId)
    };

            return db.GetDataWithParameters("sp_GetReceipt", param);
        }

        public DataTable GetPaymentHistory(int loanId)
        {
            SqlParameter[] p =
            {
        new SqlParameter("@LoanId",loanId)
    };

            return db.GetDataWithParameters("sp_PaymentHistory", p);
        }

    }
}