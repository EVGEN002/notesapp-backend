namespace TestApplication.DTOs
{
    public class EditNoteDTO
    {
        public required Guid guid { get; set; }
        public string? text { get; set; }
        public int? status { get; set; }
    }
}
