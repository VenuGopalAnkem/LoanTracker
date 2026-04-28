using Microsoft.AspNetCore.Mvc;
using LoanTracker.Services;
using LoanTracker.Models;
using System.Data;
using Microsoft.Data.SqlClient;

namespace LoanTracker.Controllers
{
    public class LoanController : Controller
    {

        private readonly LoanService service;

        public LoanController(LoanService loanService)
        {
            service = loanService;
        }

        // DASHBOARD
        //public IActionResult Dashboard()
        //{
        //    return View();
        //}

        public IActionResult Dashboard()
        {
            var model = new DashboardModel();

            DataTable borrowers = service.GetBorrowers();
            DataTable loans = service.GetLoans();

            model.TotalBorrowers = borrowers.Rows.Count;
            model.TotalLoans = loans.Rows.Count;

            decimal pending = 0;

            foreach (DataRow row in loans.Rows)
            {
                pending += Convert.ToDecimal(row["Balance"]);
            }

            model.PendingBalance = pending;

            return View(model);
        }


        // BORROWERS LIST
        public IActionResult Borrowers()
        {
            DataTable data = service.GetBorrowers();
            return View(data);
        }

        // CREATE BORROWER PAGE
        public IActionResult CreateBorrower()
        {
            return View();
        }

        // SAVE BORROWER
        [HttpPost]
        public IActionResult SaveBorrower(Borrower model)
        {

            service.AddBorrower(model);

            return RedirectToAction("Borrowers");

        }

        // LOANS LIST
        public IActionResult Loans()
        {
            DataTable data = service.GetLoans();

            return View(data);
        }

        // CREATE LOAN PAGE
        public IActionResult CreateLoan()
        {

            ViewBag.Borrowers = service.GetBorrowers();

            return View();

        }

        // SAVE LOAN
        [HttpPost]
        public IActionResult SaveLoan(
            int BorrowerId,
            decimal LoanAmount,
            decimal CommissionPercent,
            int LoanPeriod,
            string LoanPurpose)
        {

            service.AddLoan(
                BorrowerId,
                LoanAmount,
                CommissionPercent,
                LoanPeriod,
                LoanPurpose
            );

            return RedirectToAction("Loans");

        }

        // DAILY COLLECTION
        public IActionResult Collection()
        {
        //}



            DataTable data = service.GetDailyCollection();

            return View(data);

        }

        // COLLECT PAYMENT
        [HttpPost]
        public IActionResult Collect(int LoanId, decimal Amount)
        {

            service.AddPayment(LoanId, Amount);

            return RedirectToAction("Collection");

        }

        public IActionResult PaymentHistory(int loanId)
        {
            var data = service.GetPaymentHistory(loanId);

            return PartialView("_PaymentHistoryModal", data);
        }



    }
}