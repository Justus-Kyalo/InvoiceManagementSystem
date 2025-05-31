using System.Text;
using InvoiceManagementSystemAPI.Models;
using InvoiceManagementSystemAPI.Services.IServices;

namespace InvoiceManagementSystemAPI.Services;

public class IIFGeneratorService:IIIFGeneratorService
{
    public string GenerateIIFContent(List<SlipDetail> slips)
    {
        var sb = new StringBuilder();

        // IIF Headers
        sb.AppendLine("!TRNS\tTRNSTYPE\tDATE\tACCNT\tNAME\tAMOUNT\tDOCNUM\tMEMO");
        sb.AppendLine("!SPL\tTRNSTYPE\tDATE\tACCNT\tNAME\tAMOUNT\tINVITEM\tQNTY\tDESCRIPTION");
        sb.AppendLine("!ENDTRNS");

        // Group by AccountNumber
        var groupedByCustomer = slips
            .GroupBy(s => s.AccountNumber)
            .ToList();

        foreach (var group in groupedByCustomer)
        {
            var firstSlip = group.First();

            // TRNS Line (only once per AccountNumber)
            sb.AppendLine(
                $"TRNS\t" +
                $"INVOICE\t" +
                $"{DateTime.Today:MM/dd/yyyy}\t" +
                $"Accounts Receivable\t" +
                $"{firstSlip.AccountNumber}  {firstSlip.Name}\t" +
                $"\t" + 
                $"\t" + 
                $"Consolidated invoice as of {DateTime.Today:MM/dd/yyyy}"
            );

            foreach (var slip in group)
            {
                sb.AppendLine(
                    $"SPL\t" +
                    $"INVOICE\t" +
                    $"{slip.SlipDate:MM/dd/yyyy}\t" +
                    $"Sales Income\t" +
                    $"{slip.Name}\t" +
                    $"-{slip.Price}\t" +
                    $"{slip.ItemName}\t" +
                    $"{slip.Quantity}\t" +
                    $"Slip#{slip.SlipNumber}"
                );
            }

            sb.AppendLine("ENDTRNS\n");
        }

        return sb.ToString();
    }
    }
