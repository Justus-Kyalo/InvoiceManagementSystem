namespace InvoiceManagementSystemAPI.Models.Dto;

public class IIFBackupCreateDto
{
    public int IIFBackupId { get; set; }
    public string FileName { get; set; }
    public  string FileContent { get; set; }
    public  DateTime StartDate { get; set; }
    public  DateTime EndDate { get; set; }
    public  DateTime GeneratedOn { get; set; }
}