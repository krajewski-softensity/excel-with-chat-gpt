using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using CrmToRecruit.Domain;
using CrmToRecruit.Services;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;
using System.Net.Mime;
using System.Globalization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Composition;
using System.Drawing;
using System.Net;
using System.Runtime.Serialization;

namespace CrmToRecruit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExcelDataController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly IService _service;

        public ExcelDataController(MyDbContext context, IService service)
        {
            _context = context;
            _service = service;
        }

        // GET: api/ExcelData
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExcelData>>> GetExcelData()
        {
            return await _context.ExcelData.ToListAsync();
        }

        // GET: api/ExcelData/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExcelData>> GetExcelData(int id)
        {
            var excelData = await _context.ExcelData.FindAsync(id);

            if (excelData == null)
            {
                return NotFound();
            }

            return excelData;
        }

        // PUT: api/ExcelData/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExcelData(int id, ExcelData excelData)
        {
            if (id != excelData.Id)
            {
                return BadRequest();
            }

            _context.Entry(excelData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExcelDataExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ExcelData
        [HttpPost]
        public async Task<ActionResult<ExcelData>> PostExcelData(ExcelData excelData)
        {
            _context.ExcelData.Add(excelData);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExcelData", new { id = excelData.Id }, excelData);
        }



        // DELETE: api/ExcelData/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExcelData(int id)
        {
            var excelData = await _context.ExcelData.FindAsync(id);
            if (excelData == null)
            {
                return NotFound();
            }

            _context.ExcelData.Remove(excelData);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("uploadwithmapping")]
        public async Task<IActionResult> ReadExcelFile(IFormFile file)
        {
            if (file == null || file.Length <= 0)
            {
                return BadRequest("Invalid file");
            }

            if (Path.GetExtension(file.FileName) != ".xlsx")
            {
                return BadRequest("Invalid file format");
            }

            var stream = file.OpenReadStream();
            var crmToRecruitList = await _service.ReadExcelFileCrmToRecruit(stream);

            return Ok(crmToRecruitList);
        }

        [HttpPost("uploadcloseddeals")]
        public async Task<IActionResult> ReadExcelFileClosedDeals(IFormFile file)
        {
            if (file == null || file.Length <= 0)
            {
                return BadRequest("Invalid file");
            }

            if (Path.GetExtension(file.FileName) != ".xlsx")
            {
                return BadRequest("Invalid file format");
            }

            var stream = file.OpenReadStream();
            var response = await _service.ReadExcelFileClosedDeals(stream);

            return Ok(response);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> PostExcelData(IFormFile file)
        {
            using (var package = new ExcelPackage(file.OpenReadStream()))
            {
                var worksheet = package.Workbook.Worksheets[0];

                // Loop through the rows in the worksheet and save to the database
                for (int row = 2; row <= worksheet.Dimension.Rows; row++)
                {
                    var recordId = worksheet.Cells[row, 1].Value?.ToString();
                    var accountName = worksheet.Cells[row, 2].Value?.ToString();
                    var dealName = worksheet.Cells[row, 3].Value?.ToString();
                    var dealOwner = worksheet.Cells[row, 4].Value?.ToString();
                    var numberOfResources = int.Parse(worksheet.Cells[row, 5].Value?.ToString() ?? "0");
                    var stageOfOpen = worksheet.Cells[row, 6].Value?.ToString();
                    var stageOfClosed = worksheet.Cells[row, 7].Value?.ToString();
                    var jobOpeningCreationDate = DateTime.FromOADate(double.Parse(worksheet.Cells[row, 8].Value.ToString()));
                    var lastActivityTime = DateTime.FromOADate(double.Parse(worksheet.Cells[row, 9].Value.ToString()));
                    var mustHaveSkills = worksheet.Cells[row, 10].Value?.ToString();
                    var niceToHaveSkills = worksheet.Cells[row, 11].Value?.ToString();
                    var projectDescription = worksheet.Cells[row, 12].Value?.ToString();
                    var rmOwnership = worksheet.Cells[row, 13].Value?.ToString();
                    var notesPriority = worksheet.Cells[row, 14].Value?.ToString();
                    var submittedST = worksheet.Cells[row, 15].Value?.ToString();
                    var submittedVendor = worksheet.Cells[row, 16].Value?.ToString();
                    var interviewedST = worksheet.Cells[row, 17].Value?.ToString();
                    var interviewedVendor = worksheet.Cells[row, 18].Value?.ToString();
                    var confirmedST = worksheet.Cells[row, 19].Value?.ToString();
                    var confirmedVendor = worksheet.Cells[row, 20].Value?.ToString();
                    var rejectedST = worksheet.Cells[row, 21].Value?.ToString();
                    var rejectedVendor = worksheet.Cells[row, 22].Value?.ToString();

                    var item = new ExcelData
                    {
                        RecordId = recordId ?? "",
                        AccountName = accountName ?? "",
                        DealName = dealName ?? "",
                        DealOwner = dealOwner ?? "",
                        NumberOfResources = numberOfResources,
                        StageOfOpen = stageOfOpen ?? "",
                        StageOfClosed = stageOfClosed ?? "",
                        JobOpeningCreationDate = jobOpeningCreationDate,
                        LastActivityTime = lastActivityTime,
                        MustHaveSkills = mustHaveSkills ?? "",
                        NiceToHaveSkills = niceToHaveSkills ?? "",
                        ProjectDescription = projectDescription ?? "",
                        RMOwnership = rmOwnership,
                        NotesPriority = notesPriority ?? "",
                        SubmittedST = submittedST ?? "",
                        SubmittedVendor = submittedVendor ?? "",
                        InterviewedST = interviewedST,
                        InterviewedVendor = interviewedVendor,
                        ConfirmedST = confirmedST,
                        ConfirmedVendor = confirmedVendor,
                        RejectedST = rejectedST,
                        RejectedVendor = rejectedVendor
                    };

                    _context.ExcelData.Add(item);
                }

                await _context.SaveChangesAsync();
            }

            return Ok();
        }

        private bool ExcelDataExists(int id)
        {
            return _context.ExcelData.Any(e => e.Id == id);
        }

        [HttpGet("downloadreport/vertical")]
        public async Task<IActionResult> DownloadClosedDealsReportAsync()
        {
            // Get the Closed Deals report data from the repository
            var reportData = await _service.GenerateMonthlyReport();

            // Create the Excel package
            using (var package = new ExcelPackage())
            {
                // Add a new worksheet to the Excel package
                var worksheet = package.Workbook.Worksheets.Add("Closed Deals Report");

                // Write the headers to the worksheet
                worksheet.Cells[1, 1].Value = "Month/Year";
                worksheet.Cells[1, 2].Value = "Closed (Won)";
                worksheet.Cells[1, 3].Value = "Closed (Lost)";

                // Write the report data to the worksheet
                var row = 2;
                foreach (var data in reportData)
                {
                    worksheet.Cells[row, 1].Value = data.MonthYear;
                    worksheet.Cells[row, 2].Value = data.ClosedWonCount;
                    worksheet.Cells[row, 3].Value = data.ClosedLostCount;
                    row++;
                }

                // Set the column widths for the worksheet
                worksheet.Column(1).Width = 15;
                worksheet.Column(2).Width = 15;
                worksheet.Column(3).Width = 15;

                // Set the content type and file name for the response
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                var fileName = "ClosedDealsReport.xlsx";

                // Convert the Excel package to a byte array
                var fileBytes = package.GetAsByteArray();

                // Return the Excel file as a file download
                return File(fileBytes, contentType, fileName);
            }
        }

        [HttpGet("downloadreport/horizontal2")]
        public IActionResult DownloadClosedDealsReport2()
        {
            var reportData = _service.GenerateMonthlyReport().Result;

            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Dashboard");

            // Add header row
            worksheet.Cells[1, 1].Value = "Closed roles by Month";
            for (var i = 0; i < 12; i++)
            {
                var date = new DateTime(2023, i + 1, 1);
                worksheet.Cells[1, i + 2].Value = date.ToString("MMM. yy", CultureInfo.InvariantCulture);
            }
            worksheet.Cells[1, 14].Value = "Totals 23'";

            // Add Closed (Won) row
            worksheet.Cells[2, 1].Value = "Closed (Won)";
            for (var i = 0; i < 12; i++)
            {
                var date = new DateTime(2023, i + 1, 1);
                var value = reportData.FirstOrDefault(x => x.MonthYear == date.ToString("MMM. yy"));
                if (value != null)
                {
                    worksheet.Cells[2, i + 2].Value = value.ClosedWonCount;
                }
            }
            worksheet.Cells[2, 14].Formula = "SUM(B2:M2)";

            // Add Closed (Lost) row
            worksheet.Cells[3, 1].Value = "Closed (Lost)";
            for (var i = 0; i < 12; i++)
            {
                var date = new DateTime(2023, i + 1, 1);
                var value = reportData.FirstOrDefault(x => x.MonthYear == date.ToString("MMM. yy"));
                if (value != null)
                {
                    worksheet.Cells[3, i + 2].Value = value.ClosedLostCount;
                }
            }
            worksheet.Cells[3, 14].Formula = "SUM(B3:M3)";

            // Set header row and Closed (Won) row as bold
            worksheet.Cells[1, 1, 1, 14].Style.Font.Bold = true;

            // Add a table to the worksheet and format it
            var tableRange = worksheet.Cells[1, 1, 3, 14];
            var table = worksheet.Tables.Add(tableRange, "ClosedDealsReport");
            table.TableStyle = TableStyles.Medium2;

            //---------------------------------------------------------------------------------

            var lossReasonCounts = _service.GetClosedDealsLossReasons();

            // Add the second table with loss reasons data
            var lossReasonTopHeader = new[] { "Closed (Lost) - details" };
            worksheet.Cells["O1"].Value = lossReasonTopHeader;
            worksheet.Cells["O1"].Style.Font.Bold = true;
            worksheet.Cells["O1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Cells["O1:U1"].Merge = true;

            var lossReasonsHeader = new[] { "Canceled", "Competitor", "Cost", "Couldn’t find", "Diff direction", "Filled by client" };
            var lossReasonsValues = lossReasonCounts.Result.Select(x => x.ToString()).ToArray();
            var lossReasonsRange = worksheet.Cells["O2"].LoadFromArrays( new[] { lossReasonsHeader }.Concat(new[] { lossReasonsValues }));
           
            
            var lossReasonsTable = worksheet.Tables.Add(lossReasonsRange, "ClosedDealsLossReasonsTable");
            lossReasonsTable.TableStyle = TableStyles.Light16;
            lossReasonsTable.TableBorderStyle.BorderAround(ExcelBorderStyle.Dashed);

            // Auto-fit columns
            worksheet.Cells.AutoFitColumns();

            //---------------------------------------------------------------------------------

            CultureInfo ci = CultureInfo.CurrentCulture;
            Calendar cal = ci.Calendar;
            var currentWeekNumber = cal.GetWeekOfYear(DateTime.Now, ci.DateTimeFormat.CalendarWeekRule, ci.DateTimeFormat.FirstDayOfWeek);

            var weeks = Enumerable.Range(1, currentWeekNumber).ToList();
            var weekSheets = new List<(int weekNumber, ExcelWorksheet worksheet)>();

            foreach (var weekNumber in weeks)
            {
                var openDeals = _service.GetOpenDealsByWeek(weekNumber).Result;

                if (openDeals.Count > 0)
                {
                    var worksheetPerWeek = package.Workbook.Worksheets.Add(GetTabNameByWeek(weekNumber));
                    weekSheets.Add((weekNumber, worksheetPerWeek));

                    // Set header row
                    var headers = new string[] { "Record ID","Account Name","Deal Name","Deal Owner","QTY","Stage","Job Opening Date","Aging","RM Ownership" };
                    var headerRange = worksheetPerWeek.Cells["A1:G1"];
                    //headerRange.Merge = true;
                    headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    headerRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    headerRange.Style.Font.Bold = true;
                    headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    headerRange.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    headerRange.Style.Border.BorderAround(ExcelBorderStyle.Thin);

                    headerRange.LoadFromArrays(new[] { headers });

                    // Load data rows
                    var data = openDeals.Select(c => new object[] {
                        c.RecordId,
                        c.AccountName,
                        c.DealName,
                        c.DealOwner,
                        c.StageOfOpen,
                        c.NumberOfResources?.ToString(),
                        c.JobOpeningCreationDate.Value.ToLongDateString(),
                        GetAgeByDateTimeAndWeek(c.JobOpeningCreationDate.Value, weekNumber),
                        c.RmOwnership
                    }).ToList();

                    var dataRange = worksheetPerWeek.Cells["A2:G" + (data.Count + 1)];
                    dataRange.LoadFromArrays(data);

                    // Apply borders to data rows
                    dataRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    dataRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    dataRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    dataRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                    // Auto-fit columns
                    worksheetPerWeek.Cells.AutoFitColumns();
                }
            }

            // Move week sheets to the left of the Dashboard sheet
            var dashboardSheet = package.Workbook.Worksheets["Dashboard"];
            foreach (var weekSheet in weekSheets)
            {
                //weekSheet.worksheet.Hidden = eWorkSheetHidden.VeryHidden;
                //package.Workbook.Worksheets.MoveBefore(weekSheet.weekNumber.ToString(), dashboardSheet.Name);
            }

            //---------------------------------------------------------------------------------

            // Convert package to byte array
            var fileBytes = package.GetAsByteArray();

            // Download file
            var contentDisposition = new ContentDisposition
            {
                FileName = "ClosedDealsReport.xlsx",
                Inline = false
            };

            Response.Headers.Add("Content-Disposition", contentDisposition.ToString());
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        private int GetAgeByDateTimeAndWeek(DateTime dateTime, int weekNumber)
        {
            DayOfWeek firstDayOfWeek = DayOfWeek.Monday; // Assuming Monday is the first day of the week

            DateTime jan1 = new DateTime(2023, 1, 1);
            int daysOffset = (int)firstDayOfWeek - (int)jan1.DayOfWeek;

            DateTime firstWeekDay = jan1.AddDays(daysOffset + (weekNumber - 1) * 7);

            TimeSpan diff = firstWeekDay - dateTime;
            return diff.Days;
        }

        private string GetTabNameByWeek(int weekNumber)
        {
            DateTime jan1 = new DateTime(DateTime.Now.Year, 1, 1);
            int daysOffset = (int)DayOfWeek.Monday - (int)jan1.DayOfWeek;

            DateTime firstWeekDay = jan1.AddDays(daysOffset + (weekNumber - 1) * 7);
            DateTime lastWeekDay = firstWeekDay.AddDays(6);

            return "WE " + lastWeekDay.ToString("MM.dd");
        }

        [HttpGet("cdownloadreport/horizontal")]
        public async Task<IActionResult> DownloadClosedDealsReport()
        {
            var reportData = await _service.GenerateMonthlyReport();

            // Transpose the report data
            var transposedData = new List<object[]>();

            foreach (var data in reportData)
            {
                if (transposedData.Count == 0)
                {
                    transposedData.Add(new object[] { "", "Closed (Won)", "Closed (Lost)" });
                }

                var monthYear = data.MonthYear;
                var closedWonCount = data.ClosedWonCount;
                var closedLostCount = data.ClosedLostCount;

                transposedData.Add(new object[] { monthYear, closedWonCount, closedLostCount });
            }

            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Closed Deals Report");

            // Write the transposed data to the worksheet
            for (var row = 1; row <= transposedData.Count; row++)
            {
                for (var col = 1; col <= transposedData[row - 1].Length; col++)
                {
                    worksheet.Cells[row, col].Value = transposedData[row - 1][col - 1];
                }
            }

            // Set the column widths
            worksheet.Column(1).Width = 20;
            worksheet.Column(2).Width = 15;
            worksheet.Column(3).Width = 15;

            // Set the header styles
            var headerRange = worksheet.Cells[1, 1, 1, 3];
            headerRange.Style.Font.Bold = true;
            headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            // Set the number format for the count columns
            var countRange = worksheet.Cells[2, 2, transposedData.Count, 3];
            countRange.Style.Numberformat.Format = "#,##0";

            // Return the Excel file as a byte array
            var fileContents = package.GetAsByteArray();
            var fileName = "ClosedDealsReport.xlsx";

            return File(fileContents, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

    }
}
