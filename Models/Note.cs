using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotesAPI.Models
{
    [Table("notes")]
    public class Note
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]public Guid guid { get; set; }
        public string? text { get; set; }
        public int? status { get; set; }
        public bool? archived { get; set; }
        public int? total_time_spent { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public DateTime? started_at { get; set; }
        public DateTime? completed_at { get; set; }
    }
}
