using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarInsuranceFinal.Models.Schema
{
    public class FileCreation
    {
        [Key]
        public int Id { get; set; }
        public string? FileName { get; set; }
        public string? FileType { get; set; }
        public string? FileExtension { get; set; }
        [NotMapped] // This attribute prevents EF from trying to map this property to the database
        public byte[]? FileData { get; set; }
        //relationships
        public Claim? Claim { get; set; }
        [ForeignKey(nameof(ClaimId))]
        public int? ClaimId { get; set; }
    }
}
