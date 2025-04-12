using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceManagementSystemAPI.Models;

public class IIFBackup
{
    [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IIFBackupId { get; set; }
    public string FileName { get; set; }
    public  string FileContent { get; set; }
    public  DateTime StartDate { get; set; }
    public  DateTime EndDate { get; set; }
    public  DateTime GeneratedOn { get; set; }
}