using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CrmToRecruit;
using OfficeOpenXml;

namespace CrmToRecruit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExcelDataController : ControllerBase
    {
        private readonly MyDbContext _context;

        public ExcelDataController(MyDbContext context)
        {
            _context = context;
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
                        DealOwner = dealOwner?? "",
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
    }

}
