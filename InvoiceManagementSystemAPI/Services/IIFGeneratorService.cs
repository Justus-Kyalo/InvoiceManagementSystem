using System.Text;
using InvoiceManagementSystemAPI.Models;
using InvoiceManagementSystemAPI.Services.IServices;

namespace InvoiceManagementSystemAPI.Services;

public class IIFGeneratorService:IIIFGeneratorService
{
    public string GenerateIIFContent(List<Slip> slips)
    {
        var sb = new StringBuilder();

        // IIF Headers
        sb.AppendLine("!TRNS\tTRNSID\tTRNSTYPE\tDATE\tACCNT\tNAME\tAMOUNT\tDOCNUM\tMEMO");
        sb.AppendLine("!SPL\tSPLID\tTRNSTYPE\tDATE\tACCNT\tNAME\tAMOUNT\tDOCNUM\tMEMO");

        foreach (var slip in slips)
        {
            // TRNS Line (Accounts Receivable)
            sb.AppendLine(
                $"TRNS\t" +
                $"{slip.SlipId}\t" +                   
                $"INVOICE\t" +                        
                $"{slip.SlipDate:MM/dd/yyyy}\t" +      
                $"ACCOUNTS RECEIVABLE\t" +            
                //$"{slip.CustomerAccountNumber}\t" + 
                // $"{slip.Total}\t" +                
                $"{slip.SlipNumber}\t"// + 
                // $"{slip.Description}"              
            );

            // SPL Line (Sales Account)
            sb.AppendLine(
                $"SPL\t" +
                $"{slip.SlipId}\t" +                   
                $"INVOICE\t" +                        
                $"{slip.SlipDate:MM/dd/yyyy}\t" +      
                $"SALES\t" +                          
                //$"{slip.CustomerAccountNumber}\t" + 
                // $"{slip.Total}\t" +                
                $"{slip.SlipNumber}\t" //+ 
                // $"{slip.Description}"              
            );
        }

        return sb.ToString();
    }
    }
