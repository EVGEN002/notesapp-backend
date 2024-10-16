using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NotesAPI.Models
{
    public class Pause
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] public Guid guid { get; set; }
        public DateTime? paused_at { get; set; }
        public DateTime? resumed_at { get; set; }
        public Guid? task_guid { get; set; }
    }
}
