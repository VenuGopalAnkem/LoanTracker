using iText.Kernel.Pdf;
using iText.Layout.Borders;
using iText.StyledXmlParser.Jsoup.Select;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Reporting.Map.WebForms.BingMaps;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;
using System.Collections.Generic;
using System.Security.Cryptography.Xml;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System;
using iText.Commons.Utils;
using iText.StyledXmlParser.Jsoup;
using System.Diagnostics.Metrics;
using System.Drawing;

namespace LoanTracker.Models
{
    public class tablenandprocedures
    {

//        CREATE TABLE Borrowers
//(
//    Id INT IDENTITY(1,1) PRIMARY KEY,

//    FullName NVARCHAR(150),

//    DOB DATE,

//    MobileNumber NVARCHAR(20),

//    Email NVARCHAR(100),

//    LocalAddress NVARCHAR(300),

//    PermanentAddress NVARCHAR(300),

//    MaritalStatus NVARCHAR(50),

//    GuardianName NVARCHAR(150),

//    AadharNumber NVARCHAR(20),

//    PANNumber NVARCHAR(20),

//    CreatedDate DATETIME DEFAULT GETDATE()
//)

//--------------------------------------------------
//CREATE TABLE Loans
//(
//    Id INT IDENTITY(1,1) PRIMARY KEY,

//    BorrowerId INT,

//    LoanAmount DECIMAL(18,2),

//    CommissionPercent DECIMAL(5,2),

//    CommissionAmount DECIMAL(18,2),

//    FinalLoanGiven DECIMAL(18,2),

//    LoanPeriod INT,

//    DailyPay DECIMAL(18,2),

//    LoanPurpose NVARCHAR(200),

//    LoanDate DATETIME DEFAULT GETDATE(),

//    Status NVARCHAR(50) DEFAULT 'Active',

//    FOREIGN KEY(BorrowerId)
//    REFERENCES Borrowers(Id)
//)

//-------------------------------------------------
//CREATE TABLE Payments
//(
//    Id INT IDENTITY(1,1) PRIMARY KEY,

//    LoanId INT,

//    PaymentDate DATETIME DEFAULT GETDATE(),

//    AmountPaid DECIMAL(18,2),

//    FOREIGN KEY(LoanId)
//    REFERENCES Loans(Id)
//)
//---------------------------------------------------
//CREATE TABLE Receipts
//(
//    Id INT IDENTITY(1,1) PRIMARY KEY,

//    LoanId INT,

//    ReceiptNumber NVARCHAR(50),

//    PaymentId INT,

//    CreatedDate DATETIME DEFAULT GETDATE()
//)
//------------------------------------------------
//CREATE PROCEDURE sp_AddBorrower
//(
//@FullName NVARCHAR(150),
//@DOB DATE,
//@MobileNumber NVARCHAR(20),
//@Email NVARCHAR(100),
//@LocalAddress NVARCHAR(300),
//@PermanentAddress NVARCHAR(300),
//@MaritalStatus NVARCHAR(50),
//@GuardianName NVARCHAR(150),
//@Aadhar NVARCHAR(20),
//@PAN NVARCHAR(20)
//)
//AS
//BEGIN

//INSERT INTO Borrowers
//(
//FullName,
//DOB,
//MobileNumber,
//Email,
//LocalAddress,
//PermanentAddress,
//MaritalStatus,
//GuardianName,
//AadharNumber,
//PANNumber
//)

//VALUES
//(
//@FullName,
//@DOB,
//@MobileNumber,
//@Email,
//@LocalAddress,
//@PermanentAddress,
//@MaritalStatus,
//@GuardianName,
//@Aadhar,
//@PAN
//)

//END
//-----------------------------------------------------
//CREATE PROCEDURE sp_GetBorrowers
//AS
//BEGIN

//SELECT*
//FROM Borrowers
//ORDER BY Id DESC

//END
//---------------------------------------------------
//CREATE PROCEDURE sp_AddLoan
//(
//@BorrowerId INT,
//@LoanAmount DECIMAL(18,2),
//@CommissionPercent DECIMAL(5,2),
//@LoanPeriod INT,
//@LoanPurpose NVARCHAR(200)
//)
//AS
//BEGIN

//DECLARE @CommissionAmount DECIMAL(18,2)

//DECLARE @FinalLoan DECIMAL(18,2)

//DECLARE @DailyPay DECIMAL(18,2)

//SET @CommissionAmount = (@LoanAmount * @CommissionPercent) / 100

//SET @FinalLoan = @LoanAmount - @CommissionAmount

//SET @DailyPay = @LoanAmount / @LoanPeriod

//INSERT INTO Loans
//(
//BorrowerId,
//LoanAmount,
//CommissionPercent,
//CommissionAmount,
//FinalLoanGiven,
//LoanPeriod,
//DailyPay,
//LoanPurpose
//)

//VALUES
//(
//@BorrowerId,
//@LoanAmount,
//@CommissionPercent,
//@CommissionAmount,
//        @FinalLoan,
//@LoanPeriod,
//@DailyPay,
//@LoanPurpose
//)

//END
//-------------------------------------------------
//Alter PROCEDURE sp_GetLoans
//AS
//BEGIN

//SELECT
//L.Id,
//B.FullName,
//L.LoanAmount,
//L.DailyPay,
//L.Status,

//ISNULL(SUM(P.AmountPaid),0) AS TotalPaid,

//L.LoanAmount - ISNULL(SUM(P.AmountPaid),0) AS Balance

//FROM Loans L

//INNER JOIN Borrowers B
//ON B.Id = L.BorrowerId

//LEFT JOIN Payments P
//ON P.LoanId = L.Id

//where L.Status='Active'

//GROUP BY
//L.Id,
//B.FullName,
//L.LoanAmount,
//L.DailyPay,
//L.Status

//END


//--select* from Borrowers

//--select* from Loans


//--select* from Payments

//--update Loans set Status = 'Completed' where id = 1

//------------------------------------------------------
//CREATE PROCEDURE sp_DailyCollection
//AS
//BEGIN

//SELECT
//L.Id AS LoanId,

//B.FullName,

//L.LoanAmount,

//L.DailyPay,

//ISNULL(SUM(P.AmountPaid),0) AS TotalPaid,

//L.LoanAmount - ISNULL(SUM(P.AmountPaid),0) AS Balance

//FROM Loans L

//INNER JOIN Borrowers B
//ON B.Id = L.BorrowerId

//LEFT JOIN Payments P
//ON P.LoanId = L.Id

//WHERE L.Status='Active'

//GROUP BY
//L.Id,
//B.FullName,
//L.LoanAmount,
//L.DailyPay

//END
//---------------------------------------------------
//create PROCEDURE sp_AddPayment
//(
//    @LoanId INT,
//    @Amount DECIMAL(18,2)
//)
//AS
//BEGIN
//    DECLARE @Totalloan DECIMAL(18,2);
//        DECLARE @Totalpaid DECIMAL(18,2);

//    -- Insert payment
//    INSERT INTO Payments(LoanId, AmountPaid)
//    VALUES(@LoanId, @Amount);

//    -- Get total loan amount
//    SELECT @Totalloan = LoanAmount
//    FROM Loans
//    WHERE Id = @LoanId;

//    -- Get total paid so far
//    SELECT @Totalpaid = SUM(AmountPaid)
//    FROM Payments
//    WHERE LoanId = @LoanId;

//    -- If fully paid, update loan status
//    IF @Totalloan = @Totalpaid
//    BEGIN
//        UPDATE Loans
//        SET Status = 'Completed'
//        WHERE Id = @LoanId;
//        END
//    END;

//-----------------------------------------------

//CREATE PROCEDURE sp_GenerateReceiptNo
//AS
//BEGIN

//DECLARE @NextId INT

//SELECT @NextId = ISNULL(MAX(Id), 0) + 1
//FROM Receipts

//SELECT 'REC-' + RIGHT('0000'+CAST(@NextId AS VARCHAR),4) AS ReceiptNo

//END

//----------------------------------------------

//CREATE PROCEDURE sp_GetReceipt
//@PaymentId INT
//AS
//BEGIN

//SELECT
//    p.PaymentId,
//    b.Name AS BorrowerName,
//    l.LoanAmount,
//    p.AmountPaid,
//    p.PaymentDate
//FROM Payments p
//JOIN Loans l ON p.LoanId = l.LoanId
//JOIN Borrowers b ON l.BorrowerId = b.BorrowerId
//WHERE p.PaymentId = @PaymentId

//END

//------------------------------------------------------

//CREATE PROCEDURE sp_PaymentHistory
//(
//@LoanId INT
//)
//AS
//BEGIN

//SELECT
//PaymentDate,
//AmountPaid
//FROM Payments
//WHERE LoanId= @LoanId
//ORDER BY PaymentDate

//END
//--------------------------------------

    }
}
