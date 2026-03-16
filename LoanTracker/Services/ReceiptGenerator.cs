using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.IO.Image;
using iText.Layout.Properties;

namespace LoanTracker.Services
{
    public class ReceiptGenerator
    {

        public byte[] GenerateReceipt(
            string borrower,
            int loanId,
            decimal loanAmount,
            decimal paid,
            decimal balance,
            int receiptNo)
        {

            using (var stream = new MemoryStream())
            {

                PdfWriter writer = new PdfWriter(stream);

                PdfDocument pdf = new PdfDocument(writer);

                Document doc = new Document(pdf);

                // LOGO

                string logoPath = "wwwroot/images/logo.png";

                if (File.Exists(logoPath))
                {
                    ImageData imgData = ImageDataFactory.Create(logoPath);

                    Image logo = new Image(imgData)
                        .ScaleToFit(120, 60)
                        .SetHorizontalAlignment(HorizontalAlignment.CENTER);

                    doc.Add(logo);
                }

                // TITLE

                Paragraph title = new Paragraph("Loan Payment Receipt")
                .SetFontSize(18)
                .SetTextAlignment(TextAlignment.CENTER);

                doc.Add(title);

                doc.Add(new Paragraph(" "));

                // RECEIPT INFO

                doc.Add(new Paragraph("Receipt No : REC-" + receiptNo));
                doc.Add(new Paragraph("Date : " + DateTime.Now.ToString("dd-MM-yyyy")));

                doc.Add(new Paragraph(" "));

                // TABLE

                Table table = new Table(2);

                table.AddCell("Borrower Name");
                table.AddCell(borrower);

                table.AddCell("Loan ID");
                table.AddCell(loanId.ToString());

                table.AddCell("Loan Amount");
                table.AddCell("₹ " + loanAmount);

                table.AddCell("Payment Received");
                table.AddCell("₹ " + paid);

                table.AddCell("Remaining Balance");
                table.AddCell("₹ " + balance);

                doc.Add(table);

                doc.Add(new Paragraph(" "));

                doc.Add(new Paragraph("Thank you for your payment.")
                .SetTextAlignment(TextAlignment.CENTER));

                doc.Add(new Paragraph(" "));

                // SIGNATURE

                Paragraph sign = new Paragraph("Authorized Signature")
                    .SetTextAlignment(TextAlignment.RIGHT);

                doc.Add(sign);

                doc.Close();

                return stream.ToArray();
            }

        }

    }
}