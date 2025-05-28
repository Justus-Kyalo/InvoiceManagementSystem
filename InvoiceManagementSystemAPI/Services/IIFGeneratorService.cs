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
        
        // TRNS Line (Accounts Receivable)
        sb.AppendLine(
            $"TRNS\t" +
            $"INVOICE\t" +                        
            $"{DateTime.Today:MM/dd/yyyy}\t" +      
            $"Accounts Receivable\t" +            
            $"{slips.FirstOrDefault()?.AccountNumber}  {slips.FirstOrDefault()?.Name}\t" +
            $"\t" + // blank AMOUNT
            $"Consolidated invoice as of {DateTime.Today:MM/dd/yyyy}\t"
        );

        foreach (var slip in slips)
        {
            // SPL Line (Sales Account)
            sb.AppendLine(
                $"SPL\t" +
                $"INVOICE\t" +                        
                $"{slip.SlipDate:MM/dd/yyyy}\t" +      
                $"Sales Income\t" +                          
                $"{slip.Name}\t" + 
                $"-{slip.Price}\t" +                
                $"{slip.ItemName}\t" + 
                $"{slip.Quantity}\t"  +
                $"Slip#{slip.SlipNumber}"
            );
        }
        sb.AppendLine("!ENDTRNS");

        return sb.ToString();
    }
    }
