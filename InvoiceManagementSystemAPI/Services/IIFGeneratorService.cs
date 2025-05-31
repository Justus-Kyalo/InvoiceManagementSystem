using System.Text;
using InvoiceManagementSystemAPI.Models;
using InvoiceManagementSystemAPI.Services.IServices;

namespace InvoiceManagementSystemAPI.Services;

public class IIFGeneratorService:IIIFGeneratorService
{
    public string GenerateIIFContent(List<SlipDetail> slips)
    {
        var sb = new StringBuilder();
        sb.AppendLine(
            $"AccountNumber\t" +
            $"Name\t" +
            $"Taxable\t" +
            $"SlipNumber\t" +
            $"Item\t" +
            $"Quantity\t" +
            $"Price\t" +
            $"SlipDate"
        );

        var GroupByCustomer = slips.GroupBy(s => s.AccountNumber).ToList();

        foreach (var group in GroupByCustomer)
        {
            
            foreach (var slip in group)
            {
                sb.AppendLine(
                    $"{slip.AccountNumber}\t" +
                    $"{slip.Name}\t" +
                    $"{(slip.Taxable ? "Y" : "N")}\t" +
                    $"{slip.SlipNumber}\t" +
                    $"{slip.ItemName}\t" +
                    $"{slip.Quantity}\t" +
                    $"{slip.Price}\t" +
                    $"{slip.SlipDate:MM/dd/yyyy}" 
                );

            }
        }

        sb.AppendLine("\n");

        return sb.ToString();
    }
    }
