using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using CrmToRecruit.Domain;
using Newtonsoft.Json;
using CrmToRecruit.Repositories;

namespace CrmToRecruit.Services
{
    public class Service : IService
    {
        private readonly IRepository _repository;
        public Service(IRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<CrmToRecruitDto>> ReadExcelFileCrmToRecruit(Stream stream)
        {
            var crmToRecruitList = new List<CrmToRecruitDto>();

            using var package = new ExcelPackage(stream);

            var worksheet = package.Workbook.Worksheets.FirstOrDefault();
            if (worksheet == null)
            {
                throw new Exception("Excel file has no worksheets");
            }

            var mappingJson = await File.ReadAllTextAsync("mapping_file1.json");
            var mappings = JsonConvert.DeserializeObject<List<ExcelColumnMapping>>(mappingJson);

            for (var row = 2; row <= worksheet.Dimension.End.Row; row++)
            {
                var crmToRecruit = new CrmToRecruitDto();

                foreach (var mapping in mappings)
                {
                    var columnIndex = worksheet.Cells["1:1"]
                                        .First(c => c.Value.ToString() == mapping.ExcelColumnName).Start.Column;

                    var cellValue = worksheet.Cells[row, columnIndex].Value;

                    switch (mapping.DataType.ToLower())
                    {
                        case "string":
                            crmToRecruit.GetType().GetProperty(mapping.PropertyName)?.SetValue(crmToRecruit, cellValue?.ToString());
                            break;
                        case "int":
                            if (int.TryParse(cellValue?.ToString(), out var intValue))
                            {
                                crmToRecruit.GetType().GetProperty(mapping.PropertyName)?.SetValue(crmToRecruit, intValue);
                            }
                            break;
                        case "datetime":
                            if (double.TryParse(cellValue?.ToString(), out var doubleValue))
                            {
                                var finalDate = DateTime.FromOADate(doubleValue);
                                crmToRecruit.GetType().GetProperty(mapping.PropertyName)?.SetValue(crmToRecruit, finalDate);
                            }
                            break;
                    }
                }

                crmToRecruitList.Add(crmToRecruit);
            }

            await _repository.SaveCrmToRecruitList(crmToRecruitList);
            return crmToRecruitList;
        }

        public async Task<List<ClosedDealsDto>> ReadExcelFileClosedDeals(Stream stream)
        {
            var dealsList = new List<ClosedDealsDto>();

            using var package = new ExcelPackage(stream);

            var worksheet = package.Workbook.Worksheets.FirstOrDefault();
            if (worksheet == null)
            {
                throw new Exception("Excel file has no worksheets");
            }

            var mappingJson = await File.ReadAllTextAsync("mapping_file2.json");
            var mappings = JsonConvert.DeserializeObject<List<ExcelColumnMapping>>(mappingJson);

            for (var row = 2; row <= worksheet.Dimension.End.Row; row++)
            {
                var deal = new ClosedDealsDto();

                foreach (var mapping in mappings)
                {
                    var columnIndex = worksheet.Cells["1:1"]
                                        .First(c => c.Value.ToString() == mapping.ExcelColumnName).Start.Column;

                    var cellValue = worksheet.Cells[row, columnIndex].Value;

                    switch (mapping.DataType.ToLower())
                    {
                        case "string":
                            deal.GetType().GetProperty(mapping.PropertyName)?.SetValue(deal, cellValue?.ToString());
                            break;
                        case "int":
                            if (int.TryParse(cellValue?.ToString(), out var intValue))
                            {
                                deal.GetType().GetProperty(mapping.PropertyName)?.SetValue(deal, intValue);
                            }
                            break;
                        case "datetime":
                            if (double.TryParse(cellValue?.ToString(), out var doubleValue))
                            {
                                var finalDate = DateTime.FromOADate(doubleValue);
                                deal.GetType().GetProperty(mapping.PropertyName)?.SetValue(deal, finalDate);
                            }
                            break;
                    }
                }

                dealsList.Add(deal);
            }

            await _repository.SaveClosedDealsList(dealsList);
            return dealsList;
        }

        public async Task<List<ClosedDealsReportDto>> GenerateMonthlyReport()
        {
            return await _repository.GenerateMonthlyReport();
        }

        public async Task<List<int>> GetClosedDealsLossReasons() 
        {
            return await _repository.GetClosedDealsLossReasons();
        }


        public async Task<List<CrmToRecruitExtendedEntity>> GetOpenDealsByWeek(int weekNumber)
        {
            return await _repository.GetOpenDealsByWeek(weekNumber);
        }

        public async Task<Dictionary<string, int>> GetCompaniesRecruitInfo()
        {
            return await _repository.GetCompaniesRecruitInfo();
        }
    }
}
