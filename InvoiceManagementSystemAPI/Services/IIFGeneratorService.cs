using System.Text;
using InvoiceManagementSystemAPI.Models;
using InvoiceManagementSystemAPI.Services.IServices;

namespace InvoiceManagementSystemAPI.Services;

public class IIFGeneratorService:IIIFGeneratorService
{
    public string GenerateIIFContent(List<Invoice> invoices)
    {
        var sb = new StringBuilder();

        // IIF Headers
        sb.AppendLine("!TRNS\tTRNSID\tTRNSTYPE\tDATE\tACCNT\tNAME\tAMOUNT\tDOCNUM\tMEMO");
        sb.AppendLine("!SPL\tSPLID\tTRNSTYPE\tDATE\tACCNT\tNAME\tAMOUNT\tDOCNUM\tMEMO");

        foreach (var invoice in invoices)
        {
            // TRNS Line (Accounts Receivable)
            sb.AppendLine(
                $"TRNS\t" +
                $"{invoice.InvoiceId}\t" +                   
                $"INVOICE\t" +                        
                $"{invoice.InvoiceDate:MM/dd/yyyy}\t" +      
                $"ACCOUNTS RECEIVABLE\t" +            
                $"{invoice.CustomerAccountNumber}\t" + 
                $"{invoice.Total}\t" +                
                $"{invoice.CollectionSlipNumber}\t" + 
                $"{invoice.Description}"              
            );

            // SPL Line (Sales Account)
            sb.AppendLine(
                $"SPL\t" +
                $"{invoice.InvoiceId}\t" +                   
                $"INVOICE\t" +                        
                $"{invoice.InvoiceDate:MM/dd/yyyy}\t" +      
                $"SALES\t" +                          
                $"{invoice.CustomerAccountNumber}\t" + 
                $"{invoice.Total}\t" +                
                $"{invoice.CollectionSlipNumber}\t" + 
                $"{invoice.Description}"              
            );
        }

        return sb.ToString();
    }
    }
