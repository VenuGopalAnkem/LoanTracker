using Microsoft.AspNetCore.Mvc;
using Microsoft.Reporting.NETCore;
using LoanTracker.Services;
using System.Data;

namespace LoanTracker.Controllers
{
    public class ReceiptController : Controller
    {
        private readonly LoanService service;
        private readonly IWebHostEnvironment env;

        public ReceiptController(LoanService loanService, IWebHostEnvironment environment)
        {
            service = loanService;
            env = environment;
        }

        public IActionResult ViewReceipt(int LoanId)
        {
            DataTable dt = service.GetReceipt(LoanId);

            string path = Path.Combine(env.WebRootPath, "Reports", "PaymentReceipts.rdlc");

            LocalReport report = new LocalReport();
            report.ReportPath = path;

            report.DataSources.Add(new ReportDataSource("ReceiptDataSet", dt));

            byte[] pdf = report.Render("PDF");

            return File(pdf, "application/pdf");
        }
    }
}